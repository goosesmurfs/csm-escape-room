using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor;
using UnityEditor.SceneManagement;
using System.IO;

/// <summary>
/// Automated scene generator for AWS CCP Exam Prep game
/// Creates LevelSelect and QuestionScene with all UI elements
/// </summary>
public class SceneGenerator : EditorWindow
{
    [MenuItem("Tools/Generate AWS CCP Scenes")]
    public static void GenerateAllScenes()
    {
        if (EditorUtility.DisplayDialog("Generate Scenes",
            "This will create LevelSelect, QuestionScene, and 4 Escape Room scenes. Continue?",
            "Yes", "Cancel"))
        {
            CreateScenesFolder();
            CreateLevelSelectScene();
            CreateQuestionScene();

            // Generate escape room scenes
            CreateEscapeRoomScene(ExamDomain.CloudConcepts, 1, "Cloud Concepts Chamber");
            CreateEscapeRoomScene(ExamDomain.SecurityAndCompliance, 2, "Security Vault");
            CreateEscapeRoomScene(ExamDomain.Technology, 3, "Technology Lab");
            CreateEscapeRoomScene(ExamDomain.BillingAndPricing, 4, "Cost Optimization Room");

            ConfigureBuildSettings();

            EditorUtility.DisplayDialog("Success",
                "Scenes generated successfully!\n\n" +
                "Created:\n" +
                "- Assets/Scenes/LevelSelect.unity\n" +
                "- Assets/Scenes/QuestionScene.unity\n" +
                "- Assets/Scenes/Room1_CloudConcepts.unity\n" +
                "- Assets/Scenes/Room2_Security.unity\n" +
                "- Assets/Scenes/Room3_Technology.unity\n" +
                "- Assets/Scenes/Room4_Billing.unity\n\n" +
                "Build settings configured.\n" +
                "Ready to build WebGL!",
                "OK");
        }
    }

    static void CreateScenesFolder()
    {
        string scenesPath = "Assets/Scenes";
        if (!Directory.Exists(scenesPath))
        {
            Directory.CreateDirectory(scenesPath);
            AssetDatabase.Refresh();
        }
    }

    static void CreateLevelSelectScene()
    {
        // Create new scene
        Scene scene = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);

        // Create Camera
        GameObject camera = new GameObject("Main Camera");
        camera.tag = "MainCamera";
        camera.AddComponent<Camera>().backgroundColor = new Color(0.1f, 0.1f, 0.15f);
        camera.AddComponent<AudioListener>();

        // Create Canvas
        GameObject canvasObj = new GameObject("Canvas");
        Canvas canvas = canvasObj.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvasObj.AddComponent<CanvasScaler>().uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        canvasObj.GetComponent<CanvasScaler>().referenceResolution = new Vector2(1280, 720);
        canvasObj.AddComponent<GraphicRaycaster>();

        // Create EventSystem
        GameObject eventSystem = new GameObject("EventSystem");
        eventSystem.AddComponent<UnityEngine.EventSystems.EventSystem>();
        eventSystem.AddComponent<UnityEngine.EventSystems.StandaloneInputModule>();

        // Create Main Panel
        GameObject mainPanel = CreatePanel(canvasObj.transform, "MainPanel");
        RectTransform mainPanelRT = mainPanel.GetComponent<RectTransform>();
        mainPanelRT.anchorMin = Vector2.zero;
        mainPanelRT.anchorMax = Vector2.one;
        mainPanelRT.offsetMin = Vector2.zero;
        mainPanelRT.offsetMax = Vector2.zero;
        mainPanel.GetComponent<Image>().color = new Color(0.05f, 0.05f, 0.1f, 1f);

        // Create Title Text
        GameObject titleObj = CreateText(mainPanel.transform, "TitleText",
            "AWS Cloud Practitioner Exam Prep", 48, TextAnchor.MiddleCenter);
        RectTransform titleRT = titleObj.GetComponent<RectTransform>();
        titleRT.anchorMin = new Vector2(0.5f, 0.85f);
        titleRT.anchorMax = new Vector2(0.5f, 0.95f);
        titleRT.sizeDelta = new Vector2(900, 80);
        titleObj.GetComponent<Text>().fontStyle = FontStyle.Bold;
        titleObj.GetComponent<Text>().color = new Color(1f, 0.84f, 0f); // Gold

