# -*- coding: utf-8 -*-

"""
beamExample.py

Reproduce the cantilever beam example from the
Appendix of the Getting Started with
Abaqus: Interactive Edition Manual.
"""

from abaqus import *
from abaqusConstants import *

backwardCompatibility.setValues(includeDeprecated=True,
                                reportDeprecated=False)
session.journalOptions.setValues(replayGeometry=COORDINATE,recoverGeometry= COORDINATE)
# Create a model.

myModel = mdb.Model(name='Beam')

# Create a new viewport in which to display the model
# and the results of the analysis.

# myViewport = session.Viewport(name='Cantilever Beam Example',
    # origin=(20, 20), width=150, height=120)

#-----------------------------------------------------

import part

# Create a sketch for the base feature.

sketch1 = myModel.ConstrainedSketch(name='beamProfile',
    sheetSize=250.)

# Create the frame.    
sketch1.Line(point1=(0.0, 0.0), point2=(0.0, 30.0))
sketch1.Line(point1=(0.0, 30.0), point2=(45.0, 30.0))
sketch1.Line(point1=(45.0, 30.0), point2=(45.0, 0.0))
sketch1.Line(point1=(0.0, 15.0), point2=(45.0, 15.0))
sketch1.Line(point1=(15.0, 30.0), point2=(15.0, 0.0))
sketch1.Line(point1=(30., 30.0), point2=(30.0, 0.0))

sketch1.Line(point1=(0.0, 0.0), point2=(15.0, 0.0))
sketch1.Line(point1=(15.0, 0.0), point2=(30.0, 0.0))
sketch1.Line(point1=(30.0, 0.0), point2=(45.0, 0.0))

# Create a three-dimensional, deformable part.
myBeam = mdb.models['Beam'].Part(name='Part-1', dimensionality=TWO_D_PLANAR, type=DEFORMABLE_BODY)
myBeam.BaseWire(sketch=sketch1)

del sketch1

#-----------------------------------------------------

import material

# Create a material.

mySteel = myModel.Material(name='Steel')

# Create the elastic properties: youngsModulus is 209.E3
# and poissonsRatio is 0.3

elasticProperties = (200e9, 0.3)
mySteel.Elastic(table=(elasticProperties, ) )

#-------------------------------------------------------

import section

# Create the solid section.

mdb.models['Beam'].RectangularProfile(name='Profile-1', a=0.5, b=0.5)

mdb.models['Beam'].BeamSection(name='Section-1', integration=DURING_ANALYSIS, 
    poissonRatio=0.0, profile='Profile-1', material='Steel', 
    temperatureVar=LINEAR, consistentMassMatrix=False)


# Assign the section to the region. The region refers 
# to the single cell in this model.

e = myBeam.edges
edges = e.getSequenceFromMask(mask=('[#1ffff ]', ), )
region = myBeam.Set(edges=edges, name='allFrames')
myBeam.SectionAssignment(region=region, sectionName='Section-1', offset=0.0, 
    offsetType=MIDDLE_SURFACE, offsetField='', 
    thicknessAssignment=FROM_SECTION)

#-------------------------------------------------------

#: Beam orientations have been assigned to the selected regions.
p = mdb.models['Beam'].parts['Part-1']
e = p.edges
edges = e.findAt(((33.75, 15.0, 0.0), ), ((45.0, 26.25, 0.0), ), ((33.75, 30.0, 
    0.0), ), ((30.0, 26.25, 0.0), ), ((30.0, 11.25, 0.0), ), ((33.75, 0.0, 
    0.0), ), ((45.0, 11.25, 0.0), ), ((18.75, 30.0, 0.0), ), ((18.75, 15.0, 
    0.0), ), ((18.75, 0.0, 0.0), ), ((3.75, 0.0, 0.0), ), ((0.0, 3.75, 0.0), ), 
    ((0.0, 18.75, 0.0), ), ((3.75, 30.0, 0.0), ), ((15.0, 26.25, 0.0), ), ((
    15.0, 11.25, 0.0), ), ((3.75, 15.0, 0.0), ))
region=p.Set(edges=edges, name='beamSet')
p = mdb.models['Beam'].parts['Part-1']
p.assignBeamSectionOrientation(region=region, method=N1_COSINES, n1=(0.0, 0.0, -1.0))


#-------------------------------------------------------

import assembly

# Create a part instance.

myAssembly = myModel.rootAssembly
myInstance = myAssembly.Instance(name='beamInstance',
    part=myBeam, dependent=OFF)

#-------------------------------------------------------

import step

# Create a step. The time period of the static step is 1.0, 
# and the initial incrementation is 0.1; the step is created
# after the initial step. 

myModel.StaticStep(name='beamLoad', previous='Initial',
    timePeriod=1.0, initialInc=0.1,
    description='Load the top of the beam.')

#-------------------------------------------------------

import load

# Find the end face using coordinates.

a = mdb.models['Beam'].rootAssembly
v1 = a.instances['beamInstance'].vertices
verts1 = v1.getSequenceFromMask(mask=('[#ccf ]', ), )
region = a.Set(vertices=verts1, name='loadSet')
mdb.models['Beam'].ConcentratedForce(name='Load-1', createStepName='beamLoad', 
    region=region, cf1=-506250.0, distributionType=UNIFORM, field='', 
    localCsys=None)


# a = mdb.models['Beam'].rootAssembly
# e1 = a.instances['beamInstance'].edges
# edges1 = e1.findAt(((33.75, 0.0, 0.0), ), ((18.75, 0.0, 0.0), ), ((3.75, 0.0, 
    # 0.0), ))
