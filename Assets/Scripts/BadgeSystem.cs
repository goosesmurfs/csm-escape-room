using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Manages the badge and achievement system
/// </summary>
public class BadgeSystem : MonoBehaviour
{
    public static BadgeSystem Instance { get; private set; }

    [Header("Badge Database")]
    private Dictionary<string, Badge> allBadges = new Dictionary<string, Badge>();

    [Header("UI")]
    public GameObject badgePopupPrefab;
    public Transform badgePopupContainer;
    public GameObject badgeCollectionUI;
    public Transform badgeGridParent;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeBadges();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // Subscribe to player progress events
        if (PlayerProgress.Instance != null)
        {
            // Badge events would be hooked up here
        }
    }

    private void InitializeBadges()
    {
        // Cloud Concepts Badges
        AddBadge(new Badge("Cloud Novice", "Complete your first cloud concepts lesson", ExamDomain.CloudConcepts, BadgeTier.Bronze));
        AddBadge(new Badge("Cloud Architect Apprentice", "Understand deployment models", ExamDomain.CloudConcepts, BadgeTier.Bronze));
        AddBadge(new Badge("Cloud Evangelist", "Master AWS benefits", ExamDomain.CloudConcepts, BadgeTier.Silver));
        AddBadge(new Badge("Global Architect", "Design multi-region architecture", ExamDomain.CloudConcepts, BadgeTier.Silver));
        AddBadge(new Badge("Well-Architected Expert", "Master all 6 pillars", ExamDomain.CloudConcepts, BadgeTier.Gold));

        // Security & Compliance Badges
        AddBadge(new Badge("Security Novice", "Learn the shared responsibility model", ExamDomain.SecurityAndCompliance, BadgeTier.Bronze));
        AddBadge(new Badge("IAM Expert", "Master identity and access management", ExamDomain.SecurityAndCompliance, BadgeTier.Gold));
        AddBadge(new Badge("Encryption Specialist", "Secure data with KMS", ExamDomain.SecurityAndCompliance, BadgeTier.Silver));
        AddBadge(new Badge("Compliance Expert", "Understand compliance programs", ExamDomain.SecurityAndCompliance, BadgeTier.Gold));
        AddBadge(new Badge("Security Master", "Complete all security quests", ExamDomain.SecurityAndCompliance, BadgeTier.Platinum));

        // Technology Badges
        AddBadge(new Badge("Service Explorer", "Tour all AWS service categories", ExamDomain.Technology, BadgeTier.Bronze));
        AddBadge(new Badge("Compute Expert", "Master EC2 and compute services", ExamDomain.Technology, BadgeTier.Silver));
        AddBadge(new Badge("Serverless Architect", "Build serverless applications", ExamDomain.Technology, BadgeTier.Gold));
        AddBadge(new Badge("Storage Expert", "Master S3 and storage services", ExamDomain.Technology, BadgeTier.Silver));
        AddBadge(new Badge("Database Expert", "Understand all database options", ExamDomain.Technology, BadgeTier.Silver));
        AddBadge(new Badge("Scaling Master", "Implement auto-scaling solutions", ExamDomain.Technology, BadgeTier.Gold));
        AddBadge(new Badge("Technology Guru", "Complete all technology quests", ExamDomain.Technology, BadgeTier.Platinum));

        // Billing & Pricing Badges
        AddBadge(new Badge("Cost Analyst", "Understand AWS pricing models", ExamDomain.BillingAndPricing, BadgeTier.Bronze));
        AddBadge(new Badge("Cost Optimizer", "Implement cost-saving strategies", ExamDomain.BillingAndPricing, BadgeTier.Silver));
        AddBadge(new Badge("Multi-Account Manager", "Set up AWS Organizations", ExamDomain.BillingAndPricing, BadgeTier.Silver));
        AddBadge(new Badge("Financial Architect", "Master all billing concepts", ExamDomain.BillingAndPricing, BadgeTier.Gold));

        // Hands-On Lab Badges
        AddBadge(new Badge("Amazon S3 Hands-On Expert", "Complete S3 hands-on lab", ExamDomain.Technology, BadgeTier.Gold));
        AddBadge(new Badge("Amazon EC2 Hands-On Expert", "Complete EC2 hands-on lab", ExamDomain.Technology, BadgeTier.Gold));
        AddBadge(new Badge("AWS Lambda Hands-On Expert", "Complete Lambda hands-on lab", ExamDomain.Technology, BadgeTier.Gold));
        AddBadge(new Badge("AWS IAM Hands-On Expert", "Complete IAM hands-on lab", ExamDomain.SecurityAndCompliance, BadgeTier.Gold));

        // Special Achievement Badges
        AddBadge(new Badge("Practice Exam Complete", "Complete a full practice exam", ExamDomain.CloudConcepts, BadgeTier.Silver));
        AddBadge(new Badge("CCP Ready!", "Score 80%+ on final exam", ExamDomain.CloudConcepts, BadgeTier.Platinum));
        AddBadge(new Badge("Perfect Score", "Achieve 100% on any quiz", ExamDomain.CloudConcepts, BadgeTier.Gold));
        AddBadge(new Badge("Speed Runner", "Complete 10 questions in under 5 minutes", ExamDomain.CloudConcepts, BadgeTier.Silver));
        AddBadge(new Badge("Streak Master", "Achieve a 10-question correct streak", ExamDomain.CloudConcepts, BadgeTier.Gold));
        AddBadge(new Badge("Dedicated Learner", "Complete quests 5 days in a row", ExamDomain.CloudConcepts, BadgeTier.Silver));

        Debug.Log($"[BadgeSystem] Initialized {allBadges.Count} badges");
    }

    private void AddBadge(Badge badge)
    {
        if (!allBadges.ContainsKey(badge.badgeName))
        {
            allBadges.Add(badge.badgeName, badge);
        }
    }

    public void AwardBadge(string badgeName)
    {
        if (!allBadges.ContainsKey(badgeName))
        {
            Debug.LogWarning($"[BadgeSystem] Badge not found: {badgeName}");
            return;
        }

        var badge = allBadges[badgeName];

        // Check if already earned
        if (PlayerProgress.Instance != null && PlayerProgress.Instance.HasBadge(badgeName))
        {
            Debug.Log($"[BadgeSystem] Badge already earned: {badgeName}");
            return;
        }

        // Award the badge
        if (PlayerProgress.Instance != null)
        {
            PlayerProgress.Instance.AwardBadge(badgeName);
            badge.isEarned = true;
            badge.earnedDate = System.DateTime.Now.ToString();

            // Show badge popup
            ShowBadgePopup(badge);

            Debug.Log($"[BadgeSystem] Badge earned: {badgeName}");
        }
    }

    private void ShowBadgePopup(Badge badge)
    {
        Debug.Log($"[BadgeSystem] 🏆 NEW BADGE EARNED! 🏆");
        Debug.Log($"[BadgeSystem] {badge.badgeName} ({badge.tier})");
        Debug.Log($"[BadgeSystem] {badge.description}");

        // In a real implementation, this would create a UI popup
        // For now, we'll just log it
    }

    public List<Badge> GetEarnedBadges()
    {
        List<Badge> earned = new List<Badge>();

        if (PlayerProgress.Instance != null)
        {
            foreach (var badgeName in PlayerProgress.Instance.badgesEarned)
            {
                if (allBadges.ContainsKey(badgeName))
                {
                    earned.Add(allBadges[badgeName]);
                }
            }
        }

        return earned;
    }

    public List<Badge> GetBadgesByDomain(ExamDomain domain)
    {
        List<Badge> domainBadges = new List<Badge>();

        foreach (var badge in allBadges.Values)
        {
            if (badge.domain == domain)
            {
                domainBadges.Add(badge);
            }
        }

        return domainBadges;
    }

    public int GetBadgeCountByTier(BadgeTier tier)
    {
        int count = 0;

        if (PlayerProgress.Instance != null)
        {
            foreach (var badgeName in PlayerProgress.Instance.badgesEarned)
            {
                if (allBadges.ContainsKey(badgeName) && allBadges[badgeName].tier == tier)
                {
                    count++;
                }
            }
        }

        return count;
    }

    public float GetBadgeCompletionPercentage()
    {
        if (allBadges.Count == 0) return 0f;

        int earnedCount = PlayerProgress.Instance != null ? PlayerProgress.Instance.GetTotalBadges() : 0;
        return (float)earnedCount / allBadges.Count * 100f;
    }

    public Dictionary<string, Badge> GetAllBadges()
    {
        return new Dictionary<string, Badge>(allBadges);
    }
}