        // Create Welcome Text
        GameObject welcomeObj = CreateText(mainPanel.transform, "WelcomeText",
            "Welcome, Player!", 24, TextAnchor.MiddleCenter);
        RectTransform welcomeRT = welcomeObj.GetComponent<RectTransform>();
        welcomeRT.anchorMin = new Vector2(0.5f, 0.78f);
        welcomeRT.anchorMax = new Vector2(0.5f, 0.82f);
        welcomeRT.sizeDelta = new Vector2(400, 30);

        // Create Player Name Input
        GameObject nameInputObj = CreateInputField(mainPanel.transform, "PlayerNameInput", "Enter your name...");
        RectTransform nameInputRT = nameInputObj.GetComponent<RectTransform>();
        nameInputRT.anchorMin = new Vector2(0.5f, 0.72f);
        nameInputRT.anchorMax = new Vector2(0.5f, 0.76f);
        nameInputRT.sizeDelta = new Vector2(300, 35);

        // Create Domain Buttons
        float buttonY = 0.60f;
        float buttonSpacing = 0.09f;

        CreateDomainButton(mainPanel.transform, "CloudConceptsButton", "Cloud Concepts (26%)",
            new Vector2(0.25f, buttonY), new Vector2(350, 60), new Color(0.2f, 0.5f, 0.8f));
        GameObject cloudProgress = CreateText(mainPanel.transform, "CloudConceptsProgress", "Not Started", 14, TextAnchor.MiddleLeft);
        cloudProgress.GetComponent<RectTransform>().anchoredPosition = new Vector2(-420, buttonY * 720 - 360 - 35);
        cloudProgress.GetComponent<RectTransform>().sizeDelta = new Vector2(200, 20);

        buttonY -= buttonSpacing;
        CreateDomainButton(mainPanel.transform, "SecurityButton", "Security & Compliance (25%)",
            new Vector2(0.25f, buttonY), new Vector2(350, 60), new Color(0.8f, 0.3f, 0.3f));
        GameObject securityProgress = CreateText(mainPanel.transform, "SecurityProgress", "Not Started", 14, TextAnchor.MiddleLeft);
        securityProgress.GetComponent<RectTransform>().anchoredPosition = new Vector2(-420, buttonY * 720 - 360 - 35);
        securityProgress.GetComponent<RectTransform>().sizeDelta = new Vector2(200, 20);

        buttonY -= buttonSpacing;
        CreateDomainButton(mainPanel.transform, "TechnologyButton", "Technology (33%)",
            new Vector2(0.25f, buttonY), new Vector2(350, 60), new Color(0.3f, 0.7f, 0.3f));
        GameObject techProgress = CreateText(mainPanel.transform, "TechnologyProgress", "Not Started", 14, TextAnchor.MiddleLeft);
        techProgress.GetComponent<RectTransform>().anchoredPosition = new Vector2(-420, buttonY * 720 - 360 - 35);
        techProgress.GetComponent<RectTransform>().sizeDelta = new Vector2(200, 20);

        buttonY -= buttonSpacing;
        CreateDomainButton(mainPanel.transform, "BillingButton", "Billing & Pricing (16%)",
            new Vector2(0.25f, buttonY), new Vector2(350, 60), new Color(0.9f, 0.6f, 0.2f));
        GameObject billingProgress = CreateText(mainPanel.transform, "BillingProgress", "Not Started", 14, TextAnchor.MiddleLeft);
        billingProgress.GetComponent<RectTransform>().anchoredPosition = new Vector2(-420, buttonY * 720 - 360 - 35);
        billingProgress.GetComponent<RectTransform>().sizeDelta = new Vector2(200, 20);

        buttonY -= buttonSpacing;
        CreateDomainButton(mainPanel.transform, "MixedChallengeButton", "Mixed Challenge",
            new Vector2(0.25f, buttonY), new Vector2(350, 60), new Color(0.6f, 0.4f, 0.8f));

