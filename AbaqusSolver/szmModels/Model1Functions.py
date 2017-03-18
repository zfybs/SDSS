# -*- coding: utf-8 -*-
from szmEntities.uFrame import *

from abaqus import *
from abaqusConstants import *
import part, regionToolset

def CreateSketch(frame,model):
    ''' 创建草图
    :type frame: uFrame1
    '''
    sk = model.ConstrainedSketch(name=frame.name,sheetSize=250.)
    frame.strut.drawSketch(sk)

    return sk

def CreatePart(model,partName,sketch):

    pt = model.Part(name=partName, dimensionality = TWO_D_PLANAR, type = DEFORMABLE_BODY)
    pt.BaseWire(sketch=sketch)
    return pt

def defineMaterials(materials, model):
    '''
    :type materials:tuple[uMaterial]
    '''
    for m in materials:
        m.createMaterial(model)

def defineProfiles(profiles, model):
    '''
    :param profiles:
    :param model:
    :return:
    :type profiles: tuple
    '''
    for p in profiles:
        p.createProfile(model)

def defineAndAssignSections(model,part,frame):
    '''

    :param model:
    :param part:
    :param frame:
    :return:
    :type frame: uFrame1
    '''
    edges = part.edges
    for b in frame.strut.beams:
        e = edges.findAt((b.getMiddlePoint(),))
        region = regionToolset.Region(edges=e)
        section = uSection.getSectionFromModel(model,b.material,b.profile)
        part.SectionAssignment(region=region, sectionName=section.name, offset=0.0,offsetType= MIDDLE_SURFACE, offsetField='',thicknessAssignment=FROM_SECTION)

    for c in frame.strut.columns:
        e = edges.findAt((c.getMiddlePoint(),))
        region = regionToolset.Region(edges=e)
        section = uSection.getSectionFromModel(model,c.material, c.profile)
        part.SectionAssignment(region=region, sectionName=section.name, offset=0.0,offsetType= MIDDLE_SURFACE, offsetField='',thicknessAssignment=FROM_SECTION)

def assignBeamSectionOrientation(frame,part):
    '''
    :type frame: uFrame1
    '''
    # recommend using edges.findAt()
    # 定义梁截面方向
    rg = frame.strut.getFrameRegion()
    edges = part.edges.getByBoundingBox(rg[0], rg[1], rg[2], rg[3], rg[4], rg[5])
    region = regionToolset.Region(edges=edges)
    part.assignBeamSectionOrientation(region=region, method=N1_COSINES, n1=(0.0, 0.0, -1.0))

def assemblyParts(part,model,instanceName):
    '''
    :param instanceName: 创建出来的 Instance 对象的名称
    :return:
    :type instanceName: str
    '''
    ass = model.rootAssembly
    ins = ass.Instance(name=instanceName,  part=part, dependent= False)
    return ass,ins

def defineStep(model,stepName,timePeriod):
    '''
    :type stepName: str
    :type timePeriod: float
    '''
    step = model.StaticStep(name=stepName, previous='Initial', timePeriod=timePeriod, initialInc=0.1,   description='Loading on the frame.')
    return step

def defineOutputFields(model,fieldoutputName, outputFilds):
    '''
    :type fieldoutputName: str
    :type outputFilds: tuple[str]
    '''

    model.fieldOutputRequests[fieldoutputName].setValues(variables=outputFilds)

def meshInstance(assembly, instance,frame):
    '''对整个框架进行网格划分
    1. 对于柱构件，按单元个数进行划分，在保证最小网格尺寸为0.1m的前提下，将柱构件的单元个数固定为20个。
    2. 对于各层顶板梁，其单元的划分原则与柱相同；
    3. 对于底板梁单元，由于要施加弹簧边界，所以在划分网格时不考虑单元个数，统一以0.1m作为单元尺寸进行划分
    :type frame: uFrame1
    '''
    edges = instance.edges
    for c in frame.strut.columns:
        # 确定网格数量
        num = c.getSeedNumberByLS(length=c.height,minSize=0.1, fixedNumber= 20)
        # seed
        edge = edges.findAt((c.getMiddlePoint(),))
        assembly.seedEdgeByNumber(edges= edge, number= num, constraint=FINER)

    for b in frame.strut.beams:
        if b.index[1] == 0: # 说明是底板
            # 对于底板梁单元，由于要施加弹簧边界，所以在划分网格时不考虑单元个数，统一以0.1m作为单元尺寸进行划分
            num = int(b.length / 0.1)
        else:
            num = b.getSeedNumberByLS(length=b.length,minSize=0.1, fixedNumber= 20)
        # seed
        edge = edges.findAt((b.getMiddlePoint(),))
        assembly.seedEdgeByNumber(edges= edge, number= num, constraint=FINER)

    # 生成整个框架的网格
    assembly.generateMesh(regions=(instance,))

