# -*- coding: utf-8 -*-
import xml.etree.ElementTree as ET

from Frame import uMaterialType,uMaterial,uProfile,uProfileType
from szmEntities.Component import uBeam,uColumn
from szmEntities.Vertice import uVerticeFrame
from Frame import uFrame,uLoad

def ImportUserModel1(filePath):
    # filePath = r'E:\SDSS\bin\m1.sdss'
    tree = ET.ElementTree(file=filePath)
    root = tree.getroot()

    # find the element of definitions the elementTree
    eleDefinitions = 0
    for c in root:
        if c.tag=='Definitions':
            eleDefinitions = c

    # ------------------------------------------------------------------------

    materials={}
    for d in eleDefinitions :
        if d.tag=='Materials':
            for fv in d:
                matType = uMaterialType.GetMaterialType(fv.attrib['Type'])
                mat=uMaterial(fv.attrib['Name'],matType,float(fv.attrib['Density']),float(fv.attrib['Elasticity']),float(fv.attrib['PoissonRatio']))
                material={fv.attrib['Name']:mat}
                materials.update(material)
    mEs=materials.values()
    mE=tuple(mEs)


    profiles={}
    for d in eleDefinitions :
        if d.tag=='Profiles':
            for fv in d:
                profileType=uProfileType.GetProfileType(fv.attrib['Type'])
                if profileType== uProfileType.rectangular:
                    prof=uProfile(fv.attrib['Name'],profileType,float(fv.attrib['Width']),float(fv.attrib['Height']))
                elif profileType== uProfileType.T:
                    prof=uProfile(fv.attrib['Name'],profileType,float(fv.attrib['Width']),float(fv.attrib['Height']),float(fv.attrib['FlangeThickness']),float(fv.attrib['WebThickness']),)
                profile={fv.attrib['Name']:prof}
                profiles.update(profile)
    pEs=profiles.values()
    pE=tuple(pEs)

    # ------------------------------------------------------------------------

    spans = 0
    layers = 0
    vertices={}
    for d in eleDefinitions :
        if d.tag=='FrameVertices':
            for fv in d:
                idX = int(fv.attrib['Index_X'])
                if spans<idX : spans = idX

                idY = int(fv.attrib['Index_Y'])
                if layers<idY : layers = idY

                v = uVerticeFrame(x = float(fv.attrib['X']),  y = float(fv.attrib['Y']),
                                  index =(idX, idY),  connectedComponents = None)

                vertices[fv.attrib['ID']] = v

    # ------------------------------------------------------------------------

    beams=[]
    for c in root:
        if c.tag=='Beams':
            for fv in c:

                leftP = vertices[fv.attrib['LeftVerticeId']]
                rightP = vertices[fv.attrib['RightVerticeId']]

                index=(leftP.index[0] + 1, leftP.index[1])
                length= rightP.x - leftP.x
                leftpoint = (leftP.x, leftP.y)

                mat = None
                if materials.has_key(fv.attrib['MaterialName']):
                    mat = materials[fv.attrib['MaterialName']]

                name =  "b_"+str(index[0])+"_" + str(index[1])
                prof = profiles[fv.attrib['ProfileName']]
                beam=uBeam(name,prof,mat,index,length,leftpoint)

                beams.append(beam)
    beams=tuple(beams)

    columns=[]
    for c in root:
        if c.tag=='Columns':
            for fv in c:

                bottomP = vertices[fv.attrib['BottomVerticeId']]
                topP = vertices[fv.attrib['TopVerticeId']]

                index=(bottomP.index[0] , bottomP.index[1] + 1)

                height = topP.y - bottomP.y
                bottomPoint=(bottomP.x, bottomP.y)
                mat=None
                if materials.has_key(fv.attrib['MaterialName']):
                    mat = materials[fv.attrib['MaterialName']]
                column=uColumn("c_"+str(index[0])+"_"+str(index[1]),profiles[fv.attrib['ProfileName']],mat,index,height,bottomPoint)
                columns.append(column)
    columns=tuple(columns)

    # ------------------------------------------------------------------------

    load = uLoad(kx= 1e6,ky= 1e6,kc = 0.5)

    frame = uFrame('JianChuanRoad', mE,pE, spans, layers, columns, beams, load)

    return frame

pass