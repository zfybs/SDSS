# -*- coding: utf-8 -*-
__author__ = 'zengfy'

class projectPath(object):
    ''' 一个常量类，其中记录了与文件路径相关的各种常量    '''
    PathDict = {}

    @classmethod
    def get_ModelFile(cls):
        '''存放车站模型数据的 xml 文件'''
        return cls.PathDict['ModelFile']

    @classmethod
    def get_PythonSourceDir(cls):
        '''Python 源代码 所在文件夹'''
        return cls.PathDict['PythonSourceDir']

    @classmethod
    def get_AbaqusWorkingDir(cls):
        ''' Abaqus 计算的工作文件夹 '''
        return cls.PathDict['AbaqusWorkingDir']

    @classmethod
    def get_MiddleFileDir(cls):
        ''' SDSS 解决方案的中间文件夹 '''
        return cls.PathDict['MiddleFileDir']

    @classmethod
    def get_PyMessageFile(cls):
        ''' Abaqus 计算过程的信息输出文件，对应 python 中的 sys.stdout ，这些输出信息主要是程序运行中的提示，与具体的模型信息无关 '''
        return cls.PathDict['PyMessageFile']

    @classmethod
    def get_PyOutputFile(cls):
        ''' Abaqus 计算结果的信息输出文件，Python 脚本运行过程中，用户指定输出的与模型相关的数据 '''
        return cls.PathDict['PyOutputFile']

    @classmethod
    def get_AbqResultFile(cls):
        ''' Abaqus 计算结果的信息输出文件，Python 脚本运行过程中，用户指定输出的与模型相关的数据 '''
        return cls.PathDict['CalculationResultFile']

    pass