# v1 = a.instances['beamInstance'].vertices
# verts1 = v1.findAt(((30.0, 0.0, 0.0), ), ((45.0, 0.0, 0.0), ), ((15.0, 0.0, 
    # 0.0), ), ((0.0, 0.0, 0.0), ))
# region = a.Set(vertices=verts1, edges=edges1, name='BCSet')
# mdb.models['Beam'].DisplacementBC(name='BC-1', createStepName='beamLoad', 
    # region=region, u1=0.0, u2=0.0, ur3=0.0, amplitude=UNSET, fixed=OFF, 
    # distributionType=UNIFORM, fieldName='', localCsys=None)
    
import regionToolset

mdb.models['Beam'].ExpressionField(name='soilPressure', localCsys=None, 
    description='', expression='0.5*18000* (30- Y )')
    
    
a = mdb.models['Beam'].rootAssembly
e1 = a.instances['beamInstance'].edges
edges1 = e1.findAt(((0.0, 1.0, 0.0), ), ((0.0, 5.0, 0.0), ), ((0.0, 9.0, 0.0), 
    ))
region = regionToolset.Region(edges=edges1)
mdb.models['Beam'].LineLoad(name='Load-2', createStepName='beamLoad', 
    region=region, comp1=1.0, distributionType=FIELD, field='soilPressure')
    
#-------------------------------------------------------

import mesh

a = mdb.models['Beam'].rootAssembly
e1 = a.instances['beamInstance'].edges
pickedEdges = e1.findAt(((33.75, 15.0, 0.0), ), ((45.0, 26.25, 0.0), ), ((
    33.75, 30.0, 0.0), ), ((30.0, 26.25, 0.0), ), ((30.0, 11.25, 0.0), ), ((
    45.0, 11.25, 0.0), ), ((18.75, 30.0, 0.0), ), ((18.75, 15.0, 0.0), ), ((
    0.0, 3.75, 0.0), ), ((0.0, 18.75, 0.0), ), ((3.75, 30.0, 0.0), ), ((15.0, 
    26.25, 0.0), ), ((15.0, 11.25, 0.0), ), ((3.75, 15.0, 0.0), ))
a.seedEdgeByNumber(edges=pickedEdges, number=1, constraint=FINER)

a = mdb.models['Beam'].rootAssembly
e1 = a.instances['beamInstance'].edges
pickedEdges = e1.findAt(((33.75, 0.0, 0.0), ), ((18.75, 0.0, 0.0), ), ((3.75, 
    0.0, 0.0), ))
a.seedEdgeByNumber(edges=pickedEdges, number=10, constraint=FINER)

a = mdb.models['Beam'].rootAssembly
partInstances =(a.instances['beamInstance'], )
a.generateMesh(regions=partInstances)

#
#: The set 'bottomNodes' has been created (31 nodes).
a = mdb.models['Beam'].rootAssembly
n1 = a.instances['beamInstance'].nodes
nodes1 = n1[4:6]+n1[8:10]+n1[12:39]
a.Set(nodes=nodes1, name='bottomNodes')

#-------------------------------------------------------
a = mdb.models['Beam'].rootAssembly
region=a.sets['bottomNodes']
datum = None
mdb.models['Beam'].rootAssembly.engineeringFeatures.SpringDashpotToGround(
    name='bottomSpring', region=region, orientation=None, dof=1, 
    springBehavior=ON, springStiffness=10000000.0, dashpotBehavior=OFF, 
    dashpotCoefficient=0.0)

mdb.models['Beam'].rootAssembly.engineeringFeatures.SpringDashpotToGround(
    name='bottomSpring', region=region, orientation=None, dof=2, 
    springBehavior=ON, springStiffness=10000000.0, dashpotBehavior=OFF, 
    dashpotCoefficient=0.0)

    
a = mdb.models['Beam'].rootAssembly
v1 = a.instances['beamInstance'].vertices
verts1 = v1.getSequenceFromMask(mask=('[#20 ]', ), )
region = regionToolset.Region(vertices=verts1)    
mdb.models['Beam'].DisplacementBC(name='BC-1', createStepName='Initial', 
    region=region, u1=SET, u2=SET, ur3=SET, amplitude=UNSET, 
    distributionType=UNIFORM, fieldName='', localCsys=None)

#-------------------------------------------------------

mdb.models['Beam'].fieldOutputRequests['F-Output-1'].setValues(variables=('S', 
    'PE', 'PEEQ', 'PEMAG', 'LE', 'U', 'RF', 'RM', 'CF', 'SF', 'TF', 'CSTRESS', 
    'CDISP'))
    
import job

myJob= mdb.Job(name='Job-1', model='Beam', description='', type=ANALYSIS, atTime=None, 
    waitMinutes=0, waitHours=0, queue=None, memory=90, memoryUnits=PERCENTAGE, 
    getMemoryFromAnalysis=True, explicitPrecision=SINGLE, 
    nodalOutputPrecision=SINGLE, echoPrint=OFF, modelPrint=OFF, 
    contactPrint=OFF, historyPrint=OFF, userSubroutine='', scratch='', 
    multiprocessingMode=DEFAULT, numCpus=8, numDomains=8, numGPUs=0)

# Wait for the job to complete.

myJob.submit()
myJob.waitForCompletion()

#-------------------------------------------------------

import visualization

# Open the output database and display a
# default contour plot.

myOdb = visualization.openOdb(path=jobName + '.odb')
myViewport.setValues(displayedObject=myOdb)
myViewport.odbDisplay.display.setValues(plotState=CONTOURS_ON_DEF)

myViewport.odbDisplay.commonOptions.setValues(renderStyle=FILLED)

