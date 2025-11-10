using UnityEngine;

/// <summary>
/// Collectible artifact that player can pick up
/// </summary>
public class Collectible : MonoBehaviour
{
    [Header("Collectible Settings")]
    public string artifactName = "Artifact";
    public float rotationSpeed = 50f;
    public float floatSpeed = 1f;
    public float floatHeight = 0.5f;

    private Vector3 startPos;
    private bool isCollected = false;

    void Start()
    {
        startPos = transform.position;

        // Add glowing material
        var renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.material.EnableKeyword("_EMISSION");
            renderer.material.SetColor("_EmissionColor", Color.yellow * 0.5f);
        }
    }

    void Update()
    {
        if (!isCollected)
        {
            // Rotate
            transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);

            // Float up and down
            float newY = startPos.y + Mathf.Sin(Time.time * floatSpeed) * floatHeight;
            transform.position = new Vector3(transform.position.x, newY, transform.position.z);
        }
    }

    public void Collect()
    {
        if (isCollected) return;

        isCollected = true;

        // Notify game manager
        if (GameManager.Instance != null)
        {
            GameManager.Instance.CollectArtifact();
            GameManager.Instance.ShowMessage($"Collected: {artifactName}!");
        }

        // Animate to player
        StartCoroutine(CollectAnimation());
    }

    System.Collections.IEnumerator CollectAnimation()
    {
        Transform target = Camera.main.transform;
        float duration = 0.5f;
        float elapsed = 0f;
        Vector3 startPosition = transform.position;
        Vector3 startScale = transform.localScale;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;

            // Move to camera
            transform.position = Vector3.Lerp(startPosition, target.position, t);

            // Shrink
            transform.localScale = Vector3.Lerp(startScale, Vector3.zero, t);

            yield return null;
        }

        // Destroy object
        Destroy(gameObject);
    }

    void OnMouseOver()
    {
        if (GameManager.Instance != null && !isCollected)
        {
            GameManager.Instance.ShowMessage($"Press F to collect {artifactName}", 0.1f);
        }
    }
}
