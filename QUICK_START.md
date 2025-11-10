# Quick Start Guide - One-Click Scene Generation

This guide will get your AWS CCP exam prep game running in **5 minutes** using automated scene generation!

---

## Step 1: Open Unity Project (2 minutes)

1. Open **Unity Hub**
2. Click **"Open"** → Navigate to `C:\Users\Goose\CSM_EscapeRoom_Unity`
3. Wait for Unity to load the project

---

## Step 2: Generate Scenes (30 seconds)

Once Unity Editor is open:

1. In the top menu, click **Tools → Generate AWS CCP Scenes**
2. Click **"Yes"** in the confirmation dialog
3. Wait 5-10 seconds
4. You'll see a success message!

**That's it!** The script automatically creates:
- ✅ LevelSelect.unity scene with all 6 domain buttons
- ✅ QuestionScene.unity scene with all UI elements
- ✅ Configured build settings
- ✅ Wired up all script references

---

## Step 3: Test in Unity (1 minute)

1. In **Project** window → **Assets/Scenes/** → Double-click **LevelSelect.unity**
2. Click the **Play** button (▶️) at the top
3. Test the menu:
   - Enter your name
   - Click a domain button (e.g., "Cloud Concepts")
   - Answer some questions
   - Check if scoring works

---

## Step 4: Build for WebGL (10-20 minutes)

1. **File → Build Settings**
2. Select **WebGL** platform
3. Click **"Switch Platform"** (if not already selected)
4. Click **"Build"**
5. Choose folder: `C:\Users\Goose\CSM_EscapeRoom_Unity\WebGLBuild`
6. Wait for build to complete (~10-20 minutes first time)

---

## Step 5: Deploy to GitHub Pages (2 minutes)

Open PowerShell or Command Prompt:

```bash
cd C:\Users\Goose\CSM_EscapeRoom_Unity

# Copy WebGL build to docs folder
xcopy WebGLBuild docs\ /E /I /Y

# Create .nojekyll file
cd docs
echo. > .nojekyll
cd ..

# Commit and push
git add .
git commit -m "Add automated scene generation and WebGL build"
git push origin main
```

---

## Step 6: Enable GitHub Pages (1 minute)

1. Go to: https://github.com/goosesmurfs/csm-escape-room/settings/pages
2. **Source**: Deploy from a branch
3. **Branch**: `main` → **Folder**: `/docs` → Click **Save**
4. Wait 2-3 minutes for deployment

---

## Your Game is Live! 🎉

**URL**: https://goosesmurfs.github.io/csm-escape-room/

---

## Troubleshooting

### "Generate AWS CCP Scenes" menu item not showing

**Solution**:
1. Close Unity completely
2. Reopen the project
3. Wait for scripts to recompile (watch bottom-right corner)
4. Try again

### Build errors about missing scenes

**Solution**:
1. Run the scene generator again (Tools → Generate AWS CCP Scenes)
2. Check that `Assets/Scenes/` folder contains both .unity files

### WebGL build takes forever

**Normal!** First WebGL build can take 15-20 minutes. Subsequent builds are faster (~5 minutes).

### Game loads but shows errors in browser console

**Solution**:
1. Make sure `.nojekyll` file exists in `docs/` folder
2. Clear browser cache (Ctrl+Shift+Delete)
3. Hard refresh the page (Ctrl+F5)

---

## What the Scene Generator Does

The automated script creates two complete Unity scenes:

### LevelSelect Scene
- Main menu with 6 domain buttons
- Player name input field
- Progress display for each domain
- Stats panel showing total score and weak areas
- Leaderboard and Statistics buttons
- Fully wired GameManager with LevelSelectUI script

### QuestionScene
- Question display area
- 4 answer option buttons
- Feedback panel with explanations
- Score, streak, and timer displays
- Domain and difficulty indicators
- Continue button
- Fully wired GameManager with QuestionPanel script

All UI elements are positioned, styled, and connected to scripts automatically!

---

## Next Steps

Once deployed online:

1. **Test all features**:
   - Try all 6 domain modes
   - Complete a full quiz session
   - Check progress tracking
   - Verify leaderboard works

2. **Customize** (optional):
   - Open scenes in Unity Editor
   - Adjust colors, fonts, sizes
   - Add your company logo
   - Modify question database in `AWSQuestion.cs`

3. **Share with your team**:
   - Send them the GitHub Pages URL
   - Watch leaderboard competition heat up!
   - Track who's ready for the real AWS CCP exam

---

## Files Created by Generator

```
Assets/
├── Scenes/
│   ├── LevelSelect.unity      ← Main menu scene
│   └── QuestionScene.unity    ← Quiz scene
└── Editor/
    └── SceneGenerator.cs      ← The automation script
```

---

## Need Help?

**Common Issues**:
- Unity crashes: Close and reopen
- Build fails: Clear `Library/` folder and rebuild
- WebGL won't load: Check browser supports WebGL 2.0

**Still stuck?**
Check `UNITY_SETUP_GUIDE.md` for manual scene creation steps.

---

**Good luck! Your AWS CCP exam prep game will be live soon!** 🚀
