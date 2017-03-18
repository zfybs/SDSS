# -*- coding: utf-8 -*-
'''整个计算程序的主入口'''
import sys
import os

from abaqus import *

from szmModels import SSModels,Model1
from szmPostProcess import PostProcess
from szmEntities import FrameConstructor
from szmDefinitions import Constants
from szmDefinitions.ProjectPath import projectPath
from szmPostProcess import ResultWriter

'''
execfile(r'E:\GitHubProjects\SDSS\AbaqusSolver\EnvironmentBuild.py')
'''

def checkCalculationCondition(frame):
    pathsDir = projectPath.get_AbaqusWorkingDir()
    for i in os.listdir(pathsDir):
        if i.endswith('.lck'):
            __path = os.path.join(pathsDir, i)
            os.remove(__path)
        # elif i == frame.name + '.odb':
        #     __path = os.path.join(pathsDir, i)
        #     if  session.odbs[__path] != None:
        #         session.odbs[__path].close()
        #     os.remove(__path)
        else:
            pass

def Main():
    ''' 整个计算程序的主入口 '''
    # 从 xml 文件中提取模型信息
    modelFile = projectPath.get_ModelFile()

    xmlFrame = FrameConstructor.ImportUserModel1(modelFile)

    eZFrame = SSModels.ImportUserModel1(modelFile)

    if True:
        # 对模型进行计算
        checkCalculationCondition(xmlFrame)
        model,job = Model1.Calculate1(xmlFrame)
    else:
        model,job = 100, 200
        pass
    # 将 result 中的信息写入外部文件，以供生成报告时提取

    # 通过计算结果文件提取报告所需要数据
    result = PostProcess.GetResult1(job, xmlFrame)

    ResultWriter.writeResult(result)

    PostProcess.printPicture(job, xmlFrame)

    pass