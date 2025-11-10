using UnityEngine;

/// <summary>
/// Collectible items (AWS badges/tokens) that players must gather before answering questions
/// </summary>
public class AWSCollectible : MonoBehaviour
{
    [Header("Collectible Settings")]
    public ExamDomain domain;
    public string displayName = "AWS Badge";
    public Color glowColor = Color.yellow;

    [Header("Visual Effects")]
    public float rotationSpeed = 50f;
    public float bobHeight = 0.3f;
    public float bobSpeed = 2f;

    private Vector3 startPosition;
    private Light glowLight;
    private bool collected = false;

    void Start()
    {
        startPosition = transform.position;

        // Add glow light
        glowLight = gameObject.AddComponent<Light>();
        glowLight.type = LightType.Point;
        glowLight.color = glowColor;
        glowLight.range = 3f;
        glowLight.intensity = 2f;

        // Set color based on domain
        SetDomainColor();
    }

    void Update()
    {
        if (collected) return;

        // Rotate
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);

        // Bob up and down
        float newY = startPosition.y + Mathf.Sin(Time.time * bobSpeed) * bobHeight;
        transform.position = new Vector3(startPosition.x, newY, startPosition.z);

        // Pulse light
        glowLight.intensity = 2f + Mathf.Sin(Time.time * 3f) * 0.5f;
    }

    void SetDomainColor()
    {
        Color color;
        switch (domain)
        {
            case ExamDomain.CloudConcepts:
                color = new Color(0.2f, 0.5f, 0.8f); // Blue
                break;
            case ExamDomain.SecurityAndCompliance:
                color = new Color(0.8f, 0.3f, 0.3f); // Red
                break;
            case ExamDomain.Technology:
                color = new Color(0.3f, 0.7f, 0.3f); // Green
                break;
            case ExamDomain.BillingAndPricing:
                color = new Color(0.9f, 0.6f, 0.2f); // Orange
                break;
            default:
                color = Color.yellow;
                break;
        }

        glowColor = color;
        if (glowLight != null)
        {
            glowLight.color = color;
        }

        // Apply color to renderer if exists
        var renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.material.SetColor("_EmissionColor", color * 2f);
        }
    }

    public void Collect()
    {
        if (collected) return;

        collected = true;

        // Play collection effect
        if (glowLight != null)
        {
            glowLight.intensity = 5f;
        }

        // Create particle burst
        ParticleEffects.CreateCollectionBurst(transform.position, glowColor);

        // Notify room manager
        var roomManager = FindObjectOfType<RoomManager>();
        if (roomManager != null)
        {
            roomManager.CollectItem(this);
        }

        // Destroy after effect
        Destroy(gameObject, 0.5f);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Collect();
        }
    }
}
