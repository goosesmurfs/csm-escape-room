using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Interactive door that presents AWS questions when player has collected all items
/// </summary>
public class AWSDoor : MonoBehaviour
{
    [Header("Door Settings")]
    public ExamDomain domain;
    public int questionsRequired = 5;
    public Transform nextRoomSpawnPoint;

    [Header("Visual Feedback")]
    public Light doorLight;
    public Color lockedColor = Color.red;
    public Color unlockedColor = Color.yellow;
    public Color completedColor = Color.green;

    [Header("UI")]
    public Text interactionPrompt;
    public float interactionDistance = 3f;

    private bool canInteract = false;
    private bool playerNearby = false;
    private bool questionsCompleted = false;
    private Transform player;

    void Start()
    {
        // Setup door light
        if (doorLight == null)
        {
            doorLight = gameObject.AddComponent<Light>();
            doorLight.type = LightType.Spot;
            doorLight.range = 5f;
            doorLight.intensity = 3f;
        }

        doorLight.color = lockedColor;

        if (interactionPrompt != null)
        {
            interactionPrompt.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        // Find player
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player")?.transform;
            return;
        }

        // Check distance to player
        float distance = Vector3.Distance(transform.position, player.position);
        playerNearby = distance <= interactionDistance;

        // Show/hide prompt
        if (interactionPrompt != null)
        {
            bool shouldShow = playerNearby && canInteract && !questionsCompleted;
            interactionPrompt.gameObject.SetActive(shouldShow);

            if (shouldShow)
            {
                interactionPrompt.text = "Press E to take AWS Quiz";
            }
        }

        // Handle interaction
        if (playerNearby && canInteract && !questionsCompleted && Input.GetKeyDown(KeyCode.E))
        {
            StartQuiz();
        }
    }

    public void EnableInteraction()
    {
        canInteract = true;
        doorLight.color = unlockedColor;
        Debug.Log("Door is now ready for quiz!");
    }

    void StartQuiz()
    {
        // Start AWS quiz through GameManager
        if (GameManager.Instance != null)
        {
            GameManager.Instance.StartLevel(domain);
        }
    }

    public void CompleteQuiz()
    {
        questionsCompleted = true;
        doorLight.color = completedColor;

        // Open door (move player to next room)
        if (nextRoomSpawnPoint != null && player != null)
        {
            Invoke("MovePlayerToNextRoom", 2f);
        }
    }

    void MovePlayerToNextRoom()
    {
        if (player != null && nextRoomSpawnPoint != null)
        {
            var characterController = player.GetComponent<CharacterController>();
            if (characterController != null)
            {
                characterController.enabled = false;
                player.position = nextRoomSpawnPoint.position;
                player.rotation = nextRoomSpawnPoint.rotation;
                characterController.enabled = true;
            }
            else
            {
                player.position = nextRoomSpawnPoint.position;
                player.rotation = nextRoomSpawnPoint.rotation;
            }
        }
    }

    void OnDrawGizmos()
    {
        // Draw interaction range
        Gizmos.color = canInteract ? Color.green : Color.red;
        Gizmos.DrawWireSphere(transform.position, interactionDistance);
    }
}
