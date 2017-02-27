# -*- coding: utf-8 -*-
from array import array

from abaqus import *
from abaqusConstants import *

from szmDefinitions.Definition import *
from szmEntities.Load import uLoad
from szmEntities.Vertice import uVertice,uVerticeFrame,uBeam,uColumn

class uFrame(object):
    '''整个梁柱框架'''

    def __init__(self, name, materials, profiles, spans, layers, columns,beams, load):
        '''
        :param name: 框架的名称
        :param materials: 整个模型中所有的材料集合
        :param profiles: 整个模型中所有的构件剖面集合
        :param spans: 框架中一共有几跨
        :param layers: 框架中一共有几层
        :param columns: 框架中所有的柱对象的集合
        :param beams: 框架中所有的梁对象的集合
        :return:

        :type spans: int
        :type layers: int
        :type materials: tuple[uMaterial]
        :type profiles: tuple[uProfile]
        :type beams: tuple[uBeam]
        :type beams: tuple[uBeam]
        :type columns: tuple[uColumn]
        :type load: uLoad
        '''
        self.name = name
        self.materials = materials
        self.profiles = profiles
        self.spans = spans
        self.layers = layers
        self.load = load
        self.beams, self.columns = self.__sortBeamsColumns(beams,columns)

        # iNodes 表示一个元组，其元素个数为 self.spans + 1,元素的值表示框架上每一个节点 x 坐标
        # vertices 表示一个二维列表，其有（self.spans + 1）行（self.layers + 1）列，每个元素都表示一个框架节点对象
        self.iNodes, self.jNodes,self.vertices  = self.__constructVerticesSystem()
        self.__getConnectedComponentsSystem()
        self.load.pMax = self.__getPmax()

    # ================================== 框架梁柱的集合排序

    def __sortBeamsColumns(self, beams,columns):
        '''将任意排序的梁柱集合进行重新排序
        :type beams: tuple[uBeam]
        :type columns: tuple[uColumn]
        :rtype: (tuple[uBeam],tuple[uColumn])
        '''
        stBeams = list(range(self.spans * (self.layers+1)))
        for b in beams:
            i, j = b.index[0] , b.index[1]
            l1 = j * self.spans # 每一层所对应的最左边的梁的编号
            index = l1 + (i-1) # 举例而言，底板上最左边的梁的集合下标为 0 ，底板上从左向右第2根梁的下标为 1
            stBeams[index] = b

        stColumns = list(range(self.layers * (self.spans+1)))
        for c in columns:
            i, j = c.index[0] , c.index[1]
            l1 = (j-1) * (self.spans + 1) # 每一层所对应的最左边的柱的编号
            index = l1 + i  # 举例而言，最底层最左边柱集合下标为 0 ，最底层从左向右第二根柱的集合下标为 1
            stColumns[index] = c

        return tuple(stBeams) ,tuple(stColumns)

    def __constructVerticesSystem(self):
        '''定义整个框架上x与y方向上每一个节点的位置

        :return:
        :rtype: (tuple[float], tuple[float], list[list[uVerticeFrame]])
        '''
        iNodes = array('d',range(self.spans+1))
        jNodes = array('d',range(self.layers+1))
        # vertices = [0,] * ((self.spans+1) * (self.layers + 1))
        vertices = [([0] * (self.layers+1)) for i in range(self.spans+1)]   # 初始化节点，形成一个(self.spans+1)行(self.layers+1)列的二维数组

        # 通过每一个梁单元的信息来构建框架上所有节点的定位
        for b in self.beams:
            i,j= b.index[0] , b.index[1]
            x1 , x2 , y = b.leftPoint[0], b.leftPoint[0] + b.length , b.leftPoint[1]
            #
            iNodes[i-1] = x1
            iNodes[i] = x2
            #
            jNodes[j] = y
            #
            vertices[i-1][j] = uVerticeFrame(x=x1,y=y,index=(i-1,j),connectedComponents=None) # 梁构件左侧节点
            vertices[i][j] = uVerticeFrame(x=x1,y=y,index=(i,j),connectedComponents=None)   # 梁构件右侧节点

        return tuple(iNodes), tuple(jNodes) ,vertices

    # ====================================================================

    def __getIndexedBeam(self, i, j):
        '''根据梁的二维坐标来返回对应的梁对象
        :param i: 最左侧梁的 i 值为 1
        :param j: 底板梁的 j 值为 0
        :return:
        :type i: int
        :type j: int
        :rtype: uBeam
        '''
        if (i > self.spans) or (i <=0 ): return None
        if (j > self.layers)or (j <0 ):return None

        l1 = j * self.spans # 每一层所对应的最左边的梁的编号
        index = l1 + (i-1) # 举例而言，底板上最左边的梁的集合下标为 0 ，底板上从左向右第2根梁的下标为 1
        return self.beams[index]

    def __getIndexedColumn(self, i, j):
        '''根据柱的二维坐标来返回对应的柱对象
        :param i: 最左侧柱的 i 值为 0
        :param j: 最底层柱的 j 值为 1
        :return:
        :type i: int
        :type j: int
        :rtype: uColumn
        '''
        if (i > self.spans) or (i <0 ): return None
        if (j > self.layers)or (j <=0 ):return None
        l1 = (j-1) * (self.spans + 1) # 每一层所对应的最左边的柱的编号
        index = l1 + i  # 举例而言，最底层最左边柱集合下标为 0 ，最底层从左向右第二根柱的集合下标为 1

        return self.columns[index]

    def __getConnectedComponents(self, iNodeIndex, jNodeIndex):
        '''根据节点编号返回与此节点相连的梁与柱集合
        :param iNodeIndex: 整个框架中每个梁柱节点的坐标编号的i值，最左排的节点的 i 值为 0
        :param jNodeIndex: 整个框架中每个梁柱节点的坐标编号的i值，最下排的节点的 j 值为 0
        :return:
        :type iNodeIndex: int
        :type jNodeIndex: int
        :rtype: (uBeam,uBeam,uColumn,uColumn)
        '''
        b1 = self.__getIndexedBeam(i=iNodeIndex,j= jNodeIndex)      # 节点左侧的梁，如果未搜索到或者已经到达边界，则返回 None
        b2 = self.__getIndexedBeam(i=iNodeIndex + 1,j= jNodeIndex)  # 节点右侧的梁，如果未搜索到或者已经到达边界，则返回 None
        c1 = self.__getIndexedColumn(i=iNodeIndex,j= jNodeIndex)    # 节点下侧的柱，如果未搜索到或者已经到达边界，则返回 None
        c2 = self.__getIndexedColumn(i=iNodeIndex,j= jNodeIndex + 1)# 节点上侧的柱，如果未搜索到或者已经到达边界，则返回 None

        return b1,b2,c1,c2

    def __getConnectedComponentsSystem(self):
        '''整个框架系统中每一个节点所连接的梁柱构件'''
        for iN in range(0,self.iNodes.__len__()):
            for jN in range(0,self.jNodes.__len__()):
                # 得到每一个节点的可能相连的四个构件
                components = self.__getConnectedComponents(iN,jN)
                weight = 0
                for component in components:
                    # 对于框架的边界，则对应的有些构件为 None
                    if component is not None:
                        # 将对应的构件连接信息保存下来
                        self.vertices[iN][jN].connectedComponents.append(component)

                        # 计算节点所连接的构件的重量
                        weight += component.weight
                        pass
                self.vertices[iN][jN].connectedWeight = weight / 2

    def __getPmax(self):
        '''根据整个框架中所有节点上集中荷载之和，求出框架左边三角形分布力的最大分布应力值'''
        wMax = 0 # 整个框架中所有节点集中荷载之和
        for vi in self.vertices:
            for vj in vi:
                wMax += vj.connectedWeight

        # 换算为三角形分布力，求出对应的最大分布力值
        h = self.jNodes[-1] - self.jNodes[0]
        # 这里要乘以kc，是因为在计算框架节点上的集合力时，是通过框架节点所连构件的总重量之半乘以 kc 。
        return (wMax * 2 / h) * self.load.kc

    def getNodeWeight(self, iNodeIndex, jNodeIndex):
        '''根据节点编号与结点相连的所有梁柱构件的重量之半的总和
        :param iNodeIndex: 整个框架中每个梁柱节点的坐标编号的i值，最左排的节点的 i 值为 0
        :param jNodeIndex: 整个框架中每个梁柱节点的坐标编号的i值，最下排的节点的 j 值为 0
        :return:
        :rtype: int
        '''
        return self.vertices[iNodeIndex][jNodeIndex].connectedWeight
    # ====================================================================
    def getFrameRegion(self):
        '''得到整个方形框架的空间矩形的范围'''
        minX, minY, maxX, maxY = self.iNodes[0], self.jNodes[0], self.iNodes[-1], self.jNodes[-1]
        return minX, minY, 0, maxX, maxY, 0

    def getBottomSlabRegion(self):
        '''得到整个方形框架的底板的范围'''
        minX, minY, maxX, maxY = self.iNodes[0], self.jNodes[0], self.iNodes[-1], self.jNodes[0]
        return minX, minY, 0, maxX, maxY, 0

    def getFloorSlabRegion(self):
        '''
        得到每一层楼板的范围及名称，组成一个list
        :return:
        '''

        listNodesRegion = []
        listElementsRegion = []
        minX = self.iNodes[0]
        maxX = self.iNodes[-1]
        for layerNum in range(0, self.layers+1):
            layerElev = self.jNodes[layerNum]
            nameNodes = 'Floor' + str(layerNum +1) + 'Nodes'
            nameElements = 'Floor' + str(layerNum +1) + 'Elements'
            minY = layerElev
            maxY = layerElev
            listNodesRegion.append((minX, minY, 0, maxX, maxY, 0, nameNodes))
            listElementsRegion.append((minX, minY, 0, maxX, maxY, 0, nameElements))
        return listNodesRegion, listElementsRegion

    def getLayerRegion(self):
        '''
        得到每一层柱子的范围及名称，组成一个list
        listLayerElementRegion = [[[columnRegion1],[columnRegion2],...], name]
        :return:
        '''
        # listLayerElementRegion = list()
        # minX = self.iNodes[0]
        # maxX = self.iNodes[-1]
        # for layerNum in range(0, self.layers):
        #     nameLayerElements = 'Layer' + str(layerNum + 1) + 'Elements'
        #     minY = self.jNodes[layerNum]
        #     maxY = self.jNodes[layerNum+1]
        #     listLayerElementRegion.append((minX, minY, 0, maxX, maxY, 0, nameLayerElements))
        # return listLayerElementRegion
        listLayerElementRegion = []
        for layerNum in range(0, self.layers):
            nameLayerElements = 'Layer' + str(layerNum + 1) + 'Elements'
            minY = self.jNodes[layerNum]
            maxY = self.jNodes[layerNum+1]
            listLayer = []
            for spanNum in range(0, self.spans+1):
                minX = self.iNodes[spanNum]
                maxX = self.iNodes[spanNum]
                listLayer.append((minX, minY, 0, maxX, maxY, 0))
            listLayerElementRegion.append((listLayer, nameLayerElements))
        return listLayerElementRegion

    def getLeftColumns(self):
        '''得到整个方形框架的最左边的一列柱子
        :rtype: list[uColumn]
        '''
        cols = list()
        for c in self.columns:
            if c.index[0] == 0:
                cols.append(c)
        return cols

    # ====================================================================
    def drawSketch(self, sketch):
        '''在Abaqus的Sketch对象上绘制梁柱框架'''
        for b in self.beams:
            sketch.Line(point1=b.leftPoint, point2=(b.leftPoint[0] + b.length, b.leftPoint[1]))
        for c in self.columns:
            sketch.Line(point1=c.bottomPoint, point2=(c.bottomPoint[0], c.bottomPoint[1] + c.height))