[System.Serializable]
public class Badge
{
    public string badgeName;
    public string description;
    public ExamDomain domain;
    public BadgeTier tier;
    public bool isEarned;
    public string earnedDate;
    public Sprite badgeIcon;

    public Badge(string name, string desc, ExamDomain domain, BadgeTier tier)
    {
        this.badgeName = name;
        this.description = desc;
        this.domain = domain;
        this.tier = tier;
        this.isEarned = false;
        this.earnedDate = "";
    }

    public Color GetTierColor()
    {
        switch (tier)
        {
            case BadgeTier.Bronze:
                return new Color(0.8f, 0.5f, 0.2f); // Bronze
            case BadgeTier.Silver:
                return new Color(0.75f, 0.75f, 0.75f); // Silver
            case BadgeTier.Gold:
                return new Color(1f, 0.84f, 0f); // Gold
            case BadgeTier.Platinum:
                return new Color(0.9f, 0.95f, 1f); // Platinum
            default:
                return Color.white;
        }
    }
}

public enum BadgeTier
{
    Bronze,
    Silver,
    Gold,
    Platinum
}

/// <summary>
/// Skill tree visualization for AWS knowledge domains
/// </summary>
public class SkillTree : MonoBehaviour
{
    public static SkillTree Instance { get; private set; }

