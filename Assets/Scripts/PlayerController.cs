using UnityEngine;

/// <summary>
/// First-person player controller with WASD movement and mouse look
/// </summary>
[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float walkSpeed = 5f;
    public float runSpeed = 8f;
    public float jumpHeight = 2f;
    public float gravity = -9.81f;

    [Header("Mouse Look")]
    public float mouseSensitivity = 100f;
    public Transform cameraTransform;

    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    private CharacterController controller;
    private Vector3 velocity;
    private bool isGrounded;
    private float xRotation = 0f;

    void Start()
    {
        controller = GetComponent<CharacterController>();

        // Lock cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Get or create camera if not assigned
        if (cameraTransform == null)
        {
            cameraTransform = Camera.main.transform;
            cameraTransform.SetParent(transform);
            cameraTransform.localPosition = new Vector3(0, 1.6f, 0);
        }

        // Create ground check if not assigned
        if (groundCheck == null)
        {
            GameObject groundCheckObj = new GameObject("GroundCheck");
            groundCheck = groundCheckObj.transform;
            groundCheck.SetParent(transform);
            groundCheck.localPosition = new Vector3(0, -1f, 0);
        }
    }

    void Update()
    {
        HandleMouseLook();
        HandleMovement();
        HandleInteraction();
    }

    void HandleMouseLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }

    void HandleMovement()
    {
        // Ground check
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        // Get input
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        // Calculate movement
        Vector3 move = transform.right * x + transform.forward * z;

        // Apply speed
        float speed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : walkSpeed;
        controller.Move(move * speed * Time.deltaTime);

        // Jump
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        // Apply gravity
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        // ESC to unlock cursor
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        // Click to lock cursor again
        if (Input.GetMouseButtonDown(0) && Cursor.lockState == CursorLockMode.None)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    void HandleInteraction()
    {
        // Raycast from camera
        Ray ray = new Ray(cameraTransform.position, cameraTransform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 5f))
        {
            // Check for collectible
            Collectible collectible = hit.collider.GetComponent<Collectible>();
            if (collectible != null && Input.GetKeyDown(KeyCode.F))
            {
                collectible.Collect();
            }

            // Check for door
            Door door = hit.collider.GetComponent<Door>();
            if (door != null && Input.GetKeyDown(KeyCode.E))
            {
                door.Interact();
            }
        }
    }
}
