__author__ = 'Long'
# -*- coding: utf-8 -*-
from abaqus import *
from abaqusConstants import *
from Structure1 import *
from szmEntities.Vertice import uVertice,uVerticeFrame
from Component import uBeam,uColumn
from Structure1 import Structure1

class Method1 (object):
    '''惯性力法'''
    def __init__(self, kx, ky, kc):
        '''
        :param kx: 底部弹簧的水平刚度
        :param ky: 底部弹簧的竖向刚度
        :param kc: 矩形地铁车站结构等代水平地震惯性力系数
        :type kx: float
        :type ky: float
        :type pMax: float
        :type kc: float
        :return:
        '''
        self.name = "惯性力法"
        self.kx = kx
        self.ky = ky
        self.kc = kc
        # self.acceleration = acceleration


