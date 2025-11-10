using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.Collections.Generic;

/// <summary>
/// Statistics dashboard showing detailed progress analysis
/// </summary>
public class StatisticsUI : MonoBehaviour
{
    [Header("Overall Stats")]
    public Text totalQuestionsText;
    public Text totalCorrectText;
    public Text overallAccuracyText;
    public Text totalTimeText;
    public Text averageTimeText;

    [Header("Domain Breakdown")]
    public Transform domainStatsContainer;
    public GameObject domainStatPrefab;

    [Header("Weak Areas")]
    public Text weakAreasText;
    public Text recommendationsText;

    [Header("Progress Chart")]
    public Image cloudConceptsBar;
    public Image securityBar;
    public Image technologyBar;
    public Image billingBar;

    [Header("Achievement Display")]
    public Transform achievementsContainer;
    public GameObject achievementPrefab;

    void Start()
    {
        UpdateStatistics();
    }

    void UpdateStatistics()
    {
        if (GameManager.Instance == null) return;

        var progress = GameManager.Instance.playerProgress;

        // Calculate overall stats
        int totalQuestions = progress.domainProgress.Values.Sum(d => d.questionsAnswered);
        int totalCorrect = progress.domainProgress.Values.Sum(d => d.correctAnswers);
        float overallAccuracy = totalQuestions > 0 ? (totalCorrect / (float)totalQuestions * 100f) : 0f;
        float totalTime = progress.domainProgress.Values.Sum(d => d.averageTimePerQuestion * d.questionsAnswered);
        float avgTime = totalQuestions > 0 ? totalTime / totalQuestions : 0f;

        // Update overall stats UI
        if (totalQuestionsText != null)
            totalQuestionsText.text = $"{totalQuestions}";

        if (totalCorrectText != null)
            totalCorrectText.text = $"{totalCorrect}";

        if (overallAccuracyText != null)
        {
            overallAccuracyText.text = $"{overallAccuracy:F1}%";

            if (overallAccuracy >= 90f)
                overallAccuracyText.color = new Color(0.15f, 0.8f, 0.38f); // Green
            else if (overallAccuracy >= 70f)
                overallAccuracyText.color = new Color(1f, 0.84f, 0f); // Gold
            else if (overallAccuracy >= 50f)
                overallAccuracyText.color = new Color(1f, 0.6f, 0f); // Orange
            else
                overallAccuracyText.color = new Color(0.91f, 0.3f, 0.24f); // Red
        }

        if (totalTimeText != null)
        {
            int minutes = Mathf.FloorToInt(totalTime / 60f);
            int seconds = Mathf.FloorToInt(totalTime % 60f);
            totalTimeText.text = $"{minutes}m {seconds}s";
        }

        if (averageTimeText != null)
            averageTimeText.text = $"{avgTime:F1}s";

        // Update domain breakdown
        UpdateDomainBreakdown(progress);

        // Update progress bars
        UpdateProgressBars(progress);

        // Update weak areas analysis
        UpdateWeakAreasAnalysis(progress);

        // Update recommendations
        UpdateRecommendations(progress, overallAccuracy);
    }

    void UpdateDomainBreakdown(PlayerProgress progress)
    {
        if (domainStatsContainer == null || domainStatPrefab == null) return;

        // Clear existing
        foreach (Transform child in domainStatsContainer)
        {
            Destroy(child.gameObject);
        }

        // Create stat entries for each domain
        foreach (var kvp in progress.domainProgress.OrderByDescending(d => d.Value.questionsAnswered))
        {
            if (kvp.Value.questionsAnswered == 0) continue;

            GameObject statObj = Instantiate(domainStatPrefab, domainStatsContainer);

            // Domain name
            var nameText = statObj.transform.Find("DomainName")?.GetComponent<Text>();
            if (nameText != null)
                nameText.text = GetDomainDisplayName(kvp.Key);

            // Questions answered
            var questionsText = statObj.transform.Find("QuestionsText")?.GetComponent<Text>();
            if (questionsText != null)
                questionsText.text = $"{kvp.Value.correctAnswers}/{kvp.Value.questionsAnswered}";

            // Accuracy
            var accuracyText = statObj.transform.Find("AccuracyText")?.GetComponent<Text>();
            if (accuracyText != null)
            {
                float accuracy = kvp.Value.GetAccuracyPercentage();
                accuracyText.text = $"{accuracy:F1}%";

                if (accuracy >= 90f)
                    accuracyText.color = new Color(0.15f, 0.8f, 0.38f);
                else if (accuracy >= 70f)
                    accuracyText.color = new Color(1f, 0.84f, 0f);
                else if (accuracy >= 50f)
                    accuracyText.color = new Color(1f, 0.6f, 0f);
                else
                    accuracyText.color = new Color(0.91f, 0.3f, 0.24f);
            }

            // Average time
            var timeText = statObj.transform.Find("AvgTimeText")?.GetComponent<Text>();
            if (timeText != null)
                timeText.text = $"{kvp.Value.averageTimePerQuestion:F1}s";

            // Progress bar
            var progressBar = statObj.transform.Find("ProgressBar")?.GetComponent<Image>();
            if (progressBar != null)
            {
                float accuracy = kvp.Value.GetAccuracyPercentage();
                progressBar.fillAmount = accuracy / 100f;

                if (accuracy >= 70f)
                    progressBar.color = new Color(0.15f, 0.8f, 0.38f);
                else
                    progressBar.color = new Color(1f, 0.6f, 0f);
            }
        }
    }

