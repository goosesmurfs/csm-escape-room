using UnityEngine;

/// <summary>
/// 2D collectible AWS badges for top-down game
/// </summary>
[RequireComponent(typeof(CircleCollider2D))]
public class AWSCollectible2D : MonoBehaviour
{
    [Header("Collectible Settings")]
    public ExamDomain domain;
    public string displayName = "AWS Badge";

    [Header("Visual Effects")]
    public SpriteRenderer spriteRenderer;
    public Color glowColor = Color.yellow;
    public float bobHeight = 0.2f;
    public float bobSpeed = 2f;
    public float rotationSpeed = 50f;

    private Vector3 startPosition;
    private bool collected = false;
    private ParticleSystem particles;

    void Start()
    {
        startPosition = transform.position;

        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        // Set up collider as trigger
        CircleCollider2D collider = GetComponent<CircleCollider2D>();
        collider.isTrigger = true;
        collider.radius = 0.5f;

        // Set color based on domain
        SetDomainColor();

        // Add simple particle effect
        CreateParticles();
    }

    void Update()
    {
        if (collected) return;

        // Bob up and down
        float newY = startPosition.y + Mathf.Sin(Time.time * bobSpeed) * bobHeight;
        transform.position = new Vector3(startPosition.x, newY, startPosition.z);

        // Rotate for visual interest
        transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);

        // Pulse effect
        if (spriteRenderer != null)
        {
            float pulse = 0.8f + Mathf.Sin(Time.time * 3f) * 0.2f;
            spriteRenderer.color = glowColor * pulse;
        }
    }

    void SetDomainColor()
    {
        Color color;
        switch (domain)
        {
            case ExamDomain.CloudConcepts:
                color = new Color(0.2f, 0.6f, 1f); // Sky blue
                displayName = "Cloud Concepts Badge";
                break;
            case ExamDomain.SecurityAndCompliance:
                color = new Color(1f, 0.3f, 0.3f); // Red
                displayName = "Security Badge";
                break;
            case ExamDomain.Technology:
                color = new Color(0.3f, 1f, 0.3f); // Green
                displayName = "Technology Badge";
                break;
            case ExamDomain.BillingAndPricing:
                color = new Color(1f, 0.8f, 0.2f); // Gold
                displayName = "Billing Badge";
                break;
            default:
                color = Color.yellow;
                break;
        }

        glowColor = color;
        if (spriteRenderer != null)
        {
            spriteRenderer.color = color;
        }
    }

    void CreateParticles()
    {
        GameObject particlesObj = new GameObject("Particles");
        particlesObj.transform.SetParent(transform);
        particlesObj.transform.localPosition = Vector3.zero;

        particles = particlesObj.AddComponent<ParticleSystem>();
        var main = particles.main;
        main.startLifetime = 1f;
        main.startSpeed = 0.5f;
        main.startSize = 0.1f;
        main.startColor = glowColor;
        main.maxParticles = 20;
        main.loop = true;

        var emission = particles.emission;
        emission.rateOverTime = 10f;

        var shape = particles.shape;
        shape.shapeType = ParticleSystemShapeType.Circle;
        shape.radius = 0.3f;

        var colorOverLifetime = particles.colorOverLifetime;
        colorOverLifetime.enabled = true;
        Gradient grad = new Gradient();
        grad.SetKeys(
            new GradientColorKey[] {
                new GradientColorKey(glowColor, 0.0f),
                new GradientColorKey(glowColor, 1.0f)
            },
            new GradientAlphaKey[] {
                new GradientAlphaKey(1.0f, 0.0f),
                new GradientAlphaKey(0.0f, 1.0f)
            }
        );
        colorOverLifetime.color = new ParticleSystem.MinMaxGradient(grad);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (collected) return;

        if (other.CompareTag("Player"))
        {
            Collect();
        }
    }

    void Collect()
    {
        collected = true;

        // Play collection sound (if you add audio later)
        // AudioManager.PlaySound("collect");

        // Create burst effect
        CreateCollectionBurst();

        // Notify world manager
        var worldManager = FindObjectOfType<WorldManager>();
        if (worldManager != null)
        {
            worldManager.CollectBadge(this);
        }

        // Destroy after effect
        Destroy(gameObject, 0.5f);
    }

    void CreateCollectionBurst()
    {
        GameObject burstObj = new GameObject("CollectionBurst");
        burstObj.transform.position = transform.position;

        ParticleSystem ps = burstObj.AddComponent<ParticleSystem>();
        var main = ps.main;
        main.startLifetime = 0.8f;
        main.startSpeed = 3f;
        main.startSize = 0.2f;
        main.startColor = glowColor;
        main.maxParticles = 30;
        main.loop = false;

        var emission = ps.emission;
        emission.enabled = false;

        var burst = new ParticleSystem.Burst(0f, 30);
        emission.SetBursts(new ParticleSystem.Burst[] { burst });

        var shape = ps.shape;
        shape.shapeType = ParticleSystemShapeType.Circle;
        shape.radius = 0.1f;

        Destroy(burstObj, 2f);
    }
}
