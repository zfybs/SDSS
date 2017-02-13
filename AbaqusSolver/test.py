# -*- coding: utf-8 -*-
import xml.etree.ElementTree as ET

s1 = r'E:\GitHubProjects\SDSS\bin\m1.sdss'

tree = ET.ElementTree(file=s1)
root = tree.getroot()
verticeXs = []

for c in root:
    if c.tag == 'Vertices':
        for fv in c:
            verticeXs.append(fv.attrib["Index_X"])
pass