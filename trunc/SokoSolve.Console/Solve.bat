
REM .\bin\debug\SokoSolve.Console.exe SOLVE -lib:C:\Projects\Mature\SokoSolve\SokoSolve.UI\Content\Libraries\Sasquatch.ssx -puz:*
.\bin\debug\SokoSolve.Console.exe SOLVE -slib:Sasquatch.progx -maxtime:300
TYPE results.log | more
PAUSE