@ECHO OFF
ECHO Cleaning Unneeded / temp directories
RMDIR /S  /Q TestResults\
RMDIR /S /Q SokoSolve.Common\bin
RMDIR /S /Q  SokoSolve.Common\obj
RMDIR /S /Q SokoSolve.Core\bin
RMDIR /S /Q  SokoSolve.Core\obj
RMDIR /S  /Q SokoSolve.UI\bin
RMDIR /S  /Q SokoSolve.UI\obj
RMDIR /S  /Q SokoSolve.Test\bin
RMDIR /S  /Q SokoSolve.Test\obj
RMDIR /S  /Q SokoSolve.Deploy\Debug
RMDIR /S  /Q SokoSolve.Deploy\Release
RMDIR /S  /Q SokoSolve.Console\bin
RMDIR /S  /Q SokoSolve.Console\obj
DEL /S *.resharper
DEL /S *.resharper.user
DEL /S *.user
DEL *.suo
PAUSE