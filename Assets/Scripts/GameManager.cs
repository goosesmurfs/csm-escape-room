using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System.Linq;

/// <summary>
/// Main game manager - controls game state, scoring, and level progression for AWS CCP exam prep
/// </summary>
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Player Progress")]
    public PlayerProgress playerProgress;
    public GameSession currentSession;

    [Header("UI References")]
    public Text domainText;
    public Text scoreText;
    public Text progressText;
    public Text timerText;
    public Text streakText;
    public GameObject questionPanel;

    private float sessionStartTime;
    private float currentQuestionStartTime;

    void Awake()
    {
        // Singleton pattern
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadPlayerProgress();
            AWSQuestionDatabase.Initialize();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        UpdateUI();
    }

    void Update()
    {
        // Update timer if in session
        if (currentSession != null && !currentSession.IsSessionComplete())
        {
            float elapsed = Time.time - currentQuestionStartTime;
            if (timerText != null)
            {
                timerText.text = $"Time: {elapsed:F1}s";
            }
        }
    }

    void LoadPlayerProgress()
    {
        string savedData = PlayerPrefs.GetString("PlayerProgress", "");
        if (!string.IsNullOrEmpty(savedData))
        {
            playerProgress = JsonUtility.FromJson<PlayerProgress>(savedData);
        }
        else
        {
            playerProgress = new PlayerProgress();
        }
    }

    public void SavePlayerProgress()
    {
        string json = JsonUtility.ToJson(playerProgress);
        PlayerPrefs.SetString("PlayerProgress", json);
        PlayerPrefs.Save();
    }

    public void StartLevel(ExamDomain domain)
    {
        currentSession = new GameSession(domain);
        sessionStartTime = Time.time;
        currentQuestionStartTime = Time.time;

        // Load question scene if needed
        if (SceneManager.GetActiveScene().name != "QuestionScene")
        {
            SceneManager.LoadScene("QuestionScene");
        }

        UpdateUI();
        ShowNextQuestion();
    }

    public void ShowNextQuestion()
    {
        if (currentSession == null || currentSession.IsSessionComplete())
        {
            EndSession();
            return;
        }

        currentQuestionStartTime = Time.time;
        AWSQuestion question = currentSession.GetCurrentQuestion();

        if (question != null && questionPanel != null)
        {
            var panel = questionPanel.GetComponent<QuestionPanel>();
            if (panel != null)
            {
                panel.ShowAWSQuestion(question, OnQuestionAnswered);
            }
        }

        UpdateUI();
    }

    void OnQuestionAnswered(bool correct, float timeSpent)
    {
        if (currentSession == null) return;

        // Update session stats
        if (correct)
        {
            currentSession.correctAnswersInSession++;
            currentSession.consecutiveCorrect++;

            // Calculate points with streak bonus
            int basePoints = 100;
            int streakBonus = currentSession.consecutiveCorrect > 1 ? (currentSession.consecutiveCorrect - 1) * 10 : 0;
            int timeBonus = timeSpent < 10f ? 20 : timeSpent < 20f ? 10 : 0;
            int totalPoints = basePoints + streakBonus + timeBonus;

            playerProgress.totalScore += totalPoints;
        }
        else
        {
            currentSession.consecutiveCorrect = 0;
        }

        // Update domain progress
        ExamDomain domain = currentSession.domain;
        if (!playerProgress.domainProgress.ContainsKey(domain))
        {
            playerProgress.domainProgress[domain] = new DomainProgress();
        }

        var domainProg = playerProgress.domainProgress[domain];
        domainProg.questionsAnswered++;
        if (correct)
        {
            domainProg.correctAnswers++;
        }
        else
        {
            domainProg.wrongAnswers++;
        }

        // Update average time
        float totalTime = domainProg.averageTimePerQuestion * (domainProg.questionsAnswered - 1) + timeSpent;
        domainProg.averageTimePerQuestion = totalTime / domainProg.questionsAnswered;

        // Move to next question
        currentSession.currentQuestionIndex++;
        currentSession.totalTimeSpent += timeSpent;

        SavePlayerProgress();
    }

    void EndSession()
    {
        if (currentSession == null) return;

        // Mark level as completed if high score
        ExamDomain domain = currentSession.domain;
        float accuracy = currentSession.GetAccuracyPercentage();

        if (accuracy >= 70f && playerProgress.domainProgress.ContainsKey(domain))
        {
            playerProgress.domainProgress[domain].levelCompleted = true;
        }

        // Show results
        ShowSessionResults();

        SavePlayerProgress();
        currentSession = null;
    }

    void ShowSessionResults()
    {
        if (currentSession == null) return;

        float accuracy = currentSession.GetAccuracyPercentage();
        int totalQuestions = currentSession.questions.Count;
        int correct = currentSession.correctAnswersInSession;
        float avgTime = currentSession.totalTimeSpent / totalQuestions;

        string rating = accuracy >= 90f ? "EXCELLENT!" : accuracy >= 75f ? "VERY GOOD!" : accuracy >= 70f ? "PASSING" : "NEEDS IMPROVEMENT";
        string passStatus = accuracy >= 70f ? "PASSED" : "FAILED";

        string message = $"{passStatus}\n\n" +
                        $"Score: {correct}/{totalQuestions} ({accuracy:F1}%)\n" +
                        $"Rating: {rating}\n" +
                        $"Avg Time: {avgTime:F1}s per question\n" +
                        $"Total Time: {currentSession.totalTimeSpent:F0}s\n\n" +
                        $"Points Earned: {playerProgress.totalScore}";

        Debug.Log(message);

        // Return to level select after delay
        Invoke("ReturnToLevelSelect", 5f);
    }

    void ReturnToLevelSelect()
    {
        SceneManager.LoadScene("LevelSelect");
    }

    public void UpdateUI()
    {
        if (currentSession == null) return;

        // Domain name
        if (domainText != null)
        {
            string domainName = GetDomainDisplayName(currentSession.domain);
            int current = currentSession.currentQuestionIndex + 1;
            int total = currentSession.questions.Count;
            domainText.text = $"{domainName} - Question {current}/{total}";
        }

        // Score
        if (scoreText != null)
        {
            float accuracy = currentSession.GetAccuracyPercentage();
            scoreText.text = $"Score: {currentSession.correctAnswersInSession}/{currentSession.currentQuestionIndex} ({accuracy:F0}%)";
        }

        // Streak
        if (streakText != null && currentSession.consecutiveCorrect > 1)
        {
            streakText.text = $"🔥 {currentSession.consecutiveCorrect} Streak!";
            streakText.gameObject.SetActive(true);
        }
        else if (streakText != null)
        {
            streakText.gameObject.SetActive(false);
        }

        // Progress bar
        if (progressText != null)
        {
            int totalCompleted = playerProgress.domainProgress.Values.Sum(d => d.questionsAnswered);
            progressText.text = $"Total Questions: {totalCompleted}";
        }
    }

    string GetDomainDisplayName(ExamDomain domain)
    {
        switch (domain)
        {
            case ExamDomain.CloudConcepts:
                return "Cloud Concepts";
            case ExamDomain.SecurityAndCompliance:
                return "Security & Compliance";
            case ExamDomain.Technology:
                return "Technology";
            case ExamDomain.BillingAndPricing:
                return "Billing & Pricing";
            case ExamDomain.MixedChallenge:
                return "Mixed Challenge";
            case ExamDomain.FullPracticeExam:
                return "Full Practice Exam";
            default:
                return "AWS CCP Prep";
        }
    }

    public DomainProgress GetDomainProgress(ExamDomain domain)
    {
        if (playerProgress.domainProgress.ContainsKey(domain))
        {
            return playerProgress.domainProgress[domain];
        }
        return new DomainProgress();
    }

    public List<ExamDomain> GetWeakDomains()
    {
        List<ExamDomain> weakDomains = new List<ExamDomain>();

        foreach (var kvp in playerProgress.domainProgress)
        {
            if (kvp.Value.questionsAnswered >= 5 && kvp.Value.GetAccuracyPercentage() < 70f)
            {
                weakDomains.Add(kvp.Key);
            }
        }

        return weakDomains;
    }
}