        buttonY -= buttonSpacing;
        CreateDomainButton(mainPanel.transform, "FullExamButton", "Full Practice Exam",
            new Vector2(0.25f, buttonY), new Vector2(350, 60), new Color(0.8f, 0.7f, 0.2f));

        // Create Stats Panel (right side)
        GameObject statsPanel = CreatePanel(mainPanel.transform, "StatsPanel");
        RectTransform statsPanelRT = statsPanel.GetComponent<RectTransform>();
        statsPanelRT.anchorMin = new Vector2(0.55f, 0.15f);
        statsPanelRT.anchorMax = new Vector2(0.95f, 0.70f);
        statsPanelRT.offsetMin = Vector2.zero;
        statsPanelRT.offsetMax = Vector2.zero;
        statsPanel.GetComponent<Image>().color = new Color(0.1f, 0.1f, 0.15f, 0.8f);

        GameObject totalScoreText = CreateText(statsPanel.transform, "TotalScoreText",
            "Total Score: 0 pts\nQuestions Answered: 0\nOverall Accuracy: 0%",
            18, TextAnchor.UpperLeft);
        RectTransform totalScoreRT = totalScoreText.GetComponent<RectTransform>();
        totalScoreRT.anchorMin = new Vector2(0.05f, 0.70f);
        totalScoreRT.anchorMax = new Vector2(0.95f, 0.95f);
        totalScoreRT.offsetMin = Vector2.zero;
        totalScoreRT.offsetMax = Vector2.zero;

        GameObject weakAreasText = CreateText(statsPanel.transform, "WeakAreasText",
            "No weak areas yet", 16, TextAnchor.UpperLeft);
        RectTransform weakAreasRT = weakAreasText.GetComponent<RectTransform>();
        weakAreasRT.anchorMin = new Vector2(0.05f, 0.20f);
        weakAreasRT.anchorMax = new Vector2(0.95f, 0.65f);
        weakAreasRT.offsetMin = Vector2.zero;
        weakAreasRT.offsetMax = Vector2.zero;
        weakAreasText.GetComponent<Text>().color = new Color(1f, 0.6f, 0f);

        // Create Bottom Buttons
        CreateButton(mainPanel.transform, "LeaderboardButton", "Leaderboard",
            new Vector2(0.70f, 0.08f), new Vector2(180, 40), new Color(0.2f, 0.6f, 0.8f));

        CreateButton(mainPanel.transform, "StatisticsButton", "Statistics",
            new Vector2(0.85f, 0.08f), new Vector2(180, 40), new Color(0.3f, 0.7f, 0.3f));

        // Create GameManager
        GameObject gameManager = new GameObject("GameManager");
        LevelSelectUI levelSelectUI = gameManager.AddComponent<LevelSelectUI>();

        // Wire up references
        levelSelectUI.cloudConceptsButton = GameObject.Find("CloudConceptsButton").GetComponent<Button>();
        levelSelectUI.securityButton = GameObject.Find("SecurityButton").GetComponent<Button>();
        levelSelectUI.technologyButton = GameObject.Find("TechnologyButton").GetComponent<Button>();
        levelSelectUI.billingButton = GameObject.Find("BillingButton").GetComponent<Button>();
        levelSelectUI.mixedChallengeButton = GameObject.Find("MixedChallengeButton").GetComponent<Button>();
        levelSelectUI.fullExamButton = GameObject.Find("FullExamButton").GetComponent<Button>();

        levelSelectUI.cloudConceptsProgress = cloudProgress.GetComponent<Text>();
        levelSelectUI.securityProgress = securityProgress.GetComponent<Text>();
        levelSelectUI.technologyProgress = techProgress.GetComponent<Text>();
        levelSelectUI.billingProgress = billingProgress.GetComponent<Text>();
        levelSelectUI.totalScoreText = totalScoreText.GetComponent<Text>();
        levelSelectUI.weakAreasText = weakAreasText.GetComponent<Text>();
        levelSelectUI.playerNameInput = nameInputObj.GetComponent<InputField>();
        levelSelectUI.welcomeText = welcomeObj.GetComponent<Text>();

