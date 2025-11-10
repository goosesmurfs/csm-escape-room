using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Manages leaderboard functionality with backend API integration
/// </summary>
public class LeaderboardManager : MonoBehaviour
{
    [Header("UI References")]
    public Transform leaderboardContent;
    public GameObject leaderboardEntryPrefab;
    public Text loadingText;
    public Text errorText;
    public Button refreshButton;
    public Dropdown filterDropdown;

    [Header("Local Leaderboard")]
    public Text localScoreText;
    public Text localRankText;

    // Backend API endpoint (can be configured to use AWS Lambda, Firebase, etc.)
    private string apiEndpoint = "YOUR_API_ENDPOINT_HERE";

    private List<LeaderboardEntry> leaderboardData = new List<LeaderboardEntry>();

    void Start()
    {
        if (refreshButton != null)
        {
            refreshButton.onClick.AddListener(RefreshLeaderboard);
        }

        if (filterDropdown != null)
        {
            filterDropdown.onValueChanged.AddListener(OnFilterChanged);
        }

        LoadLocalLeaderboard();
        RefreshLeaderboard();
    }

    void LoadLocalLeaderboard()
    {
        // Load saved leaderboard data from PlayerPrefs
        string json = PlayerPrefs.GetString("LocalLeaderboard", "{}");
        var wrapper = JsonUtility.FromJson<LeaderboardDataWrapper>(json);

        if (wrapper != null && wrapper.entries != null)
        {
            leaderboardData = wrapper.entries;
        }

        // Add current player if not exists
        if (GameManager.Instance != null)
        {
            AddOrUpdateLocalEntry(GameManager.Instance.playerProgress);
        }
    }

    void SaveLocalLeaderboard()
    {
        var wrapper = new LeaderboardDataWrapper { entries = leaderboardData };
        string json = JsonUtility.ToJson(wrapper);
        PlayerPrefs.SetString("LocalLeaderboard", json);
        PlayerPrefs.Save();
    }

    void AddOrUpdateLocalEntry(PlayerProgress progress)
    {
        var existing = leaderboardData.FirstOrDefault(e => e.playerName == progress.playerName);

        if (existing != null)
        {
            // Update existing entry
            existing.totalScore = progress.totalScore;
            existing.totalQuestions = progress.domainProgress.Values.Sum(d => d.questionsAnswered);
            existing.correctAnswers = progress.domainProgress.Values.Sum(d => d.correctAnswers);
            existing.overallAccuracy = existing.totalQuestions > 0 ?
                (existing.correctAnswers / (float)existing.totalQuestions * 100f) : 0f;
        }
        else
        {
            // Add new entry
            var entry = new LeaderboardEntry
            {
                playerName = progress.playerName,
                totalScore = progress.totalScore,
                totalQuestions = progress.domainProgress.Values.Sum(d => d.questionsAnswered),
                correctAnswers = progress.domainProgress.Values.Sum(d => d.correctAnswers)
            };
            entry.overallAccuracy = entry.totalQuestions > 0 ?
                (entry.correctAnswers / (float)entry.totalQuestions * 100f) : 0f;

            leaderboardData.Add(entry);
        }

        SaveLocalLeaderboard();
    }

    public void RefreshLeaderboard()
    {
        if (loadingText != null)
            loadingText.gameObject.SetActive(true);

        if (errorText != null)
            errorText.gameObject.SetActive(false);

        // Try to fetch from backend API
        StartCoroutine(FetchLeaderboardFromAPI());
    }

    IEnumerator FetchLeaderboardFromAPI()
    {
        // For now, use local leaderboard
        // In production, this would fetch from your backend API
        yield return new WaitForSeconds(0.5f); // Simulate network delay

        // Sort by total score
        leaderboardData = leaderboardData.OrderByDescending(e => e.totalScore).ToList();

        DisplayLeaderboard();

        if (loadingText != null)
            loadingText.gameObject.SetActive(false);
    }

