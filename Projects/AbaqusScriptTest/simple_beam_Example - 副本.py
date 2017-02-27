#!/user/bin/python
# -* - coding:UTF-8 -*-

# �ļ�����simple_beam_Example.py

# ���иýű����Զ�ʵ����������ѹ�����������µĽ�ģ���ύ�����ͺ���
# �ȸ�����Ĳ�����

from abaqus import *
import testUtils
testUtils.setBackwardCompatibility()
from abaqusConstants import *

# д��ӭ��
print '�װ��Ķ������ѣ��ܸ�����ʶ��ң�'
print '����ͨ������Ϥ�ļ�ʵ���������ҽ���Python��̵��������磡'

#����ģ��
myModel = mdb.Model(name='Beam')

# �������ӿ�����ʾģ�ͺͷ��������
myViewport = session.Viewport(name='Cantilever Beam Example',
    origin=(20, 20), width=150, height=120)
    
# ����partģ�顣
import part

# �������������Ĳ�ͼ��
mySketch = myModel.ConstrainedSketch(name='beamProfile',sheetSize=250.)

# ���ƾ��ν��档
mySketch.rectangle(point1=(-100,10), point2=(100,-10))

# ������ά�����岿����
myBeam = myModel.Part(name='Beam', dimensionality=THREE_D,
         type=DEFORMABLE_BODY)

# ͨ���Բ�ͼ����25.0������������
myBeam.BaseSolidExtrude(sketch=mySketch, depth=25.0)

# ����materialģ�顣
import material

# �������ϡ�
mySteel = myModel.Material(name='Steel')

# ���嵯�Բ������ԣ�����ģ��Ϊ209.E3�����ɱ�Ϊ0.3��
elasticProperties = (209.E3, 0.3)
mySteel.Elastic(table=(elasticProperties, ) )

# ����sectionģ�顣
import section

# ����ʵ����档
mySection = myModel.HomogeneousSolidSection(name='beamSection',
    material='Steel', thickness=1.0)

# Ϊ��������������ԡ�
region = (myBeam.cells,)
myBeam.SectionAssignment(region=region,sectionName='beamSection')

# ����assemblyģ�顣
import assembly

# ��������ʵ����
myAssembly = myModel.rootAssembly
myInstance = myAssembly.Instance(name='beamInstance',part=myBeam, dependent=OFF)

# ����stepģ�顣
import step

# �ڳ�ʼ������Initial֮�󴴽�һ����������������������ʱ��Ϊ1.0����ʼ����Ϊ0.1��
myModel.StaticStep(name='beamLoad', previous='Initial', timePeriod=1.0,
   initialInc=0.1,description='Load the top of the beam.')

# ����loadģ�顣
import load

# ͨ�������ҳ��˲������档
endFaceCenter = (-100,0,12.5)
endFace = myInstance.faces.findAt((endFaceCenter,) )

# �����˲������̶���Լ����
endRegion = (endFace,)
myModel.EncastreBC(name='Fixed',createStepName='beamLoad',region=endRegion)

# ͨ�������ҵ��ϱ��档
topFaceCenter = (0,10,12.5)
topFace = myInstance.faces.findAt((topFaceCenter,) )

# �������ϱ���ʩ��ѹ�����ء�
topSurface = ((topFace, SIDE1), )
myModel.Pressure(name='Pressure', createStepName='beamLoad',
    region=topSurface, magnitude=0.5)

# ����meshģ�顣
import mesh

# Ϊ����ʵ��ָ����Ԫ���͡�
region = (myInstance.cells,)
elemType = mesh.ElemType(elemCode=C3D8I, elemLibrary=STANDARD)
myAssembly.setElementType(regions=region, elemTypes=(elemType,))

# Ϊ����ʵ�������ӡ�
myAssembly.seedPartInstance(regions=(myInstance,), size=10.0)

# Ϊ����ʵ����������
myAssembly.generateMesh(regions=(myInstance,))

# ��ʾ������������ģ�͡�
myViewport.assemblyDisplay.setValues(mesh=ON)
myViewport.assemblyDisplay.meshOptions.setValues(meshTechnique=ON)
myViewport.setValues(displayedObject=myAssembly)

# ����jobģ�顣
import job

# Ϊģ�ʹ������ύ������ҵ��
jobName = 'beam_tutorial'
myJob = mdb.Job(name=jobName, model='Beam',description='Cantilever beam tutorial')

# �ȴ�������ҵ��ɡ�
myJob.submit()
myJob.waitForCompletion()
print '������˳����ɣ�������к���'

# ����visualizationģ�顣
import visualization

# ��������ݿ⣬��ʾĬ�ϵĵ�ֵ��ͼ��
myOdb = visualization.openOdb(path=jobName + '.odb')
myViewport.setValues(displayedObject=myOdb)
myViewport.odbDisplay.display.setValues(plotState=CONTOURS_ON_DEF)
myViewport.odbDisplay.commonOptions.setValues(renderStyle=FILLED)

# ��Mises��ֵ��ͼ���ΪPNG��ʽ���ļ���
session.printToFile(fileName='Mises', format=PNG,  canvasObjects=(myViewport,))
print '�ļ�Mises.png�����ڹ���Ŀ¼�£���鿴��'