# -*- coding: utf-8 -*-
__author__ = 'zengfy'
from os import path

from szmPostProcess.Result import uResult1
from szmDefinitions.ProjectPath import projectPath
from szmPostProcess.EnvirmtHandler import envirmtHandler
from szmDefinitions.Utils import fileWriter
from szmDefinitions.Constants import uConstants

sep = uConstants.fieldSeperator

def writeResult(result):
    '''
    :type result: uResult1
    :param result:
    :return:
    '''
    fw = fileWriter(projectPath.get_AbqResultFile())
    # 写入一维向量
    fw.writeLines('coordinatesMaxLM is :',result.coordinatesMaxLM)
    fw.writeLines('coordinateMaxF1 is :', result.coordinateMaxF1)
    fw.writeLines('coordinateMaxF1 is :', result.coordinateMaxF1)

    pass
