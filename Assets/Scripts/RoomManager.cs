using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Manages a single room in the escape room - handles collectibles and door unlocking
/// </summary>
public class RoomManager : MonoBehaviour
{
    [Header("Room Configuration")]
    public int roomNumber = 1;
    public ExamDomain roomDomain;
    public string roomName = "Cloud Concepts Chamber";

    [Header("Collectibles")]
    public int requiredCollectibles = 3;
    private int collectedCount = 0;
    private List<AWSCollectible> collectibles = new List<AWSCollectible>();

    [Header("Door")]
    public AWSDoor exitDoor;

    [Header("UI")]
    public Text collectiblesText;
    public Text roomTitleText;
    public GameObject instructionsPanel;

    void Start()
    {
        // Find all collectibles in the room
        collectibles = FindObjectsOfType<AWSCollectible>()
            .Where(c => c.domain == roomDomain).ToList();

        requiredCollectibles = collectibles.Count;

        UpdateUI();

        // Show instructions for first room
        if (roomNumber == 1 && instructionsPanel != null)
        {
            instructionsPanel.SetActive(true);
            Invoke("HideInstructions", 5f);
        }
    }

    void Update()
    {
        UpdateUI();
    }

    public void CollectItem(AWSCollectible collectible)
    {
        collectedCount++;

        Debug.Log($"Collected {collectedCount}/{requiredCollectibles} items");

        // Check if all collected
        if (collectedCount >= requiredCollectibles && exitDoor != null)
        {
            exitDoor.EnableInteraction();
        }

        UpdateUI();
    }

    void UpdateUI()
    {
        if (collectiblesText != null)
        {
            collectiblesText.text = $"AWS Badges: {collectedCount}/{requiredCollectibles}";

            if (collectedCount >= requiredCollectibles)
            {
                collectiblesText.text += "\n<color=green>✓ Approach the door!</color>";
                collectiblesText.color = new Color(0.15f, 0.8f, 0.38f);
            }
        }

        if (roomTitleText != null)
        {
            roomTitleText.text = $"Room {roomNumber}: {roomName}";
        }
    }

    void HideInstructions()
    {
        if (instructionsPanel != null)
        {
            instructionsPanel.SetActive(false);
        }
    }

    public bool CanOpenDoor()
    {
        return collectedCount >= requiredCollectibles;
    }
}
