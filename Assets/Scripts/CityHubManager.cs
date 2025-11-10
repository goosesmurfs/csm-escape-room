using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Manages the AWS City Hub - a central 3D world for navigating between AWS learning areas
/// Similar to AWS Cloud Quest's city environment
/// </summary>
public class CityHubManager : MonoBehaviour
{
    public static CityHubManager Instance { get; private set; }

    [Header("City Districts")]
    public CityDistrict cloudConceptsDistrict;
    public CityDistrict securityDistrict;
    public CityDistrict technologyDistrict;
    public CityDistrict billingDistrict;

    [Header("Central Plaza")]
    public Transform centralPlaza;
    public Transform playerSpawnPoint;

    [Header("UI")]
    public Text welcomeText;
    public GameObject questBoardUI;
    public GameObject mapUI;
    public GameObject progressDashboard;

    private FirstPersonController playerController;
    private List<QuestNPC> npcs = new List<QuestNPC>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        InitializeCityHub();
        SpawnPlayer();
        ShowWelcomeMessage();
    }

    private void InitializeCityHub()
    {
        Debug.Log("[CityHub] Initializing AWS City Hub...");

        // Initialize districts
        if (cloudConceptsDistrict != null)
            cloudConceptsDistrict.Initialize();
        if (securityDistrict != null)
            securityDistrict.Initialize();
        if (technologyDistrict != null)
            technologyDistrict.Initialize();
        if (billingDistrict != null)
            billingDistrict.Initialize();

        // Find all NPCs in the scene
        npcs.AddRange(FindObjectsOfType<QuestNPC>());
        Debug.Log($"[CityHub] Found {npcs.Count} NPCs in city");
    }

    private void SpawnPlayer()
    {
        // Find or create player
        playerController = FindObjectOfType<FirstPersonController>();

        if (playerController != null && playerSpawnPoint != null)
        {
            playerController.transform.position = playerSpawnPoint.position;
            playerController.transform.rotation = playerSpawnPoint.rotation;
        }
    }

    private void ShowWelcomeMessage()
    {
        if (welcomeText != null)
        {
            string playerName = PlayerProgress.Instance?.playerName ?? "Cloud Practitioner";
            welcomeText.text = $"Welcome to AWS City, {playerName}!\nExplore the districts to learn AWS services and complete quests.";

            // Hide after 5 seconds
            Invoke("HideWelcomeMessage", 5f);
        }
    }

    private void HideWelcomeMessage()
    {
        if (welcomeText != null)
        {
            welcomeText.gameObject.SetActive(false);
        }
    }

    public void ShowQuestBoard()
    {
        if (questBoardUI != null)
        {
            questBoardUI.SetActive(true);
            UpdateQuestBoard();
        }
    }

    private void UpdateQuestBoard()
    {
        // Update UI with available quests
        if (QuestManager.Instance != null)
        {
            var availableQuests = QuestManager.Instance.GetAvailableQuests();
            Debug.Log($"[CityHub] {availableQuests.Count} quests available");
        }
    }

    public void ShowMap()
    {
        if (mapUI != null)
        {
            mapUI.SetActive(!mapUI.activeSelf);
        }
    }

    public void ShowProgressDashboard()
    {
        if (progressDashboard != null)
        {
            progressDashboard.SetActive(!progressDashboard.activeSelf);
            UpdateProgressDashboard();
        }
    }

    private void UpdateProgressDashboard()
    {
        if (PlayerProgress.Instance != null)
        {
            float certReadiness = PlayerProgress.Instance.GetCertificationReadiness();
            int level = PlayerProgress.Instance.level;
            int badges = PlayerProgress.Instance.GetTotalBadges();

            Debug.Log($"[CityHub] Certification Readiness: {certReadiness:F1}%");
            Debug.Log($"[CityHub] Level: {level}, Badges: {badges}");
        }
    }

    public CityDistrict GetDistrictByDomain(ExamDomain domain)
    {
        switch (domain)
        {
            case ExamDomain.CloudConcepts:
                return cloudConceptsDistrict;
            case ExamDomain.SecurityAndCompliance:
                return securityDistrict;
            case ExamDomain.Technology:
                return technologyDistrict;
            case ExamDomain.BillingAndPricing:
                return billingDistrict;
            default:
                return null;
        }
    }

    private void Update()
    {
        // Keyboard shortcuts
        if (Input.GetKeyDown(KeyCode.M))
        {
            ShowMap();
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            ShowQuestBoard();
        }
        else if (Input.GetKeyDown(KeyCode.P))
        {
            ShowProgressDashboard();
        }
    }
}

/// <summary>
/// Represents a district in the AWS City
/// </summary>
[System.Serializable]
public class CityDistrict
{
    public string districtName;
    public ExamDomain domain;
    public Color districtColor;
    public Transform districtCenter;
    public List<Transform> buildingLocations;
    public List<Transform> npcSpawnPoints;
    public GameObject districtPortal;
    public string districtDescription;

    [HideInInspector]
    public bool isUnlocked = true;

    public void Initialize()
    {
        Debug.Log($"[CityHub] Initialized {districtName} district");

        // Apply district theming
        ApplyDistrictTheme();
    }

    private void ApplyDistrictTheme()
    {
        // Apply color theming to district
        if (districtCenter != null)
        {
            // Color ground, buildings, etc. based on district theme
            var renderers = districtCenter.GetComponentsInChildren<Renderer>();
            foreach (var renderer in renderers)
            {
                if (renderer.material != null)
                {
                    renderer.material.color = districtColor;
                }
            }
        }

        // Activate portal if unlocked
        if (districtPortal != null)
        {
            districtPortal.SetActive(isUnlocked);
        }
    }

