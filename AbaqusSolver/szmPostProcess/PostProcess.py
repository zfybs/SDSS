# -*- coding: utf-8 -*-
__author__ = 'zengfy'

from szmDefinitions.Constants import projectPath
from szmModels.Result import uResult1


def GetResult1(model,job):
    print(job.name)
    return uResult1(u"this is my name")

def writeResult(result):
    '''

    :param result:
    :type result: uResult1
    :return:
    '''

    pass
