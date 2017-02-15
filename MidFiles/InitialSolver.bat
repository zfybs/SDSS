@echo off
rem : The directory containing the files created during the calcution as well as the results.
cd /d D:\Workspace\abaqus\ModelStationTest

rem : Execute Abaqus without showing the users interface.
abaqus cae script=E:\GitHubProjects\SDSS\AbaqusSolver\EnvironmentBuild.py