/// <summary>
/// Tracks player's overall progress
/// </summary>
[System.Serializable]
public class PlayerProgress
{
    public string playerName = "Player";
    public int totalScore = 0;
    public Dictionary<ExamDomain, DomainProgress> domainProgress = new Dictionary<ExamDomain, DomainProgress>();

    public PlayerProgress()
    {
        // Initialize domain progress for all domains
        foreach (ExamDomain domain in System.Enum.GetValues(typeof(ExamDomain)))
        {
            if (domain != ExamDomain.MixedChallenge && domain != ExamDomain.FullPracticeExam)
            {
                domainProgress[domain] = new DomainProgress();
            }
        }
    }
}

/// <summary>
/// Tracks progress for a specific exam domain
/// </summary>
[System.Serializable]
public class DomainProgress
{
    public int questionsAnswered = 0;
    public int correctAnswers = 0;
    public int wrongAnswers = 0;
    public float averageTimePerQuestion = 0f;
    public bool levelCompleted = false;

    public float GetAccuracyPercentage()
    {
        if (questionsAnswered == 0) return 0f;
        return (correctAnswers / (float)questionsAnswered) * 100f;
    }
}

/// <summary>
/// Represents a single game session
/// </summary>
[System.Serializable]
public class GameSession
{
    public ExamDomain domain;
    public List<AWSQuestion> questions;
    public int currentQuestionIndex = 0;
    public int correctAnswersInSession = 0;
    public int consecutiveCorrect = 0;
    public float totalTimeSpent = 0f;

    public GameSession(ExamDomain domain)
    {
        this.domain = domain;
        this.questions = AWSQuestionDatabase.GetQuestionsForDomain(domain);
    }

    public AWSQuestion GetCurrentQuestion()
    {
        if (currentQuestionIndex < questions.Count)
            return questions[currentQuestionIndex];
        return null;
    }

    public bool IsSessionComplete()
    {
        return currentQuestionIndex >= questions.Count;
    }

    public float GetAccuracyPercentage()
    {
        if (currentQuestionIndex == 0) return 0f;
        return (correctAnswersInSession / (float)currentQuestionIndex) * 100f;
    }
}
