@echo off

SET /p BUILDNUM=< buildNum.txt

SET UNITYVERSION=2018.2.2f1
IF NOT [%1]==[] (set UNITYVERSION=%1)

SET PRODUCTNAME="Product Name"
IF NOT [%2]==[] (set PRODUCTNAME=%2)

SET COMPANYNAME="Company Name"
IF NOT [%3]==[] (set COMPANYNAME=%3)

SET TARGET=Windows
IF NOT [%4]==[] (set TARGET=%4)

SET VERSION=0.0.0
IF NOT [%5]==[] (set VERSION=%5)

SET BUILDLOCATION="./Build/%TARGET%"

rmdir -S %BUILDLOCATION%
mkdir %BUILDLOCATION%

>buildManifest.txt (
    echo ProductName=%PRODUCTNAME%
    echo CompanyName=%COMPANYNAME%
    echo Version=%VERSION%.%BUILDNUM%
    echo BuildLocation=%BUILDLOCATION%
)

"E:\Programs\Unity\%UNITYVERSION%\Editor\Unity.exe" -quit -batchMode -executeMethod BuildHelper.%TARGET%

del /f buildManifest.txt