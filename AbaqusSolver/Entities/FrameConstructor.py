# -*- coding: utf-8 -*-
import xml.etree.ElementTree as ET
from Frame import uMaterialType,uMaterial,uProfile,uProfileType,uBeam,uColumn
from Frame import uFrame,uLoad

def ImportUserModel1(filePath):
    s1 = r'E:\SDSS\bin\m1.sdss'
    tree = ET.ElementTree(file=s1)
    root = tree.getroot()

    materials={}
    for c in root:
        if c.tag=='Definitions':
            for d in c :
                if d.tag=='Materials':
                    for fv in d:
                        matType = uMaterialType.GetMaterialType(fv.attrib['Type'])
                        mat=uMaterial(fv.attrib['Name'],matType,float(fv.attrib['Density']),float(fv.attrib['Elasticity']),float(fv.attrib['PoissonRatio']))
                        material={fv.attrib['Name']:mat}
                        materials.update(material)
    mEs=materials.values()
    mE=tuple(mEs)


    profiles={}
    for c in root:
        if c.tag=='Definitions':
            for d in c :
                if d.tag=='Profiles':
                    for fv in d:
                        profileType=uProfileType.GetProfileType(fv.attrib['Type'])
                        if profileType==1:
                            prof=uProfile(fv.attrib['Name'],profileType,float(fv.attrib['Width']),float(fv.attrib['Height']))
                        elif profileType==2:
                            prof=uProfile(fv.attrib['Name'],profileType,float(fv.attrib['Width']),float(fv.attrib['Height']),float(fv.attrib['GeneralThickness']))
                        profile={fv.attrib['Name']:prof}
                        profiles.update(profile)
    pEs=profiles.values()
    pE=tuple(pEs)

    index_Xs=[]
    for c in root:
        if c.tag=='Definitions':
            for d in c :
                if d.tag=='FrameVertices':
                    for fv in d:
                        index_Xs.append(fv.attrib['Index_X'])
    index_X=[int(i)for i in index_Xs]
    spans=max(index_X)

    index_Ys=[]
    for c in root:
        if c.tag=='Definitions':
            for d in c :
                if d.tag=='FrameVertices':
                    for fv in d:
                        index_Ys.append(fv.attrib['Index_Y'])
    index_Y=[int(i)for i in index_Ys]
    layers=max(index_Y)

    vertices={}
    for c in root:
        if c.tag=='Definitions':
            for d in c :
                if d.tag=='FrameVertices':
                    for fv in d:
                        position=(float(fv.attrib['X']),float(fv.attrib['Y']))
                        a={fv.attrib['ID']:position}
                        vertices.update(a)


    beams=[]
    for c in root:
        if c.tag=='Beams':
            for fv in c:
                index=(int(fv.attrib['LeftVerticeId'])-int(fv.attrib['ID']),int(fv.attrib['ID'])%(spans+1))
                length=float(vertices[fv.attrib['RightVerticeId']][0])-float(vertices[fv.attrib['LeftVerticeId']][0])
                leftpoint=vertices[fv.attrib['LeftVerticeId']]
                mat = None
                if materials.has_key(fv.attrib['MaterialName']):
                    mat = materials[fv.attrib['MaterialName']]
                beam=uBeam("b_"+str(index[0])+"_"+str(index[1]),profiles[fv.attrib['ProfileName']],mat,index,length,leftpoint)
                beams.append(beam)
    beams=tuple(beams)

    columns=[]
    for c in root:
        if c.tag=='Columns':
            for fv in c:
                index=((int(fv.attrib['BottomVerticeId'])-1)%(spans+1),int((int(fv.attrib['BottomVerticeId'])-1)/(spans+1)))
                height=float(vertices[fv.attrib['TopVerticeId']][1])-float(vertices[fv.attrib['BottomVerticeId']][1])
                bottomPoint=vertices[fv.attrib['BottomVerticeId']]
                mat=None
                if materials.has_key(fv.attrib['MaterialName']):
                    mat = materials[fv.attrib['MaterialName']]
                column=uColumn("c_"+str(index[0])+"_"+str(index[1]),profiles[fv.attrib['ProfileName']],mat,index,height,bottomPoint)
                columns.append(column)
    columns=tuple(columns)


    load = uLoad(kx= 1e6,ky= 1e6,kc = 0.5)

    frame = uFrame('JianChuanRoad', mE,pE, spans, layers, columns, beams, load)

    return frame

pass