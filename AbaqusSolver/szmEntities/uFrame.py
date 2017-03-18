# -*- coding: utf-8 -*-
__author__ = 'Long'
from Structure1 import *
from Method1 import *
from array import array
from szmDefinitions.Definition import *

class uFrame1(object):
    def __init__(self, name, materials, profiles, strut, method):
        '''
        :type strut: Structure1
        :type method: Method1
        :type materials: tuple[uMaterial]
        :type profiles: tuple[uProfile]
        :type name: string
        :param name: 计算模型名称
        :param materials: 整个框架的材料集合
        :param profiles: 整个框架的剖面集合
        :param strut: 模型对应的结构
        :param method: 模型对应的计算方法
        :return:
        '''
        self.name = name
        self.materials = materials
        self.profiles = profiles
        self.strut = strut
        self.method = method
        self.pMax = self.__getPmax()  # 三角形分布力底部最大值

    def __getPmax(self):
        '''根据整个框架中所有节点上集中荷载之和，求出框架左边三角形分布力的最大分布应力值
        :type structure: Structure1
        '''
        wMax = 0 # 整个框架中所有节点集中荷载之和
        for vi in self.strut.vertices:
            for vj in vi:
                wMax += vj.connectedWeight

        # 换算为三角形分布力，求出对应的最大分布力值
        h = self.strut.jNodes[-1] - self.strut.jNodes[0]
        # 这里要乘以kc，是因为在计算框架节点上的集合力时，是通过框架节点所连构件的总重量之半乘以 kc 。
        return (wMax * 2 / h) * self.method.kc