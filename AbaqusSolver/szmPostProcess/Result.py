__author__ = 'zengfy'
# -*- coding: utf-8 -*-
import sys,os

class uResult(object):

    def __init__(self,maxDeflectionX,coordinateMaxU1,
                 maxDeflectionY,coordinateMaxU2,
                 maxAxialF,coordinateMaxF1,
                 maxShearF,coordinateMaxF2,
                 maxMoment,coordinateMaxM):

        '''
        :param maxDeflectionX : 最大水平位移
        :param maxDeflectionY : 最大竖向位移
        :param maxMoment : 最大弯矩
        :param maxShearF : 最大剪力
        :param maxAxialF : 最大轴力
        :param coordinateMaxU1 : 最大水平位移点位置
        :param coordinateMaxU2 : 最大竖向位移点位置
        :param coordinateMaxM : 最大弯矩点位置
        :param coordinateMaxF2 : 最大剪力点位置
        :param coordinateMaxF1 : 最大轴力点位置
        :return:

        :type maxDeflectionX : float
        :type maxDeflectionY : float
        :type maxAxialF : float
        :type maxMoment : float
        :type maxShearF : float
        '''
        self.maxDeflectionX = maxDeflectionX
        self.coordinateMaxU1 = coordinateMaxU1
        self.maxDeflectionY = maxDeflectionY
        self.coordinateMaxU2 = coordinateMaxU2
        self.maxAxialF=maxAxialF
        self.coordinateMaxF1 = coordinateMaxF1
        self.maxShearF=maxShearF
        self.coordinateMaxF2 = coordinateMaxF2
        self.maxMoment=maxMoment
        self.coordinateMaxM = coordinateMaxM


class uResult1(uResult):

    def __init__(self,maxDeflectionX,coordinateMaxU1,
                 maxDeflectionY,coordinateMaxU2,
                 maxAxialF,coordinateMaxF1,
                 maxShearF,coordinateMaxF2,
                 maxMoment,coordinateMaxM,
                 maxLayerX,coordinatesMaxLX,
                 maxLayerY,coordinatesMaxLY,
                 maxLayerAxialF,coordinatesMaxLAF,
                 maxLayerShearF,coordinatesMaxLSF,
                 maxLayerMoment,coordinatesMaxLM):
        '''
        :param maxLayerX : 每一层的最大水平位移，第一个数表示最下层
        :type maxLayerX : tuple
        :param coordinatesMaxLX : 每一层最大水平位移点对应的坐标， 第一个元素表示最下层
        :type coordinatesMaxLX : tuple
        '''

        super(uResult1, self).__init__(maxDeflectionX,coordinateMaxU1,
                                       maxDeflectionY,coordinateMaxU2,
                                       maxAxialF,coordinateMaxF1,
                                       maxShearF,coordinateMaxF2,
                                       maxMoment,coordinateMaxM,)
        self.maxLayerX =maxLayerX
        self.coordinatesMaxLX = coordinatesMaxLX
        self.maxLayerY =maxLayerY
        self.coordinatesMaxLY = coordinatesMaxLY
        self.maxLayerAxialF =maxLayerAxialF
        self.coordinatesMaxLAF = coordinatesMaxLAF
        self.maxLayerShearF =maxLayerShearF
        self.coordinatesMaxLSF = coordinatesMaxLSF
        self.maxLayerMoment =maxLayerMoment
        self.coordinatesMaxLM = coordinatesMaxLM

    def getcoordinatesMaxLM(self):

        self.coordinatesMaxLM = 0
        pass