    [Header("Skill Nodes")]
    private Dictionary<string, SkillNode> skillNodes = new Dictionary<string, SkillNode>();

    [Header("UI")]
    public GameObject skillTreeUI;
    public Transform nodeContainer;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeSkillTree();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void InitializeSkillTree()
    {
        // Cloud Concepts Branch
        AddSkillNode(new SkillNode("Cloud Fundamentals", ExamDomain.CloudConcepts, 1, null));
        AddSkillNode(new SkillNode("Deployment Models", ExamDomain.CloudConcepts, 2, "Cloud Fundamentals"));
        AddSkillNode(new SkillNode("AWS Global Infrastructure", ExamDomain.CloudConcepts, 3, "Deployment Models"));
        AddSkillNode(new SkillNode("Well-Architected Framework", ExamDomain.CloudConcepts, 4, "AWS Global Infrastructure"));

        // Security Branch
        AddSkillNode(new SkillNode("Security Basics", ExamDomain.SecurityAndCompliance, 1, null));
        AddSkillNode(new SkillNode("IAM", ExamDomain.SecurityAndCompliance, 2, "Security Basics"));
        AddSkillNode(new SkillNode("Encryption & KMS", ExamDomain.SecurityAndCompliance, 3, "IAM"));
        AddSkillNode(new SkillNode("Compliance", ExamDomain.SecurityAndCompliance, 4, "Encryption & KMS"));

        // Technology Branch
        AddSkillNode(new SkillNode("AWS Services Overview", ExamDomain.Technology, 1, null));
        AddSkillNode(new SkillNode("Compute Services", ExamDomain.Technology, 2, "AWS Services Overview"));
        AddSkillNode(new SkillNode("Storage Services", ExamDomain.Technology, 2, "AWS Services Overview"));
        AddSkillNode(new SkillNode("Database Services", ExamDomain.Technology, 2, "AWS Services Overview"));
        AddSkillNode(new SkillNode("Serverless Architecture", ExamDomain.Technology, 3, "Compute Services"));
        AddSkillNode(new SkillNode("Auto Scaling & Load Balancing", ExamDomain.Technology, 3, "Compute Services"));

        // Billing Branch
        AddSkillNode(new SkillNode("Pricing Models", ExamDomain.BillingAndPricing, 1, null));
        AddSkillNode(new SkillNode("Cost Optimization", ExamDomain.BillingAndPricing, 2, "Pricing Models"));
        AddSkillNode(new SkillNode("AWS Organizations", ExamDomain.BillingAndPricing, 3, "Cost Optimization"));

        Debug.Log($"[SkillTree] Initialized {skillNodes.Count} skill nodes");
    }

    private void AddSkillNode(SkillNode node)
    {
        if (!skillNodes.ContainsKey(node.nodeName))
        {
            skillNodes.Add(node.nodeName, node);
        }
    }

    public void UnlockNode(string nodeName)
    {
        if (skillNodes.ContainsKey(nodeName))
        {
            var node = skillNodes[nodeName];
            node.isUnlocked = true;
            Debug.Log($"[SkillTree] Node unlocked: {nodeName}");
        }
    }

    public bool IsNodeUnlocked(string nodeName)
    {
        return skillNodes.ContainsKey(nodeName) && skillNodes[nodeName].isUnlocked;
    }

    public List<SkillNode> GetSkillNodesByDomain(ExamDomain domain)
    {
        List<SkillNode> nodes = new List<SkillNode>();

        foreach (var node in skillNodes.Values)
        {
            if (node.domain == domain)
            {
                nodes.Add(node);
            }
        }

        return nodes;
    }
}

[System.Serializable]
public class SkillNode
{
    public string nodeName;
    public ExamDomain domain;
    public int tier;
    public string prerequisiteNode;
    public bool isUnlocked;

    public SkillNode(string name, ExamDomain domain, int tier, string prerequisite)
    {
        this.nodeName = name;
        this.domain = domain;
        this.tier = tier;
        this.prerequisiteNode = prerequisite;
        this.isUnlocked = prerequisite == null; // First tier nodes are unlocked by default
    }
}
