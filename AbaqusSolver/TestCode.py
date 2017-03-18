# -*- coding: utf-8 -*-
import re
import traceback
from os import path



class Friend():
    def __init__(self, name):
        self.name = name

    def __str__(self):
        return "Friend : %s" % self.name

friend = Friend('Liang')
print friend
