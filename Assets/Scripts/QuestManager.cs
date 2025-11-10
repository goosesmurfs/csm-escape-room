using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public static QuestManager Instance { get; private set; }

    [SerializeField] private List<Quest> allQuests = new List<Quest>();
    [SerializeField] private List<Quest> activeQuests = new List<Quest>();
    [SerializeField] private List<Quest> completedQuests = new List<Quest>();

    // Events
    public event Action<Quest> OnQuestStarted;
    public event Action<Quest> OnQuestCompleted;
    public event Action<Quest, QuestObjective> OnObjectiveCompleted;
    public event Action<QuestReward> OnRewardEarned;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeQuests();
            LoadProgress();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void InitializeQuests()
    {
        allQuests.Clear();

        // ===== CLOUD CONCEPTS QUESTS =====
        CreateCloudConceptsQuests();

        // ===== SECURITY & COMPLIANCE QUESTS =====
        CreateSecurityQuests();

        // ===== TECHNOLOGY QUESTS =====
        CreateTechnologyQuests();

        // ===== BILLING & PRICING QUESTS =====
        CreateBillingQuests();

        // ===== FINAL ASSESSMENT QUESTS =====
        CreateAssessmentQuests();

        Debug.Log($"[QuestManager] Initialized {allQuests.Count} quests");
    }

    private void CreateCloudConceptsQuests()
    {
        // Quest 1: Introduction to AWS Cloud
        var quest1 = new Quest(
            "CC_INTRO_001",
            "Welcome to AWS Cloud",
            "Learn the fundamental concepts of cloud computing and why organizations move to AWS.",
            ExamDomain.CloudConcepts,
            QuestType.Tutorial
        );
        quest1.storyContext = "A local startup wants to understand if moving to the cloud is right for their business. Help them learn the basics!";
        quest1.AddObjective(new QuestObjective("learn_cloud_basics", "Complete 'What is Cloud Computing?' lesson", 1));
        quest1.AddObjective(new QuestObjective("answer_questions", "Answer 3 cloud concept questions correctly", 3));
        quest1.reward = new QuestReward(100, new List<string> { "Cloud Novice" }, new List<string> { "CC_DEPLOY_001" });
        quest1.status = QuestStatus.Available;
        allQuests.Add(quest1);

        // Quest 2: AWS Deployment Models
        var quest2 = new Quest(
            "CC_DEPLOY_001",
            "Cloud Deployment Models",
            "Understand Public, Private, and Hybrid cloud deployment models.",
            ExamDomain.CloudConcepts,
            QuestType.Learning
        );
        quest2.storyContext = "A healthcare company needs to decide between public, private, and hybrid cloud. Guide them through the options.";
        quest2.AddObjective(new QuestObjective("learn_models", "Study deployment model types", 1));
        quest2.AddObjective(new QuestObjective("answer_deployment", "Answer 5 deployment model questions", 5));
        quest2.prerequisiteQuestIds.Add("CC_INTRO_001");
        quest2.reward = new QuestReward(150, new List<string> { "Cloud Architect Apprentice" }, new List<string> { "CC_BENEFITS_001" });
        allQuests.Add(quest2);

        // Quest 3: AWS Benefits & Value
        var quest3 = new Quest(
            "CC_BENEFITS_001",
            "The Power of AWS",
            "Discover the key benefits of AWS: economies of scale, elasticity, and agility.",
            ExamDomain.CloudConcepts,
            QuestType.Learning
        );
        quest3.storyContext = "An e-commerce business is struggling with traffic spikes. Show them how AWS can solve their scaling problems.";
        quest3.AddObjective(new QuestObjective("study_benefits", "Learn about AWS benefits", 1));
        quest3.AddObjective(new QuestObjective("solve_scenario", "Complete scaling scenario challenge", 1));
        quest3.prerequisiteQuestIds.Add("CC_DEPLOY_001");
        quest3.reward = new QuestReward(200, new List<string> { "Cloud Evangelist" }, new List<string> { "CC_GLOBAL_001" });
        allQuests.Add(quest3);

        // Quest 4: AWS Global Infrastructure
        var quest4 = new Quest(
            "CC_GLOBAL_001",
            "AWS Around the World",
            "Explore AWS Regions, Availability Zones, and Edge Locations.",
            ExamDomain.CloudConcepts,
            QuestType.HandsOn
        );
        quest4.storyContext = "A global news site needs to serve content quickly worldwide. Design their global AWS infrastructure.";
        quest4.AddObjective(new QuestObjective("explore_regions", "Explore AWS Regions on the map", 1));
        quest4.AddObjective(new QuestObjective("design_global", "Design a multi-region architecture", 1));
        quest4.AddObjective(new QuestObjective("quiz_infrastructure", "Pass infrastructure quiz (80%)", 1));
        quest4.prerequisiteQuestIds.Add("CC_BENEFITS_001");
        quest4.reward = new QuestReward(250, new List<string> { "Global Architect" }, new List<string> { "CC_WELLARCH_001" });
        allQuests.Add(quest4);

        // Quest 5: Well-Architected Framework
        var quest5 = new Quest(
            "CC_WELLARCH_001",
            "Building Great Architectures",
            "Master the 6 pillars of the AWS Well-Architected Framework.",
            ExamDomain.CloudConcepts,
            QuestType.Challenge
        );
        quest5.storyContext = "Review architectures from multiple companies and identify how they align with the Well-Architected Framework.";
        quest5.AddObjective(new QuestObjective("study_pillars", "Study all 6 pillars", 6));
        quest5.AddObjective(new QuestObjective("review_arch", "Review 3 architecture scenarios", 3));
        quest5.difficulty = DifficultyLevel.Medium;
        quest5.prerequisiteQuestIds.Add("CC_GLOBAL_001");
        quest5.reward = new QuestReward(300, new List<string> { "Well-Architected Expert" }, new List<string> { "SEC_INTRO_001" });
        allQuests.Add(quest5);
    }

    private void CreateSecurityQuests()
    {
        // Quest 6: Security Fundamentals
        var quest6 = new Quest(
            "SEC_INTRO_001",
            "AWS Security Basics",
            "Learn the AWS Shared Responsibility Model and core security principles.",
            ExamDomain.SecurityAndCompliance,
            QuestType.Tutorial
        );
        quest6.storyContext = "A financial services company is concerned about security. Teach them the shared responsibility model.";
        quest6.AddObjective(new QuestObjective("learn_shared_resp", "Understand Shared Responsibility Model", 1));
        quest6.AddObjective(new QuestObjective("security_quiz", "Answer 5 security questions", 5));
        quest6.prerequisiteQuestIds.Add("CC_WELLARCH_001");
        quest6.reward = new QuestReward(150, new List<string> { "Security Novice" }, new List<string> { "SEC_IAM_001" });
        allQuests.Add(quest6);

        // Quest 7: IAM Mastery
        var quest7 = new Quest(
            "SEC_IAM_001",
            "Identity and Access Management",
            "Master AWS IAM: users, groups, roles, and policies.",
            ExamDomain.SecurityAndCompliance,
            QuestType.HandsOn
        );
        quest7.storyContext = "A startup has grown to 50 employees. Help them set up proper IAM controls for their AWS environment.";
        quest7.AddObjective(new QuestObjective("learn_iam", "Study IAM concepts", 1));
        quest7.AddObjective(new QuestObjective("create_policy", "Create a custom IAM policy (simulator)", 1));
        quest7.AddObjective(new QuestObjective("iam_quiz", "Pass IAM quiz (85%)", 1));
        quest7.difficulty = DifficultyLevel.Medium;
        quest7.prerequisiteQuestIds.Add("SEC_INTRO_001");
        quest7.reward = new QuestReward(300, new List<string> { "IAM Expert" }, new List<string> { "SEC_ENCRYPT_001" });
        allQuests.Add(quest7);

        // Quest 8: Encryption & Key Management
        var quest8 = new Quest(
            "SEC_ENCRYPT_001",
            "Securing Data with Encryption",
            "Learn AWS KMS and data encryption strategies.",
            ExamDomain.SecurityAndCompliance,
            QuestType.Learning
        );
        quest8.storyContext = "A healthcare provider must encrypt patient data at rest and in transit. Show them how with AWS KMS.";
        quest8.AddObjective(new QuestObjective("learn_kms", "Study AWS KMS", 1));
        quest8.AddObjective(new QuestObjective("encryption_scenarios", "Complete 3 encryption scenarios", 3));
        quest8.difficulty = DifficultyLevel.Medium;
        quest8.prerequisiteQuestIds.Add("SEC_IAM_001");
        quest8.reward = new QuestReward(250, new List<string> { "Encryption Specialist" }, new List<string> { "SEC_COMPLIANCE_001" });
        allQuests.Add(quest8);

        // Quest 9: Compliance & Governance
        var quest9 = new Quest(
            "SEC_COMPLIANCE_001",
            "Meeting Compliance Requirements",
            "Understand AWS compliance programs: HIPAA, PCI DSS, SOC 2, GDPR.",
            ExamDomain.SecurityAndCompliance,
            QuestType.Challenge
        );
        quest9.storyContext = "Multiple clients need compliance certifications. Match each client to the right AWS compliance program.";
        quest9.AddObjective(new QuestObjective("study_compliance", "Study compliance programs", 4));
        quest9.AddObjective(new QuestObjective("match_requirements", "Match 5 clients to compliance needs", 5));
        quest9.difficulty = DifficultyLevel.Hard;
        quest9.prerequisiteQuestIds.Add("SEC_ENCRYPT_001");
        quest9.reward = new QuestReward(350, new List<string> { "Compliance Expert" }, new List<string> { "TECH_INTRO_001" });
        allQuests.Add(quest9);
    }

    private void CreateTechnologyQuests()
    {
        // Quest 10: AWS Services Overview
        var quest10 = new Quest(
            "TECH_INTRO_001",
            "AWS Service Landscape",
            "Get introduced to the major AWS service categories.",
            ExamDomain.Technology,
            QuestType.Tutorial
        );
        quest10.storyContext = "Tour the AWS city and visit each service district to learn what AWS offers.";
        quest10.AddObjective(new QuestObjective("visit_compute", "Visit Compute District", 1));
        quest10.AddObjective(new QuestObjective("visit_storage", "Visit Storage District", 1));
        quest10.AddObjective(new QuestObjective("visit_database", "Visit Database District", 1));
        quest10.AddObjective(new QuestObjective("visit_networking", "Visit Networking District", 1));
        quest10.prerequisiteQuestIds.Add("SEC_COMPLIANCE_001");
        quest10.reward = new QuestReward(200, new List<string> { "Service Explorer" }, new List<string> { "TECH_COMPUTE_001", "TECH_STORAGE_001", "TECH_DATABASE_001" });
        allQuests.Add(quest10);

        // Quest 11: EC2 & Compute
        var quest11 = new Quest(
            "TECH_COMPUTE_001",
            "Compute Power with EC2",
            "Learn EC2, Lambda, and container services.",
            ExamDomain.Technology,
            QuestType.HandsOn
        );
        quest11.storyContext = "A web application needs hosting. Help choose between EC2, Lambda, and containers.";
        quest11.AddObjective(new QuestObjective("learn_ec2", "Study EC2 instance types", 1));
        quest11.AddObjective(new QuestObjective("configure_instance", "Configure an EC2 instance (simulator)", 1));
        quest11.AddObjective(new QuestObjective("compute_quiz", "Pass compute quiz (80%)", 1));
        quest11.difficulty = DifficultyLevel.Medium;
        quest11.prerequisiteQuestIds.Add("TECH_INTRO_001");
        quest11.reward = new QuestReward(300, new List<string> { "Compute Expert" }, new List<string> { "TECH_LAMBDA_001" });
        allQuests.Add(quest11);

        // Quest 12: Serverless with Lambda
        var quest12 = new Quest(
            "TECH_LAMBDA_001",
            "Going Serverless",
            "Master AWS Lambda and serverless architectures.",
            ExamDomain.Technology,
            QuestType.HandsOn
        );
        quest12.storyContext = "Build a serverless image processing pipeline using Lambda, S3, and DynamoDB.";
        quest12.AddObjective(new QuestObjective("learn_lambda", "Study Lambda concepts", 1));
        quest12.AddObjective(new QuestObjective("deploy_function", "Deploy a Lambda function (simulator)", 1));
        quest12.AddObjective(new QuestObjective("serverless_quiz", "Pass serverless quiz (85%)", 1));
        quest12.difficulty = DifficultyLevel.Medium;
        quest12.prerequisiteQuestIds.Add("TECH_COMPUTE_001");
        quest12.reward = new QuestReward(350, new List<string> { "Serverless Architect" }, new List<string> { "TECH_SCALING_001" });
        allQuests.Add(quest12);

        // Quest 13: Storage Services
        var quest13 = new Quest(
            "TECH_STORAGE_001",
            "AWS Storage Solutions",
            "Master S3, EBS, EFS, and storage classes.",
            ExamDomain.Technology,
            QuestType.HandsOn
        );
        quest13.storyContext = "A media company needs to store petabytes of video. Design their storage solution using S3 and Glacier.";
        quest13.AddObjective(new QuestObjective("learn_storage", "Study storage services", 1));
        quest13.AddObjective(new QuestObjective("create_bucket", "Create and configure S3 bucket (simulator)", 1));
        quest13.AddObjective(new QuestObjective("storage_quiz", "Pass storage quiz (80%)", 1));
        quest13.difficulty = DifficultyLevel.Medium;
        quest13.prerequisiteQuestIds.Add("TECH_INTRO_001");
        quest13.reward = new QuestReward(300, new List<string> { "Storage Expert" }, new List<string> { "TECH_SCALING_001" });
        allQuests.Add(quest13);

        // Quest 14: Databases
        var quest14 = new Quest(
            "TECH_DATABASE_001",
            "Database Services",
            "Learn RDS, DynamoDB, Aurora, and Redshift.",
            ExamDomain.Technology,
            QuestType.Learning
        );
        quest14.storyContext = "Different applications need different databases. Match each application to the right database service.";
        quest14.AddObjective(new QuestObjective("learn_databases", "Study database services", 4));
        quest14.AddObjective(new QuestObjective("match_databases", "Match 5 use cases to databases", 5));
        quest14.difficulty = DifficultyLevel.Medium;
        quest14.prerequisiteQuestIds.Add("TECH_INTRO_001");
        quest14.reward = new QuestReward(300, new List<string> { "Database Expert" }, new List<string> { "TECH_SCALING_001" });
        allQuests.Add(quest14);

        // Quest 15: Auto Scaling & Load Balancing
        var quest15 = new Quest(
            "TECH_SCALING_001",
            "Scaling for Success",
            "Master Auto Scaling and Elastic Load Balancing.",
            ExamDomain.Technology,
            QuestType.Challenge
        );
        quest15.storyContext = "An online retailer faces massive traffic spikes during sales. Build an auto-scaling solution.";
        quest15.AddObjective(new QuestObjective("learn_scaling", "Study Auto Scaling", 1));
        quest15.AddObjective(new QuestObjective("configure_asg", "Configure Auto Scaling Group (simulator)", 1));
        quest15.AddObjective(new QuestObjective("scaling_quiz", "Pass scaling quiz (85%)", 1));
        quest15.difficulty = DifficultyLevel.Hard;
        quest15.prerequisiteQuestIds.AddRange(new[] { "TECH_LAMBDA_001", "TECH_STORAGE_001", "TECH_DATABASE_001" });
        quest15.reward = new QuestReward(400, new List<string> { "Scaling Master" }, new List<string> { "BILL_INTRO_001" });
        allQuests.Add(quest15);
    }

    private void CreateBillingQuests()
    {
        // Quest 16: AWS Pricing Models
        var quest16 = new Quest(
            "BILL_INTRO_001",
            "Understanding AWS Pricing",
            "Learn On-Demand, Reserved, Spot, and Savings Plans pricing.",
            ExamDomain.BillingAndPricing,
            QuestType.Tutorial
        );
        quest16.storyContext = "A company is spending too much on AWS. Help them understand pricing models to reduce costs.";
        quest16.AddObjective(new QuestObjective("learn_pricing", "Study pricing models", 4));
        quest16.AddObjective(new QuestObjective("pricing_scenarios", "Solve 3 pricing scenarios", 3));
        quest16.prerequisiteQuestIds.Add("TECH_SCALING_001");
        quest16.reward = new QuestReward(200, new List<string> { "Cost Analyst" }, new List<string> { "BILL_OPTIMIZE_001" });
        allQuests.Add(quest16);

        // Quest 17: Cost Optimization
        var quest17 = new Quest(
            "BILL_OPTIMIZE_001",
            "Cost Optimization Strategies",
            "Master AWS Cost Explorer, Budgets, and Trusted Advisor.",
            ExamDomain.BillingAndPricing,
            QuestType.HandsOn
        );
        quest17.storyContext = "Analyze a company's AWS bill and identify opportunities to reduce costs by 30%.";
        quest17.AddObjective(new QuestObjective("learn_tools", "Study cost management tools", 3));
        quest17.AddObjective(new QuestObjective("analyze_bill", "Analyze sample AWS bill", 1));
        quest17.AddObjective(new QuestObjective("create_budget", "Create a budget alert (simulator)", 1));
        quest17.difficulty = DifficultyLevel.Medium;
        quest17.prerequisiteQuestIds.Add("BILL_INTRO_001");
        quest17.reward = new QuestReward(300, new List<string> { "Cost Optimizer" }, new List<string> { "BILL_ORGANIZATIONS_001" });
        allQuests.Add(quest17);

        // Quest 18: AWS Organizations
        var quest18 = new Quest(
            "BILL_ORGANIZATIONS_001",
            "Managing Multiple Accounts",
            "Learn AWS Organizations and consolidated billing.",
            ExamDomain.BillingAndPricing,
            QuestType.Learning
        );
        quest18.storyContext = "A company with 20 AWS accounts needs centralized management and billing. Set up AWS Organizations.";
        quest18.AddObjective(new QuestObjective("learn_orgs", "Study AWS Organizations", 1));
        quest18.AddObjective(new QuestObjective("org_quiz", "Pass Organizations quiz (80%)", 1));
        quest18.difficulty = DifficultyLevel.Medium;
        quest18.prerequisiteQuestIds.Add("BILL_OPTIMIZE_001");
        quest18.reward = new QuestReward(250, new List<string> { "Multi-Account Manager" }, new List<string> { "ASSESS_PRACTICE_001" });
        allQuests.Add(quest18);
    }

    private void CreateAssessmentQuests()
    {
        // Quest 19: Practice Exam 1
        var quest19 = new Quest(
            "ASSESS_PRACTICE_001",
            "CCP Practice Exam 1",
            "Take your first full-length practice exam (65 questions).",
            ExamDomain.CloudConcepts,
            QuestType.Assessment
        );
        quest19.storyContext = "Test your knowledge with a full practice exam. You need 70% to pass.";
        quest19.AddObjective(new QuestObjective("complete_exam", "Complete 65-question exam", 1));
        quest19.AddObjective(new QuestObjective("pass_exam", "Score 70% or higher", 1));
        quest19.timeLimit = 5400f; // 90 minutes
        quest19.difficulty = DifficultyLevel.Hard;
        quest19.prerequisiteQuestIds.Add("BILL_ORGANIZATIONS_001");
        quest19.reward = new QuestReward(500, new List<string> { "Practice Exam Complete" }, new List<string> { "ASSESS_PRACTICE_002" });
        allQuests.Add(quest19);

        // Quest 20: Final Certification Challenge
        var quest20 = new Quest(
            "ASSESS_FINAL_001",
            "AWS CCP Certification Ready",
            "Final comprehensive exam - prove you're ready for the real test!",
            ExamDomain.CloudConcepts,
            QuestType.Assessment
        );
        quest20.storyContext = "This is it! Your final test before taking the real AWS CCP exam. Good luck!";
        quest20.AddObjective(new QuestObjective("final_exam", "Complete final exam", 1));
        quest20.AddObjective(new QuestObjective("pass_final", "Score 80% or higher", 1));
        quest20.timeLimit = 5400f; // 90 minutes
        quest20.difficulty = DifficultyLevel.Hard;
        quest20.prerequisiteQuestIds.Add("ASSESS_PRACTICE_001");
        quest20.reward = new QuestReward(1000, new List<string> { "CCP Ready!" }, null, "AWS CCP Certification Readiness");
        allQuests.Add(quest20);
    }

    // Quest Management Methods
    public Quest GetQuest(string questId)
    {
        return allQuests.Find(q => q.questId == questId);
    }

    public List<Quest> GetAvailableQuests()
    {
        return allQuests.FindAll(q => q.status == QuestStatus.Available);
    }

    public List<Quest> GetQuestsByDomain(ExamDomain domain)
    {
        return allQuests.FindAll(q => q.primaryDomain == domain);
    }

    public bool StartQuest(string questId)
    {
        var quest = GetQuest(questId);
        if (quest == null || quest.status != QuestStatus.Available)
            return false;

        // Check prerequisites
        foreach (var prereqId in quest.prerequisiteQuestIds)
        {
            var prereq = GetQuest(prereqId);
            if (prereq == null || prereq.status != QuestStatus.Completed)
            {
                Debug.LogWarning($"[QuestManager] Cannot start quest {questId}: prerequisite {prereqId} not completed");
                return false;
            }
        }

        quest.status = QuestStatus.InProgress;
        activeQuests.Add(quest);
        OnQuestStarted?.Invoke(quest);
        SaveProgress();

        Debug.Log($"[QuestManager] Started quest: {quest.questName}");
        return true;
    }

    public void CompleteObjective(string questId, string objectiveId, int amount = 1)
    {
        var quest = GetQuest(questId);
        if (quest == null || quest.status != QuestStatus.InProgress)
            return;

        var objective = quest.objectives.Find(o => o.id == objectiveId);
        if (objective == null)
            return;

        bool wasCompleted = objective.isCompleted;
        quest.CompleteObjective(objectiveId, amount);

        if (!wasCompleted && objective.isCompleted)
        {
            OnObjectiveCompleted?.Invoke(quest, objective);
            Debug.Log($"[QuestManager] Completed objective: {objective.description}");
        }

        // Check if quest is complete
        if (quest.status == QuestStatus.Completed)
        {
            CompleteQuest(questId);
        }

        SaveProgress();
    }

    public void CompleteQuest(string questId)
    {
        var quest = GetQuest(questId);
        if (quest == null)
            return;

        quest.status = QuestStatus.Completed;
        activeQuests.Remove(quest);
        completedQuests.Add(quest);

        // Award rewards
        if (quest.reward != null)
        {
            AwardReward(quest.reward);
        }

        // Unlock next quests
        if (quest.reward?.unlockedQuests != null)
        {
            foreach (var unlockedQuestId in quest.reward.unlockedQuests)
            {
                var unlockedQuest = GetQuest(unlockedQuestId);
                if (unlockedQuest != null && unlockedQuest.status == QuestStatus.Locked)
                {
                    unlockedQuest.status = QuestStatus.Available;
                    Debug.Log($"[QuestManager] Unlocked quest: {unlockedQuest.questName}");
                }
            }
        }

        OnQuestCompleted?.Invoke(quest);
        SaveProgress();

        Debug.Log($"[QuestManager] Completed quest: {quest.questName}");
    }

    private void AwardReward(QuestReward reward)
    {
        // Award XP
        if (PlayerProgress.Instance != null)
        {
            PlayerProgress.Instance.AddExperience(reward.experiencePoints);
        }

        // Award badges
        foreach (var badge in reward.badgesAwarded)
        {
            if (PlayerProgress.Instance != null)
            {
                PlayerProgress.Instance.AwardBadge(badge);
            }
        }

        OnRewardEarned?.Invoke(reward);
        Debug.Log($"[QuestManager] Awarded reward: {reward.experiencePoints} XP, {reward.badgesAwarded.Count} badges");
    }

    public void SaveProgress()
    {
        // Save quest status
        for (int i = 0; i < allQuests.Count; i++)
        {
            var quest = allQuests[i];
            PlayerPrefs.SetInt($"Quest_{quest.questId}_Status", (int)quest.status);

            // Save objective progress
            for (int j = 0; j < quest.objectives.Count; j++)
            {
                var obj = quest.objectives[j];
                PlayerPrefs.SetInt($"Quest_{quest.questId}_Obj_{obj.id}_Count", obj.currentCount);
                PlayerPrefs.SetInt($"Quest_{quest.questId}_Obj_{obj.id}_Complete", obj.isCompleted ? 1 : 0);
            }
        }

        PlayerPrefs.Save();
        Debug.Log("[QuestManager] Progress saved");
    }

    public void LoadProgress()
    {
        foreach (var quest in allQuests)
        {
            if (PlayerPrefs.HasKey($"Quest_{quest.questId}_Status"))
            {
                quest.status = (QuestStatus)PlayerPrefs.GetInt($"Quest_{quest.questId}_Status");

                // Load objective progress
                foreach (var obj in quest.objectives)
                {
                    if (PlayerPrefs.HasKey($"Quest_{quest.questId}_Obj_{obj.id}_Count"))
                    {
                        obj.currentCount = PlayerPrefs.GetInt($"Quest_{quest.questId}_Obj_{obj.id}_Count");
                        obj.isCompleted = PlayerPrefs.GetInt($"Quest_{quest.questId}_Obj_{obj.id}_Complete") == 1;
                    }
                }

                // Rebuild active/completed lists
                if (quest.status == QuestStatus.InProgress && !activeQuests.Contains(quest))
                {
                    activeQuests.Add(quest);
                }
                else if (quest.status == QuestStatus.Completed && !completedQuests.Contains(quest))
                {
                    completedQuests.Add(quest);
                }
            }
        }

        Debug.Log($"[QuestManager] Progress loaded: {activeQuests.Count} active, {completedQuests.Count} completed");
    }

    public int GetTotalQuestCount()
    {
        return allQuests.Count;
    }

    public int GetCompletedQuestCount()
    {
        return completedQuests.Count;
    }

    public float GetCompletionPercentage()
    {
        return allQuests.Count > 0 ? (float)completedQuests.Count / allQuests.Count * 100f : 0f;
    }
}
