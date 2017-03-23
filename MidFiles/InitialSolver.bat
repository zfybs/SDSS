@echo off
cd /d D:\Workspace\Ansys

"C:\Softwares\Civil Engineering\Ansys\v150\ANSYS\bin\winx64\ANSYS150.exe"  -p struct -dir "D:\Workspace\Ansys" -j "XinZhuang" - s read -l en-us -b -i "E:\GitHubProjects\SDSS\AnsysSolver\Model1.sdinp" -o "D:\Workspace\Ansys\Output.sdo"
