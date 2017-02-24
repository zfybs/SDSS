# -*- coding: utf-8 -*-
import  sys
from szmDefinitions.Constants import uConstants
from szmDefinitions.ProjectPath import projectPath

__author__ = 'zengfy'


class envirmtHandler(object):
    '''    为了Abaqus计算过程与后处理过程进行对接的一些环境变量的设置，比如异常数据的输出位置，    '''
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
        self.openMessageFile(filePath = projectPath.get_PyOutputFile())

        pass

    # sys.stdout -----------------------------------------------
    def setStdoutMessage(self):
        '''
        将 print() 函数的输出路径修改为指定的文本文件
        :return:
        '''
        f_h = open(projectPath.get_PyMessageFile(),'w')
        self.newStdout = f_h
        sys.stdout = f_h

    def restoreStdout(self):
        self.safeCloseFile(self.newStdout)

        #
        self.newStdout = None
        if  self.originalStdout != None:
            sys.stdout = self.originalStdout

    # message -----------------------------------------------

    __messageWriter = None

    @staticmethod
    def safeCloseFile(fid):
        ''' 以安全的方式关闭指定的文件
        :type fid: file
        :param fid: open() 函数的返回值
        '''
        if fid!=None and (not fid.closed):
            fid.close()

    @classmethod
    def openMessageFile(cls, filePath):
        '''

        :param filePath:
        :return:
        '''
        cls.safeCloseFile(cls.__messageWriter)

        # 如下下面的 filePath 不存在，则会创建一个新的文件
        cls.__messageWriter = open(filePath, mode = 'w')

    @classmethod
    def writeLine(cls,str):
        str = str + uConstants.crLf
        cls.__messageWriter.write(str)

    # dispose -----------------------------------------------
    def dispose(self):
        '''       在整个Python脚本执行完成后，将所有的资源进行释放        '''
        # 模块对象操作
        tp = type(self)
        self.safeCloseFile(tp.__messageWriter)

        # 实例对象操作
        self.restoreStdout()

        #

        pass

