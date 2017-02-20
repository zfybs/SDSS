# -*- coding: utf-8 -*-
__author__ = 'zengfy'

class uConstants(object):
    ''' 类中保存了一些全局的常量 '''
    g = 9.8 # 重力加速度


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
        ''' Abaqus 计算过程的信息输出文件，对应 python 中的 sys.stdout '''
        return cls.PathDict['PyMessageFile']

    #
    # modelFile = PathDict["ModelFile"]
    # pythonSourceDir = PathDict["PythonSourceDir"]
    # abaqusWorkingDir = PathDict["AbaqusWorkingDir"]
    # middleFiles = PathDict["MiddleFiles"]

    pass