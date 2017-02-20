# -*- coding: utf-8 -*-
__author__ = 'zengfy'
import sys
from os import path
import codecs
from codecs import StreamWriter
sout = sys.stdout
sin = sys.stdin
a = sin.readline()
#
t = codecs.lookup("utf-8")
msgTxt = r'D:\UserFiles\Documents\Abauqs\message.txt'

f_h = open(msgTxt,'w')
sys.stdout = f_h

# fin = codecs.open(msgTxt, "w", "utf-8")
# print fin.write(sout.name)

lst = range(0,100)
for i in lst:
    print(str(i))

pass