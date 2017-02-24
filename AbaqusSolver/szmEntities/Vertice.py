# -*- coding: utf-8 -*-

from  Component import uBeam,uColumn

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

class uVerticeFrame(uVertice):
    '''
    二维平面上，矩形框架结构的梁柱节点
    '''
    def __init__(self, x,y,index, connectedComponents):
        '''
        :param x:节点的x坐标
        :param y:节点的y坐标
        :param index:二维元组，表示节点的下标
        :param connectedComponents:与此节点相连的梁柱构件
        :return:
        :type index: (int,int)
        :type connectedComponents: list[Component]
        '''
        super(uVerticeFrame, self).__init__(x,y)
        self.index = index
        if connectedComponents is None:
            self.connectedComponents = list()
        else:
            self.connectedComponents = connectedComponents
        self.connectedWeight = 0

    def __str__(self):
        b,c = list(),list()
        for cp in self.connectedComponents:
            tp = type(cp)
            if tp is uBeam:
                c.append(cp.index)
            elif tp is uColumn:
                b.append(cp.index)

        s = 'beams: %s columns: %s' %(str(b),str(c))
        return s
