using UnityEngine;

/// <summary>
/// Applies professional visual themes to rooms based on AWS domain
/// </summary>
public class RoomThemeManager : MonoBehaviour
{
    public ExamDomain domain;

    void Start()
    {
        ApplyTheme();
    }

    void ApplyTheme()
    {
        switch (domain)
        {
            case ExamDomain.CloudConcepts:
                ApplyCloudTheme();
                break;
            case ExamDomain.SecurityAndCompliance:
                ApplySecurityTheme();
                break;
            case ExamDomain.Technology:
                ApplyTechnologyTheme();
                break;
            case ExamDomain.BillingAndPricing:
                ApplyBillingTheme();
                break;
        }
    }

    void ApplyCloudTheme()
    {
        // Sky blue and white cloud theme
        Camera.main.backgroundColor = new Color(0.53f, 0.81f, 0.92f); // Sky blue

        ApplyMaterialToObject("Floor", CreateGradientMaterial(
            new Color(0.9f, 0.95f, 1f), // Light blue-white
            new Color(0.7f, 0.85f, 0.95f)
        ));

        ApplyMaterialToObject("Ceiling", CreateGradientMaterial(
            new Color(0.85f, 0.92f, 0.98f),
            new Color(0.7f, 0.8f, 0.9f)
        ));

        ApplyMaterialToWalls(new Color(0.88f, 0.94f, 0.99f), new Color(0.4f, 0.7f, 0.9f));

        // Add fog for cloud effect
        RenderSettings.fog = true;
        RenderSettings.fogMode = FogMode.Exponential;
        RenderSettings.fogDensity = 0.01f;
        RenderSettings.fogColor = new Color(0.8f, 0.9f, 0.95f);

        // Ambient lighting
        RenderSettings.ambientLight = new Color(0.7f, 0.85f, 0.95f);
    }

    void ApplySecurityTheme()
    {
        // Dark red and black security vault theme
        Camera.main.backgroundColor = new Color(0.1f, 0.05f, 0.05f);

        ApplyMaterialToObject("Floor", CreateMetallicMaterial(
            new Color(0.15f, 0.15f, 0.15f), 0.8f, 0.5f
        ));

        ApplyMaterialToObject("Ceiling", CreateMetallicMaterial(
            new Color(0.1f, 0.1f, 0.1f), 0.7f, 0.4f
        ));

        ApplyMaterialToWalls(new Color(0.2f, 0.05f, 0.05f), new Color(0.8f, 0.1f, 0.1f));

        // Red ambient lighting
        RenderSettings.ambientLight = new Color(0.3f, 0.1f, 0.1f);

        // Add dramatic fog
        RenderSettings.fog = true;
        RenderSettings.fogMode = FogMode.Exponential;
        RenderSettings.fogDensity = 0.015f;
        RenderSettings.fogColor = new Color(0.2f, 0.05f, 0.05f);

        // Add red point lights
        AddAtmosphericLights(new Color(1f, 0.2f, 0.2f), 4);
    }

    void ApplyTechnologyTheme()
    {
        // Neon green and dark cyber theme
        Camera.main.backgroundColor = new Color(0.05f, 0.08f, 0.05f);

        ApplyMaterialToObject("Floor", CreateEmissiveMaterial(
            new Color(0.1f, 0.15f, 0.1f),
            new Color(0f, 0.3f, 0f),
            0.3f
        ));

        ApplyMaterialToObject("Ceiling", CreateMetallicMaterial(
            new Color(0.08f, 0.12f, 0.08f), 0.9f, 0.6f
        ));

        ApplyMaterialToWalls(new Color(0.1f, 0.2f, 0.15f), new Color(0.2f, 0.8f, 0.3f));

        // Green ambient glow
        RenderSettings.ambientLight = new Color(0.2f, 0.4f, 0.2f);

        // Tech fog
        RenderSettings.fog = true;
        RenderSettings.fogMode = FogMode.Exponential;
        RenderSettings.fogDensity = 0.012f;
        RenderSettings.fogColor = new Color(0.1f, 0.2f, 0.1f);

        // Add green neon lights
        AddAtmosphericLights(new Color(0.3f, 1f, 0.3f), 6);
    }

