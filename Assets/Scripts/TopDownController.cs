using UnityEngine;

/// <summary>
/// 2.5D top-down player controller for Stardew Valley-style movement
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
public class TopDownController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;
    public bool canMove = true;

    [Header("Animation")]
    public SpriteRenderer spriteRenderer;
    public Sprite idleDown;
    public Sprite idleUp;
    public Sprite idleLeft;
    public Sprite idleRight;

    private Rigidbody2D rb;
    private Vector2 movement;
    private Vector2 lastMovement;

    // Direction tracking for animations
    private enum Direction { Down, Up, Left, Right }
    private Direction currentDirection = Direction.Down;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0; // No gravity in top-down view
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;

        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        // Set initial sprite
        if (spriteRenderer != null && idleDown != null)
        {
            spriteRenderer.sprite = idleDown;
        }
    }

    void Update()
    {
        if (!canMove) return;

        // Get input
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        // Normalize diagonal movement
        movement = movement.normalized;

        // Update direction and animation
        if (movement != Vector2.zero)
        {
            lastMovement = movement;
            UpdateDirection();
            UpdateAnimation();
        }
    }

    void FixedUpdate()
    {
        if (!canMove) return;

        // Move the player
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    void UpdateDirection()
    {
        // Prioritize vertical movement for clearer animations
        if (Mathf.Abs(movement.y) > Mathf.Abs(movement.x))
        {
            currentDirection = movement.y > 0 ? Direction.Up : Direction.Down;
        }
        else if (movement.x != 0)
        {
            currentDirection = movement.x > 0 ? Direction.Right : Direction.Left;
        }
    }

    void UpdateAnimation()
    {
        if (spriteRenderer == null) return;

        // Simple sprite swapping based on direction
        // In a full game, you'd use an Animator with walking animations
        switch (currentDirection)
        {
            case Direction.Down:
                if (idleDown != null) spriteRenderer.sprite = idleDown;
                break;
            case Direction.Up:
                if (idleUp != null) spriteRenderer.sprite = idleUp;
                break;
            case Direction.Left:
                if (idleLeft != null) spriteRenderer.sprite = idleLeft;
                break;
            case Direction.Right:
                if (idleRight != null) spriteRenderer.sprite = idleRight;
                break;
        }
    }

    public void SetCanMove(bool value)
    {
        canMove = value;
        if (!value)
        {
            rb.velocity = Vector2.zero;
            movement = Vector2.zero;
        }
    }
}
