# -*- coding: utf-8 -*-
import re
import traceback
from os import path

p = r'E:\GitHubProjects\SDSS\AbaqusSolver'

m = path.join(p, path.pardir, r'MidFiles')  # E:\GitHubProjects\SDSS\AbaqusSolver\..\MidFiles

try:
    try:
        raise IOError()
    except ZeroDivisionError as ex:
        print('error captured in inner try...catch block')
except IOError as ex:
    print('error captured in outer try...catch block')
