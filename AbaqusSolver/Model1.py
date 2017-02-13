# -*- coding: utf-8 -*-
import sys,os

def environmentSetup():
    # os.path.split(os.path.realpath(__file__))[0]
    __file__ = r'D:\Workspace\PycharmProjects\SDSS\Model1.py'

    fileDir = os.path.split(os.path.realpath(__file__))[0]
    # fileDir = 'D:\Workspace\PycharmProjects\SDSS'
    print(fileDir)
    sys.path.append(fileDir)

    # execfile(r'D:\Workspace\PycharmProjects\SDSS\Model1.py')
    # os.chdir(r'D:\Workspace\PycharmProjects\SDSS')
    # del(sys.modules["Model1"])
    print('\n'*2 + '-'*200 + '\n'*2)
environmentSetup()

from abaqus import *
from abaqusConstants import *
import part,material,section,assembly,step,load,mesh,job,visualization,regionToolset

from Model1Functions import *
from SSModels import ImportUserModel1

if __name__ == '__main__':

    myFrame = ImportUserModel1('')

    # ============== Part =======================================================
    session.journalOptions.setValues(replayGeometry=COORDINATE,recoverGeometry= COORDINATE)
    #
    myModel = mdb.Model(name=myFrame.name)
    # delete the original model, FYI, the 'Model-1'

    # create a skecth
    mySketch = CreateSketch(myFrame,myModel)

    # create a part
    myPart = CreatePart(myModel,myFrame.name,mySketch)
    # myPart = myModel.Part(name=myFrame.name, dimensionality=TWO_D_PLANAR, type=DEFORMABLE_BODY)
    # myPart.BaseWire(sketch=mySketch)
    del mySketch

    # ============= Property ========================================================
    # define materials and profiles
    defineMaterials(myFrame.materials,myModel)
    defineProfiles(myFrame.profiles, myModel)
    # assign sections to the components of the model
    defineAndAssignSections(myModel,myPart,myFrame)

    #  assign BeamSection Orientation
    assignBeamSectionOrientation(myFrame,myPart)

    # =============== Assembly ======================================================
    # assembly the parts
    myAssembly,myInstance = assemblyParts(myPart, myModel, instanceName = myFrame.name)

    # =============== Step ======================================================
    # define steps
    myStep = defineStep(myModel,'Loading',1.0)
    # outputFilds = ('S', 'PE', 'PEEQ', 'PEMAG', 'LE', 'U', 'RF', 'RM', 'CF', 'SF', 'TF', 'CSTRESS',    'CDISP')
    outputFilds = ('U', 'RF', 'SF',)
    defineOutputFields(myModel,fieldoutputName= 'F-Output-1',outputFilds= outputFilds)

    # =============== Mesh ======================================================
    # mesh the instance
    meshInstance(myAssembly,myInstance,myFrame)

    # =============== Load ======================================================
    # set boundaries
    setBoundary(myAssembly,myInstance,myFrame,myFrame.load)

    # concentrated force on each intersect point between columns and beams
    setConcentratedForce(myModel,myInstance,myStep.name, myFrame,myFrame.load)
    # apply line load on the columns at the left side of the frame
    setLineLoad(myModel,myInstance,myStep.name,myFrame,myFrame.load)

    # =============== Job ======================================================
    # create a job
    description = 'seismic design of subway structures'
    numCpus = 8
    myJob = createJob(myModel,"myJob",numCpus,description)

    # Wait for the job to complete.
    myJob.submit()
    myJob.waitForCompletion()

    # =============== visualization ======================================================
    # import visualization
    # vpName = 'bending moment'
    # myVieport = session.Viewport(name=vpName, origin=(20, 20), width=150, height=120)
    # myOdb = visualization.openOdb(path=myJob.name + '.odb')
    # myViewport.setValues(displayedObject=myOdb)
    # myViewport.odbDisplay.display.setValues(plotState=CONTOURS_ON_DEF)
    #
    # myViewport.odbDisplay.commonOptions.setValues(renderStyle=FILLED)
    # myViewport.odbDisplay.setPrimaryVariable(
    #     variableLabel='U', outputPosition=NODAL, refinement=(INVARIANT, 'Magnitude'), )
    #
    # session.printToFile(fileName='1', format=PNG, canvasObjects=(session.viewports[vpName], ))
