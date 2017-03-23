@echo off
rem : The directory containing the files created during the calcution as well as the results.
cd /d D:\UserFiles\Documents\Abaqus

rem : Execute Abaqus without showing the users interface.
abaqus cae noGUI=E:\GitHubProjects\SDSS\AbaqusSolver\EnvironmentBuild.py
