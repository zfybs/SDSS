# -*- coding: utf-8 -*-
import re
import traceback

def writeLine(string):
    string = string + '\n'
    print(string)

def writeLines( *strs):
    for s in strs:
        writeLine(s)

writeLines("1","3")