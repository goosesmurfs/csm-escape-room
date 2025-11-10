@echo off
echo ================================================================
echo   Unity License Setup - Quick Guide
echo ================================================================
echo.
echo This will open the necessary pages in your browser.
echo Follow along with the steps shown in the console.
echo.
pause

echo.
echo ================================================================
echo   STEP 1: Get Activation File Template
echo ================================================================
echo.
echo Opening: https://github.com/game-ci/unity-request-activation-file
echo.
echo What to do:
echo   1. Click "Use this template"
echo   2. Create a repository (any name is fine)
echo   3. Go to the Actions tab
echo   4. Click "Acquire activation file" workflow
echo   5. Click "Run workflow" button
echo   6. Wait for it to complete (takes about 1 minute)
echo   7. Download the .alf file from Artifacts
echo.
start https://github.com/game-ci/unity-request-activation-file
echo.
echo Press any key when you have the .alf file downloaded...
pause >nul

echo.
echo ================================================================
echo   STEP 2: Convert .alf to .ulf License
echo ================================================================
echo.
echo Opening: https://license.unity3d.com/manual
echo.
echo What to do:
echo   1. Upload your .alf file
echo   2. Choose "Unity Personal Edition"
echo   3. Click "Next"
echo   4. Download the .ulf file
echo   5. Open the .ulf file in Notepad
echo   6. Press Ctrl+A to select all
echo   7. Press Ctrl+C to copy
echo.
start https://license.unity3d.com/manual
echo.
echo Press any key when you have copied the .ulf file contents...
pause >nul

echo.
echo ================================================================
echo   STEP 3: Add License to GitHub
echo ================================================================
echo.
echo Opening: https://github.com/goosesmurfs/csm-escape-room/settings/secrets/actions
echo.
echo What to do:
echo   1. Click "New repository secret"
echo   2. Name: UNITY_LICENSE
echo   3. Value: Press Ctrl+V to paste the .ulf contents
echo   4. Click "Add secret"
echo.
start https://github.com/goosesmurfs/csm-escape-room/settings/secrets/actions
echo.
echo Press any key when you have added the secret...
pause >nul

echo.
echo ================================================================
echo   STEP 4: Re-run Build
echo ================================================================
echo.
echo Opening: https://github.com/goosesmurfs/csm-escape-room/actions
echo.
echo What to do:
echo   1. Click the latest workflow run
echo   2. Click "Re-run all jobs" (top right)
echo   3. Wait 15-20 minutes for build to complete
echo.
start https://github.com/goosesmurfs/csm-escape-room/actions
echo.
echo ================================================================
echo   ALL DONE!
echo ================================================================
echo.
echo Your game will be live at:
echo https://goosesmurfs.github.io/csm-escape-room/
echo.
echo The build takes 15-20 minutes on first run.
echo Check the Actions page to see progress.
echo.
pause
