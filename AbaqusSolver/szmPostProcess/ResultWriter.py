# -*- coding: utf-8 -*-
__author__ = 'zengfy'
from os import path

from szmPostProcess.Result import uResult1
from szmDefinitions.ProjectPath import projectPath
from szmPostProcess.EnvirmtHandler import envirmtHandler
from szmDefinitions.Utils import fileWriter
from szmDefinitions.Constants import uConstants

class resultDataType(object):
    string = "string" # this is a string
    number = "number"  # 0.0
    vector = "vector"  # [0.0, 1.0, 2.0, 3.0] ...
    array2d = "array2d" # [0.0,1.0,2.0] crlf [0.0,1.0,2.0] ...
    array2dNU = "array2dNU" # 2D array with non-uniform length in multiple rows.
sep = uConstants.fieldSeperator

def writeResult(result):
    '''
    :type result: uResult1
    :param result:
    :return:
    '''
    fw = fileWriter(projectPath.get_AbqResultFile())
    __t = 'T'
    fw.writeLines(__t + sep + 'maxDeflectionX' + sep + resultDataType.number + sep, result.maxDeflectionX)
    fw.writeLines(__t + sep + 'coordinateMaxU1' + sep + resultDataType.vector + sep, list(result.coordinateMaxU1))
    fw.writeLines(__t + sep + 'maxDeflectionY' + sep + resultDataType.number + sep,result.maxDeflectionY)
    fw.writeLines(__t + sep + 'coordinateMaxU2' + sep + resultDataType.vector + sep,list(result.coordinateMaxU2))
    fw.writeLines(__t + sep + 'maxAxialF' + sep + resultDataType.number + sep,result.maxAxialF)
    fw.writeLines(__t + sep + 'coordinateMaxF1' + sep + resultDataType.vector + sep,list(result.coordinateMaxF1))
    fw.writeLines(__t + sep + 'maxShearF' + sep + resultDataType.number + sep,result.maxShearF)
    fw.writeLines(__t + sep + 'coordinateMaxF2' + sep + resultDataType.vector + sep,list(result.coordinateMaxF2))
    fw.writeLines(__t + sep + 'maxMoment' + sep + resultDataType.number + sep,result.maxMoment)
    fw.writeLines(__t + sep + 'coordinateMaxM' + sep + resultDataType.vector + sep,list(result.coordinateMaxM))

    __maxLayerX = [] # list中的第一个元素为框架结构最下层板的水平位移最大值
    for i in range(len(result.maxLayerX)):
        __maxLayerX.append(result.maxLayerX[i])
    fw.writeLines(__t + sep + 'maxLayerX' + sep + resultDataType.vector + sep, __maxLayerX)

    fw.writeLine(__t + sep + 'coordinatesMaxLX' + sep + resultDataType.array2d + sep)
    for i in range(len(result.coordinatesMaxLX)):
        fw.writeLine(list(result.coordinatesMaxLX[i])) # 第一行元素为框架结构最下层板水平位移最大点的坐标

    __maxLayerY = [] # list中的第一个元素为框架结构最下层板的竖向位移最大值
    for i in range(len(result.maxLayerY)):
        __maxLayerY.append(result.maxLayerY[i])
    fw.writeLines(__t + sep + 'maxLayerY' + sep + resultDataType.vector + sep, __maxLayerY)

    fw.writeLine(__t + sep + 'coordinatesMaxLY' + sep + resultDataType.array2d + sep)
    for i in range(len(result.coordinatesMaxLY)):
        fw.writeLine(list(result.coordinatesMaxLY[i])) # 第一行元素为框架结构最下层板竖向位移最大点的坐标

    __maxLayerAxialF = [] # list中的第一个元素为框架结构最下层板的轴力最大值
    for i in range(len(result.maxLayerAxialF)):
        __maxLayerAxialF.append(result.maxLayerAxialF[i])
    fw.writeLines(__t + sep + 'maxLayerAxialF' + sep + resultDataType.vector + sep, __maxLayerAxialF)

    fw.writeLine(__t + sep + 'coordinatesMaxLAF' + sep + resultDataType.array2d + sep)
    for i in range(len(result.coordinatesMaxLAF)):
        fw.writeLine(list(result.coordinatesMaxLAF[i])) # 第一行元素为框架结构最下层板轴力最大点的坐标

    __maxLayerShearF = [] # list中的第一个元素为框架结构最下层墙柱的剪力最大值
    for i in range(len(result.maxLayerShearF)):
        __maxLayerShearF.append(result.maxLayerShearF[i])
    fw.writeLines(__t + sep + 'maxLayerShearF' + sep + resultDataType.vector + sep, __maxLayerShearF)

    fw.writeLine(__t + sep + 'coordinatesMaxLSF' + sep + resultDataType.array2d + sep)
    for i in range(len(result.coordinatesMaxLSF)):
        fw.writeLine(list(result.coordinatesMaxLSF[i])) # 第一行元素为框架结构最下层墙柱剪力最大点的坐标

    __maxLayerMoment = [] # list中的第一个元素为框架结构最下层墙柱的弯矩最大值
    for i in range(len(result.maxLayerMoment)):
        __maxLayerMoment.append(result.maxLayerMoment[i])
    fw.writeLines(__t + sep + 'maxLayerMoment' + sep + resultDataType.vector + sep, __maxLayerMoment)

    fw.writeLine(__t + sep + 'coordinatesMaxLM' + sep + resultDataType.array2d + sep)
    for i in range(len(result.coordinatesMaxLM)):
        fw.writeLine(list(result.coordinatesMaxLM[i])) # 第一行元素为框架结构最下层墙柱弯矩最大点的坐标
