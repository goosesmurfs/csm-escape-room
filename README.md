# 🎮 CSM Escape Room - Unity Edition

A professional 3D escape room game built in Unity for learning Certified Scrum Master concepts.

---

## ✨ Features

✅ **Professional Unity Build** - Industry-standard game engine
✅ **First-Person 3D** - WASD + Mouse controls
✅ **5 Themed Rooms** - Each testing different CSM topics
✅ **Collectible System** - Find artifacts before answering
✅ **Interactive Questions** - Full UI with feedback
✅ **Cross-Platform** - Build for Windows, Mac, Linux, WebGL
✅ **Fully Customizable** - Easy to add rooms/questions
✅ **Production Ready** - Can be deployed to web or desktop

---

## 🚀 Quick Start

### For Users (Playing the Game)

1. Download the built game (`.exe` or WebGL build)
2. Run the executable
3. Play!

### For Developers (Unity Project)

1. Install Unity Hub & Unity 2021.3 LTS
2. Open project in Unity
3. See **[SETUP_GUIDE.md](SETUP_GUIDE.md)** for detailed instructions
4. Press Play to test
5. Build → Deploy

---

## 📁 Project Structure

```
CSM_EscapeRoom_Unity/
├── Assets/
│   ├── Scripts/           # All C# game scripts
│   │   ├── GameManager.cs        # Core game management
│   │   ├── PlayerController.cs   # First-person movement
│   │   ├── Collectible.cs        # Artifact collection
│   │   ├── Door.cs               # Door interactions
│   │   ├── QuestionPanel.cs      # UI for questions
│   │   └── RoomBuilder.cs        # Procedural room generation
│   ├── Scenes/            # Unity scenes
│   ├── Prefabs/           # Reusable objects
│   └── Materials/         # Textures and materials
├── ProjectSettings/       # Unity project settings
├── SETUP_GUIDE.md        # Complete setup instructions
└── README.md             # This file
```

---

## 🎯 Game Features

### Gameplay Loop

1. **Spawn** in first room
2. **Explore** the environment
3. **Collect** 3 golden artifacts (Press F)
4. **Approach** door (Press E)
5. **Answer** CSM questions in UI
6. **Pass** with enough correct answers
7. **Unlock** door and proceed
8. **Complete** all 5 rooms to win!

### 5 Rooms

| Room | Name | Focus | Questions |
|------|------|-------|-----------|
| 1 | Foundation | Scrum Pillars, Sprints | 2 |
| 2 | Guardians | Roles (PO, SM, Team) | 2 |
| 3 | Ceremony | Scrum Events | 2 |
| 4 | Artifacts | Backlog, Increment, DoD | 2 |
| 5 | Mastery | Values, Advanced Topics | 2 |

### Controls

| Input | Action |
|-------|--------|
| W | Move forward |
| A | Strafe left |
| S | Move backward |
| D | Strafe right |
| Mouse | Look around |
| F | Collect artifact |
| E | Interact with door |
| SPACE | Continue (in UI) |
| ESC | Unlock cursor |

---

## 🔧 Technical Details

### Built With

- **Engine:** Unity 2021.3 LTS
- **Language:** C# (.NET)
- **Renderer:** Universal Render Pipeline (URP)
- **Physics:** Unity Physics System
- **UI:** Unity UI (uGUI)

### System Requirements

**Minimum:**
- OS: Windows 7 SP1+, macOS 10.13+, Ubuntu 16.04+
- Graphics: DX10, DX11, DX12 capable
- RAM: 4 GB
- Storage: 500 MB

**Recommended:**
- OS: Windows 10, macOS 10.15+
- Graphics: Dedicated GPU
- RAM: 8 GB
- Storage: 1 GB

### Build Targets

✅ Windows (x64)
✅ macOS
✅ Linux
✅ WebGL (browser)
✅ Android (with modifications)
✅ iOS (with modifications)

---

## 🎨 Customization Guide

### Adding Questions

