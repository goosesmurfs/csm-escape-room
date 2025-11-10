# 🚀 GitHub Automated Build Setup

Complete guide to set up automated Unity builds on GitHub - **NO Unity installation required!**

---

## 🎯 What This Does

When you push code to GitHub:
1. ✅ **Automatically builds** Windows, Linux, and WebGL versions
2. ✅ **Deploys WebGL** to GitHub Pages (playable in browser!)
3. ✅ **Creates releases** with downloadable builds
4. ✅ **All free** - no cost for open source projects

You never need to install Unity locally! GitHub does all the building.

---

## 📋 Prerequisites

1. **GitHub Account** (free): https://github.com/signup
2. **Unity License** (free Personal license)

---

## 🔧 Step-by-Step Setup

### Step 1: Get Unity License File

Even though you won't install Unity, you need a license for GitHub Actions.

**Option A: Use Activation File (Recommended)**

1. Go to: https://github.com/game-ci/unity-request-activation-file
2. Click "Use this template" → "Create a new repository"
3. Go to your new repo → Actions tab
4. Run the "Acquire activation file" workflow
5. Download the `.alf` file from artifacts
6. Go to: https://license.unity3d.com/manual
7. Upload the `.alf` file
8. Download the `.ulf` license file
9. Open the `.ulf` file in a text editor and copy ALL contents

**Option B: If you have Unity installed locally**

1. Open Unity Hub
2. Help → Manage License → Manual Activation
3. Download `.alf` file
4. Activate at: https://license.unity3d.com/manual
5. Copy the `.ulf` file contents

### Step 2: Create GitHub Repository

1. Go to: https://github.com/new
2. Repository name: `csm-escape-room`
3. Description: "CSM Escape Room - Unity 3D Game"
4. Public (required for free GitHub Actions)
5. Click "Create repository"

### Step 3: Add Unity License Secret

1. Go to your repository on GitHub
2. Settings → Secrets and variables → Actions
3. Click "New repository secret"
4. Name: `UNITY_LICENSE`
5. Value: Paste the ENTIRE `.ulf` file contents
6. Click "Add secret"

### Step 4: Initialize Git Locally

Open PowerShell in `C:\Users\Goose\CSM_EscapeRoom_Unity\`:

```powershell
# Initialize git
git init

# Add all files
git add .

# First commit
git commit -m "Initial commit: Complete CSM Escape Room Unity project"

# Add remote (replace YOUR_USERNAME)
git remote add origin https://github.com/YOUR_USERNAME/csm-escape-room.git

# Push to GitHub
git branch -M main
git push -u origin main
```

### Step 5: Enable GitHub Pages

1. Go to your repository on GitHub
2. Settings → Pages
3. Source: Deploy from a branch
4. Branch: `gh-pages` / `root`
5. Save

### Step 6: Watch the Magic! ✨

1. Go to your repo → **Actions** tab
2. You'll see the build running automatically!
3. Wait 10-15 minutes for first build
4. When complete:
   - **Windows build**: Available in Artifacts
   - **Linux build**: Available in Artifacts
   - **WebGL build**: Live at `https://YOUR_USERNAME.github.io/csm-escape-room/`

---

## 🎮 Accessing Your Builds

### Play WebGL Version (Browser)

Visit: `https://YOUR_USERNAME.github.io/csm-escape-room/`

**Example:** `https://john-doe.github.io/csm-escape-room/`

Share this link with anyone - they can play instantly!

### Download Desktop Builds

1. Go to your repo → **Actions**
2. Click latest successful workflow run
3. Scroll to **Artifacts** section
4. Download:
   - `Build-StandaloneWindows64` (Windows .exe)
   - `Build-StandaloneLinux64` (Linux)

---

## 🔄 Making Changes

### Update Code

```powershell
# Make changes to scripts
# Edit files in Assets/Scripts/

# Commit changes
git add .
git commit -m "Updated player movement"
git push

# GitHub automatically rebuilds everything!
```

### Add More Questions

```powershell
# Edit: Assets/Scripts/RoomBuilder.cs
# Add questions in GetQuestionsForRoom()

git add Assets/Scripts/RoomBuilder.cs
git commit -m "Added more CSM questions"
git push

# New build with questions ready in 10 minutes!
```

---

## 📦 Creating Releases

### Automatic Releases

1. Create a tag:
```powershell
git tag v1.0.0
git push origin v1.0.0
```

2. GitHub automatically:
   - Builds all platforms
   - Creates a Release
   - Attaches .zip files for download

### Manual Release

1. GitHub → Releases → "Create a new release"
2. Tag: `v1.0.0`
3. Title: "CSM Escape Room v1.0.0"
4. Description: Changelog
5. Attach build artifacts
6. Publish!

---

## 🛠️ Advanced Configuration

### Build Only on Main Branch

Edit `.github/workflows/build.yml`:
```yaml
on:
  push:
    branches: [ main ]  # Remove 'develop'
```

### Add More Platforms

Add to `matrix.targetPlatform`:
```yaml
- StandaloneOSX     # Mac
- Android           # Android
- iOS               # iOS (requires paid Apple account)
```

