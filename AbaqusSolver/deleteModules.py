# -*- coding: utf-8 -*-
import sys

'''

execfile(r'E:\GitHubProjects\SDSS\AbaqusSolver\deleteModules.py')
execfile(r'E:\GitHubProjects\SDSS\AbaqusSolver\EnvironmentBuild.py')

'''

names = ("SSModels","Model1","Model1Functions","uFrame","uEntity","TestCode",'EnvironmentBuild','ProgramEntrance')
# del(sys.modules['SSModels'])

for n in names:
    try:
        del(sys.modules[n])  # 从进程中删除某些已经导入过的模块
        print('module " ' + n + ' " deleted')
    except:
        pass
    finally:
        pass

pass


