# -*- coding: utf-8 -*-
import  sys
from szmDefinitions.Constants import uConstants
from szmDefinitions.ProjectPath import projectPath

__author__ = 'zengfy'


class envirmtHandler(object):
    '''  为了Abaqus计算过程与后处理过程进行对接的一些环境变量的设置，比如异常数据的输出位置，
        此类中维护两个文件：message与output，其中，
            1. message 用来作为Abaqus 计算过程中， sys.stdout 所对应的输出流文件，此文件中的信息没有任何代码上的特殊意义，只供用户自行查看。
            2. output 用来记录 Python 脚本运行过程中，用户指定输出的与模型相关的数据，其中最关键的是标识着 Abaqus 计算过程是否正常结束信息。
    '''
    # 文本中字段与值之间的分隔
    fieldSeperator = r' * '
    __abqWorkingDir = ""
    def __init__(self,abqWorkingdir):
        tp = type(self)
        tp.__abqWorkingDir = abqWorkingdir

        #
        self.originalStdout = sys.stdout
        self.newStdout = None

    def setupForPostprocess(self):
        # self.setStdoutMessage()
        self.openOutputFile(filePath = projectPath.get_PyOutputFile())

        pass

    @staticmethod
    def __safeCloseFile(fid):
        ''' 以安全的方式关闭指定的文件
        :type fid: file
        :param fid: open() 函数的返回值
        '''
        if fid!=None and (not fid.closed):
            fid.close()

    # message 文件：sys.stdout 与 print() 函数所对应的输出流文件，无实际意义 -----------------------------------------------
    def setStdoutMessage(self):
        '''
        将 print() 函数的输出路径修改为指定的文本文件
        :return:
        '''
        f_h = open(projectPath.get_PyMessageFile(),'w')
        self.newStdout = f_h
        sys.stdout = f_h

    def restoreStdout(self):
        self.__safeCloseFile(self.newStdout)

        #
        self.newStdout = None
        if  self.originalStdout != None:
            sys.stdout = self.originalStdout

    # output 文件： Python 脚本运行过程中，用户指定输出的与模型相关的数据，以及计算过程是否正常成功 ------------

    __outputWriter = None

    @classmethod
    def openOutputFile(cls, filePath):
        '''

        :param filePath:
        :return:
        '''
        cls.__safeCloseFile(cls.__outputWriter)

        # 如下下面的 filePath 不存在，则会创建一个新的文件
        cls.__outputWriter = open(filePath, mode = 'w')

    @classmethod
    def writeLine(cls,str):
        '''在 output 文件中进行写入'''
        str = str + uConstants.crLf
        cls.__outputWriter.write(str)

    # dispose ： 关闭所有文本 -----------------------------------------------
    def dispose(self):
        '''       在整个Python脚本执行完成后，将所有的资源进行释放        '''
        # 模块对象操作
        tp = type(self)
        self.__safeCloseFile(tp.__outputWriter)

        # 实例对象操作
        self.restoreStdout()

        #

        pass