    public void UnlockDistrict()
    {
        isUnlocked = true;
        if (districtPortal != null)
        {
            districtPortal.SetActive(true);
        }
        Debug.Log($"[CityHub] {districtName} district unlocked!");
    }

    public Transform GetRandomBuildingLocation()
    {
        if (buildingLocations != null && buildingLocations.Count > 0)
        {
            int index = Random.Range(0, buildingLocations.Count);
            return buildingLocations[index];
        }
        return districtCenter;
    }

    public Transform GetRandomNPCSpawnPoint()
    {
        if (npcSpawnPoints != null && npcSpawnPoints.Count > 0)
        {
            int index = Random.Range(0, npcSpawnPoints.Count);
            return npcSpawnPoints[index];
        }
        return districtCenter;
    }
}

/// <summary>
/// NPC that gives quests in the city hub
/// </summary>
public class QuestNPC : MonoBehaviour
{
    [Header("NPC Info")]
    public string npcName = "AWS Instructor";
    public string npcTitle = "Cloud Expert";
    public Sprite npcPortrait;

    [Header("Quest Info")]
    public string associatedQuestId;
    public ExamDomain npcDomain;

    [Header("Interaction")]
    public float interactionRange = 3f;
    public KeyCode interactKey = KeyCode.E;

    [Header("UI")]
    public GameObject interactionPrompt;
    public Text promptText;

    private Transform player;
    private bool playerInRange = false;

    private void Start()
    {
        // Find player
        var fpc = FindObjectOfType<FirstPersonController>();
        if (fpc != null)
        {
            player = fpc.transform;
        }

        if (interactionPrompt != null)
        {
            interactionPrompt.SetActive(false);
        }

        // Subscribe to quest completion
        if (QuestManager.Instance != null)
        {
            QuestManager.Instance.OnQuestCompleted += OnQuestCompleted;
        }
    }

    private void Update()
    {
        if (player == null) return;

        // Check distance to player
        float distance = Vector3.Distance(transform.position, player.position);
        playerInRange = distance <= interactionRange;

        // Show/hide interaction prompt
        if (interactionPrompt != null)
        {
            interactionPrompt.SetActive(playerInRange && CanInteract());
        }

        // Handle interaction
        if (playerInRange && Input.GetKeyDown(interactKey) && CanInteract())
        {
            Interact();
        }
    }

    private bool CanInteract()
    {
        if (string.IsNullOrEmpty(associatedQuestId))
            return true;

        // Check if quest is available
        if (QuestManager.Instance != null)
        {
            var quest = QuestManager.Instance.GetQuest(associatedQuestId);
            return quest != null && (quest.status == QuestStatus.Available || quest.status == QuestStatus.InProgress);
        }

        return true;
    }

    private void Interact()
    {
        Debug.Log($"[QuestNPC] Interacting with {npcName}");

        // Start associated quest
        if (!string.IsNullOrEmpty(associatedQuestId) && QuestManager.Instance != null)
        {
            var quest = QuestManager.Instance.GetQuest(associatedQuestId);

            if (quest != null && quest.status == QuestStatus.Available)
            {
                ShowQuestDialog(quest);
            }
            else if (quest != null && quest.status == QuestStatus.InProgress)
            {
                ShowQuestProgress(quest);
            }
            else
            {
                ShowGenericDialog();
            }
        }
        else
        {
            ShowGenericDialog();
        }
    }

    private void ShowQuestDialog(Quest quest)
    {
        Debug.Log($"[QuestNPC] {npcName}: {quest.questDescription}");
        Debug.Log($"[QuestNPC] Do you accept this quest? (Press Y to accept)");

        // In a real implementation, show UI dialog
        // For now, auto-accept after a delay
        if (QuestManager.Instance != null)
        {
            QuestManager.Instance.StartQuest(quest.questId);
        }
    }

    private void ShowQuestProgress(Quest quest)
    {
        float progress = quest.GetOverallProgress();
        Debug.Log($"[QuestNPC] {npcName}: You're making great progress! {progress:F0}% complete.");

        // Show objectives
        foreach (var objective in quest.objectives)
        {
            string status = objective.isCompleted ? "✓" : "○";
            Debug.Log($"{status} {objective.description} ({objective.currentCount}/{objective.targetCount})");
        }
    }

    private void ShowGenericDialog()
    {
        string[] genericMessages = new string[]
        {
            $"{npcName}: Welcome to the {npcDomain} district! Keep learning and you'll be AWS certified in no time!",
            $"{npcName}: Don't forget to check the quest board for new challenges!",
            $"{npcName}: Practice makes perfect. Try the hands-on labs to solidify your knowledge!",
            $"{npcName}: The AWS CCP exam covers {npcDomain}. Make sure you understand it well!"
        };

        int index = Random.Range(0, genericMessages.Length);
        Debug.Log($"[QuestNPC] {genericMessages[index]}");
    }

    private void OnQuestCompleted(Quest quest)
    {
        if (quest.questId == associatedQuestId)
        {
            Debug.Log($"[QuestNPC] {npcName}: Congratulations on completing the quest!");
        }
    }

    private void OnDestroy()
    {
        if (QuestManager.Instance != null)
        {
            QuestManager.Instance.OnQuestCompleted -= OnQuestCompleted;
        }
    }

    private void OnDrawGizmosSelected()
    {
        // Draw interaction range
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, interactionRange);
    }
}
