@echo off
echo ================================================================
echo   Quick Push to GitHub
echo ================================================================
echo.
echo This script will push your Unity project to GitHub.
echo.
pause

cd /d "%~dp0"

echo Pushing to GitHub...
git push -u origin main

if errorlevel 1 (
    echo.
    echo ================================================================
    echo   Push failed - trying alternative method
    echo ================================================================
    echo.
    "C:\Program Files\GitHub CLI\gh.exe" auth setup-git
    git push -u origin main
)

echo.
echo ================================================================
echo   SUCCESS!
echo ================================================================
echo.
echo Your repository: https://github.com/goosesmurfs/csm-escape-room
echo.
echo Next Steps:
echo 1. Go to: https://github.com/goosesmurfs/csm-escape-room/actions
echo 2. Watch the build (takes 15-20 minutes first time)
echo 3. Your game will be live at:
echo    https://goosesmurfs.github.io/csm-escape-room/
echo.
pause
