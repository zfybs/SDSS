# -*- coding: utf-8 -*-
import sys , os

'''
execfile(r'E:\GitHubProjects\SDSS\AbaqusSolver\deleteModules.py')
execfile(r'E:\GitHubProjects\SDSS\AbaqusSolver\EnvironmentBuild.py')
'''

names = ("SSModels","Model1","Model1Functions","Frame","Entity","TestCode",'EnvironmentBuild','ProgramEntrance','PostProcess')
# del(sys.modules['SSModels'])

sourceCodeDir = r'E:\GitHubProjects\SDSS\AbaqusSolver'

def __deleteModules(sourceDir):
    '''     删除Python代码模块，以使修改后的源代码可以更新
    :param sourceDir: Python 源代码所在的文件夹
    :return:
    '''
    count = len(sourceDir)
    fileNames = []
    walker = os.walk(sourceDir)
    for cc in walker:

        # 作为模块名，不仅是一个名称，而是一个相对路径，
        # 比如 szmEntities\Component.py 这个模块，在Abaqus中的模块名为：szmEntities.Component

        # 先减去整个父文件夹的长度，但是如果是在子文件夹中，则会在中间多出一个字节的字符 '\\' ，所以要取 Count + 1
        parentModuleName = cc[0][count + 1:].replace("\\", ".")
        for f in cc[2]:
            if f.endswith('.py'):
                # 将文件路径转换为模块名
                if len(parentModuleName) == 0:
                    fileNames.append(f[0:f.__len__() - 3])
                else:
                    fileNames.append(parentModuleName + '.' + f[0:f.__len__() - 3])

    # 根据文件名寻找对应的模块名
    print('* --------------------------------- \n')
    for moduleName in fileNames:
        # 作为模块名，不仅是一个名称，而是一个相对路径，
        # 比如 szmEntities\Component.py 这个模块，在Abaqus中的模块名为：szmEntities.Component

        # names = ("SSModels","Model1","Model1Functions","Frame","Entity","TestCode",'EnvironmentBuild','ProgramEntrance','PostProcess')
        if moduleName != "EnvironmentBuild":
            try:
                del(sys.modules[moduleName])  # 从进程中删除某些已经导入过的模块
                print('module "' + moduleName + '" deleted')
            except:
                pass
    print('* --------------------------------- \n')


__deleteModules(sourceCodeDir)

