using UnityEngine;
using UnityEngine.UI;
using System;

/// <summary>
/// UI Panel for displaying AWS CCP questions and handling answers
/// </summary>
public class QuestionPanel : MonoBehaviour
{
    [Header("UI Elements")]
    public GameObject panel;
    public Text questionText;
    public Button[] optionButtons;
    public Text feedbackText;
    public Button continueButton;
    public Slider progressBar;
    public Text difficultyText;
    public Text domainTagText;

    private AWSQuestion currentQuestion;
    private Action<bool, float> onAnswerCallback;
    private bool answered = false;
    private float questionStartTime;

    void Start()
    {
        // Hide panel initially
        if (panel != null)
        {
            panel.SetActive(false);
        }

        // Setup continue button
        if (continueButton != null)
        {
            continueButton.onClick.AddListener(HidePanel);
            continueButton.gameObject.SetActive(false);
        }

        // Setup option buttons
        for (int i = 0; i < optionButtons.Length; i++)
        {
            int index = i; // Capture for lambda
            optionButtons[i].onClick.AddListener(() => CheckAnswer(index));
        }
    }

    void Update()
    {
        // Allow SPACE to continue
        if (Input.GetKeyDown(KeyCode.Space) && continueButton != null && continueButton.gameObject.activeSelf)
        {
            HidePanel();
        }
    }

    public void ShowAWSQuestion(AWSQuestion question, Action<bool, float> callback)
    {
        currentQuestion = question;
        onAnswerCallback = callback;
        answered = false;
        questionStartTime = Time.time;

        // Show panel
        if (panel != null)
        {
            panel.SetActive(true);
        }

        // Set question text
        if (questionText != null)
        {
            questionText.text = question.question;
        }

        // Set difficulty indicator
        if (difficultyText != null)
        {
            string diffColor = question.difficulty == DifficultyLevel.Easy ? "#4CAF50" :
                              question.difficulty == DifficultyLevel.Medium ? "#FF9800" : "#F44336";
            difficultyText.text = $"<color={diffColor}>{question.difficulty}</color>";
        }

        // Set domain tag
        if (domainTagText != null && question.tags != null && question.tags.Length > 0)
        {
            domainTagText.text = string.Join(", ", question.tags);
        }

        // Set options
        for (int i = 0; i < optionButtons.Length && i < question.options.Length; i++)
        {
            optionButtons[i].GetComponentInChildren<Text>().text = question.options[i];
            optionButtons[i].interactable = true;
            optionButtons[i].gameObject.SetActive(true);

            // Reset colors
            var colors = optionButtons[i].colors;
            colors.normalColor = new Color(0.17f, 0.24f, 0.31f);
            colors.highlightedColor = new Color(0.2f, 0.29f, 0.37f);
            optionButtons[i].colors = colors;
        }

        // Hide extra buttons if question has fewer options
        for (int i = question.options.Length; i < optionButtons.Length; i++)
        {
            optionButtons[i].gameObject.SetActive(false);
        }

        // Hide feedback
        if (feedbackText != null)
        {
            feedbackText.gameObject.SetActive(false);
        }

        if (continueButton != null)
        {
            continueButton.gameObject.SetActive(false);
        }

        // Unlock cursor
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void CheckAnswer(int selectedIndex)
    {
        if (answered) return;

        answered = true;
        float timeSpent = Time.time - questionStartTime;
        bool correct = selectedIndex == currentQuestion.correctIndex;

        // Disable all buttons and color them
        for (int i = 0; i < optionButtons.Length; i++)
        {
            optionButtons[i].interactable = false;

            var colors = optionButtons[i].colors;

            if (i == currentQuestion.correctIndex)
            {
                // Correct answer - green
                colors.normalColor = new Color(0.15f, 0.68f, 0.38f);
                colors.disabledColor = new Color(0.15f, 0.68f, 0.38f);
            }
            else if (i == selectedIndex && !correct)
            {
                // Wrong answer - red
                colors.normalColor = new Color(0.75f, 0.22f, 0.17f);
                colors.disabledColor = new Color(0.75f, 0.22f, 0.17f);
            }

            optionButtons[i].colors = colors;
        }

        // Show feedback
        if (feedbackText != null)
        {
            string prefix = correct ? "✓ CORRECT!\n\n" : "✗ INCORRECT\n\n";
            string timeInfo = $"\n\nTime: {timeSpent:F1}s";
            feedbackText.text = prefix + currentQuestion.explanation + timeInfo;
            feedbackText.color = correct ? new Color(0.15f, 0.8f, 0.38f) : new Color(0.91f, 0.3f, 0.24f);
            feedbackText.gameObject.SetActive(true);
        }

        // Show continue button
        if (continueButton != null)
        {
            continueButton.gameObject.SetActive(true);
        }

        // Callback with time spent
        if (onAnswerCallback != null)
        {
            onAnswerCallback(correct, timeSpent);
        }
    }

    void HidePanel()
    {
        if (panel != null)
        {
            panel.SetActive(false);
        }

        // Show next question
        if (GameManager.Instance != null)
        {
            GameManager.Instance.ShowNextQuestion();
        }
    }
}
