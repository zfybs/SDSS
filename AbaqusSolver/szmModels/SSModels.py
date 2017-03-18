# -*- coding: utf-8 -*-

from szmEntities.uFrame import *
from szmEntities.Component import uBeam,uColumn
from szmEntities.Method1 import Method1
from szmEntities.Structure1 import Structure1


# ===============================================================================
# construct the whole frame

def ImportUserModel1(filePath):
    mE = uMaterial('steel', uMaterialType.elastic, 2000, 200e9, 0.3)
    pBeam = uProfile('beam', uProfileType.rectangular, 0.5, 0.8)
    pCol = uProfile('column', uProfileType.rectangular, 1, 1)
    beamLength = 10
    columnHeight = 5
    spans = 5 # 15
    layers = 3 # 10

    # define the beams
    beams = []
    for bi in range(1, spans + 1):
        for bj in range(0, layers + 1):
            beams.append(uBeam('b_' + str(bi) + '_' + str(bj),
                               pBeam, mE, (bi, bj), beamLength,
                               leftPoint=((bi - 1) * beamLength, (bj) * columnHeight)))
    # define the columns
    columns = []
    for bi in range(0, spans+1):
        for bj in range(1, layers+1):
            columns.append(uColumn('c_' + str(bi) + '_' + str(bj),
                                   pCol, mE, (bi, bj), columnHeight,
                                   bottomPoint=((bi) * beamLength, (bj - 1) * columnHeight)))
    beams = tuple(beams)
    columns = tuple(columns)
    strut = Structure1(spans, layers, columns, beams)
    method = Method1(10000, 10000, 0.5)
    frame = uFrame1('JianChuanRoad', (mE,), (pBeam, pCol), strut, method)

    return frame

pass