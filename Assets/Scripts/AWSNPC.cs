using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Interactive NPC that gives AWS quizzes when player talks to them
/// </summary>
public class AWSNPC : MonoBehaviour
{
    [Header("NPC Settings")]
    public string npcName = "AWS Instructor";
    public ExamDomain quizDomain;
    public string dialogueText = "Ready for your AWS quiz?";

    [Header("Interaction")]
    public float interactionRange = 2f;
    public KeyCode interactionKey = KeyCode.E;

    [Header("UI")]
    public GameObject interactionPrompt;
    public Text promptText;

    private Transform player;
    private bool playerInRange = false;
    private bool quizActive = false;

    void Start()
    {
        // Create interaction prompt if not assigned
        if (interactionPrompt == null)
        {
            CreateInteractionPrompt();
        }

        if (interactionPrompt != null)
        {
            interactionPrompt.SetActive(false);
        }
    }

    void Update()
    {
        // Find player if not found
        if (player == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
            {
                player = playerObj.transform;
            }
            return;
        }

        // Check distance to player
        float distance = Vector2.Distance(transform.position, player.position);
        playerInRange = distance <= interactionRange;

        // Show/hide interaction prompt
        if (interactionPrompt != null)
        {
            interactionPrompt.SetActive(playerInRange && !quizActive);
        }

        // Handle interaction
        if (playerInRange && !quizActive && Input.GetKeyDown(interactionKey))
        {
            StartQuiz();
        }
    }

    void CreateInteractionPrompt()
    {
        // Create world-space prompt above NPC
        GameObject promptObj = new GameObject("InteractionPrompt");
        promptObj.transform.SetParent(transform);
        promptObj.transform.localPosition = new Vector3(0, 1.5f, 0);

        Canvas canvas = promptObj.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.WorldSpace;
        canvas.GetComponent<RectTransform>().sizeDelta = new Vector2(200, 50);
        canvas.GetComponent<RectTransform>().localScale = new Vector3(0.01f, 0.01f, 0.01f);

        GameObject textObj = new GameObject("Text");
        textObj.transform.SetParent(promptObj.transform, false);

        promptText = textObj.AddComponent<Text>();
        promptText.font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
        promptText.text = $"[E] Talk to {npcName}";
        promptText.fontSize = 20;
        promptText.alignment = TextAnchor.MiddleCenter;
        promptText.color = Color.white;

        RectTransform textRT = textObj.GetComponent<RectTransform>();
        textRT.anchorMin = Vector2.zero;
        textRT.anchorMax = Vector2.one;
        textRT.sizeDelta = Vector2.zero;

        // Add background
        GameObject bgObj = new GameObject("Background");
        bgObj.transform.SetParent(promptObj.transform, false);
        bgObj.transform.SetAsFirstSibling();

        Image bgImage = bgObj.AddComponent<Image>();
        bgImage.color = new Color(0, 0, 0, 0.7f);

        RectTransform bgRT = bgObj.GetComponent<RectTransform>();
        bgRT.anchorMin = Vector2.zero;
        bgRT.anchorMax = Vector2.one;
        bgRT.sizeDelta = Vector2.zero;

        interactionPrompt = promptObj;
    }

    void StartQuiz()
    {
        quizActive = true;

        // Hide interaction prompt
        if (interactionPrompt != null)
        {
            interactionPrompt.SetActive(false);
        }

        // Disable player movement
        var playerController = player.GetComponent<TopDownController>();
        if (playerController != null)
        {
            playerController.SetCanMove(false);
        }

        // Start quiz through GameManager
        if (GameManager.Instance != null)
        {
            GameManager.Instance.StartLevel(quizDomain);
            GameManager.Instance.OnQuizComplete += OnQuizComplete;
        }
    }

    void OnQuizComplete()
    {
        quizActive = false;

        // Re-enable player movement
        if (player != null)
        {
            var playerController = player.GetComponent<TopDownController>();
            if (playerController != null)
            {
                playerController.SetCanMove(true);
            }
        }

        // Unsubscribe
        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnQuizComplete -= OnQuizComplete;
        }
    }

    void OnDrawGizmos()
    {
        // Draw interaction range in editor
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, interactionRange);
    }

    void OnDestroy()
    {
        // Clean up event subscription
        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnQuizComplete -= OnQuizComplete;
        }
    }
}