### Faster Builds

Enable caching (already in workflow):
```yaml
- uses: actions/cache@v3
  with:
    path: Library
    key: Library-${{ hashFiles('Assets/**') }}
```

---

## 💰 Cost Breakdown

| Service | Cost |
|---------|------|
| GitHub Repository (Public) | **FREE** |
| GitHub Actions (2000 min/month) | **FREE** |
| GitHub Pages Hosting | **FREE** |
| Unity Personal License | **FREE** |
| Total Monthly Cost | **$0.00** |

For private repos:
- GitHub Pro: $4/month (3000 Actions minutes)
- Or use GitLab CI (also free)

---

## 🐛 Troubleshooting

### Build Fails: "Invalid License"

**Solution:**
1. Verify UNITY_LICENSE secret is set correctly
2. Ensure you copied ENTIRE .ulf file contents
3. Check license hasn't expired

### Build Fails: "Can't find scene"

**Solution:**
1. Create a scene: `Assets/Scenes/MainGame.unity`
2. Add to Build Settings
3. Commit and push

### WebGL Not Deploying

**Solution:**
1. Check Actions log for errors
2. Verify gh-pages branch exists
3. Settings → Pages → Source: gh-pages

### Build Takes Too Long

**Solution:**
1. Enable caching (already added)
2. First build: 15-20 min (normal)
3. Subsequent builds: 5-10 min

---

## 📊 Build Status Badge

Add to your README.md:

```markdown
![Build Status](https://github.com/YOUR_USERNAME/csm-escape-room/workflows/Build%20Unity%20Project/badge.svg)
```

Shows if builds are passing!

---

## 🌐 Sharing Your Game

### Share WebGL Link

Send to anyone:
```
https://YOUR_USERNAME.github.io/csm-escape-room/
```

They can play instantly in browser!

### Share Downloads

1. Create a Release
2. Share release URL:
```
https://github.com/YOUR_USERNAME/csm-escape-room/releases
```

### Embed in Website

```html
<iframe src="https://YOUR_USERNAME.github.io/csm-escape-room/"
        width="960" height="600" frameborder="0">
</iframe>
```

---

## 🚀 Deploy to Other Platforms

### itch.io (Recommended)

1. Create account: https://itch.io
2. Create new project
3. Upload WebGL build from GitHub artifacts
4. Set "This file will be played in the browser"
5. Publish!

**Advantages:**
- Better player than GitHub Pages
- Analytics
- Comments
- Can set price/"pay what you want"

### Netlify

1. Create account: https://netlify.com
2. New site from Git
3. Connect GitHub repo
4. Build command: (none needed)
5. Publish directory: `builds/WebGL`
6. Deploy!

### Your Own Domain

1. Buy domain (e.g., Namecheap)
2. Point to GitHub Pages
3. Settings → Pages → Custom domain
4. Game at: `csm-escape-room.com`

---

## 📈 Analytics

### Add Google Analytics

Edit WebGL template:
```html
<!-- Add to index.html template -->
<script async src="https://www.googletagmanager.com/gtag/js?id=YOUR-GA-ID"></script>
```

Track:
- Player count
- Session duration
- Geographic location
- Device types

---

## 🎯 Complete Workflow Example

```powershell
# Day 1: Setup
git init
git add .
git commit -m "Initial commit"
git remote add origin https://github.com/YOU/csm-escape-room.git
git push -u origin main

# Wait 15 minutes → builds complete
# Game live at: https://YOU.github.io/csm-escape-room/

# Day 2: Add more questions
# Edit RoomBuilder.cs
git add .
git commit -m "Added Room 6"
git push

# Wait 10 minutes → new build deployed automatically!

# Day 3: Create release
git tag v1.0.0
git push origin v1.0.0

# GitHub creates release with downloadable .zip files!
```

---

## ✅ Checklist

- [ ] Created GitHub account
- [ ] Got Unity license (.ulf file)
- [ ] Created GitHub repository
- [ ] Added UNITY_LICENSE secret
- [ ] Pushed code to GitHub
- [ ] Enabled GitHub Pages
- [ ] Watched first build complete
- [ ] Tested WebGL build
- [ ] Downloaded desktop builds
- [ ] Shared game link with team!

---

## 🎉 You're Done!

**You now have:**
✅ Automated Unity builds (no local install needed!)
✅ WebGL game playable in browser
✅ Windows/Linux downloads
✅ Automatic deployments
✅ Free hosting
✅ Professional CI/CD pipeline

**Every time you push code:**
→ GitHub rebuilds everything
→ WebGL auto-deploys
→ New builds available

**No Unity installation required!** 🚀

---

## 📚 Resources

- [Game CI Documentation](https://game.ci/)
- [Unity Cloud Build](https://unity.com/products/cloud-build) (alternative)
- [GitHub Actions Docs](https://docs.github.com/en/actions)
- [Unity Manual](https://docs.unity3d.com/)

---

**Questions?** Check the [Game CI Discord](https://game.ci/discord)

**Ready to go!** Push your code and watch the magic happen! ✨
