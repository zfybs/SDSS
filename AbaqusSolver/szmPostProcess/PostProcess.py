__author__ = 'zengfy'
# -*- coding: utf-8 -*-
from szmPostProcess.Result import uResult,uResult1
from szmDefinitions.ProjectPath import projectPath
from textRepr import *
from abaqus import *
from odbAccess import *
from abaqusConstants import *
from szmEntities.uFrame import *

def GetResult1(job, myFrame):
    '''

    :param model:
    :param job:
    :param myFrame:
    :type myFrame: uFrame1
    :return:
    '''

    name = job.name+'.odb'
    odbFilePath = projectPath.get_AbaqusWorkingDir() + '\\' + name

    # 获取当前目录下的ODB文件
    o = session.openOdb(name=odbFilePath, readOnly = False)

    # ============== Instant (模型数据) 'inst'============================================
    #模型数据存储在material，rootAssembly，对象中
    a = o.rootAssembly
    instes = a.instances
    inst = instes[myFrame.name.upper()]
    # ============== Step (结果数据) 'fopU', 'fopSF', 'fopSM'=============================
    #结果数据储存在steps仓库中，最后一步分析步默认为“Loading”
    steps = o.steps
    step = steps['Loading']

    frames = step.frames
    frame = frames[-1]  #获取最后一帧OdbFrame对象

    fop = frame.fieldOutputs
    fopU = fop['U']   #对象的位移场（U.values包含了U1，U2，magnitude）
    fopSF = fop['SF']  #对象的内力场
    fopSM = fop['SM']  #对象的弯矩场
    # ============== dictionary of nodes (节点字典) 'dic'=================================
    #得到节点编号和坐标，并形成相应的字典
    labels, xyz = [], []
    nodes = inst.nodes
    for node in nodes:
        labels.append(node.label)
        xyz.append(node.coordinates)
    dic = dict(zip(labels,xyz))
    # ============== dictionary of U, F, M (供后面数据提取) 'dicU1', 'dicU2', 'dicF1', 'dicF2', 'dicM' =========
    #获取节点位移，与相应nodeLabel组成字典
    labelsU1, u1 = [], []
    for i in range(0,len(fopU.values)):
        u1.append(fopU.values[i].data[0])
        labelsU1.append(fopU.values[i].nodeLabel)
    dicU1 = dict(zip(labelsU1, u1))

    labelsU2, u2 = [], []
    for i in range(0,len(fopU.values)):
        u2.append(fopU.values[i].data[1])
        labelsU2.append(fopU.values[i].nodeLabel)
    dicU2 = dict(zip(labelsU2, u2))

    #获取单元内力，与相应elementLabel组成字典
    labelsF1, f1 = [], []
    for i in range(0,len(fopSF.values)):
        f1.append(fopSF.values[i].data[0])
        labelsF1.append(fopSF.values[i].elementLabel)
    dicF1 = dict(zip(labelsF1, f1))

    labelsF2, f2 = [], []
    for i in range(0,len(fopSF.values)):
        f2.append(fopSF.values[i].data[1])
        labelsF2.append(fopSF.values[i].elementLabel)
    dicF2 = dict(zip(labelsF2, f2))

    #获取单元的弯矩，与相应的elementLabel组成字典
    labelsM, m = [], []
    for i in range(0,len(fopSM.values)):
        m.append(fopSM.values[i].data[0])
        labelsM.append(fopSM.values[i].elementLabel)
    dicM = dict(zip(labelsM, m))
    # ============== 获取最大水平位移、竖直位移、轴力、剪力、弯矩和相应坐标 ===================
    #寻找水平位移最大值及相应的位置
    maxU1 = max(dicU1.items(), key=lambda x:x[1])
    minU1 = min(dicU1.items(), key=lambda x:x[1])
    if abs(maxU1[1]) >= abs(minU1[1]):
        coordinateMaxU1 = dic[maxU1[0]]
        maxDeflectionX = maxU1[1]
    else:
        coordinateMaxU1=dic[minU1[0]]
        maxDeflectionX=minU1[1]


    #寻找竖向位移最大值及相应的位置
    maxU2 = max(dicU2.items(), key=lambda x:x[1])
    minU2 = min(dicU2.items(), key=lambda x:x[1])
    if abs(maxU2[1]) >= abs(minU2[1]):
        coordinateMaxU2 = dic[maxU2[0]]
        maxDeflectionY = maxU2[1]
    else:
        coordinateMaxU2 = dic[minU2[0]]
        maxDeflectionY = minU2[1]


    #寻找轴力最大值及相应的位置
    maxF1 = max(dicF1.items(), key=lambda x:x[1])
    minF1 = min(dicF1.items(), key=lambda x:x[1])
    if abs(maxF1[1]) >= abs(minF1[1]):
        elementF1 = inst.getElementFromLabel(maxF1[0])
        coordinateMaxF1 = dic[elementF1.connectivity[0]]
        maxAxialF = maxF1[1]
    else:
        elementF1 = inst.getElementFromLabel(minF1[0])
        coordinateMaxF1 = dic[elementF1.connectivity[0]]
        maxAxialF = minF1[1]


    #寻找剪力最大值及相应的位置
    maxF2 = max(dicF2.items(), key=lambda x:x[1])
    minF2 = min(dicF2.items(), key=lambda x:x[1])
    if abs(maxF2[1]) >= abs(minF2[1]):
        elementF2 = inst.getElementFromLabel(maxF2[0])
        coordinateMaxF2 = dic[elementF2.connectivity[0]]
        maxShearF = maxF2[1]
    else:
        elementF2 = inst.getElementFromLabel(minF2[0])
        coordinateMaxF2 = dic[elementF2.connectivity[0]]
        maxShearF = minF2[1]


    #寻找弯矩最大值及相应的位置
    maxM = max(dicM.items(), key=lambda x:x[1])
    minM = min(dicM.items(), key=lambda x:x[1])
    if abs(maxM[1]) >= abs(minM[1]):
        elementM = inst.getElementFromLabel(maxM[0])
        coordinateMaxM = dic[elementM.connectivity[0]]
        maxMoment = maxM[1]
    else:
        elementM = inst.getElementFromLabel(minM[0])
        coordinateMaxM = dic[elementM.connectivity[0]]
        maxMoment = minM[1]
    # ============== 获取每一层楼板的最大水平、竖直位移和相应坐标=================
    '''
    :param maxLayerX: 每一层的最大水平位移，第一个元素为车站地板的位移
    :param floorsCoordinatesX：每一层最大水平位移点的坐标
    '''
    maxLayerX, coordinatesMaxLX,maxLayerY, coordinatesMaxLY = [], [], [], []

    for i in range(0, myFrame.strut.layers+1):
        #获取每一层节点的值和相应节点label
        nameSet = 'FLOOR'+str(i+1)+'NODES'
        nodeSetI=o.rootAssembly.nodeSets[nameSet]
        uI = fopU.getSubset(region = nodeSetI)
        lenU = len(uI.values)

        labelsUi1, ui1 = [], []
        for i in range(0, lenU):
            ui1.append(uI.values[i].data[0])
            labelsUi1.append(uI.values[i].nodeLabel)
        dicUi1 = dict(zip(labelsUi1, ui1))

        labelsUi2, ui2 = [], []
        for i in range(0,lenU):
            ui2.append(uI.values[i].data[1])
            labelsUi2.append(uI.values[i].nodeLabel)
        dicUi2 = dict(zip(labelsUi2, ui2))

        #对每一层的结果列表寻找最大值
        maxUi1 = max(dicUi1.items(), key=lambda x:x[1])
        minUi1 = min(dicUi1.items(), key=lambda x:x[1])
        if abs(maxUi1[1]) >= abs(minUi1[1]):
            coordinateMaxU1i = dic[maxUi1[0]]
            maxDeflectionXi = maxUi1[1]
        else:
            coordinateMaxU1i=dic[minUi1[0]]
            maxDeflectionXi=minUi1[1]

        maxUi2 = max(dicUi2.items(), key=lambda x:x[1])
        minUi2 = min(dicUi2.items(), key=lambda x:x[1])
        if abs(maxUi2[1]) >= abs(minUi2[1]):
            coordinateMaxU2i = dic[maxUi2[0]]
            maxDeflectionYi = maxUi2[1]
        else:
            coordinateMaxU2i = dic[minUi2[0]]
            maxDeflectionYi = minUi2[1]

        #将最值和坐标添加到maxLayerX，coordinatesMaxLX，maxLayerY，coordinatesMaxLY
        maxLayerX.append(maxDeflectionXi)
        coordinatesMaxLX.append(coordinateMaxU1i)
        maxLayerY.append(maxDeflectionYi)
        coordinatesMaxLY.append(coordinateMaxU2i)

    maxLayerX = tuple(maxLayerX)
    coordinatesMaxLX = tuple(coordinatesMaxLX)
    maxLayerY = tuple(maxLayerY)
    coordinatesMaxLY = tuple(coordinatesMaxLY)
    # ============== 获取每一层楼板的最大轴力、剪力、弯矩和相应坐标=================
    '''
    :param maxLayerAxialF: 每一层的最大轴力，第一个元素为车站地板的轴力
    :param coordinatesMaxLAF：每一层最大轴力点的坐标
    '''
    maxLayerAxialF, coordinatesMaxLAF, maxLayerShearF, coordinatesMaxLSF,maxLayerMoment, coordinatesMaxLM = [], [], [], [], [], []
    for i in range(myFrame.strut.layers+1):
        #获取每一层单元的值和相应单元的label
        nameSetF = 'FLOOR' + str(i+1) + 'ELEMENTS'
        elementSetF = o.rootAssembly.elementSets[nameSetF]
        sFIF = fopSF.getSubset(region = elementSetF)

        labelsFi1, fi1 = [], []
        for i in range(0,len(sFIF.values)):
            fi1.append(sFIF.values[i].data[0])
            labelsFi1.append(sFIF.values[i].elementLabel)
        dicFi1 = dict(zip(labelsFi1, fi1))

        #对每一层的结果列表寻找最大值
        maxFi1 = max(dicFi1.items(), key=lambda x:x[1])
        minFi1 = min(dicFi1.items(), key=lambda x:x[1])
        if abs(maxFi1[1]) >= abs(minFi1[1]):
            elementFi1 = inst.getElementFromLabel(maxFi1[0])
            coordinateMaxFi1 = dic[elementFi1.connectivity[0]]
            maxAxialFi = maxFi1[1]
        else:
            elementFi1 = inst.getElementFromLabel(minFi1[0])
            coordinateMaxFi1 = dic[elementFi1.connectivity[0]]
            maxAxialFi = minFi1[1]

        #将最值和坐标添加到 maxLayerAxialF, coordinatesMaxLAF
        maxLayerAxialF.append(maxAxialFi)
        coordinatesMaxLAF.append(coordinateMaxFi1)

    for i in range(myFrame.strut.layers):
        #获取每一层单元的值和相应单元的label
        nameSetL = 'LAYER' + str(i+1) + 'ELEMENTS'
        elementSetL = o.rootAssembly.elementSets[nameSetL]
        sFIL = fopSF.getSubset(region = elementSetL)
        sMI = fopSM.getSubset(region = elementSetL)

        labelsFi2, fi2 = [], []
        for i in range(0,len(sFIL.values)):
            fi2.append(sFIL.values[i].data[1])
            labelsFi2.append(sFIL.values[i].elementLabel)
        dicFi2 = dict(zip(labelsFi2, fi2))

        labelsMi, Mi = [], []
        for i in range(0,len(sMI.values)):
            Mi.append(sMI.values[i].data[0])
            labelsMi.append(sMI.values[i].elementLabel)
        dicMi = dict(zip(labelsMi, Mi))

        #对每一层的结果列表寻找最大值
        maxFi2 = max(dicFi2.items(), key=lambda x:x[1])
        minFi2 = min(dicFi2.items(), key=lambda x:x[1])
        if abs(maxFi2[1]) >= abs(minFi2[1]):
            elementFi2 = inst.getElementFromLabel(maxFi2[0])
            coordinateMaxFi2 = dic[elementFi2.connectivity[0]]
            maxShearFi = maxFi2[1]
        else:
            elementFi2 = inst.getElementFromLabel(minFi2[0])
            coordinateMaxFi2 = dic[elementFi2.connectivity[0]]
            maxShearFi = minFi2[1]

        maxMi = max(dicMi.items(), key=lambda x:x[1])
        minMi = min(dicMi.items(), key=lambda x:x[1])
        if abs(maxMi[1]) >= abs(minMi[1]):
            elementMi = inst.getElementFromLabel(maxMi[0])
            coordinateMaxMi = dic[elementMi.connectivity[0]]
            maxMomenti = maxMi[1]
        else:
            elementMi = inst.getElementFromLabel(minMi[0])
            coordinateMaxMi = dic[elementMi.connectivity[0]]
            maxMomenti = minMi[1]

        #将最值和坐标添加到 maxLayerShearF, coordinatesMaxLSF,maxLayerMoment, coordinatesMaxLM
        maxLayerShearF.append(maxShearFi)
        coordinatesMaxLSF.append(coordinateMaxFi2)
        maxLayerMoment.append(maxMomenti)
        coordinatesMaxLM.append(coordinateMaxMi)


    maxLayerAxialF = tuple(maxLayerAxialF)
    coordinatesMaxLAF = tuple(coordinatesMaxLAF)
    maxLayerShearF = tuple(maxLayerShearF)
    coordinatesMaxLSF = tuple(coordinatesMaxLSF)
    maxLayerMoment = tuple(maxLayerMoment)
    coordinatesMaxLM = tuple(coordinatesMaxLM)

    myResult = uResult1(maxDeflectionX,coordinateMaxU1,
                        maxDeflectionY,coordinateMaxU2,
                        maxAxialF,coordinateMaxF1,
                        maxShearF,coordinateMaxF2,
                        maxMoment,coordinateMaxM,
                        maxLayerX,coordinatesMaxLX,
                        maxLayerY,coordinatesMaxLY,
                        maxLayerAxialF,coordinatesMaxLAF,
                        maxLayerShearF,coordinatesMaxLSF,
                        maxLayerMoment,coordinatesMaxLM)
    return myResult

