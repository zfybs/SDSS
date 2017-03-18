# -*- coding: utf-8 -*-
import xml.etree.ElementTree as ET

from szmDefinitions.Definition import uMaterialType,uMaterial,uProfile,uProfileType
from szmEntities.Component import uBeam,uColumn
from szmEntities.Vertice import uVerticeFrame
from szmEntities.uFrame import uFrame1
from szmEntities.Structure1 import Structure1
from szmEntities.Method1 import Method1

def ImportUserModel1(filePath):
    # filePath = r'E:\SDSS\bin\StationDesginModel.sdss'
    tree = ET.ElementTree(file=filePath)
    root = tree.getroot()

    modelName = root.attrib['ModelName']

    # -------------- 根节点下的第一级子节点的搜索 ----------------------------------------------------------

    # find the element of definitions the elementTree
    eleDefinitions = root
    eleSoilProperty = root
    eleFrame = root
    for c in root:
        if c.tag == 'Definitions':
            eleDefinitions = c
        elif c.tag == 'MethodProperty':
            eleSoilProperty = c
        elif c.tag == 'Frame':
            eleFrame = c
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
                prof = None
                if profileType== uProfileType.rectangular:
                    prof=uProfile(fv.attrib['Name'],profileType,float(fv.attrib['Width']),float(fv.attrib['Height']))
                elif profileType== uProfileType.T:
                    prof=uProfile(fv.attrib['Name'],profileType,float(fv.attrib['Width']),float(fv.attrib['Height']),float(fv.attrib['FlangeThickness']),float(fv.attrib['WebThickness']),)
                profile={fv.attrib['Name']: prof}
                profiles.update(profile)
    pEs=profiles.values()
    pE=tuple(pEs)

    # ------------------------------------------------------------------------

    spans = 0
    layers = 0
    vertices={}
    for d in eleFrame :
        if d.tag=='FrameVertices':
            for fv in d:
                idX = int(fv.attrib['Index_X'])
                if spans<idX : spans = idX

                idY = int(fv.attrib['Index_Y'])
                if layers<idY : layers = idY

                v = uVerticeFrame(x = float(fv.attrib['X']),  y = float(fv.attrib['Y']),
                                  index =(idX, idY),  connectedComponents = None)

                vertices[fv.attrib['ID']] = v
            break
    # ------------------------------------------------------------------------

    beams=[]
    for c in eleFrame:
        if c.tag=='Beams':
            for fv in c:

                leftP = vertices[fv.attrib['LeftVerticeId']]
                rightP = vertices[fv.attrib['RightVerticeId']]

                index=(leftP.index[0] + 1, leftP.index[1])
                length= rightP.x - leftP.x
                leftpoint = (leftP.x, leftP.y)
                ##
                mat = materials[fv.attrib['Material']]
                prof = profiles[fv.attrib['Profile']]
                ##
                name =  "b_"+str(index[0])+"_" + str(index[1])
                beam=uBeam(name,prof,mat,index,length,leftpoint)

                beams.append(beam)
            break
    beams=tuple(beams)

    columns=[]
    for c in eleFrame:
        if c.tag=='Columns':
            for fv in c:

                bottomP = vertices[fv.attrib['BottomVerticeId']]
                topP = vertices[fv.attrib['TopVerticeId']]

                index=(bottomP.index[0] , bottomP.index[1] + 1)
                ##
                height = topP.y - bottomP.y
                bottomPoint=(bottomP.x, bottomP.y)
                ##
                mat = materials[fv.attrib['Material']]
                prof = profiles[fv.attrib['Profile']]
                column=uColumn("c_"+str(index[0])+"_"+ str(index[1]),prof, mat, index, height, bottomPoint)
                columns.append(column)
            break
    columns=tuple(columns)

    # ------------------------------------------------------------------------

    kx = float(eleSoilProperty.attrib['Kx'])
    ky = float(eleSoilProperty.attrib['Ky'])
    kc = float(eleSoilProperty.attrib['Kc'])

    ##
    method1 = Method1(kx= kx, ky= ky, kc = kc)
    strut = Structure1(spans, layers, columns, beams)
    frame = uFrame1(modelName, mE, pE, strut, method1)
    return frame

pass
