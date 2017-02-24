# -*- coding: utf-8 -*-
__author__ = 'zengfy'
from Constants import uConstants

class fileWriter(object):
    '''    文本写入：支持写入行，支持手动或者自动关闭文本    '''

    def __init__(self, filePath):
        ''' 文本写入：支持写入行，支持手动或者自动关闭文本
        :param filePath:
        :return:
        '''
        self.__writer = open(filePath, 'w')

    def write(self, string):
        self.__writer.write(string)

    def writeLine(self,string):
        string = str(string) + uConstants.crLf
        self.__writer.write(string)

    def writeLines(self, *strs):
        for s in strs:
            self.writeLine(s)

    def __safeCloseResultFile(self,fid):
        '''
        :param fid:
        :return:
        '''
        if fid != None and (not fid.closed):
            fid.close()

    def dispose(self):
        self.__safeCloseResultFile(self.__writer)

    def __del__(self):
        ''' 析构函数 '''
        self.dispose()


