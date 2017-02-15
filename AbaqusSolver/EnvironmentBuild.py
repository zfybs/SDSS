# -*- coding: utf-8 -*-
''' 环境搭建 并 启动计算程序 '''
__author__ = 'zengfy'

import io, sys,os
from os import path

def __findSourceCodeDir():
    ''' 找到记录有各种路径信息的那个文本文件

    :return:记录有各种文件位置的那个文件的绝对路径
    '''
    p1 = os.getcwd()  # Abaqus 的工作空间文件夹
    p1 = r"D:\Workspace\abaqus\ModelStationTest"
    pathsInfo = path.join(p1,r"CalculationFiles.paths")
    return pathsInfo

def __getPath(filepath):
    ''' 从文件中读取各种路径信息
    :param filepath:
    :return: 一个字典，其中保存了各种文件对象所在的路径
    :rtype: dict
    '''

    # 从文件中读取各种路径信息
    txtFile=open(filepath, 'r')
    sepTag  = ' * '
    paths = {}

    line = txtFile.readline()
    while line:
        line =line.strip('\r\n') # 删除字符串结尾的换行符
        kv = line.split(sepTag)
        paths[kv[0]] = kv[1]
        line = txtFile.readline()

    txtFile.close()
    return paths

def build():
    ''' 从文件中读取各种路径信息
    :param filepath:
    :return: 一个字典，其中保存了各种文件对象所在的路径
    :rtype: dict
    '''
    pathsInfo = __findSourceCodeDir()
    paths = __getPath(pathsInfo)
    return paths

if __name__ == '__main__':
    paths =  build()
    # 将源代码所在文件夹添加到系统路径中，这样才能搜索到源代码中的指定模块
    sys.path.append(paths['PythonSourceDir'])

    import ProgramEntrance
    ProgramEntrance.Main(paths= paths)



