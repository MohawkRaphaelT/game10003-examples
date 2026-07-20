:: Run this script to update .net version and MohawkGame2D code files

:: Update runtime
call update-dotnet-runtime.bat

:: Update MohawkGame2D code
python update-mohawkgame2d.py

:: Read output
pause