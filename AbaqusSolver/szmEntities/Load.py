# -*- coding: utf-8 -*-
__author__ = 'zengfy'

class uLoad(object):
    '''整个梁柱框架上的各种荷载以及与荷载相关的信息，比如弹簧刚度等'''

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
        self.kx = kx
        self.ky = ky
        self.kc = kc
        # self.acceleration = acceleration
        self.pMax = 0  # 三角形分布力底部最大值
