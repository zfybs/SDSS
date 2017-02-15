# -*- coding: utf-8 -*-

# sys.path.append( r'E:\GitHubProjects\SDSS\AbaqusSolver')

import sys,os
from abaqus import *
from abaqusConstants import *
import part,material,section,assembly,step,load,mesh,job,visualization,regionToolset

from Model1Functions import *
from  SSModels import ImportUserModel1
from Entities.Frame import uFrame

def main(myFrame):
    '''

    :param myFrame:
    :type myFrame: uFrame
    :return:
    '''

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


if __name__ == '__main__':
    s = os.getcwd()
    # s= sys.argv[0]
    #getWarningReply(s,(YES,NO))
    main()

