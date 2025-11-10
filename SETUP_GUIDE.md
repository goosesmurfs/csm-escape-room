## 🎮 CSM Escape Room - Unity Project Setup Guide

Complete setup instructions to get the game running in Unity!

---

## 📋 Prerequisites

### 1. Install Unity Hub
Download from: https://unity.com/download

### 2. Install Unity Editor
**Recommended Version: Unity 2021.3 LTS or newer**

1. Open Unity Hub
2. Click "Installs" → "Install Editor"
3. Select "2021.3 LTS" (Long Term Support)
4. Add modules:
   - ✅ Windows Build Support (IL2CPP)
   - ✅ WebGL Build Support (for web deployment)
   - ✅ Documentation

---

## 🚀 Step-by-Step Setup

### Step 1: Create New Unity Project

1. Open **Unity Hub**
2. Click **"New Project"**
3. Select **"3D (URP)"** or **"3D"** template
4. Project Name: `CSM_EscapeRoom`
5. Location: Choose where to save
6. Click **"Create Project"**

### Step 2: Copy Scripts

Copy all files from `CSM_EscapeRoom_Unity\Assets\` to your Unity project's `Assets\` folder:

```
YourProject/
  Assets/
    Scripts/
      ├── GameManager.cs
      ├── PlayerController.cs
      ├── Collectible.cs
      ├── Door.cs
      ├── QuestionPanel.cs
      └── RoomBuilder.cs