    void DisplayLeaderboard()
    {
        // Clear existing entries
        foreach (Transform child in leaderboardContent)
        {
            Destroy(child.gameObject);
        }

        // Display top entries
        int rank = 1;
        foreach (var entry in leaderboardData.Take(100))
        {
            CreateLeaderboardEntry(rank, entry);
            rank++;
        }

        // Update local rank
        if (GameManager.Instance != null)
        {
            var localEntry = leaderboardData.FirstOrDefault(e =>
                e.playerName == GameManager.Instance.playerProgress.playerName);

            if (localEntry != null && localRankText != null)
            {
                int localRank = leaderboardData.IndexOf(localEntry) + 1;
                localRankText.text = $"Your Rank: #{localRank}";
            }

            if (localScoreText != null)
            {
                localScoreText.text = $"Your Score: {GameManager.Instance.playerProgress.totalScore}";
            }
        }
    }

    void CreateLeaderboardEntry(int rank, LeaderboardEntry entry)
    {
        GameObject entryObj = Instantiate(leaderboardEntryPrefab, leaderboardContent);

        // Set rank
        var rankText = entryObj.transform.Find("RankText")?.GetComponent<Text>();
        if (rankText != null)
        {
            rankText.text = $"#{rank}";

            // Special formatting for top 3
            if (rank == 1)
                rankText.text = "🥇 #1";
            else if (rank == 2)
                rankText.text = "🥈 #2";
            else if (rank == 3)
                rankText.text = "🥉 #3";
        }

        // Set player name
        var nameText = entryObj.transform.Find("NameText")?.GetComponent<Text>();
        if (nameText != null)
        {
            nameText.text = entry.playerName;

            // Highlight current player
            if (GameManager.Instance != null &&
                entry.playerName == GameManager.Instance.playerProgress.playerName)
            {
                nameText.fontStyle = FontStyle.Bold;
                nameText.color = new Color(1f, 0.84f, 0f); // Gold
            }
        }

        // Set score
        var scoreText = entryObj.transform.Find("ScoreText")?.GetComponent<Text>();
        if (scoreText != null)
        {
            scoreText.text = $"{entry.totalScore} pts";
        }

        // Set accuracy
        var accuracyText = entryObj.transform.Find("AccuracyText")?.GetComponent<Text>();
        if (accuracyText != null)
        {
            accuracyText.text = $"{entry.overallAccuracy:F1}%";

            if (entry.overallAccuracy >= 90f)
                accuracyText.color = new Color(0.15f, 0.8f, 0.38f); // Green
            else if (entry.overallAccuracy >= 70f)
                accuracyText.color = new Color(1f, 0.84f, 0f); // Gold
            else
                accuracyText.color = Color.white;
        }

        // Set questions answered
        var questionsText = entryObj.transform.Find("QuestionsText")?.GetComponent<Text>();
        if (questionsText != null)
        {
            questionsText.text = $"{entry.correctAnswers}/{entry.totalQuestions}";
        }
    }

    void OnFilterChanged(int filterIndex)
    {
        // 0 = All time, 1 = This week, 2 = Today, 3 = Friends
        // TODO: Implement filtering logic
        DisplayLeaderboard();
    }

    public void SubmitScore()
    {
        if (GameManager.Instance == null) return;

        // Update local leaderboard
        AddOrUpdateLocalEntry(GameManager.Instance.playerProgress);

        // Submit to backend API
        StartCoroutine(SubmitScoreToAPI());
    }

    IEnumerator SubmitScoreToAPI()
    {
        // TODO: Implement API submission
        // For now, just save locally
        yield return null;

        Debug.Log("Score submitted successfully");
    }

    public void BackToMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("LevelSelect");
    }
}

[System.Serializable]
public class LeaderboardEntry
{
    public string playerName;
    public int totalScore;
    public int totalQuestions;
    public int correctAnswers;
    public float overallAccuracy;
    public string timestamp;
}

[System.Serializable]
public class LeaderboardDataWrapper
{
    public List<LeaderboardEntry> entries;
}
