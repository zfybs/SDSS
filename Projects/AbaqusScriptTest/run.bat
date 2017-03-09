@echo off
rem : The directory for the files created during the calcution as well as the results.
cd /d C:\Users\zengfy\Desktop\AbaqusScriptTest

rem : Execute Abaqus without showing the users interface.
abaqus cae noGUI=beamExample.py