# -*- coding: utf-8 -*-
# import enum

from abaqus import *
from abaqusConstants import *
import part,material,section,assembly,step,load,mesh,job,visualization,regionToolset

class uConstants(object):
    ''' 类中保存了一些全局的常量 '''
    g = 9.8 # 重力加速度

class uVertice(object):
    '''二维平面上的几何节点'''
    def __init__(self, x ,y):
        '''二维平面上的几何节点
        :param x: 节点的二维空间坐标中的 x 值
        :type x: float
        :type y: float
        :return:
        '''
        self.x = x
        self.y = y

class uProfileType(object):
    '''构件横截面类型'''
    rectangular = 1  # 其对应的截面参数依次为：截面宽度b、截面高度h
    T = 2  # 其对应的截面参数依次为：截面宽度b、截面高度h、翼缘厚度tf、腹板宽度tw


class uMaterialType(object):
    '''材料类型'''
    elastic = 1  # 其对应的材料参数依次为：密度、弹性模量、泊松比
    plastic = 2


class uAbqEntity(object):
    '''abaqus 中对应的模块对象，其每一个对象都有一个名称信息，而且对应在Abaqus中的 repository 中时，这些名称都是唯一的。'''

    def __init__(self, name):
        self.name = name


# ===============================================================================
class uProfile(uAbqEntity):
    '''各种横截面信息'''

    def __init__(self, name, profileType, *args):
        '''
        :type profileType: uProfileType
        '''
        super(uProfile, self).__init__(name)
        self.sectionType = profileType
        self.parameters = args  # 对应于不同类型的截面，其参数的个数与对应的意义不同
        self.area = self.__getArea()

    def createProfile(self, model):
        '''在Abaqus模型中创建一种新的材料'''
        if self.sectionType == uProfileType.rectangular:
            p = model.RectangularProfile(name=self.name, a=self.parameters[0], b=self.parameters[1])
        elif self.sectionType == uProfileType.T:
            raise NotImplementedError()

    def __getArea(self):
        '''计算截面的面积
        :rtype: float '''
        if self.sectionType == uProfileType.rectangular:
            return self.parameters[0] * self.parameters[1]
        elif self.sectionType == uProfileType.T:
            raise NotImplementedError()

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
            raise NotImplementedError()

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
                        poissonRatio=material.poisson, profile=profile.name, material=material.name,
                        temperatureVar= LINEAR, consistentMassMatrix=False)
            return section


class uComponent(uAbqEntity):
    '''梁或柱等框架构件'''

    def __init__(self, name, profile, material):
        '''
        :type profile: uProfile
        :type material: uMaterial
        '''
        super(uComponent, self).__init__(name)
        self.profile = profile
        self.material = material
        self.sectionName = material.name + '_' + profile.name  # 截面与材料组成的Abaqus中的section的名称
        self.weight = 0 # 构件的重量

    def getSeedNumberByLS(self,length,minSize,fixedNumber):
        ''' 根据构件长度与最小网格尺寸来确定此构件要划分为多少个单元
        1. 对于柱构件，按单元个数进行划分，在保证最小网格尺寸为0.1m的前提下，将柱构件的单元个数固定为20个。
        2. 对于各层顶板梁，其单元的划分原则与柱相同；
        :param length: 直线构件的长度
        :param minSize: 网格的最小尺寸
        :param fixedNumber: 固定的单元个数
        :rtype: int
        :return:
        '''
        minLength = fixedNumber * minSize
        if length<minLength:
            return int(length/minSize)
        else:
            return fixedNumber