        // Wire up button events
        GameObject.Find("LeaderboardButton").GetComponent<Button>().onClick.AddListener(levelSelectUI.ViewLeaderboard);
        GameObject.Find("StatisticsButton").GetComponent<Button>().onClick.AddListener(levelSelectUI.ViewStats);

        // Save scene
        EditorSceneManager.SaveScene(scene, "Assets/Scenes/LevelSelect.unity");
        Debug.Log("Created LevelSelect.unity");
    }

    static void CreateQuestionScene()
    {
        // Create new scene
        Scene scene = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);

        // Create Camera
        GameObject camera = new GameObject("Main Camera");
        camera.tag = "MainCamera";
        camera.AddComponent<Camera>().backgroundColor = new Color(0.1f, 0.1f, 0.15f);
        camera.AddComponent<AudioListener>();

        // Create Canvas
        GameObject canvasObj = new GameObject("Canvas");
        Canvas canvas = canvasObj.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvasObj.AddComponent<CanvasScaler>().uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        canvasObj.GetComponent<CanvasScaler>().referenceResolution = new Vector2(1280, 720);
        canvasObj.AddComponent<GraphicRaycaster>();

        // Create EventSystem
        GameObject eventSystem = new GameObject("EventSystem");
        eventSystem.AddComponent<UnityEngine.EventSystems.EventSystem>();
        eventSystem.AddComponent<UnityEngine.EventSystems.StandaloneInputModule>();

        // Create Question Panel
        GameObject questionPanel = CreatePanel(canvasObj.transform, "QuestionPanel");
        RectTransform panelRT = questionPanel.GetComponent<RectTransform>();
        panelRT.anchorMin = new Vector2(0.1f, 0.1f);
        panelRT.anchorMax = new Vector2(0.9f, 0.9f);
        panelRT.offsetMin = Vector2.zero;
        panelRT.offsetMax = Vector2.zero;
        questionPanel.GetComponent<Image>().color = new Color(0.05f, 0.05f, 0.1f, 0.95f);

        // Top Info Bar
        GameObject domainText = CreateText(questionPanel.transform, "DomainText", "Cloud Concepts", 24, TextAnchor.MiddleLeft);
        domainText.GetComponent<RectTransform>().anchoredPosition = new Vector2(-450, 280);
        domainText.GetComponent<RectTransform>().sizeDelta = new Vector2(400, 40);

        GameObject difficultyText = CreateText(questionPanel.transform, "DifficultyText", "Medium", 18, TextAnchor.MiddleRight);
        difficultyText.GetComponent<RectTransform>().anchoredPosition = new Vector2(450, 280);
        difficultyText.GetComponent<RectTransform>().sizeDelta = new Vector2(150, 30);

        GameObject scoreText = CreateText(questionPanel.transform, "ScoreText", "Score: 0", 20, TextAnchor.MiddleLeft);
        scoreText.GetComponent<RectTransform>().anchoredPosition = new Vector2(-450, 240);
        scoreText.GetComponent<RectTransform>().sizeDelta = new Vector2(200, 30);

        GameObject streakText = CreateText(questionPanel.transform, "StreakText", "Streak: 0", 20, TextAnchor.MiddleCenter);
        streakText.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 240);
        streakText.GetComponent<RectTransform>().sizeDelta = new Vector2(200, 30);

        GameObject timerText = CreateText(questionPanel.transform, "TimerText", "Time: 0s", 20, TextAnchor.MiddleRight);
        timerText.GetComponent<RectTransform>().anchoredPosition = new Vector2(450, 240);
        timerText.GetComponent<RectTransform>().sizeDelta = new Vector2(200, 30);

        // Question Text
        GameObject questionTextObj = CreateText(questionPanel.transform, "QuestionText",
            "Question will appear here...", 22, TextAnchor.UpperLeft);
        RectTransform questionRT = questionTextObj.GetComponent<RectTransform>();
        questionRT.anchoredPosition = new Vector2(0, 140);
        questionRT.sizeDelta = new Vector2(900, 180);

        // Option Buttons
        for (int i = 0; i < 4; i++)
        {
            GameObject optionBtn = CreateButton(questionPanel.transform, $"OptionButton{i + 1}",
                $"Option {i + 1}", new Vector2(0.5f, 0.45f - i * 0.12f), new Vector2(900, 60),
                new Color(0.2f, 0.3f, 0.5f));

            // Make button text left-aligned
            Text btnText = optionBtn.GetComponentInChildren<Text>();
            btnText.alignment = TextAnchor.MiddleLeft;
            RectTransform btnTextRT = btnText.GetComponent<RectTransform>();
            btnTextRT.offsetMin = new Vector2(20, 0);
            btnTextRT.offsetMax = new Vector2(-20, 0);
        }

        // Feedback Text
        GameObject feedbackTextObj = CreateText(questionPanel.transform, "FeedbackText",
            "", 18, TextAnchor.UpperLeft);
        RectTransform feedbackRT = feedbackTextObj.GetComponent<RectTransform>();
        feedbackRT.anchoredPosition = new Vector2(0, -120);
        feedbackRT.sizeDelta = new Vector2(900, 120);
        feedbackTextObj.GetComponent<Text>().color = Color.white;
        feedbackTextObj.SetActive(false);

        // Continue Button
        GameObject continueBtn = CreateButton(questionPanel.transform, "ContinueButton",
            "Continue", new Vector2(0.5f, 0.08f), new Vector2(200, 50),
            new Color(0.3f, 0.7f, 0.3f));
        continueBtn.SetActive(false);

        // Create GameManager
        GameObject gameManager = new GameObject("GameManager");
        GameManager gmScript = gameManager.AddComponent<GameManager>();
        QuestionPanel questionPanelScript = gameManager.AddComponent<QuestionPanel>();

        // Wire up QuestionPanel references
        questionPanelScript.panel = questionPanel;
        questionPanelScript.questionText = questionTextObj.GetComponent<Text>();
        questionPanelScript.optionButtons = new Button[4];
        for (int i = 0; i < 4; i++)
        {
            questionPanelScript.optionButtons[i] = GameObject.Find($"OptionButton{i + 1}").GetComponent<Button>();
        }
        questionPanelScript.feedbackText = feedbackTextObj.GetComponent<Text>();
        questionPanelScript.continueButton = continueBtn.GetComponent<Button>();
        questionPanelScript.difficultyText = difficultyText.GetComponent<Text>();

        // Wire up GameManager references
        gmScript.domainText = domainText.GetComponent<Text>();
        gmScript.scoreText = scoreText.GetComponent<Text>();
        gmScript.timerText = timerText.GetComponent<Text>();
        gmScript.streakText = streakText.GetComponent<Text>();
        gmScript.questionPanel = questionPanel;

        // Save scene
        EditorSceneManager.SaveScene(scene, "Assets/Scenes/QuestionScene.unity");
        Debug.Log("Created QuestionScene.unity");
    }

    static void CreateEscapeRoomScene(ExamDomain domain, int roomNumber, string roomName)
    {
        // Create new scene
        Scene scene = EditorSceneManager.NewScene(NewSceneSetup.DefaultGameObjects, NewSceneMode.Single);

        // Remove default objects
        foreach (GameObject obj in GameObject.FindObjectsOfType<GameObject>())
        {
            if (obj.transform.parent == null)
            {
                GameObject.DestroyImmediate(obj);
            }
        }

        // Create Player
        GameObject player = new GameObject("Player");
        player.tag = "Player";
        player.transform.position = new Vector3(0, 1, -8);

        CharacterController cc = player.AddComponent<CharacterController>();
        cc.center = new Vector3(0, 1, 0);
        cc.radius = 0.5f;
        cc.height = 2f;

        FirstPersonController fpc = player.AddComponent<FirstPersonController>();

        GameObject camera = new GameObject("Main Camera");
        camera.tag = "MainCamera";
        camera.transform.SetParent(player.transform);
        camera.transform.localPosition = new Vector3(0, 0.6f, 0);
        camera.AddComponent<Camera>();
        camera.AddComponent<AudioListener>();

        GameObject groundCheck = new GameObject("GroundCheck");
        groundCheck.transform.SetParent(player.transform);
        groundCheck.transform.localPosition = new Vector3(0, -1, 0);
        fpc.groundCheck = groundCheck.transform;

        // Create Room (floor, walls, ceiling)
        CreateRoomGeometry(domain);

        // Create Collectibles (3 per room)
        CreateCollectible(domain, new Vector3(-5, 1, 0));
        CreateCollectible(domain, new Vector3(5, 1, 0));
        CreateCollectible(domain, new Vector3(0, 1, 5));

        // Create Door
        GameObject doorObj = GameObject.CreatePrimitive(PrimitiveType.Cube);
        doorObj.name = "ExitDoor";
        doorObj.transform.position = new Vector3(0, 1.5f, 9.5f);
        doorObj.transform.localScale = new Vector3(2, 3, 0.2f);

        AWSDoor door = doorObj.AddComponent<AWSDoor>();
        door.domain = domain;

        Light doorLight = doorObj.AddComponent<Light>();
        doorLight.type = LightType.Spot;
        doorLight.range = 5f;
        door.doorLight = doorLight;

        // Create Room Manager
        GameObject roomManagerObj = new GameObject("RoomManager");
        RoomManager roomManager = roomManagerObj.AddComponent<RoomManager>();
        roomManager.roomNumber = roomNumber;
        roomManager.roomDomain = domain;
        roomManager.roomName = roomName;
        roomManager.exitDoor = door;

        // Create UI Canvas
        GameObject canvasObj = new GameObject("Canvas");
        Canvas canvas = canvasObj.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvasObj.AddComponent<CanvasScaler>().uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        canvasObj.GetComponent<CanvasScaler>().referenceResolution = new Vector2(1280, 720);
        canvasObj.AddComponent<GraphicRaycaster>();

        // Add EventSystem
        GameObject eventSystem = new GameObject("EventSystem");
        eventSystem.AddComponent<UnityEngine.EventSystems.EventSystem>();
        eventSystem.AddComponent<UnityEngine.EventSystems.StandaloneInputModule>();

        // Create UI elements
        GameObject collectiblesText = CreateText(canvas.transform, "CollectiblesText", "AWS Badges: 0/3", 24, TextAnchor.UpperLeft);
        collectiblesText.GetComponent<RectTransform>().anchoredPosition = new Vector2(20, -20);
        collectiblesText.GetComponent<RectTransform>().sizeDelta = new Vector2(300, 50);
        roomManager.collectiblesText = collectiblesText.GetComponent<Text>();

        GameObject roomTitleText = CreateText(canvas.transform, "RoomTitleText", roomName, 32, TextAnchor.UpperCenter);
        roomTitleText.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -30);
        roomTitleText.GetComponent<RectTransform>().sizeDelta = new Vector2(600, 60);
        roomManager.roomTitleText = roomTitleText.GetComponent<Text>();

        // Create Question Panel (same as QuestionScene but initially hidden)
        GameObject questionPanel = CreatePanel(canvas.transform, "QuestionPanel");
        RectTransform panelRT = questionPanel.GetComponent<RectTransform>();
        panelRT.anchorMin = new Vector2(0.1f, 0.1f);
        panelRT.anchorMax = new Vector2(0.9f, 0.9f);
        panelRT.offsetMin = Vector2.zero;
        panelRT.offsetMax = Vector2.zero;
        questionPanel.SetActive(false);

        // Add GameManager
        GameObject gameManagerObj = new GameObject("GameManager");
        GameManager gm = gameManagerObj.AddComponent<GameManager>();
        gm.questionPanel = questionPanel;

        // Add Room Theme Manager for visual enhancements
        GameObject themeManagerObj = new GameObject("RoomThemeManager");
        RoomThemeManager themeManager = themeManagerObj.AddComponent<RoomThemeManager>();
        themeManager.domain = domain;

        // Save scene
        string sceneName = $"Assets/Scenes/Room{roomNumber}_{domain}.unity";
        EditorSceneManager.SaveScene(scene, sceneName);
        Debug.Log($"Created {sceneName}");
    }

    static void CreateRoomGeometry(ExamDomain domain)
    {
        // Floor
        GameObject floor = GameObject.CreatePrimitive(PrimitiveType.Cube);
        floor.name = "Floor";
        floor.transform.position = new Vector3(0, -0.1f, 0);
        floor.transform.localScale = new Vector3(20, 0.2f, 20);

        // Ceiling
        GameObject ceiling = GameObject.CreatePrimitive(PrimitiveType.Cube);
        ceiling.name = "Ceiling";
        ceiling.transform.position = new Vector3(0, 4.1f, 0);
        ceiling.transform.localScale = new Vector3(20, 0.2f, 20);

        // Walls
        GameObject wallN = GameObject.CreatePrimitive(PrimitiveType.Cube);
        wallN.name = "Wall_North";
        wallN.transform.position = new Vector3(0, 2, 10);
        wallN.transform.localScale = new Vector3(20, 4, 0.2f);

        GameObject wallS = GameObject.CreatePrimitive(PrimitiveType.Cube);
        wallS.name = "Wall_South";
        wallS.transform.position = new Vector3(0, 2, -10);
        wallS.transform.localScale = new Vector3(20, 4, 0.2f);

        GameObject wallE = GameObject.CreatePrimitive(PrimitiveType.Cube);
        wallE.name = "Wall_East";
        wallE.transform.position = new Vector3(10, 2, 0);
        wallE.transform.localScale = new Vector3(0.2f, 4, 20);

        GameObject wallW = GameObject.CreatePrimitive(PrimitiveType.Cube);
        wallW.name = "Wall_West";
        wallW.transform.position = new Vector3(-10, 2, 0);
        wallW.transform.localScale = new Vector3(0.2f, 4, 20);

        // Directional Light
        GameObject lightObj = new GameObject("Directional Light");
        lightObj.transform.rotation = Quaternion.Euler(50, -30, 0);
        Light light = lightObj.AddComponent<Light>();
        light.type = LightType.Directional;
        light.intensity = 1f;
    }

    static void CreateCollectible(ExamDomain domain, Vector3 position)
    {
        GameObject collectible = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        collectible.name = $"Collectible_{domain}";
        collectible.transform.position = position;
        collectible.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);

        AWSCollectible awsCollectible = collectible.AddComponent<AWSCollectible>();
        awsCollectible.domain = domain;

        SphereCollider collider = collectible.GetComponent<SphereCollider>();
        collider.isTrigger = true;
    }

    static void ConfigureBuildSettings()
    {
        // Add all scenes to build settings
        EditorBuildSettingsScene[] scenes = new EditorBuildSettingsScene[]
        {
            new EditorBuildSettingsScene("Assets/Scenes/LevelSelect.unity", true),
            new EditorBuildSettingsScene("Assets/Scenes/Room1_CloudConcepts.unity", true),
            new EditorBuildSettingsScene("Assets/Scenes/Room2_SecurityAndCompliance.unity", true),
            new EditorBuildSettingsScene("Assets/Scenes/Room3_Technology.unity", true),
            new EditorBuildSettingsScene("Assets/Scenes/Room4_BillingAndPricing.unity", true),
            new EditorBuildSettingsScene("Assets/Scenes/QuestionScene.unity", true)
        };

        EditorBuildSettings.scenes = scenes;
        Debug.Log("Build settings configured with all scenes");
    }

    // Helper methods for UI creation
    static GameObject CreatePanel(Transform parent, string name)
    {
        GameObject panel = new GameObject(name);
        panel.transform.SetParent(parent, false);
        panel.AddComponent<CanvasRenderer>();
        Image img = panel.AddComponent<Image>();
        img.color = new Color(0.2f, 0.2f, 0.25f, 0.9f);
        RectTransform rt = panel.GetComponent<RectTransform>();
        return panel;
    }

    static GameObject CreateText(Transform parent, string name, string text, int fontSize, TextAnchor alignment)
    {
        GameObject textObj = new GameObject(name);
        textObj.transform.SetParent(parent, false);
        Text textComp = textObj.AddComponent<Text>();
        textComp.text = text;
        textComp.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
        textComp.fontSize = fontSize;
        textComp.alignment = alignment;
        textComp.color = Color.white;
        RectTransform rt = textObj.GetComponent<RectTransform>();
        rt.anchorMin = new Vector2(0.5f, 0.5f);
        rt.anchorMax = new Vector2(0.5f, 0.5f);
        return textObj;
    }

    static GameObject CreateButton(Transform parent, string name, string text, Vector2 anchorPos, Vector2 size, Color color)
    {
        GameObject buttonObj = new GameObject(name);
        buttonObj.transform.SetParent(parent, false);

        Image img = buttonObj.AddComponent<Image>();
        img.color = color;

        Button btn = buttonObj.AddComponent<Button>();
        ColorBlock colors = btn.colors;
        colors.highlightedColor = color * 1.2f;
        colors.pressedColor = color * 0.8f;
        btn.colors = colors;

        RectTransform rt = buttonObj.GetComponent<RectTransform>();
        rt.anchorMin = anchorPos;
        rt.anchorMax = anchorPos;
        rt.anchoredPosition = new Vector2(0, anchorPos.y * 720 - 360);
        rt.sizeDelta = size;

        GameObject textObj = CreateText(buttonObj.transform, "Text", text, 18, TextAnchor.MiddleCenter);
        RectTransform textRT = textObj.GetComponent<RectTransform>();
        textRT.anchorMin = Vector2.zero;
        textRT.anchorMax = Vector2.one;
        textRT.offsetMin = Vector2.zero;
        textRT.offsetMax = Vector2.zero;
        textObj.GetComponent<Text>().fontStyle = FontStyle.Bold;

        return buttonObj;
    }

    static GameObject CreateDomainButton(Transform parent, string name, string text, Vector2 anchorPos, Vector2 size, Color color)
    {
        return CreateButton(parent, name, text, anchorPos, size, color);
    }

    static GameObject CreateInputField(Transform parent, string name, string placeholder)
    {
        GameObject inputObj = new GameObject(name);
        inputObj.transform.SetParent(parent, false);

        Image img = inputObj.AddComponent<Image>();
        img.color = new Color(0.15f, 0.15f, 0.2f, 1f);

        InputField inputField = inputObj.AddComponent<InputField>();

        // Create Text component for the input
        GameObject textObj = CreateText(inputObj.transform, "Text", "", 18, TextAnchor.MiddleLeft);
        RectTransform textRT = textObj.GetComponent<RectTransform>();
        textRT.anchorMin = new Vector2(0, 0);
        textRT.anchorMax = new Vector2(1, 1);
        textRT.offsetMin = new Vector2(10, 0);
        textRT.offsetMax = new Vector2(-10, 0);

        // Create Placeholder
        GameObject placeholderObj = CreateText(inputObj.transform, "Placeholder", placeholder, 18, TextAnchor.MiddleLeft);
        RectTransform placeholderRT = placeholderObj.GetComponent<RectTransform>();
        placeholderRT.anchorMin = new Vector2(0, 0);
        placeholderRT.anchorMax = new Vector2(1, 1);
        placeholderRT.offsetMin = new Vector2(10, 0);
        placeholderRT.offsetMax = new Vector2(-10, 0);
        placeholderObj.GetComponent<Text>().color = new Color(0.5f, 0.5f, 0.5f, 0.7f);

        inputField.textComponent = textObj.GetComponent<Text>();
        inputField.placeholder = placeholderObj.GetComponent<Text>();

        RectTransform rt = inputObj.GetComponent<RectTransform>();
        rt.anchorMin = new Vector2(0.5f, 0.5f);
        rt.anchorMax = new Vector2(0.5f, 0.5f);

        return inputObj;
    }
}
