using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Displays certification readiness dashboard showing progress toward AWS CCP exam
/// </summary>
public class CertificationReadiness : MonoBehaviour
{
    public static CertificationReadiness Instance { get; private set; }

    [Header("UI Elements")]
    public GameObject readinessDashboard;
    public Slider overallReadinessSlider;
    public Text overallReadinessText;
    public Text readinessLevelText;

    [Header("Domain Readiness")]
    public Slider cloudConceptsSlider;
    public Text cloudConceptsText;
    public Slider securitySlider;
    public Text securityText;
    public Slider technologySlider;
    public Text technologyText;
    public Slider billingSlider;
    public Text billingText;

    [Header("Statistics")]
    public Text totalQuestionsText;
    public Text accuracyText;
    public Text levelText;
    public Text badgesText;
    public Text questsCompletedText;

    [Header("Recommendations")]
    public Text recommendationsText;
    public GameObject weakAreasPanel;
    public Text weakAreasText;

    [Header("Certification Status")]
    public Text statusText;
    public Image statusIcon;
    public Color notReadyColor = Color.red;
    public Color learningColor = Color.yellow;
    public Color readyColor = Color.green;

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
        if (readinessDashboard != null)
        {
            readinessDashboard.SetActive(false);
        }
    }

    public void ShowDashboard()
    {
        if (readinessDashboard != null)
        {
            readinessDashboard.SetActive(true);
            UpdateDashboard();
        }
    }

    public void HideDashboard()
    {
        if (readinessDashboard != null)
        {
            readinessDashboard.SetActive(false);
        }
    }

    public void ToggleDashboard()
    {
        if (readinessDashboard != null)
        {
            bool isActive = readinessDashboard.activeSelf;
            if (isActive)
            {
                HideDashboard();
            }
            else
            {
                ShowDashboard();
            }
        }
    }

    private void UpdateDashboard()
    {
        if (PlayerProgress.Instance == null)
        {
            Debug.LogWarning("[CertificationReadiness] PlayerProgress not found");
            return;
        }

        // Calculate overall readiness
        float overallReadiness = PlayerProgress.Instance.GetCertificationReadiness();
        UpdateOverallReadiness(overallReadiness);

        // Update domain readiness
        UpdateDomainReadiness();

        // Update statistics
        UpdateStatistics();

        // Update recommendations
        UpdateRecommendations();

        // Update certification status
        UpdateCertificationStatus(overallReadiness);
    }

    private void UpdateOverallReadiness(float readiness)
    {
        // Update slider
        if (overallReadinessSlider != null)
        {
            overallReadinessSlider.value = readiness / 100f;
        }

        // Update text
        if (overallReadinessText != null)
        {
            overallReadinessText.text = $"{readiness:F1}%";
        }

        // Update readiness level
        if (readinessLevelText != null)
        {
            string level = GetReadinessLevel(readiness);
            readinessLevelText.text = level;

            // Color code the level
            if (readiness < 50f)
            {
                readinessLevelText.color = notReadyColor;
            }
            else if (readiness < 80f)
            {
                readinessLevelText.color = learningColor;
            }
            else
            {
                readinessLevelText.color = readyColor;
            }
        }
    }

    private string GetReadinessLevel(float readiness)
    {
        if (readiness >= 90f)
            return "EXCELLENT - You're ready to ace the exam!";
        else if (readiness >= 80f)
            return "READY - You should pass the exam";
        else if (readiness >= 70f)
            return "NEARLY READY - A bit more practice recommended";
        else if (readiness >= 50f)
            return "LEARNING - Keep studying and practicing";
        else if (readiness >= 30f)
            return "BEGINNING - You're making progress";
        else
            return "STARTING OUT - Lots to learn!";
    }

    private void UpdateDomainReadiness()
    {
        var domains = new ExamDomain[]
        {
            ExamDomain.CloudConcepts,
            ExamDomain.SecurityAndCompliance,
            ExamDomain.Technology,
            ExamDomain.BillingAndPricing
        };

        Slider[] sliders = { cloudConceptsSlider, securitySlider, technologySlider, billingSlider };
        Text[] texts = { cloudConceptsText, securityText, technologyText, billingText };

        for (int i = 0; i < domains.Length; i++)
        {
            var domain = domains[i];
            var progress = GameManager.Instance.GetDomainProgress(domain);

            float accuracy = progress.GetAccuracyPercentage();
            int questionsAnswered = progress.questionsAnswered;

            if (sliders[i] != null)
            {
                sliders[i].value = accuracy / 100f;
            }

            if (texts[i] != null)
            {
                texts[i].text = $"{accuracy:F0}% ({questionsAnswered} questions)";
            }
        }
    }

    private void UpdateStatistics()
    {
        if (PlayerProgress.Instance == null) return;

        // Total questions
        int totalQuestions = 0;
        int totalCorrect = 0;

        foreach (var domainProgress in PlayerProgress.Instance.domainProgress.Values)
        {
            totalQuestions += domainProgress.questionsAnswered;
            totalCorrect += domainProgress.correctAnswers;
        }

        if (totalQuestionsText != null)
        {
            totalQuestionsText.text = $"Total Questions: {totalQuestions}";
        }

        // Overall accuracy
        float overallAccuracy = totalQuestions > 0 ? (float)totalCorrect / totalQuestions * 100f : 0f;
        if (accuracyText != null)
        {
            accuracyText.text = $"Overall Accuracy: {overallAccuracy:F1}%";
        }

        // Level
        if (levelText != null)
        {
            int level = PlayerProgress.Instance.level;
            int xp = PlayerProgress.Instance.experiencePoints;
            int requiredXP = PlayerProgress.Instance.GetXPForNextLevel();
            levelText.text = $"Level: {level} ({xp}/{requiredXP} XP)";
        }

        // Badges
        if (badgesText != null)
        {
            int badges = PlayerProgress.Instance.GetTotalBadges();
            badgesText.text = $"Badges Earned: {badges}";
        }

        // Quests completed
        if (questsCompletedText != null && QuestManager.Instance != null)
        {
            int completed = QuestManager.Instance.GetCompletedQuestCount();
            int total = QuestManager.Instance.GetTotalQuestCount();
            questsCompletedText.text = $"Quests: {completed}/{total}";
        }
    }

    private void UpdateRecommendations()
    {
        List<string> recommendations = GenerateRecommendations();

        if (recommendationsText != null)
        {
            if (recommendations.Count > 0)
            {
                recommendationsText.text = "Recommendations:\n" + string.Join("\n", recommendations);
            }
            else
            {
                recommendationsText.text = "Great job! Keep up the excellent work!";
            }
        }

        // Show weak areas
        var weakDomains = GameManager.Instance.GetWeakDomains();
        if (weakAreasPanel != null)
        {
            weakAreasPanel.SetActive(weakDomains.Count > 0);
        }

        if (weakAreasText != null && weakDomains.Count > 0)
        {
            List<string> domainNames = new List<string>();
            foreach (var domain in weakDomains)
            {
                domainNames.Add(GetDomainDisplayName(domain));
            }
            weakAreasText.text = "Focus on: " + string.Join(", ", domainNames);
        }
    }

    private List<string> GenerateRecommendations()
    {
        List<string> recommendations = new List<string>();

        if (PlayerProgress.Instance == null) return recommendations;

        // Check each domain
        foreach (var kvp in PlayerProgress.Instance.domainProgress)
        {
            var domain = kvp.Key;
            var progress = kvp.Value;

            if (progress.questionsAnswered < 10)
            {
                recommendations.Add($"• Complete more {GetDomainDisplayName(domain)} questions (only {progress.questionsAnswered} answered)");
            }
            else if (progress.GetAccuracyPercentage() < 70f)
            {
                recommendations.Add($"• Review {GetDomainDisplayName(domain)} - accuracy below 70%");
            }
        }

        // Check hands-on labs
        bool hasS3Lab = PlayerProgress.Instance.HasBadge("Amazon S3 Hands-On Expert");
        bool hasEC2Lab = PlayerProgress.Instance.HasBadge("Amazon EC2 Hands-On Expert");
        bool hasLambdaLab = PlayerProgress.Instance.HasBadge("AWS Lambda Hands-On Expert");
        bool hasIAMLab = PlayerProgress.Instance.HasBadge("AWS IAM Hands-On Expert");

        if (!hasS3Lab)
            recommendations.Add("• Complete the S3 hands-on lab for practical experience");
        if (!hasEC2Lab)
            recommendations.Add("• Try the EC2 hands-on lab");
        if (!hasLambdaLab)
            recommendations.Add("• Practice with the Lambda hands-on lab");
        if (!hasIAMLab)
            recommendations.Add("• Master IAM with the hands-on lab");

        // Check practice exams
        bool hasPracticeExam = PlayerProgress.Instance.HasBadge("Practice Exam Complete");
        if (!hasPracticeExam)
        {
            recommendations.Add("• Take a full practice exam to assess your readiness");
        }

        return recommendations;
    }

    private void UpdateCertificationStatus(float readiness)
    {
        if (statusText != null)
        {
            if (readiness >= 80f)
            {
                statusText.text = "✓ CERTIFICATION READY";
                statusText.color = readyColor;
            }
            else if (readiness >= 70f)
            {
                statusText.text = "⚠ ALMOST READY";
                statusText.color = learningColor;
            }
            else
            {
                statusText.text = "✗ NOT READY YET";
                statusText.color = notReadyColor;
            }
        }

        if (statusIcon != null)
        {
            if (readiness >= 80f)
            {
                statusIcon.color = readyColor;
            }
            else if (readiness >= 70f)
            {
                statusIcon.color = learningColor;
            }
            else
            {
                statusIcon.color = notReadyColor;
            }
        }
    }

    private string GetDomainDisplayName(ExamDomain domain)
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

    private void Update()
    {
        // Keyboard shortcut to toggle dashboard
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ToggleDashboard();
        }
    }
}
