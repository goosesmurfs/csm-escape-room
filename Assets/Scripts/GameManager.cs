using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

/// <summary>
/// Main game manager - controls game state, scoring, and room progression
/// </summary>
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Game State")]
    public int currentRoom = 0;
    public int totalScore = 0;
    public int totalQuestions = 0;
    public int collectiblesFound = 0;
    public int requiredCollectibles = 3;

    [Header("UI References")]
    public Text roomText;
    public Text scoreText;
    public Text collectiblesText;
    public Text interactPromptText;
    public GameObject questionPanel;

    [Header("Room Prefabs")]
    public GameObject[] roomPrefabs;

    [Header("Player")]
    public Transform playerTransform;

    private List<GameObject> currentRoomObjects = new List<GameObject>();

    void Awake()
    {
        // Singleton pattern
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        BuildRoom(currentRoom);
        UpdateUI();
    }

    public void BuildRoom(int roomIndex)
    {
        // Clear previous room
        foreach (var obj in currentRoomObjects)
        {
            Destroy(obj);
        }
        currentRoomObjects.Clear();

        // Reset collectibles
        collectiblesFound = 0;

        // Create new room
        if (roomIndex < roomPrefabs.Length && roomPrefabs[roomIndex] != null)
        {
            Vector3 spawnPos = new Vector3(0, 0, roomIndex * 50);
            GameObject room = Instantiate(roomPrefabs[roomIndex], spawnPos, Quaternion.identity);
            currentRoomObjects.Add(room);
        }

        UpdateUI();
    }

    public void CollectArtifact()
    {
        collectiblesFound++;
        UpdateUI();

        if (collectiblesFound >= requiredCollectibles)
        {
            ShowMessage("All artifacts collected! Find the door!");
        }
    }

    public void AnswerQuestion(bool correct)
    {
        totalQuestions++;
        if (correct)
        {
            totalScore++;
        }
        UpdateUI();
    }

    public void AdvanceRoom()
    {
        currentRoom++;

        if (currentRoom < 5)
        {
            // Move player to next room
            if (playerTransform != null)
            {
                playerTransform.position = new Vector3(0, 2, currentRoom * 50 + 10);
            }

            BuildRoom(currentRoom);
            ShowMessage($"Entering Room {currentRoom + 1}!");
        }
        else
        {
            ShowVictory();
        }
    }

    public void UpdateUI()
    {
        string[] roomNames = { "Foundation", "Guardians", "Ceremony", "Artifacts", "Mastery" };

        if (roomText != null)
        {
            roomText.text = $"Room {currentRoom + 1}/5: {roomNames[currentRoom]}";
        }

        if (scoreText != null)
        {
            float percentage = totalQuestions > 0 ? (float)totalScore / totalQuestions * 100 : 0;
            scoreText.text = $"Score: {totalScore}/{totalQuestions} ({percentage:F0}%)";
        }

        if (collectiblesText != null)
        {
            collectiblesText.text = $"Artifacts: {collectiblesFound}/{requiredCollectibles}";
        }
    }

    public void ShowMessage(string message, float duration = 2f)
    {
        if (interactPromptText != null)
        {
            interactPromptText.text = message;
            interactPromptText.gameObject.SetActive(true);
            Invoke("HideMessage", duration);
        }
    }

    void HideMessage()
    {
        if (interactPromptText != null)
        {
            interactPromptText.gameObject.SetActive(false);
        }
    }

    void ShowVictory()
    {
        float percentage = totalQuestions > 0 ? (float)totalScore / totalQuestions * 100 : 0;
        string rating = percentage >= 90 ? "PERFECT!" : percentage >= 75 ? "EXCELLENT!" : "GOOD!";

        ShowMessage($"🎉 VICTORY! 🎉\nScore: {totalScore}/{totalQuestions} ({percentage:F1}%)\n{rating}", 10f);

        // Disable player movement
        var playerController = FindObjectOfType<PlayerController>();
        if (playerController != null)
        {
            playerController.enabled = false;
        }

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public bool CanAnswerQuestions()
    {
        return collectiblesFound >= requiredCollectibles;
    }
}