```

### Step 3: Create the Scene

1. In Unity, go to **File → New Scene**
2. Save as `MainGame.scene` in `Assets/Scenes/`

### Step 4: Setup Game Manager

1. In Hierarchy, right-click → **Create Empty**
2. Rename to `GameManager`
3. Add the `GameManager` script component
4. The script will appear in Inspector

### Step 5: Create the Player

1. **Hierarchy → 3D Object → Capsule**
2. Rename to `Player`
3. Position: (0, 2, -10)
4. Add Component → **Character Controller**
   - Height: 2
   - Radius: 0.5
   - Center: (0, 0, 0)
5. Add Component → **PlayerController** script
6. Create **Camera**:
   - Right-click Player → **Camera**
   - Position: (0, 0.6, 0)
   - Tag as "MainCamera"
7. Assign camera to PlayerController:
   - Drag Camera to "Camera Transform" field

### Step 6: Create Ground Check

1. Right-click Player → **Create Empty**
2. Rename to `GroundCheck`
3. Position: (0, -1, 0)
4. In PlayerController, assign this to "Ground Check" field
5. Create Layer Mask:
   - Top right → Layers → Add Layer
   - Add "Ground" layer
   - Select floor objects → set Layer to "Ground"
   - In PlayerController, set Ground Mask to "Ground"

### Step 7: Build UI System

#### A. Create Canvas

1. **Hierarchy → UI → Canvas**
2. Canvas Scaler:
   - UI Scale Mode: Scale With Screen Size
   - Reference Resolution: 1920 x 1080

#### B. Create HUD Elements

**Room Text:**
1. Right-click Canvas → **UI → Text**
2. Rename: `RoomText`
3. Position: Top-left
4. Text: "Room 1/5"
5. Font Size: 24
6. Color: Yellow

**Score Text:**
1. Right-click Canvas → **UI → Text**
2. Rename: `ScoreText`
3. Anchor: Top-right
4. Text: "Score: 0/0"

**Collectibles Text:**
1. Right-click Canvas → **UI → Text**
2. Rename: `CollectiblesText`
3. Anchor: Top-right (below score)
4. Text: "Artifacts: 0/3"
5. Color: Gold

**Interact Prompt:**
1. Right-click Canvas → **UI → Text**
2. Rename: `InteractPrompt`
3. Anchor: Bottom-center
4. Text: ""
5. Font Size: 20
6. Initially: Disable this object

#### C. Create Question Panel

1. Right-click Canvas → **UI → Panel**
2. Rename: `QuestionPanel`
3. Center screen, size: (800, 600)
4. Background color: Black with 90% alpha

**Add to QuestionPanel:**

**Question Text:**
- UI → Text
- Name: `QuestionText`
- Top of panel
- Font Size: 20

**4 Option Buttons:**
- UI → Button (create 4)
- Names: `Option1`, `Option2`, `Option3`, `Option4`
- Stack vertically
- Each button text: "Option A", "Option B", etc.

**Feedback Text:**
- UI → Text
- Name: `FeedbackText`
- Below buttons
- Initially disabled

**Continue Button:**
- UI → Button
- Name: `ContinueButton`
- Text: "Continue (SPACE)"
- Initially disabled

**Progress Bar:**
- UI → Slider
- Name: `ProgressBar`
- Top of panel
- Remove handle, just background + fill

5. Add `QuestionPanel` script component to QuestionPanel
6. Assign all UI elements in Inspector

7. **Disable QuestionPanel** initially (uncheck at top of Inspector)

### Step 8: Connect GameManager UI

1. Select `GameManager` in Hierarchy
2. In Inspector, assign all UI elements:
   - Room Text
   - Score Text
   - Collectibles Text
   - Interact Prompt Text
   - Question Panel (the panel GameObject)

### Step 9: Create Room Prefab

#### Option A: Use RoomBuilder (Automatic)

1. Create Empty GameObject → `Room1`
2. Add `RoomBuilder` script
3. Configure in Inspector:
   - Room Index: 0
   - Room Size: (25, 20)
   - Wall Color: Brown/Red
   - Room Name: "Foundation"
4. Script will auto-build room on Play!

#### Option B: Manual Room (More Control)

1. Create Floor:
   - 3D Object → Cube
   - Scale: (25, 0.5, 20)
   - Material: Gray

2. Create Walls:
   - 4 cubes for walls
   - Position around floor
   - Material: Colored

3. Create Collectibles:
   - 3D Object → Sphere
   - Scale: (0.8, 0.8, 0.8)
   - Material: Yellow, Emissive
   - Add `Collectible` script
   - Create 3 copies, position around room

4. Create Door:
   - 3D Object → Cube
   - Scale: (4, 5, 0.3)
   - Position: Front of room
   - Add `Door` script
   - In Inspector, add questions

### Step 10: Add Lighting

1. **Directional Light** (Main light):
   - Already in scene
   - Rotation: (50, -30, 0)
   - Intensity: 1

2. **Point Lights** (Ambiance):
   - Add 4 point lights around room
   - Range: 15
   - Intensity: 1
   - Color: Match room theme

### Step 11: Configure Project Settings

1. **Edit → Project Settings**

**Player:**
- Company Name: Your name
- Product Name: CSM Escape Room
- Default Cursor: Lock Cursor

**Quality:**
- Anti Aliasing: 4x Multi Sampling
- Shadows: Soft Shadows
- Shadow Resolution: High

**Physics:**
- Default Contact Offset: 0.01

### Step 12: Final Connections

1. Select **GameManager**
2. Assign **Player** to "Player Transform" field
3. Create **Room Prefabs**:
   - Create 5 room prefabs (duplicate Room1)
   - Drag each to Project window to make prefab
   - Assign to GameManager "Room Prefabs" array

---

## ✅ Testing

1. **Press Play** in Unity
2. You should see:
   - Player spawns in first room
   - UI shows at top
   - Yellow collectibles floating
   - Door at end of room

3. **Test Controls:**
   - WASD to move
   - Mouse to look
   - Walk to collectible, press F
   - Walk to door, press E
   - Answer questions

---

## 🏗️ Building the Game

### Build for Windows

1. **File → Build Settings**
2. Click **"Add Open Scenes"**
3. Platform: **Windows**
4. Architecture: **x86_64**
5. Click **"Build"**
6. Choose output folder
7. Creates `.exe` file

### Build for WebGL (Browser)

1. **File → Build Settings**
2. Platform: **WebGL**
3. Click **"Switch Platform"**
4. Click **"Build"**
5. Upload to:
   - itch.io
   - Simmer.io
   - Your own server

### Build Settings

**Player Settings:**
- Fullscreen Mode: Fullscreen Window
- Default Screen Width: 1920
- Default Screen Height: 1080
- Run In Background: Yes

---

## 🎨 Customization

### Add More Questions

1. Select Door in scene
2. Inspector → Door component
3. Expand "Questions" list
4. Click "+" to add
5. Fill in:
   - Question text
   - 4 options
   - Correct index (0-3)
   - Explanation

### Change Colors

**Walls:**
- Select wall object
- Inspector → Renderer → Material
- Change color

**Collectibles:**
- Select collectible
- Material → Emission → Enable
- Emission Color: Bright yellow

### Add More Rooms

1. Duplicate existing room
2. Change position (Z + 50 for each room)
3. Change wall colors
4. Add to GameManager Room Prefabs array

---

## 🐛 Troubleshooting

**Player falls through floor:**
- Add Box Collider to floor
- Make sure floor layer is "Ground"

**Can't look around:**
- Check Player → PlayerController has Camera assigned
- Make sure camera is child of Player

**UI not showing:**
- Check Canvas → Canvas Scaler
- Make sure UI objects are active
- Check GameManager has all UI assigned

**Questions not appearing:**
- Check QuestionPanel has all elements assigned
- Make sure Door script has questions added
- Check QuestionPanel script is on panel

**Can't collect items:**
- Make sure Collectible script is attached
- Check collectible has collider
- Player needs to be close (< 5 units)

---

## 📚 Resources

**Unity Learn:**
- https://learn.unity.com

**Documentation:**
- https://docs.unity3d.com

**Assets:**
- Unity Asset Store (free assets)
- Kenney.nl (free game assets)

---

## 🎯 Next Steps

**Enhancements:**
- ✨ Better graphics/materials
- 🎵 Sound effects
- 🎨 Particle effects
- 🏆 Leaderboard
- 📱 Mobile controls
- 🌐 Multiplayer

**Polish:**
- Main menu
- Pause menu
- Settings
- Save system
- More questions

---

## 📦 Project Files Included

```
CSM_EscapeRoom_Unity/
├── Assets/
│   ├── Scripts/
│   │   ├── GameManager.cs
│   │   ├── PlayerController.cs
│   │   ├── Collectible.cs
│   │   ├── Door.cs
│   │   ├── QuestionPanel.cs
│   │   └── RoomBuilder.cs
│   ├── Scenes/
│   ├── Prefabs/
│   └── Materials/
├── ProjectSettings/
└── SETUP_GUIDE.md (this file)
```

---

## 🎮 Ready to Play!

Once setup is complete:
1. Press **Play** in Unity
2. Test the game
3. Build for your target platform
4. Share with your team!

**Enjoy your Unity CSM Escape Room!** 🚀
