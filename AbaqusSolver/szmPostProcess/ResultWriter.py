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
    tag = 'T'

    fw.writeLines(tag + sep + '位移（水平）' + sep + resultDataType.number + sep + "整个矩形框架的最大水平位移", result.maxDeflectionX)
    fw.writeLines(tag + sep + '位移点（水平）' + sep + resultDataType.vector + sep + "整个矩形框架的最大水平位移出现的位置", list(result.coordinateMaxU1))
    fw.writeLines(tag + sep + '位移（竖向）' + sep + resultDataType.number + sep + "整个矩形框架的最大竖向位移", result.maxDeflectionY)
    fw.writeLines(tag + sep + '位移点（竖向）' + sep + resultDataType.vector + sep + "整个矩形框架的最大竖向位移出现的位置", list(result.coordinateMaxU2))
    fw.writeLines(tag + sep + '轴力' + sep + resultDataType.number + sep + "整个矩形框架的最大轴力", result.maxAxialF)
    fw.writeLines(tag + sep + '轴力点' + sep + resultDataType.vector + sep + "整个矩形框架的最大轴力出现的位置", list(result.coordinateMaxF1))
    fw.writeLines(tag + sep + '剪力' + sep + resultDataType.number + sep + "整个矩形框架的最大剪力", result.maxShearF)
    fw.writeLines(tag + sep + '剪力点' + sep + resultDataType.vector + sep + "整个矩形框架的最大剪力出现的位置", list(result.coordinateMaxF2))
    fw.writeLines(tag + sep + '弯矩' + sep + resultDataType.number + sep + "整个矩形框架的最大弯矩", result.maxMoment)
    fw.writeLines(tag + sep + '弯矩点' + sep + resultDataType.vector + sep + "整个矩形框架的最大弯矩出现的位置", list(result.coordinateMaxM))

    __maxLayerX = [] # list中的第一个元素为框架结构最下层板的水平位移最大值
    for i in range(len(result.maxLayerX)):
        __maxLayerX.append(result.maxLayerX[i])
    fw.writeLines(tag + sep + '层位移（水平）' + sep + resultDataType.vector + sep + "矩形框架每一层的最大水平位移，第一个数表示最底层", __maxLayerX)

    fw.writeLine(tag + sep + '层位移点（水平）' + sep + resultDataType.array2d + sep + "矩形框架每一层最大水平位移出现的位置， 第一个元素表示最底层")
    for i in range(len(result.coordinatesMaxLX)):
        fw.writeLine(list(result.coordinatesMaxLX[i])) # 第一行元素为框架结构最下层板水平位移最大点的坐标

    __maxLayerY = [] # list中的第一个元素为框架结构最下层板的竖向位移最大值
    for i in range(len(result.maxLayerY)):
        __maxLayerY.append(result.maxLayerY[i])
    fw.writeLines(tag + sep + '层位移（竖向）' + sep + resultDataType.vector + sep + "矩形框架每一层的最大竖向位移，第一个数表示最底层", __maxLayerY)

    fw.writeLine(tag + sep + '层位移点（竖向）' + sep + resultDataType.array2d + sep + "矩形框架每一层最大竖向位移出现的位置， 第一个元素表示最底层")
    for i in range(len(result.coordinatesMaxLY)):
        fw.writeLine(list(result.coordinatesMaxLY[i])) # 第一行元素为框架结构最下层板竖向位移最大点的坐标

    __maxLayerAxialF = [] # list中的第一个元素为框架结构最下层板的轴力最大值
    for i in range(len(result.maxLayerAxialF)):
        __maxLayerAxialF.append(result.maxLayerAxialF[i])
    fw.writeLines(tag + sep + '层轴力' + sep + resultDataType.vector + sep + "矩形框架每一层的最大轴力，第一个数表示最底层", __maxLayerAxialF)

    fw.writeLine(tag + sep + '层轴力点' + sep + resultDataType.array2d + sep + "矩形框架每一层的最大轴力出现的位置，第一个元素表示最底层")
    for i in range(len(result.coordinatesMaxLAF)):
        fw.writeLine(list(result.coordinatesMaxLAF[i])) # 第一行元素为框架结构最下层板轴力最大点的坐标

    __maxLayerShearF = [] # list中的第一个元素为框架结构最下层墙柱的剪力最大值
    for i in range(len(result.maxLayerShearF)):
        __maxLayerShearF.append(result.maxLayerShearF[i])
    fw.writeLines(tag + sep + '层剪力' + sep + resultDataType.vector + sep + "矩形框架每一层的最大剪力，第一个数表示最底层", __maxLayerShearF)

    fw.writeLine(tag + sep + '层剪力点' + sep + resultDataType.array2d + sep + "矩形框架每一层的最大剪力出现的位置，第一个元素表示最底层")
    for i in range(len(result.coordinatesMaxLSF)):
        fw.writeLine(list(result.coordinatesMaxLSF[i])) # 第一行元素为框架结构最下层墙柱剪力最大点的坐标

    __maxLayerMoment = [] # list中的第一个元素为框架结构最下层墙柱的弯矩最大值
    for i in range(len(result.maxLayerMoment)):
        __maxLayerMoment.append(result.maxLayerMoment[i])
    fw.writeLines(tag + sep + '层弯矩' + sep + resultDataType.vector + sep + "矩形框架每一层的最大弯矩，第一个数表示最底层", __maxLayerMoment)

    fw.writeLine(tag + sep + '层弯矩点' + sep + resultDataType.array2d + sep + "矩形框架每一层的最大弯矩出现的位置，第一个元素表示最底层")
    for i in range(len(result.coordinatesMaxLM)):
        fw.writeLine(list(result.coordinatesMaxLM[i])) # 第一行元素为框架结构最下层墙柱弯矩最大点的坐标