using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Level selection menu for AWS CCP exam domains
/// </summary>
public class LevelSelectUI : MonoBehaviour
{
    [Header("Level Buttons")]
    public Button cloudConceptsButton;
    public Button securityButton;
    public Button technologyButton;
    public Button billingButton;
    public Button mixedChallengeButton;
    public Button fullExamButton;

    [Header("Progress Display")]
    public Text cloudConceptsProgress;
    public Text securityProgress;
    public Text technologyProgress;
    public Text billingProgress;
    public Text totalScoreText;
    public Text weakAreasText;

    [Header("Player Info")]
    public InputField playerNameInput;
    public Text welcomeText;

    void Start()
    {
        // Setup button listeners
        if (cloudConceptsButton != null)
            cloudConceptsButton.onClick.AddListener(() => StartLevel(ExamDomain.CloudConcepts));

        if (securityButton != null)
            securityButton.onClick.AddListener(() => StartLevel(ExamDomain.SecurityAndCompliance));

        if (technologyButton != null)
            technologyButton.onClick.AddListener(() => StartLevel(ExamDomain.Technology));

        if (billingButton != null)
            billingButton.onClick.AddListener(() => StartLevel(ExamDomain.BillingAndPricing));

        if (mixedChallengeButton != null)
            mixedChallengeButton.onClick.AddListener(() => StartLevel(ExamDomain.MixedChallenge));

        if (fullExamButton != null)
            fullExamButton.onClick.AddListener(() => StartLevel(ExamDomain.FullPracticeExam));

        // Setup player name
        if (playerNameInput != null)
        {
            string savedName = PlayerPrefs.GetString("PlayerName", "Player");
            playerNameInput.text = savedName;
            playerNameInput.onEndEdit.AddListener(OnPlayerNameChanged);
        }

        UpdateUI();
    }

    void UpdateUI()
    {
        if (GameManager.Instance == null) return;

        var progress = GameManager.Instance.playerProgress;

        // Update welcome text
        if (welcomeText != null)
        {
            welcomeText.text = $"Welcome, {progress.playerName}!";
        }

        // Update total score
        if (totalScoreText != null)
        {
            int totalQuestions = progress.domainProgress.Values.Sum(d => d.questionsAnswered);
            int totalCorrect = progress.domainProgress.Values.Sum(d => d.correctAnswers);
            float overallAccuracy = totalQuestions > 0 ? (totalCorrect / (float)totalQuestions * 100f) : 0f;

            totalScoreText.text = $"Total Score: {progress.totalScore} pts\n" +
                                 $"Questions Answered: {totalQuestions}\n" +
                                 $"Overall Accuracy: {overallAccuracy:F1}%";
        }

        // Update individual domain progress
        UpdateDomainProgress(ExamDomain.CloudConcepts, cloudConceptsProgress);
        UpdateDomainProgress(ExamDomain.SecurityAndCompliance, securityProgress);
        UpdateDomainProgress(ExamDomain.Technology, technologyProgress);
        UpdateDomainProgress(ExamDomain.BillingAndPricing, billingProgress);

        // Show weak areas
        if (weakAreasText != null)
        {
            var weakDomains = GameManager.Instance.GetWeakDomains();
            if (weakDomains.Count > 0)
            {
                string weakText = "⚠️ Focus Areas:\n";
                foreach (var domain in weakDomains)
                {
                    var domainProg = GameManager.Instance.GetDomainProgress(domain);
                    weakText += $"• {GetDomainDisplayName(domain)} ({domainProg.GetAccuracyPercentage():F0}%)\n";
                }
                weakAreasText.text = weakText;
                weakAreasText.gameObject.SetActive(true);
            }
            else
            {
                weakAreasText.gameObject.SetActive(false);
            }
        }
    }

    void UpdateDomainProgress(ExamDomain domain, Text progressText)
    {
        if (progressText == null) return;

        var progress = GameManager.Instance.GetDomainProgress(domain);

        if (progress.questionsAnswered == 0)
        {
            progressText.text = "Not Started";
            progressText.color = Color.gray;
        }
        else
        {
            float accuracy = progress.GetAccuracyPercentage();
            string status = progress.levelCompleted ? "✓" : "";

            progressText.text = $"{status} {progress.correctAnswers}/{progress.questionsAnswered} ({accuracy:F0}%)";

            if (accuracy >= 90f)
                progressText.color = new Color(0.15f, 0.8f, 0.38f); // Green
            else if (accuracy >= 70f)
                progressText.color = new Color(1f, 0.84f, 0f); // Gold
            else if (accuracy >= 50f)
                progressText.color = new Color(1f, 0.6f, 0f); // Orange
            else
                progressText.color = new Color(0.91f, 0.3f, 0.24f); // Red
        }
    }

    void StartLevel(ExamDomain domain)
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.StartLevel(domain);
        }
    }

    void OnPlayerNameChanged(string newName)
    {
        if (string.IsNullOrWhiteSpace(newName))
            newName = "Player";

        PlayerPrefs.SetString("PlayerName", newName);
        PlayerPrefs.Save();

        if (GameManager.Instance != null)
        {
            GameManager.Instance.playerProgress.playerName = newName;
            GameManager.Instance.SavePlayerProgress();
        }

        UpdateUI();
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
                return domain.ToString();
        }
    }

    public void ViewLeaderboard()
    {
        SceneManager.LoadScene("Leaderboard");
    }

    public void ViewStats()
    {
        SceneManager.LoadScene("Statistics");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
