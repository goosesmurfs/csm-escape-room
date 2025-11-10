using UnityEngine;
using UnityEngine.UI;
using System;

/// <summary>
/// UI Panel for displaying questions and handling answers
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

    private CSMQuestion currentQuestion;
    private Action<bool> onAnswerCallback;
    private bool answered = false;

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

    public void ShowQuestion(CSMQuestion question, Action<bool> callback)
    {
        currentQuestion = question;
        onAnswerCallback = callback;
        answered = false;

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

        // Set options
        for (int i = 0; i < optionButtons.Length && i < question.options.Length; i++)
        {
            optionButtons[i].GetComponentInChildren<Text>().text = question.options[i];
            optionButtons[i].interactable = true;

            // Reset colors
            var colors = optionButtons[i].colors;
            colors.normalColor = new Color(0.17f, 0.24f, 0.31f);
            colors.highlightedColor = new Color(0.2f, 0.29f, 0.37f);
            optionButtons[i].colors = colors;
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
        bool correct = selectedIndex == currentQuestion.correctIndex;

        // Update game manager
        if (GameManager.Instance != null)
        {
            GameManager.Instance.AnswerQuestion(correct);
        }

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
            feedbackText.text = prefix + currentQuestion.explanation;
            feedbackText.color = correct ? new Color(0.15f, 0.8f, 0.38f) : new Color(0.91f, 0.3f, 0.24f);
            feedbackText.gameObject.SetActive(true);
        }

        // Show continue button
        if (continueButton != null)
        {
            continueButton.gameObject.SetActive(true);
        }

        // Callback
        if (onAnswerCallback != null)
        {
            onAnswerCallback(correct);
        }
    }

    void HidePanel()
    {
        if (panel != null)
        {
            panel.SetActive(false);
        }

        // Lock cursor again
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
