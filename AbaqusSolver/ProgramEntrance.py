# -*- coding: utf-8 -*-
'''整个计算程序的主入口'''
import sys
from szmModels import SSModels,Model1
from szmPostProcess import PostProcess
from szmEntities import FrameConstructor
from szmDefinitions import Constants
from szmDefinitions.ProjectPath import projectPath
from szmPostProcess import ResultWriter

'''
execfile(r'E:\GitHubProjects\SDSS\AbaqusSolver\EnvironmentBuild.py')
'''

def checkCalculationCondition():
    pass

def Main():
    ''' 整个计算程序的主入口 '''
    # 从 xml 文件中提取模型信息
    modelFile = projectPath.get_ModelFile()

    xmlFrame = FrameConstructor.ImportUserModel1(modelFile)

    eZFrame = SSModels.ImportUserModel1(modelFile)

    if True:
        # 对模型进行计算
        model,job = Model1.Calculate1(xmlFrame)
    else:
        model,job = 100, 200
        pass
    # 将 result 中的信息写入外部文件，以供生成报告时提取

    # 通过计算结果文件提取报告所需要数据
    result = PostProcess.GetResult1(model,job,xmlFrame)

    ResultWriter.writeResult(result)

    pass