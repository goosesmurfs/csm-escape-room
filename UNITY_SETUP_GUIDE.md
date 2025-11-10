# Unity WebGL Setup Guide - AWS CCP Exam Prep

This guide will help you set up the Unity scenes to get your AWS Cloud Practitioner exam prep game running online.

## Prerequisites

- Unity Hub installed
- Unity 2021.3 LTS or higher
- Project already open in Unity Editor

---

## Quick Setup (15 minutes)

### Step 1: Open Unity Project

1. Open Unity Hub
2. Click "Open" → Navigate to `C:\Users\Goose\CSM_EscapeRoom_Unity`
3. Wait for Unity to load the project (~2-3 minutes first time)

---

### Step 2: Create Main Menu Scene

1. **File → New Scene** (or Ctrl+N)
2. Right-click in Hierarchy → **UI → Canvas**
3. Right-click Canvas → **UI → Panel** (name it "MenuPanel")
4. Right-click MenuPanel → **UI → Text** (name it "TitleText")
   - Set text to: "AWS Cloud Practitioner Exam Prep"
   - Font size: 48
   - Center alignment
5. Right-click MenuPanel → **UI → Button** (repeat 6 times for each domain)

   **Button Names and Text:**
   - CloudConceptsButton → "Cloud Concepts (26%)"
   - SecurityButton → "Security & Compliance (25%)"
   - TechnologyButton → "Technology (33%)"
   - BillingButton → "Billing & Pricing (16%)"
   - MixedButton → "Mixed Challenge"
   - FullExamButton → "Full Practice Exam"

6. Create **Empty GameObject** in Hierarchy (name it "GameManager")
7. Select GameManager → **Add Component → LevelSelectUI** (our script)
8. Drag each button to corresponding field in Inspector

9. **File → Save As** → Save scene as `Assets/Scenes/LevelSelect.unity`

---

### Step 3: Create Question Scene

1. **File → New Scene**
2. Right-click Hierarchy → **UI → Canvas**
3. Right-click Canvas → **UI → Panel** (name it "QuestionPanel")
4. Add to QuestionPanel:
   - **UI → Text** (name "QuestionText") - for the question
   - **UI → Button** x4 (name "OptionButton1" through "OptionButton4") - for answers
   - **UI → Text** (name "FeedbackText") - for explanation
   - **UI → Button** (name "ContinueButton") - to proceed
   - **UI → Text** (name "DomainText") - shows current domain
   - **UI → Text** (name "ScoreText") - shows score
   - **UI → Text** (name "TimerText") - shows time
   - **UI → Text** (name "StreakText") - shows streak

5. Create **Empty GameObject** (name "GameManager")
6. Select GameManager → **Add Component → GameManager** (our script)
7. Select GameManager → **Add Component → QuestionPanel** (our script)
8. Drag UI elements to corresponding fields in Inspector

9. **File → Save As** → Save scene as `Assets/Scenes/QuestionScene.unity`

---

### Step 4: Configure Build Settings

1. **File → Build Settings**
2. Click "Add Open Scenes" to add LevelSelect
3. **File → Open Scene** → Open QuestionScene.unity
4. Back to Build Settings → Click "Add Open Scenes"
5. Make sure **LevelSelect is at index 0** (drag to reorder if needed)
6. Platform: Select **WebGL** → Click "Switch Platform"
7. Click "Player Settings":
   - **Company Name**: Your name
   - **Product Name**: AWS CCP Exam Prep
   - **WebGL → Resolution**:
     - Default Canvas Width: 1280
     - Default Canvas Height: 720
   - **WebGL → Publishing Settings**:
     - Compression Format: Gzip (smaller file size)
     - Enable Exceptions: None (smaller build)

---

### Step 5: Build for WebGL

1. **File → Build Settings**
2. Platform: **WebGL**
3. Click **Build** (NOT "Build and Run")
4. Choose folder: `C:\Users\Goose\CSM_EscapeRoom_Unity\WebGLBuild`
5. Wait 10-20 minutes for build to complete
6. Build will create:
   ```
   WebGLBuild/
   ├── Build/
   ├── TemplateData/
   └── index.html
   ```

---

### Step 6: Test Locally

1. After build completes, click "Build and Run" in Build Settings
2. Unity will start local web server and open browser
3. Test the game:
   - Click through domains
   - Answer some questions
   - Check if scoring works
   - Verify progress tracking

---

### Step 7: Deploy to GitHub Pages

1. **Copy WebGL build to repo root:**
   ```bash
   cd C:\Users\Goose\CSM_EscapeRoom_Unity
   xcopy WebGLBuild docs\ /E /I /Y
   ```

2. **Create `.nojekyll` file** (tells GitHub Pages not to process files):
   ```bash
   cd docs
   echo. > .nojekyll
   ```

3. **Commit and push:**
   ```bash
   cd C:\Users\Goose\CSM_EscapeRoom_Unity
   git add docs/
   git commit -m "Add WebGL build for GitHub Pages"
   git push origin main
   ```

4. **Enable GitHub Pages:**
   - Go to: https://github.com/goosesmurfs/csm-escape-room/settings/pages
   - Source: Deploy from a branch
   - Branch: `main` → Folder: `/docs` → Save
   - Wait 2-3 minutes for deployment

5. **Your game will be live at:**
   https://goosesmurfs.github.io/csm-escape-room/

---

## Troubleshooting

### Build Errors

**"No scenes in build"**
- Go to File → Build Settings
- Click "Add Open Scenes" for each scene

**"Missing script references"**
- Select GameManager object
- Re-add the scripts from Assets/Scripts/

**Build takes forever**
- First build is slow (10-20 min)
- Subsequent builds are faster (~5 min)

### Game Not Working

**Nothing loads**
- Check browser console (F12) for errors
- Make sure .nojekyll file exists in docs/

**Questions don't show**
- Verify AWSQuestion.cs script exists
- Check GameManager has QuestionPanel reference

**UI elements overlap**
- Go back to Unity
- Select Canvas → Canvas Scaler:
  - UI Scale Mode: Scale With Screen Size
  - Reference Resolution: 1280x720

---

## Alternative: Use Existing CSM Scene

If the above seems too complex, you can adapt the existing working CSM scene:

1. Open `Assets/Scenes/SampleScene.unity`
2. Find "GameManager" object
3. Replace old scripts with new AWS scripts
4. Update question database reference
5. Build and deploy

This will give you a working 3D escape room but with AWS questions instead of CSM ones.

---

## Next Steps After Deployment

Once deployed online:

1. **Test thoroughly:**
   - Try all 6 domains
   - Complete a full quiz
   - Check leaderboard
   - Verify stats dashboard

2. **Add more questions:**
   - Edit `Assets/Scripts/AWSQuestion.cs`
   - Add questions to database
   - Rebuild and redeploy

3. **Customize appearance:**
   - Change colors in Unity UI
   - Add your company logo
   - Adjust fonts and styling

4. **Add backend (optional):**
   - Set up Firebase/Supabase
   - Enable real leaderboard sync
   - Track analytics

---

## Need Help?

**Common Issues:**
- Unity crashes: Close and reopen
- Build fails: Clear Library folder and rebuild
- WebGL won't load: Check browser supports WebGL 2.0

**Still stuck?**
The automated build in GitHub Actions is configured but needs the Unity scenes to exist first. Complete steps 1-4 above, then just push to GitHub and Actions will handle the build.

Good luck! Your AWS CCP exam prep game will be live soon! 🚀
