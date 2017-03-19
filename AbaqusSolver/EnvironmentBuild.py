# -*- coding: utf-8 -*-
''' 环境搭建 并 启动计算程序 '''
__file__ = r'EnvironmentBuild.py'

import traceback
import io, sys,os
from os import path
import shutil
fieldSeperator = r' * '

def __getPathDict(filepath):
    ''' 从文件中读取各种路径信息
    :param filepath:
    :return: 一个字典，其中保存了各种文件对象所在的路径
    :rtype: dict
    '''

    # 从文件中读取各种路径信息
    txtFile=open(filepath, 'r')
    paths = {}

    line = txtFile.readline()
    while line:
        line =line.strip('\r\n') # 删除字符串结尾的换行符
        kv = line.split(fieldSeperator)
        if len(kv) == 2 :
            paths[kv[0]] = kv[1]
        line = txtFile.readline()
    txtFile.close()

    return paths

def __deleteOneModule(moduleName):
    '''    根据模块名删除对应的模块
    :param moduleName: 模块名，如 ProgramEntrance ， szmDefinitin.Constants 等
    '''
    # 作为模块名，不仅是一个名称，而是一个相对路径，
    # 比如 szmEntities\Component.py 这个模块，在Abaqus中的模块名为：szmEntities.Component

    # names = ("SSModels","Model1","Model1Functions","Frame","Entity","TestCode",'EnvironmentBuild','ProgramEntrance','PostProcess')
    if sys.modules.has_key(moduleName):
        # # 删除模块所对应的文件
        del(sys.modules[moduleName])  # 从进程中删除某些已经导入过的模块
        # print('module "' + moduleName + '" deleted')
    else:
        pass

    pass

def __deleteModules(sourceDir):
    '''     删除Python代码模块，以使修改后的源代码可以更新
    :param sourceDir: Python 源代码所在的文件夹
    :return:
    '''
    count = len(sourceDir)
    modulesToBeDeleted = []
    for cc in os.walk(sourceDir):

        # 作为模块名，不仅是一个名称，而是一个相对路径，
        # 比如 szmEntities\Component.py 这个模块，在Abaqus中的模块名为：szmEntities.Component

        # 先减去整个父文件夹的长度，但是如果是在子文件夹中，则会在中间多出一个字节的字符 '\\' ，所以要取 Count + 1
        parentModuleName = cc[0][count + 1:].replace("\\", ".")

        for f in cc[2]:
            if f.endswith('.py'):
                # 将文件路径转换为模块名
                if len(parentModuleName) == 0:
                    modulesToBeDeleted.append(f[0:f.__len__() - 3])
                else:
                    modulesToBeDeleted.append(parentModuleName + '.' + f[0:f.__len__() - 3])

        # 不能仅删除.py文件所对应的模块，文件夹本身也是一种模块，必须要进行删除，否则其里面的 .py 模块不会被刷新。
        modulesToBeDeleted.append(parentModuleName)

    # 根据文件名寻找对应的模块名
    # print('\n* -------------------- Delete modules ------------------------------- ')
    for moduleName in modulesToBeDeleted:
        __deleteOneModule(moduleName)
    # print('* ------------------------------------------------------------------- \n')

def build(pathsDir,pathsTextFile,pythonSourceDirField,):
    ''' 从文件中读取各种路径信息
    :param pathsDir: 记录有 SDSS 程序的各种路径的 txt 文件所在的文件夹
    :param pathsTextFile: 记录有 SDSS 程序的各种路径的 txt 文件的文件名
    :param pythonSourceDirField: pathsTextFile 文件中记录“Python 源代码所在的文件夹”的字段的名称
    :return: 一个字典，其中保存了各种文件对象所在的路径
    :rtype: dict
    '''

    # 记录有各种路径信息的那个文本文件
    pathsFile = path.join(pathsDir,pathsTextFile)

    # 从文件中提取路径数据
    pathsdict = __getPathDict(pathsFile)

    # 删除已经加载过的模块，以更新Python源代码的修改
    # 这一句必须在 下面的 import 语句之前，不然 Constants 模块在导入后又被删除了
    __deleteModules(pathsdict[pythonSourceDirField])

    # 将源代码所在文件夹添加到系统路径中，这样才能搜索到源代码中的指定模块
    sys.path.append(pathsdict[pythonSourceDirField])

    return pathsdict

def getPathsDirectory(allowMultiInstance):
    ''' 记录有各种文件位置的那个文件（CalculationFiles.sdp）所在的文件夹
    :param allowMultiInstance: 即 allowMultiCalculationInstance，表示 SDSS 程序是否支持同时打开多个 Abaqus 进程进行不同模型的计算。
    在整个程序最后开发完成后，此参数的值应该设设置为常数 True。
    '''

    if allowMultiInstance:
        pathsDir = os.getcwd()  # Abaqus 的工作空间文件夹
        return pathsDir
    else:
        sourceCodePathsDir = r"E:\GitHubProjects\SDSS\AbaqusSolver"

        try:
            # pass
            raise IOError()
        except Exception:
            stackTrace = traceback.format_exc()
            import re
            ptn = r'file "(.*)"'
            flag = re.IGNORECASE or re.MULTILINE
            matches = re.findall(ptn,stackTrace, flag)
            if matches != None:
                tagFile = 'EnvironmentBuild.py'
                for m in matches:
                    if m.endswith(tagFile):
                        sourceCodePathsDir = m[:len(m)-len(tagFile)-1]
                        # print("find source code dir in the stackTrack code block.:", pathsDir)
                        break
        # 将 Python 源代码所在文件夹转换到  CalculationFiles.sdp 所在的文件夹
        pathsDir = path.join(sourceCodePathsDir, path.pardir, r'MidFiles')
        pathsDir = path.abspath(pathsDir)

        return pathsDir

if __name__ == '__main__':
    envirmttHdl = None
    finishTag = ""  # 用来表示整个Abaqus计算过程是否正常结果的标志字符串。
    try:

        # 记录有各种文件位置的那个文件（CalculationFiles.sdp）所在的文件夹
        pathsDir = getPathsDirectory(allowMultiInstance = False)

        pathsdict = build(pathsDir = pathsDir,
            pathsTextFile = r"CalculationFiles.sdp",
            pythonSourceDirField= 'PythonSourceDir')   # buildInDeveloperMode  build

        # 构造 路径字典 数据，以供后面调用
        from szmDefinitions.Constants import uConstants
        from szmDefinitions.ProjectPath import projectPath
        from szmPostProcess.EnvirmtHandler import envirmtHandler
        projectPath.PathDict = pathsdict

        # change the abaqus working directory
        os.chdir(projectPath.get_AbaqusWorkingDir())

        #
        envirmttHdl = envirmtHandler(projectPath.get_AbaqusWorkingDir())
        envirmttHdl.setupForPostprocess()
        #
        import ProgramEntrance
        ProgramEntrance.Main()
        #
        finishTag = r'*** Calculation finished successfully! ***'
    except Exception as ex:
        #
        finishTag = r'*** Calculation terminated with error! ***'
        print(finishTag)
        stackTrace = traceback.format_exc()
        print(ex.args)
        print(stackTrace)
        finishTag +=  uConstants.crLf + str(ex.args)
        finishTag +=  uConstants.crLf + str(stackTrace)
        finishTag +=  uConstants.crLf +  r'*** Error message ends here ***'

    finally:
        if envirmttHdl != None:
            envirmttHdl.writeLine(finishTag)
            envirmttHdl.dispose()
        pass
