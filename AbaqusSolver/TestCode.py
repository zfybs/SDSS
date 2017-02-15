# -*- coding: utf-8 -*-
import os,io,sys
# execfile(r"E:\GitHubProjects\SDSS\AbaqusSolver\TestCode.py")

from Definitions.constants import uConstants
print('s1 = '+ str( uConstants.g))

def openFile():
    '''
    :rtype: file
    :return:
    '''
    filePath = __file__
    filePath = r'C:\Users\zengfy\Desktop\1.txt'
    f = open(filePath,'a+',0)
    return f

fp = openFile()

print ('open',fp.tell())
x = fp.read()
print ('open read()',fp.tell())
print (x)
fp.seek(2)
fp.write("add\n")
print ('write 1-6',fp.tell())
x = fp.read()
print ("first read\n",x)
fp.flush()
x = fp.read()
print ("second read\n", x,fp.tell())

fp.tell()
line = fp.readline()
ln = 1
while line:
    if ln == 1:
        print(line)
        fp.seek(5)
        fp.write("add")
    line = fp.readline()
    ln += 1
fp.close()
pass
