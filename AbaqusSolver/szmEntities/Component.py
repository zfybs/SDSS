# -*- coding: utf-8 -*-
__author__ = 'zengfy'

from szmDefinitions.Definition import uAbqEntity
from szmDefinitions.Constants import uConstants

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
        if length < minLength:
            return int(length/minSize)
        else:
            return fixedNumber

class uBeam(uComponent):
    def __init__(self, name, profile, material, index, length, leftPoint):
        '''水平梁构件的构造函数
        :param name:
        :param profile: 横截面
        :param material: 材料
        :param index: 梁构件所在位置，(1,0)表示整个地铁车站框架的底板、最左侧梁，(2,1)表示最底层的顶板中从左至右第2根梁
        :param length: 长度
        :param leftPoint: 水平梁的最左边点的二维空间坐标，其数据类型为 tuple，如 (1,2.0)
        :return:
        :type index: (int, int)
        :type leftPoint: (int, int)
        '''
        super(uBeam, self).__init__(name, profile, material)
        self.index = index
        self.length = length
        self.leftPoint = leftPoint
        self.weight = self.profile.area * self.length * self.material.density * uConstants.g


    def getMiddlePoint(self):
        '''构件中间点的三维坐标
        :return: 构件中间点的三维坐标
        :rtype: (float,float,float)
        '''
        return (self.leftPoint[0]+self.length/2 , self.leftPoint[1] , 0)

    def getBoundingBox(self):
        '''此构件所在的三维空间立方体的位置
        :return: 返回6个值，分别对应 minX, minY, 0, maxX, maxY, 0
        :rtype: tuple[float]
          '''
        return (self.leftPoint[0] ,             self.leftPoint[1],      0 ,
                self.leftPoint[0]+self.length,   self.leftPoint[1] ,     0)


    def __str__(self):
        return 'index:%s, p1:%s, p2:%s' %(self.index,
                                         self.leftPoint,
                                         (self.leftPoint[0]+self.length, self.leftPoint[1]))

class uColumn(uComponent):
    def __init__(self, name, profile, material, index, height, bottomPoint):
        ''' 竖向柱构件的构造函数
        :param name:
        :param profile:
        :param material:
        :param index: 柱构件所在位置，(0,1)表示整个地铁车站框架最底层中最左侧的柱，(2,2)表示倒数第2层中，从左向右数第3根柱
        :param height:
        :param bottomPoint: 竖向柱的最下边点的二维空间坐标，其数据类型为 tuple，如 (1,2.0)
        :return:
        :type profile: uProfile
        :type material: uMaterial
         '''
        super(uColumn, self).__init__(name, profile, material)
        self.index = index
        self.height = height
        self.bottomPoint = bottomPoint
        self.weight = self.profile.area * self.height * self.material.density * uConstants.g

    def getMiddlePoint(self):
        '''构件中间点的三维坐标
        :return: 构件中间点的三维坐标
        :rtype: (float,float,float)
        '''
        return (self.bottomPoint[0] , self.bottomPoint[1]+ self.height/2 ,  0)

    def getBoundingBox(self):
        '''此构件所在的三维空间立方体的位置
        :return: 返回6个值，分别对应 minX, minY, 0, maxX, maxY, 0
        :rtype: tuple[float]
        '''
        return self.bottomPoint[0] , self.bottomPoint[1], 0 , self.bottomPoint[0], self.bottomPoint[1] + self.height , 0


    def __str__(self):
        return 'index:%s, p1:%s, p2:%s' %(self.index,
                                         self.bottomPoint,
                                         (self.bottomPoint[0], self.bottomPoint[1] + self.height))
