# ⚡ Quick Start - GitHub Auto-Build

**Get your game built and deployed in 15 minutes without installing Unity!**

---

## 🚀 Steps (Copy & Paste Ready)

### 1. Get Unity License (5 minutes)

```
1. Go to: https://github.com/game-ci/unity-request-activation-file
2. Click "Use this template"
3. Create repository
4. Actions → Run "Acquire activation file"
5. Download .alf file
6. Go to: https://license.unity3d.com/manual
7. Upload .alf → Download .ulf
8. Open .ulf in Notepad → Copy ALL contents
```

### 2. Create GitHub Repo (2 minutes)

```
1. Go to: https://github.com/new
2. Name: csm-escape-room
3. Public repo
4. Create
```

### 3. Add License Secret (1 minute)

```
1. Your repo → Settings → Secrets → Actions
2. New secret
3. Name: UNITY_LICENSE
4. Value: Paste .ulf contents
5. Add secret
```

### 4. Push Code (3 minutes)

Open PowerShell in `C:\Users\Goose\CSM_EscapeRoom_Unity\`:

```powershell
# Replace YOUR_USERNAME with your GitHub username!

git init
git add .
git commit -m "Initial commit"
git remote add origin https://github.com/YOUR_USERNAME/csm-escape-room.git
git branch -M main
git push -u origin main
```

If it asks for login:
- Username: your GitHub username
- Password: create a Personal Access Token at https://github.com/settings/tokens

### 5. Enable GitHub Pages (1 minute)

```
1. Repo → Settings → Pages
2. Branch: gh-pages
3. Save
```

### 6. Wait for Build (10-15 minutes)

```
1. Repo → Actions tab
2. Watch build progress
3. When green checkmark appears → Done!
```

---

## 🎮 Access Your Game

### Play in Browser

```
https://YOUR_USERNAME.github.io/csm-escape-room/
```

### Download Builds

```
1. Actions tab
2. Click latest workflow
3. Scroll to Artifacts
4. Download:
   - Windows build
   - Linux build
```

---

## 🔄 Make Changes

```powershell
# Edit any .cs file
# Then:

git add .
git commit -m "Your changes"
git push

# GitHub rebuilds automatically!
```

---

## 📦 Create Release

```powershell
git tag v1.0.0
git push origin v1.0.0

# GitHub creates release with downloadable .zip files!
```

---

## ✅ Success Checklist

- [ ] Got .ulf license file
- [ ] Created GitHub repo
- [ ] Added UNITY_LICENSE secret
- [ ] Pushed code
- [ ] Enabled Pages
- [ ] Build completed (green checkmark)
- [ ] Tested game at your GitHub Pages URL

---

## 🆘 Quick Troubleshooting

**Build failed?**
→ Check UNITY_LICENSE secret has full .ulf contents

**Can't push?**
→ Create Personal Access Token: https://github.com/settings/tokens

**Pages not working?**
→ Wait 5 minutes after build, check Settings → Pages

**License issues?**
→ Redo Step 1, make sure you copy ENTIRE .ulf file

---

## 🎯 That's It!

You now have:
✅ Game building automatically
✅ WebGL version live online
✅ Windows/Linux downloads
✅ No Unity install needed!

**Share your game:** `https://YOUR_USERNAME.github.io/csm-escape-room/`

---

For detailed setup: See [GITHUB_SETUP.md](GITHUB_SETUP.md)
