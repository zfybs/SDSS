# -*- coding: utf-8 -*-
'''整个计算程序的主入口'''

# from Definitions.uEntity import uProfile,uProfileType

from Models import SSModels

def Main(paths):
    ''' 整个计算程序的主入口
    :param paths: 前处理中各种文件的路径信息
    :type paths: dict    '''
    print(paths)

    modelFile = paths['ModelFile']

    myFrame = SSModels.ImportUserModel1(modelFile)

    # Model1.main(myFrame)

    pass
    pass
    pass

