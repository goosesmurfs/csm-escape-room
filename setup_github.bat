@echo off
echo ================================================================
echo   GitHub Repository Setup - CSM Escape Room Unity
echo ================================================================
echo.

REM Check if git is installed
git --version >nul 2>&1
if errorlevel 1 (
    echo ERROR: Git is not installed!
    echo.
    echo Please install Git from: https://git-scm.com/download/win
    echo.
    pause
    exit /b 1
)

echo Git is installed!
echo.

REM Get GitHub username
set /p GITHUB_USER="Enter your GitHub username: "

if "%GITHUB_USER%"=="" (
    echo ERROR: GitHub username is required!
    pause
    exit /b 1
)

echo.
echo Using GitHub username: %GITHUB_USER%
echo.

REM Set repository name
set REPO_NAME=csm-escape-room

echo Repository will be created at:
echo https://github.com/%GITHUB_USER%/%REPO_NAME%
echo.

echo ================================================================
echo   Step 1: Initializing Git Repository
echo ================================================================
git init

echo.
echo ================================================================
echo   Step 2: Adding All Files
echo ================================================================
git add .

echo.
echo ================================================================
echo   Step 3: Creating Initial Commit
echo ================================================================
git commit -m "Initial commit: Complete CSM Escape Room Unity project with automated CI/CD"

echo.
echo ================================================================
echo   Step 4: Setting Main Branch
echo ================================================================
git branch -M main

echo.
echo ================================================================
echo   Step 5: Adding Remote Repository
echo ================================================================
git remote add origin https://github.com/%GITHUB_USER%/%REPO_NAME%.git

echo.
echo ================================================================
echo   IMPORTANT: Create GitHub Repository
echo ================================================================
echo.
echo Before pushing, you need to create the repository on GitHub:
echo.
echo 1. Go to: https://github.com/new
echo 2. Repository name: %REPO_NAME%
echo 3. Description: CSM Escape Room - Unity 3D Game with Auto-Build
echo 4. Choose: Public (required for free GitHub Actions)
echo 5. DO NOT initialize with README, .gitignore, or license
echo 6. Click "Create repository"
echo.
echo Have you created the repository? (Press any key when ready)
pause >nul

echo.
echo ================================================================
echo   Step 6: Pushing to GitHub
echo ================================================================
echo.
echo If this is your first time, Git will ask for credentials:
echo   Username: Your GitHub username
echo   Password: Use a Personal Access Token (NOT your password)
echo.
echo To create a token:
echo   1. Go to: https://github.com/settings/tokens
echo   2. Generate new token (classic)
echo   3. Select scopes: repo, workflow
echo   4. Copy the token and paste it as password
echo.
echo Pushing to GitHub...
echo.

git push -u origin main

if errorlevel 1 (
    echo.
    echo ================================================================
    echo   PUSH FAILED - Authentication Needed
    echo ================================================================
    echo.
    echo You need to authenticate. Here's how:
    echo.
    echo OPTION 1: Using Personal Access Token (Recommended)
    echo   1. Go to: https://github.com/settings/tokens
    echo   2. Click "Generate new token (classic)"
    echo   3. Note: "CSM Escape Room Access"
    echo   4. Select scopes: repo, workflow
    echo   5. Click "Generate token"
    echo   6. Copy the token
    echo   7. Run this command:
    echo      git push -u origin main
    echo   8. Username: %GITHUB_USER%
    echo   9. Password: Paste the token
    echo.
    echo OPTION 2: Using GitHub CLI (Easier)
    echo   1. Install: https://cli.github.com
    echo   2. Run: gh auth login
    echo   3. Run: git push -u origin main
    echo.
    pause
    exit /b 1
)

echo.
echo ================================================================
echo   SUCCESS! Repository Created and Pushed
echo ================================================================
echo.
echo Your repository: https://github.com/%GITHUB_USER%/%REPO_NAME%
echo.
echo ================================================================
echo   NEXT STEPS:
echo ================================================================
echo.
echo 1. Add Unity License Secret
echo    - Go to: https://github.com/%GITHUB_USER%/%REPO_NAME%/settings/secrets/actions
echo    - Click "New repository secret"
echo    - Name: UNITY_LICENSE
echo    - Value: Contents of your .ulf license file
echo    - See GITHUB_SETUP.md for how to get license
echo.
echo 2. Enable GitHub Pages
echo    - Go to: https://github.com/%GITHUB_USER%/%REPO_NAME%/settings/pages
echo    - Source: Deploy from branch
echo    - Branch: gh-pages (will appear after first build)
echo    - Save
echo.
echo 3. Watch Build Progress
echo    - Go to: https://github.com/%GITHUB_USER%/%REPO_NAME%/actions
echo    - First build takes 15-20 minutes
echo.
echo 4. Your Game Will Be Live At:
echo    https://%GITHUB_USER%.github.io/%REPO_NAME%/
echo.
echo ================================================================
echo.
echo Opening repository in browser...
start https://github.com/%GITHUB_USER%/%REPO_NAME%
echo.
pause