    void ApplyBillingTheme()
    {
        // Gold and luxury theme
        Camera.main.backgroundColor = new Color(0.15f, 0.12f, 0.08f);

        ApplyMaterialToObject("Floor", CreateMetallicMaterial(
            new Color(0.85f, 0.65f, 0.13f), 1.0f, 0.8f
        ));

        ApplyMaterialToObject("Ceiling", CreateMetallicMaterial(
            new Color(0.9f, 0.75f, 0.25f), 0.9f, 0.7f
        ));

        ApplyMaterialToWalls(new Color(0.3f, 0.25f, 0.15f), new Color(1f, 0.84f, 0.2f));

        // Warm golden ambient
        RenderSettings.ambientLight = new Color(0.6f, 0.5f, 0.3f);

        // Subtle warm fog
        RenderSettings.fog = true;
        RenderSettings.fogMode = FogMode.Linear;
        RenderSettings.fogStartDistance = 5f;
        RenderSettings.fogEndDistance = 25f;
        RenderSettings.fogColor = new Color(0.4f, 0.35f, 0.2f);

        // Add warm orange lights
        AddAtmosphericLights(new Color(1f, 0.7f, 0.3f), 3);
    }

    Material CreateGradientMaterial(Color topColor, Color bottomColor)
    {
        Material mat = new Material(Shader.Find("Standard"));
        mat.color = Color.Lerp(topColor, bottomColor, 0.5f);
        mat.SetFloat("_Glossiness", 0.3f);
        mat.SetFloat("_Metallic", 0.1f);
        return mat;
    }

    Material CreateMetallicMaterial(Color baseColor, float metallic, float smoothness)
    {
        Material mat = new Material(Shader.Find("Standard"));
        mat.color = baseColor;
        mat.SetFloat("_Metallic", metallic);
        mat.SetFloat("_Glossiness", smoothness);
        return mat;
    }

    Material CreateEmissiveMaterial(Color baseColor, Color emissionColor, float emissionIntensity)
    {
        Material mat = new Material(Shader.Find("Standard"));
        mat.color = baseColor;
        mat.EnableKeyword("_EMISSION");
        mat.SetColor("_EmissionColor", emissionColor * emissionIntensity);
        mat.SetFloat("_Glossiness", 0.5f);
        return mat;
    }

    void ApplyMaterialToObject(string objectName, Material material)
    {
        GameObject obj = GameObject.Find(objectName);
        if (obj != null)
        {
            Renderer renderer = obj.GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.material = material;
            }
        }
    }

    void ApplyMaterialToWalls(Color baseColor, Color accentColor)
    {
        string[] wallNames = { "Wall_North", "Wall_South", "Wall_East", "Wall_West" };

        foreach (string wallName in wallNames)
        {
            GameObject wall = GameObject.Find(wallName);
            if (wall != null)
            {
                Renderer renderer = wall.GetComponent<Renderer>();
                if (renderer != null)
                {
                    Material mat = CreateMetallicMaterial(baseColor, 0.3f, 0.4f);

                    // Add subtle grid pattern using emission
                    mat.EnableKeyword("_EMISSION");
                    mat.SetColor("_EmissionColor", accentColor * 0.1f);

                    renderer.material = mat;
                }
            }
        }
    }

    void AddAtmosphericLights(Color lightColor, int count)
    {
        float roomRadius = 8f;

        for (int i = 0; i < count; i++)
        {
            float angle = (i / (float)count) * Mathf.PI * 2f;
            Vector3 position = new Vector3(
                Mathf.Cos(angle) * roomRadius,
                2f + Random.Range(-0.5f, 0.5f),
                Mathf.Sin(angle) * roomRadius
            );

            GameObject lightObj = new GameObject($"AtmosphericLight_{i}");
            lightObj.transform.position = position;

            Light light = lightObj.AddComponent<Light>();
            light.type = LightType.Point;
            light.color = lightColor;
            light.intensity = 2f + Random.Range(-0.5f, 0.5f);
            light.range = 8f;
            light.shadows = LightShadows.Soft;

            // Add pulsing effect
            LightPulse pulse = lightObj.AddComponent<LightPulse>();
            pulse.minIntensity = 1.5f;
            pulse.maxIntensity = 2.5f;
            pulse.pulseSpeed = 1f + Random.Range(-0.3f, 0.3f);
        }
    }
}

public class LightPulse : MonoBehaviour
{
    public float minIntensity = 1.5f;
    public float maxIntensity = 2.5f;
    public float pulseSpeed = 1f;

    private Light lightComponent;

    void Start()
    {
        lightComponent = GetComponent<Light>();
    }

    void Update()
    {
        if (lightComponent != null)
        {
            float intensity = Mathf.Lerp(minIntensity, maxIntensity,
                (Mathf.Sin(Time.time * pulseSpeed) + 1f) * 0.5f);
            lightComponent.intensity = intensity;
        }
    }
}