def setBoundary(assembly,instance,frame):
    ''' 设置框架边界
    为了将弹簧边界指定给我网格节点，必须要先将这些节点放置到一个 set 中
    :type load: uLoad
    :type frame: uFrame1
    :return:
    '''
    # 创建对应的 node set
    bottomRg = frame.strut.getBottomSlabRegion()
    n1 = instance.nodes.getByBoundingBox(bottomRg[0], bottomRg[1], bottomRg[2], bottomRg[3], bottomRg[4], bottomRg[5], )
    s1 = assembly.Set(nodes = n1, name = 'bottomNodes')

    # 创建弹簧边界
    if frame.method.kx>0:   # 看情况考虑是否设置水平弹簧
        assembly.engineeringFeatures.SpringDashpotToGround(
                name='bottomSpring1', region=s1, orientation=None, dof=1,
                springBehavior=True, springStiffness=frame.method.kx, dashpotBehavior=False,
                dashpotCoefficient=0.0)

    assembly.engineeringFeatures.SpringDashpotToGround(
            name='bottomSpring2', region=s1, orientation=None, dof=2,
            springBehavior=True, springStiffness=frame.method.ky, dashpotBehavior=False,
            dashpotCoefficient=0.0)

def setConcentratedForce(model,instance,stepName,frame):
    '''
    :param model:
    :param instance:
    :param stepName: 添加在哪一个分析步
    :return:
    :type stepName: str
    :type load: uLoad
    :type frame: uFrame1
    '''
    for iN in range(0,frame.strut.iNodes.__len__()):
        for jN in range(0,frame.strut.jNodes.__len__()):
            x,y = frame.strut.iNodes[iN] , frame.strut.jNodes[jN] # 节点的二维坐标
            weight = frame.strut.getNodeWeight(iNodeIndex= iN,jNodeIndex= jN) # 与节点相连的所有构件的重量之各的一半

            # 在节点上施加集中荷载
            vertice = instance.vertices.findAt(((x, y, 0.0), ))
            rg = regionToolset.Region(vertices = vertice)
            fName = 'CFN_' + str(iN) + str(jN) # CFN 表示 Concentrated Force acted on Node
            force = -1 * weight * frame.method.kc  # 施加的集中力的值

            model.ConcentratedForce(name = fName, createStepName=stepName,
                region=rg, cf1=force, distributionType=UNIFORM, field='',  localCsys=None)

def setLineLoad(model, instance, stepName, frame):
    ''' 在框架的左侧柱上添加三角形分布的线荷载
    :param model:
    :param frame:
    :return:
    :type frame: uFrame1
    '''
    # 定义分布力的表达式
    expreName = 'soilPressure'
    yt, yb = frame.strut.jNodes[-1] , frame.strut.jNodes[0] # 分别对应框架的顶部与底部标高
    expression = '(' + str(yt) +  '-Y)/' + str(yt-yb) # 比如 “(yt-Y)/(yt-yb)”
    expre = model.ExpressionField(name=expreName, localCsys=None,  description='triangulated distributed loads acted on the left columns of the frame', expression=expression)

    #
    cols = frame.strut.getLeftColumns()
    edgesRange = list()

    for c in cols:
        coor = c.getMiddlePoint()
        edgesRange.append((coor,))

    edgesRange = tuple(edgesRange) # 由 list 转换为 tuple

    # leftColumnsEdge = seperateParameters(instance.edges.findAt,edgesRange)
    leftColumnsEdge = instance.edges.findAt(*edgesRange)

    region = regionToolset.Region(edges = leftColumnsEdge)
    loadName = 'LLLcolumns'     # 表示 line load acted on left columns
    model.LineLoad(name=loadName, createStepName=stepName, region=region, comp1=frame.pMax, distributionType=FIELD, field=expreName)

    pass

def createJob(model,jobName,numCpus,description):
    myJob= mdb.Job(name=jobName, model=model.name, description=description, type=ANALYSIS, atTime=None,
        waitMinutes=0, waitHours=0, queue=None, memory=90, memoryUnits=PERCENTAGE,
        getMemoryFromAnalysis=True, explicitPrecision=SINGLE,
        nodalOutputPrecision=SINGLE, echoPrint=False, modelPrint=False,
        contactPrint=False, historyPrint=False, userSubroutine='', scratch='',
        multiprocessingMode=DEFAULT, numCpus=numCpus, numDomains=numCpus, numGPUs=0)

    return myJob

def createNodesSet (assembly,instance,frame):
    '''
    :param assembly:
    :param instance:
    :param frame:
    :type frame: uFrame1
    :return:
    '''
    for list in frame.strut.getFloorSlabRegion()[0]:
        nRg = instance.nodes.getByBoundingBox(list[0], list[1], list[2], list[3], list[4], list[5] )
        sRg = assembly.Set(nodes = nRg, name = list[6])

def createElementsSet (assembly, instance, frame):
    '''
    :param assembly:
    :param instance:
    :param frame:
    :type frame: uFrame1
    :return:
    '''
    listsFG = frame.strut.getFloorSlabRegion()[1]
    listsLR = frame.strut.getLayerRegion ()
    elements = instance.elements
    for list in listsFG:
        eRg = elements.getByBoundingBox(list[0], list[1], list[2], list[3], list[4], list[5])
        eSRg = assembly.Set(elements = eRg, name = list[6])
    for list in listsLR:
        eLRG0 = elements.getByBoundingBox(list[0][0][0], list[0][0][1], list[0][0][2], list[0][0][3], list[0][0][4], list[0][0][5])
        eLRG = eLRG0
        for i in range(1, frame.strut.spans+1):
            eLRGI = elements.getByBoundingBox(list[0][i][0], list[0][i][1], list[0][i][2], list[0][i][3], list[0][i][4], list[0][i][5])
            eLRG = eLRG + eLRGI
        eSLRG = assembly.Set(elements = eLRG, name = list[1])
