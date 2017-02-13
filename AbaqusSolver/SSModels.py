# -*- coding: utf-8 -*-
from uFrame import uMaterialType,uMaterial,uProfile,uProfileType,uBeam,uColumn
from uFrame import uFrame,uLoad


# ===============================================================================
# construct the whole frame

def ImportUserModel1(filePath):
    mE = uMaterial('steel', uMaterialType.elastic, 2000, 200e9, 0.3)
    pBeam = uProfile('beam', uProfileType.rectangular, 0.5, 0.8)
    pCol = uProfile('column', uProfileType.rectangular, 1, 1)
    beamLength = 10
    columnHeight = 5
    spans = 3
    layers = 2

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

    # loads
    load = uLoad(kx= 1e6,ky= 1e6,kc = 0.5)
    frame = uFrame('JianChuanRoad', (mE,), (pBeam, pCol), spans, layers, columns, beams, load)

    return frame

# frame = ImportUserModel1('')
pass