# =================================输出结果图片================================
def printPicture(job,frame):
    '''

    :param job:
    :param frame:
    :type frame: uFrame1
    :return:
    '''

    name = job.name+'.odb'
    nameUMagnitude = job.name + '-UM'
    nameU1 = job.name + '-U1'
    nameU2 = job.name + '-U2'
    nameSF1 = job.name + '-SF1'
    nameSF2 = job.name + '-SF2'
    nameSM1 = job.name + '-SM'

    odbFilePath = projectPath.get_AbaqusWorkingDir() + '\\' + name

    # 获取当前目录下的ODB文件
    o = session.openOdb(name=odbFilePath, readOnly = False)

    # 创建一个新的视口，并在新视口中打开ODB文件
    width = None
    if (frame.strut.iNodes[-1] > frame.strut.jNodes[-1]):
        width = frame.strut.iNodes[-1]*120/frame.strut.jNodes[-1] + 66
    else:
        width = 200

    myViewport=session.Viewport(name='Print contour plots',
                                origin=(0, 0), width=width,
                                height=120)
    myViewport.setValues(displayedObject=
                         o, border = False)

    # 设置云图模式
    myViewport.odbDisplay.display.setValues(plotState=(CONTOURS_ON_DEF, ))

    # 设置 common plot option 里面的参数
    myViewport.odbDisplay.commonOptions.setValues(deformationScaling=UNIFORM, uniformScaleFactor=0)
    myViewport.odbDisplay.commonOptions.setValues(edgeLineThickness=MEDIUM)

    # 设置 contour plot options 里面的参数
    myViewport.odbDisplay.contourOptions.setValues(showMinLocation=ON, showMaxLocation=ON)

    # 设置 viewportAnnotationOptions 里面的参数，主要针对一些注释性的内容
    myViewport.viewportAnnotationOptions.setValues(compass=OFF, triad=ON, legend=ON, title=OFF, state=OFF, annotations=OFF)
    myViewport.viewportAnnotationOptions.setValues(legendBox=ON,legendMinMax=OFF)
    myViewport.viewportAnnotationOptions.setValues(legendFont='-*-verdana-bold-r-normal-*-*-100-*-*-p-*-*-*')
    myViewport.viewportAnnotationOptions.setValues(triadPosition=(3, 6))

    # 设置 view 里面的参数
    myViewport.view.fitView()
    myViewport.view.setValues(width=1.25*frame.strut.iNodes[-1], height=1.25*frame.strut.jNodes[-1], viewOffsetX=-41.25*frame.strut.iNodes[-1]/width)

    # ====================== 打印位移云图 ========================
    # 设置显示的结果种类
    myViewport.odbDisplay.setPrimaryVariable(variableLabel='U', outputPosition=NODAL, refinement=(INVARIANT, 'Magnitude'), )
    # 打印图片
    session.printOptions.setValues(vpBackground=ON, compass=ON, rendition=COLOR)
    session.printToFile(fileName= projectPath.get_AbaqusWorkingDir() + '\\' + nameUMagnitude, format=PNG, canvasObjects=(myViewport, ))

    # ====================== 打印水平位移云图 ========================
    myViewport.odbDisplay.setPrimaryVariable(variableLabel='U', outputPosition=NODAL, refinement=(COMPONENT, 'U1'), )
    session.printOptions.setValues(vpBackground=ON, compass=ON, rendition=COLOR)
    session.printToFile(fileName= projectPath.get_AbaqusWorkingDir() + '\\' + nameU1, format=PNG, canvasObjects=(myViewport, ))

    # ====================== 打印竖向位移云图 ========================
    myViewport.odbDisplay.setPrimaryVariable(variableLabel='U', outputPosition=NODAL, refinement=(COMPONENT, 'U2'), )
    session.printOptions.setValues(vpBackground=ON, compass=ON, rendition=COLOR)
    session.printToFile(fileName= projectPath.get_AbaqusWorkingDir() + '\\' + nameU2, format=PNG, canvasObjects=(myViewport, ))

    # ====================== 打印轴力云图 ========================
    myViewport.odbDisplay.setPrimaryVariable(variableLabel='SF', outputPosition=INTEGRATION_POINT, refinement=(COMPONENT, 'SF1'), )
    session.printOptions.setValues(vpBackground=ON, compass=ON, rendition=COLOR)
    session.printToFile(fileName= projectPath.get_AbaqusWorkingDir() + '\\' + nameSF1, format=PNG, canvasObjects=(myViewport, ))

    # ====================== 打印剪力云图 ========================
    myViewport.odbDisplay.setPrimaryVariable(variableLabel='SF', outputPosition=INTEGRATION_POINT, refinement=(COMPONENT, 'SF2'), )
    session.printOptions.setValues(vpBackground=ON, compass=ON, rendition=COLOR)
    session.printToFile(fileName= projectPath.get_AbaqusWorkingDir() + '\\' + nameSF2, format=PNG, canvasObjects=(myViewport, ))

    # ====================== 打印弯矩云图 ========================
    myViewport.odbDisplay.setPrimaryVariable(variableLabel='SM', outputPosition=INTEGRATION_POINT, refinement=(COMPONENT, 'SM1'), )
    session.printOptions.setValues(vpBackground=ON, compass=ON, rendition=COLOR)
    session.printToFile(fileName= projectPath.get_AbaqusWorkingDir() + '\\' + nameSM1, format=PNG, canvasObjects=(myViewport, ))
    pass

