using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Interactive door that requires answering questions
/// </summary>
public class Door : MonoBehaviour
{
    [Header("Door Settings")]
    public int roomIndex = 0;
    public int requiredCorrect = 2;
    public List<CSMQuestion> questions = new List<CSMQuestion>();

    [Header("UI")]
    public GameObject questionPanelPrefab;

    private int currentQuestionIndex = 0;
    private int correctAnswers = 0;
    private bool isUnlocked = false;
    private Material doorMaterial;
    private QuestionPanel questionPanel;

    void Start()
    {
        doorMaterial = GetComponent<Renderer>().material;
        UpdateDoorColor();

        // Find or create question panel
        questionPanel = FindObjectOfType<QuestionPanel>();
    }

    public void Interact()
    {
        if (isUnlocked)
        {
            // Open door and advance
            if (GameManager.Instance != null)
            {
                GameManager.Instance.AdvanceRoom();
            }
            return;
        }

        // Check if player has collected all artifacts
        if (GameManager.Instance != null && !GameManager.Instance.CanAnswerQuestions())
        {
            int needed = GameManager.Instance.requiredCollectibles - GameManager.Instance.collectiblesFound;
            GameManager.Instance.ShowMessage($"Find {needed} more artifact(s) first!");
            return;
        }

        // Show next question
        if (currentQuestionIndex < questions.Count)
        {
            ShowQuestion(questions[currentQuestionIndex]);
        }
    }

    void ShowQuestion(CSMQuestion question)
    {
        if (questionPanel != null)
        {
            questionPanel.ShowQuestion(question, OnAnswerSubmitted);
        }
    }

    void OnAnswerSubmitted(bool correct)
    {
        if (correct)
        {
            correctAnswers++;
        }

        currentQuestionIndex++;

        // Check if unlocked
        if (correctAnswers >= requiredCorrect)
        {
            UnlockDoor();
        }
        else if (currentQuestionIndex >= questions.Count)
        {
            // Failed - reset
            if (GameManager.Instance != null)
            {
                GameManager.Instance.ShowMessage($"Failed! Need {requiredCorrect} correct. Retry in 3s...");
            }
            Invoke("ResetDoor", 3f);
        }
    }

    void UnlockDoor()
    {
        isUnlocked = true;
        UpdateDoorColor();

        if (GameManager.Instance != null)
        {
            GameManager.Instance.ShowMessage("DOOR UNLOCKED! Press E to proceed!");
        }

        // Particle effect
        CreateParticleEffect();
    }

    void ResetDoor()
    {
        currentQuestionIndex = 0;
        correctAnswers = 0;
    }

    void UpdateDoorColor()
    {
        if (doorMaterial != null)
        {
            doorMaterial.color = isUnlocked ? Color.green : new Color(0.55f, 0.27f, 0.07f);
        }
    }

    void CreateParticleEffect()
    {
        // Simple particle effect
        for (int i = 0; i < 20; i++)
        {
            GameObject particle = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            particle.transform.position = transform.position + Random.insideUnitSphere * 2f;
            particle.transform.localScale = Vector3.one * 0.2f;
            particle.GetComponent<Renderer>().material.color = Color.yellow;
            Destroy(particle.GetComponent<Collider>());
            Destroy(particle, 1f);
        }
    }

    void OnMouseOver()
    {
        if (GameManager.Instance != null)
        {
            string message = isUnlocked ? "Press E to enter next room" : "Press E to interact";
            GameManager.Instance.ShowMessage(message, 0.1f);
        }
    }
}

[System.Serializable]
public class CSMQuestion
{
    [TextArea(2, 4)]
    public string question;
    public string[] options = new string[4];
    public int correctIndex;
    [TextArea(2, 4)]
    public string explanation;
}