    void UpdateProgressBars(PlayerProgress progress)
    {
        UpdateDomainProgressBar(ExamDomain.CloudConcepts, cloudConceptsBar, progress);
        UpdateDomainProgressBar(ExamDomain.SecurityAndCompliance, securityBar, progress);
        UpdateDomainProgressBar(ExamDomain.Technology, technologyBar, progress);
        UpdateDomainProgressBar(ExamDomain.BillingAndPricing, billingBar, progress);
    }

    void UpdateDomainProgressBar(ExamDomain domain, Image bar, PlayerProgress progress)
    {
        if (bar == null) return;

        if (progress.domainProgress.ContainsKey(domain))
        {
            var domainProg = progress.domainProgress[domain];
            if (domainProg.questionsAnswered > 0)
            {
                float accuracy = domainProg.GetAccuracyPercentage();
                bar.fillAmount = accuracy / 100f;

                if (accuracy >= 90f)
                    bar.color = new Color(0.15f, 0.8f, 0.38f); // Green
                else if (accuracy >= 70f)
                    bar.color = new Color(1f, 0.84f, 0f); // Gold
                else if (accuracy >= 50f)
                    bar.color = new Color(1f, 0.6f, 0f); // Orange
                else
                    bar.color = new Color(0.91f, 0.3f, 0.24f); // Red
            }
            else
            {
                bar.fillAmount = 0f;
                bar.color = Color.gray;
            }
        }
    }

    void UpdateWeakAreasAnalysis(PlayerProgress progress)
    {
        if (weakAreasText == null) return;

        var weakDomains = GameManager.Instance.GetWeakDomains();

        if (weakDomains.Count == 0)
        {
            weakAreasText.text = "No weak areas identified yet!\nKeep practicing to maintain your performance.";
            weakAreasText.color = new Color(0.15f, 0.8f, 0.38f);
        }
        else
        {
            string text = "Focus on these areas:\n\n";

            foreach (var domain in weakDomains)
            {
                var domainProg = progress.domainProgress[domain];
                text += $"• {GetDomainDisplayName(domain)}\n";
                text += $"  Current: {domainProg.GetAccuracyPercentage():F1}% ({domainProg.correctAnswers}/{domainProg.questionsAnswered})\n";
                text += $"  Target: 70%+ for passing\n\n";
            }

            weakAreasText.text = text;
            weakAreasText.color = new Color(1f, 0.6f, 0f);
        }
    }

    void UpdateRecommendations(PlayerProgress progress, float overallAccuracy)
    {
        if (recommendationsText == null) return;

        List<string> recommendations = new List<string>();

        // Check overall progress
        int totalQuestions = progress.domainProgress.Values.Sum(d => d.questionsAnswered);

        if (totalQuestions < 50)
        {
            recommendations.Add("• Complete at least 50 practice questions to get a good baseline");
        }

        if (overallAccuracy < 70f && totalQuestions >= 10)
        {
            recommendations.Add("• Focus on understanding explanations for missed questions");
            recommendations.Add("• Try the Mixed Challenge mode to practice weak areas");
        }

        if (overallAccuracy >= 90f && totalQuestions >= 100)
        {
            recommendations.Add("• You're ready! Try the Full Practice Exam to test your skills");
            recommendations.Add("• Review weak domains one more time before the real exam");
        }

        // Check domain-specific recommendations
        foreach (var kvp in progress.domainProgress)
        {
            if (kvp.Value.questionsAnswered >= 5)
            {
                float accuracy = kvp.Value.GetAccuracyPercentage();
                float avgTime = kvp.Value.averageTimePerQuestion;

                if (accuracy < 60f)
                {
                    recommendations.Add($"• {GetDomainDisplayName(kvp.Key)}: Review fundamentals");
                }
                else if (avgTime > 60f)
                {
                    recommendations.Add($"• {GetDomainDisplayName(kvp.Key)}: Work on response speed");
                }
            }
            else if (kvp.Value.questionsAnswered == 0)
            {
                recommendations.Add($"• Start practicing {GetDomainDisplayName(kvp.Key)}");
            }
        }

        if (recommendations.Count == 0)
        {
            recommendations.Add("• Great job! Keep practicing to maintain your skills");
            recommendations.Add("• Try the Full Practice Exam when you feel ready");
        }

        recommendationsText.text = string.Join("\n", recommendations);
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
            default:
                return domain.ToString();
        }
    }

    public void BackToMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("LevelSelect");
    }

    public void ResetProgress()
    {
        if (GameManager.Instance != null)
        {
            // Confirm with user first
            GameManager.Instance.playerProgress = new PlayerProgress();
            GameManager.Instance.SavePlayerProgress();
            UpdateStatistics();
        }
    }
}
