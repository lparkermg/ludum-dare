@echo off

SET GAME=a-game
IF NOT [%1]==[] (set GAME=%1)

SET TARGET=Windows
IF NOT [%2]==[] (set TARGET=%2)

SET VERSION=0.0.0
IF NOT [%3]==[] (set VERSION=%3)

SET CHANNEL=windows
IF NOT [%4]==[] (set CHANNEL=%4)

butler push ./build/%TARGET% lparkermg/%GAME%:%CHANNEL% --userversion %VERSION%