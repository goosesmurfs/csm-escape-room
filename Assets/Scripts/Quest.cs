using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum QuestType
{
    Tutorial,           // Basic introduction
    Learning,          // Educational content
    Challenge,         // Test knowledge
    HandsOn,           // Practical AWS service simulation
    Assessment,        // Final evaluation
    Story              // Story-driven scenario
}

[System.Serializable]
public enum QuestStatus
{
    Locked,            // Not yet available
    Available,         // Can be started
    InProgress,        // Currently active
    Completed,         // Successfully finished
    Failed             // Failed (can retry)
}

[System.Serializable]
public class QuestObjective
{
    public string id;
    public string description;
    public bool isCompleted;
    public int targetCount;        // How many times to complete
    public int currentCount;       // Current progress

    public QuestObjective(string id, string description, int targetCount = 1)
    {
        this.id = id;
        this.description = description;
        this.targetCount = targetCount;
        this.currentCount = 0;
        this.isCompleted = false;
    }

    public void UpdateProgress(int amount = 1)
    {
        currentCount += amount;
        if (currentCount >= targetCount)
        {
            currentCount = targetCount;
            isCompleted = true;
        }
    }

    public float GetProgressPercentage()
    {
        return targetCount > 0 ? (float)currentCount / targetCount * 100f : 0f;
    }
}

[System.Serializable]
public class QuestReward
{
    public int experiencePoints;
    public List<string> badgesAwarded;
    public List<string> unlockedQuests;
    public string certificateAwarded;

    public QuestReward(int xp, List<string> badges = null, List<string> unlocks = null, string certificate = null)
    {
        experiencePoints = xp;
        badgesAwarded = badges ?? new List<string>();
        unlockedQuests = unlocks ?? new List<string>();
        certificateAwarded = certificate;
    }
}

[System.Serializable]
public class Quest
{
    public string questId;
    public string questName;
    public string questDescription;
    public string storyContext;        // Story/scenario background
    public QuestType questType;
    public QuestStatus status;
    public ExamDomain primaryDomain;
    public List<ExamDomain> secondaryDomains;

    public List<QuestObjective> objectives;
    public QuestReward reward;

    public List<string> prerequisiteQuestIds;  // Must complete these first
    public int requiredLevel;                  // Player level requirement
    public DifficultyLevel difficulty;

    public float timeLimit;                    // 0 = no time limit
    public float timeSpent;

    public string npcGiverId;                  // Which NPC gives this quest
    public Vector3 questLocation;              // Where to find this quest

    public Quest(string id, string name, string description, ExamDomain domain, QuestType type = QuestType.Learning)
    {
        questId = id;
        questName = name;
        questDescription = description;
        questType = type;
        status = QuestStatus.Locked;
        primaryDomain = domain;
        secondaryDomains = new List<ExamDomain>();
        objectives = new List<QuestObjective>();
        prerequisiteQuestIds = new List<string>();
        requiredLevel = 1;
        difficulty = DifficultyLevel.Easy;
        timeLimit = 0f;
        timeSpent = 0f;
    }

    public void AddObjective(QuestObjective objective)
    {
        objectives.Add(objective);
    }

    public bool AreAllObjectivesComplete()
    {
        foreach (var objective in objectives)
        {
            if (!objective.isCompleted)
                return false;
        }
        return objectives.Count > 0;
    }

    public float GetOverallProgress()
    {
        if (objectives.Count == 0) return 0f;

        float totalProgress = 0f;
        foreach (var objective in objectives)
        {
            totalProgress += objective.GetProgressPercentage();
        }
        return totalProgress / objectives.Count;
    }

    public void CompleteObjective(string objectiveId, int amount = 1)
    {
        var objective = objectives.Find(o => o.id == objectiveId);
        if (objective != null)
        {
            objective.UpdateProgress(amount);
        }

        // Check if all objectives are complete
        if (AreAllObjectivesComplete())
        {
            status = QuestStatus.Completed;
        }
    }
}
