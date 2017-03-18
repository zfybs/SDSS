# -*- coding: utf-8 -*-
# sys.path.append( r'E:\GitHubProjects\SDSS\AbaqusSolver')

import sys,os
from abaqus import *
from abaqusConstants import *
# from symbolicConstants import *
import part,material,section,assembly,step,load,mesh,job,visualization,regionToolset

from Model1Functions import *
from szmPostProcess.PostProcess import  *
from SSModels import ImportUserModel1
from szmEntities.uFrame import uFrame1

def Calculate1(myFrame):
    '''
    :param myFrame:
    :type myFrame: uFrame1
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
    setBoundary(myAssembly,myInstance,myFrame)

    # create sets
    createNodesSet (myAssembly,myInstance,myFrame)
    createElementsSet(myAssembly,myInstance,myFrame)

    # concentrated force on each intersect point between columns and beams
    setConcentratedForce(myModel,myInstance,myStep.name, myFrame)
    # apply line load on the columns at the left side of the frame
    setLineLoad(myModel,myInstance,myStep.name,myFrame)

    # =============== Job ======================================================
    # create a job
    description = 'seismic design of subway structures'
    numCpus = 8
    myJob = createJob(myModel, myModel.name, numCpus,description)

    # Wait for the job to complete.
    myJob.submit()
    myJob.waitForCompletion()

    # =============== visualization ======================================================
    # printPicture(myJob, myModel)
    return myModel, myJob

def Calculate2(myFrame):
    '''

    :param myFrame:
    :type myFrame: uFrame
    :return:
    '''

if __name__ == '__main__':
    s = os.getcwd()
    # s= sys.argv[0]
    #getWarningReply(s,(YES,NO))
    main()