1. Open Unity project
2. Select Door object in scene
3. Inspector → Door Component
4. Expand "Questions" list
5. Click "+" and fill in:
   - Question text
   - 4 options (A, B, C, D)
   - Correct index (0-3)
   - Explanation

### Creating New Rooms

```csharp
// Edit RoomBuilder.cs
// Add new room in GetQuestionsForRoom()

case 5: // New Room
    return new CSMQuestion[]
    {
        new CSMQuestion {
            question = "Your question?",
            options = new string[] {"A)", "B)", "C)", "D)"},
            correctIndex = 1,
            explanation = "Your explanation"
        }
    };
```

### Changing Appearance

**Materials:**
1. Assets → Create → Material
2. Assign color/texture
3. Drag onto objects

**Lighting:**
- Adjust Directional Light intensity
- Add Point Lights for ambiance
- Use colored lights for atmosphere

**Particle Effects:**
- Window → Package Manager
- Install "Visual Effect Graph"
- Create particle systems

---

## 📦 Building & Deploying

### Build for Windows

```
1. File → Build Settings
2. Platform: Windows
3. Add Scenes
4. Build
5. Share .exe
```

### Build for WebGL

```
1. File → Build Settings
2. Platform: WebGL
3. Switch Platform
4. Build
5. Upload to:
   - itch.io
   - Newgrounds
   - Your website
```

### Build for Mobile

```
1. Install Android/iOS build support
2. File → Build Settings
3. Switch to Android/iOS
4. Configure settings
5. Build & Run
```

---

## 🌐 Deployment Options

### Web Hosting (FREE)

**itch.io:**
- Upload WebGL build
- Instant playable link
- Free hosting forever

**Simmer.io:**
- Unity game hosting
- Embeddable player

**GitHub Pages:**
- Host WebGL build
- Custom domain support

**Netlify/Vercel:**
- Drag-and-drop deployment
- Auto SSL

### Desktop Distribution

**Steam:**
- Upload .exe build
- Reach millions of players

**itch.io:**
- Desktop downloads
- Pay what you want

**Your Website:**
- Download link
- Direct distribution

---

## 📚 Learning Resources

### Unity

- [Unity Learn](https://learn.unity.com) - Free tutorials
- [Unity Manual](https://docs.unity3d.com/Manual/) - Official docs
- [Unity Scripting API](https://docs.unity3d.com/ScriptReference/) - C# reference

### C# Programming

- [Microsoft C# Docs](https://docs.microsoft.com/en-us/dotnet/csharp/)
- [Unity C# Tutorials](https://learn.unity.com/course/beginning-programming-with-unity)

### CSM Content

- [Scrum Guide](https://scrumguides.org/) - Official reference
- [Scrum.org](https://www.scrum.org/) - Practice assessments

---

## 🔄 Version History

**v1.0** - Initial Release
- 5 complete rooms
- Full CSM question set
- Collectible system
- Professional UI
- Cross-platform builds

---

## 🤝 Contributing

Want to improve the game?

1. Fork the project
2. Create your feature branch
3. Add your improvements
4. Submit a pull request

**Ideas for contributions:**
- More CSM questions
- Additional rooms
- Better graphics
- Sound effects
- Translations
- Mobile controls

---

## 📄 License

Free to use for educational purposes.

CSM content based on official Scrum Guide.

---

## 🎉 Credits

- **Engine:** Unity Technologies
- **CSM Content:** Scrum.org / Scrum Alliance
- **Concept:** AWS Skill Builder Escape Rooms
- **Built by:** [Your Name]

---

## 🆘 Support

**Issues?**
- Check [SETUP_GUIDE.md](SETUP_GUIDE.md)
- Unity Forums
- Stack Overflow

**Questions about CSM?**
- Visit Scrum.org
- Read the Scrum Guide

---

## 🚀 Ready to Build!

This is a complete, production-ready Unity project.

**Next steps:**
1. Read [SETUP_GUIDE.md](SETUP_GUIDE.md)
2. Open in Unity
3. Customize as needed
4. Build and deploy
5. Share with your team!

**Enjoy creating your CSM training game!** 🎮
