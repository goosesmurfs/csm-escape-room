using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Manages the 2.5D top-down world - collectibles, NPCs, and progress
/// </summary>
public class WorldManager : MonoBehaviour
{
    [Header("Progress Tracking")]
    public int totalBadges = 0;
    public int collectedBadges = 0;

    [Header("UI")]
    public Text badgeCountText;
    public Text instructionText;

    private List<AWSCollectible2D> allBadges = new List<AWSCollectible2D>();

    void Start()
    {
        // Find all badges in the world
        allBadges = FindObjectsOfType<AWSCollectible2D>().ToList();
        totalBadges = allBadges.Count;

        UpdateUI();

        // Show initial instructions
        if (instructionText != null)
        {
            instructionText.text = "Explore the world! Collect AWS badges and talk to NPCs to take quizzes.\n[WASD] Move | [E] Interact";
            Invoke("HideInstructions", 5f);
        }
    }

    public void CollectBadge(AWSCollectible2D badge)
    {
        collectedBadges++;
        Debug.Log($"Collected badge! {collectedBadges}/{totalBadges}");

        UpdateUI();

        // Check if all badges collected
        if (collectedBadges >= totalBadges)
        {
            OnAllBadgesCollected();
        }
    }

    void UpdateUI()
    {
        if (badgeCountText != null)
        {
            badgeCountText.text = $"AWS Badges: {collectedBadges}/{totalBadges}";

            if (collectedBadges >= totalBadges)
            {
                badgeCountText.text += "\n<color=green>All badges collected!</color>";
            }
        }
    }

    void OnAllBadgesCollected()
    {
        Debug.Log("Congratulations! All AWS badges collected!");

        if (instructionText != null)
        {
            instructionText.gameObject.SetActive(true);
            instructionText.text = "<color=green>Amazing! You collected all AWS badges!\nNow complete all the NPC quizzes to master AWS!</color>";
        }
    }

    void HideInstructions()
    {
        if (instructionText != null)
        {
            instructionText.gameObject.SetActive(false);
        }
    }

    public int GetCollectedCount()
    {
        return collectedBadges;
    }

    public int GetTotalCount()
    {
        return totalBadges;
    }
}
