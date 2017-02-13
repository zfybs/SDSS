# -*- coding: utf-8 -*-
import sys

'''
execfile(r'D:\Workspace\PycharmProjects\SDSS\deleteModules.py')
execfile(r'D:\Workspace\PycharmProjects\SDSS\Model1.py')

'''

names = ("SSModels","Model1","Model1Functions","uFrame","uEntity")
# del(sys.modules['SSModels'])

for n in names:
    try:
        del(sys.modules[n])
        print('module " ' + n + ' " deleted')
    except:
        pass
    finally:
        pass

pass