# -*- coding: utf-8 -*-
# from enum import Enum

from abaqus import *
from abaqusConstants import *
import part,material,section,assembly,step,load,mesh,job,visualization,regionToolset

class uProfileType(object):
    '''构件横截面类型'''
    rectangular = 1  # 其对应的截面参数依次为：截面宽度b、截面高度h
    T = 2  # 其对应的截面参数依次为：截面宽度b、截面高度h、翼缘厚度tf、腹板宽度tw

    __nameProfile = {'Rectangular':rectangular,
                      'T':T,
                      }
    @classmethod
    def GetProfileType(cls,name):
        '''根据截面名称获取对应的截面类型
        :param name: 截面名称
        :type name: str
        '''
        if cls.__nameProfile.has_key(name):
            return cls.__nameProfile[name]
        else:
            return None

class uMaterialType(object):
    '''材料类型'''
    elastic = 1  # 其对应的材料参数依次为：密度、弹性模量、泊松比
    plastic = 2

    __nameMaterial = {'Elastic':elastic,
                      'MohrCoulomb':plastic,
                      }
    @classmethod
    def GetMaterialType(cls,name):
        '''根据材料名称获取对应的材料类型
        :param name: 材料名称
        :type name: str
        '''
        if cls.__nameMaterial.has_key(name):
            return cls.__nameMaterial[name]
        else:
            return None

class uAbqEntity(object):
    '''abaqus 中对应的模块对象，其每一个对象都有一个名称信息，而且对应在Abaqus中的 repository 中时，这些名称都是唯一的。'''

    def __init__(self, name):
        self._name = name

    @property
    def name(self):
        ''' abaqus 中对应的模块对象，其每一个对象都有一个名称信息，而且对应在Abaqus中的 repository 中时，这些名称都是唯一的。 '''
        return  self._name

# ===============================================================================
class uProfile(uAbqEntity):
    '''各种横截面信息'''

    def __init__(self, name, profileType, *args):
        '''
        :type profileType: uProfileType
        :type args:tuple[float]
        '''
        super(uProfile, self).__init__(name)
        self.sectionType = profileType
        self.parameters = args  # 对应于不同类型的截面，其参数的个数与对应的意义不同
        self.area = self.__getArea()

    def createProfile(self, model):
        '''在Abaqus模型中创建一种新的材料'''
        print(self.name)
        if self.sectionType == uProfileType.rectangular:
            p = model.RectangularProfile(name=self.name, a=self.parameters[0], b=self.parameters[1])
        elif self.sectionType == uProfileType.T:
            p = model.RectangularProfile(name=self.name, a=self.parameters[0], b=self.parameters[1])
            pass
            # raise NotImplementedError()

    def __getArea(self):
        '''计算截面的面积
        :rtype: float '''
        if self.sectionType == uProfileType.rectangular:
            return self.parameters[0] * self.parameters[1]
        elif self.sectionType == uProfileType.T:
            p = self.parameters
            return p[0]*p[2] + (p[1]-p[2])*p[3]

class uMaterial(uAbqEntity):
    '''材料信息'''

    def __init__(self, name, materialType, density, elas, poisson, *args):
        '''
        :param args:除了弹性材料的必需参数外，其他的材料所必需的参数
        :type materialType: uMaterialType
        '''
        super(uMaterial, self).__init__(name)
        self.materialType = materialType

        self.density = density
        self.elas = elas
        self.poisson = poisson

        self.parameters = args  # 对应于不同类型的材料，其参数的个数与对应的意义不同

    def createMaterial(self, model):
        '''在Abaqus模型中创建一种新的材料'''

        # 所有的材料都会有的三种属性
        m = model.Material(name=self.name)
        m.Elastic(table=((self.elas, self.poisson),))
        m.Density(table=((self.density, ), ))

        if self.materialType == uMaterialType.elastic:
            pass
        elif self.materialType == uMaterialType.plastic:
            pass
            # raise NotImplementedError()

class uSection(uAbqEntity):
    '''材料信息'''

    @staticmethod
    def getSectionFromModel(model, material, profile):
        '''从Abaqus的模型中提取或者创建一种section

        :type material: uMaterial
        :type profile: uProfile
        '''
        sName = material.name  + '_' + profile.name
        sections = model.sections
        if sections.has_key(sName):
            # 直接提取对应的 section
            return sections[sName]
        else:
            #  创建一个新的 section
            section = model.BeamSection(name=sName, integration = DURING_ANALYSIS,
                        poissonRatio=material.poisson, profile=profile.name, material = material.name,
                        temperatureVar= LINEAR, consistentMassMatrix=False)
            return section

