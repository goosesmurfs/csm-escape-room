using UnityEngine;

/// <summary>
/// Adds professional particle effects to game objects
/// </summary>
public class ParticleEffects : MonoBehaviour
{
    public static void AddCollectibleParticles(GameObject obj, Color color)
    {
        // Create particle system
        ParticleSystem ps = obj.AddComponent<ParticleSystem>();
        var main = ps.main;
        main.startLifetime = 2f;
        main.startSpeed = 0.5f;
        main.startSize = 0.1f;
        main.startColor = color;
        main.maxParticles = 50;
        main.loop = true;

        var emission = ps.emission;
        emission.rateOverTime = 20f;

        var shape = ps.shape;
        shape.shapeType = ParticleSystemShapeType.Sphere;
        shape.radius = 0.3f;

        var colorOverLifetime = ps.colorOverLifetime;
        colorOverLifetime.enabled = true;
        Gradient grad = new Gradient();
        grad.SetKeys(
            new GradientColorKey[] {
                new GradientColorKey(color, 0.0f),
                new GradientColorKey(color, 1.0f)
            },
            new GradientAlphaKey[] {
                new GradientAlphaKey(1.0f, 0.0f),
                new GradientAlphaKey(0.0f, 1.0f)
            }
        );
        colorOverLifetime.color = new ParticleSystem.MinMaxGradient(grad);

        var sizeOverLifetime = ps.sizeOverLifetime;
        sizeOverLifetime.enabled = true;
        AnimationCurve curve = new AnimationCurve();
        curve.AddKey(0.0f, 1.0f);
        curve.AddKey(1.0f, 0.0f);
        sizeOverLifetime.size = new ParticleSystem.MinMaxCurve(1f, curve);

        // Add glow trail
        var trails = ps.trails;
        trails.enabled = true;
        trails.lifetime = 0.5f;
        trails.minVertexDistance = 0.1f;
        trails.ratio = 0.5f;
    }

    public static void AddDoorParticles(GameObject doorObj, Color color)
    {
        GameObject particlesObj = new GameObject("DoorParticles");
        particlesObj.transform.SetParent(doorObj.transform);
        particlesObj.transform.localPosition = Vector3.zero;

        ParticleSystem ps = particlesObj.AddComponent<ParticleSystem>();
        var main = ps.main;
        main.startLifetime = 3f;
        main.startSpeed = 1f;
        main.startSize = 0.2f;
        main.startColor = color;
        main.maxParticles = 100;
        main.loop = true;

        var emission = ps.emission;
        emission.rateOverTime = 15f;

        var shape = ps.shape;
        shape.shapeType = ParticleSystemShapeType.Box;
        shape.scale = new Vector3(2f, 3f, 0.2f);

        var velocityOverLifetime = ps.velocityOverLifetime;
        velocityOverLifetime.enabled = true;
        velocityOverLifetime.y = new ParticleSystem.MinMaxCurve(0.5f);

        var colorOverLifetime = ps.colorOverLifetime;
        colorOverLifetime.enabled = true;
        Gradient grad = new Gradient();
        grad.SetKeys(
            new GradientColorKey[] {
                new GradientColorKey(color, 0.0f),
                new GradientColorKey(color * 0.5f, 1.0f)
            },
            new GradientAlphaKey[] {
                new GradientAlphaKey(0.8f, 0.0f),
                new GradientAlphaKey(0.0f, 1.0f)
            }
        );
        colorOverLifetime.color = new ParticleSystem.MinMaxGradient(grad);
    }

    public static void CreateCollectionBurst(Vector3 position, Color color)
    {
        GameObject burstObj = new GameObject("CollectionBurst");
        burstObj.transform.position = position;

        ParticleSystem ps = burstObj.AddComponent<ParticleSystem>();
        var main = ps.main;
        main.startLifetime = 1f;
        main.startSpeed = 3f;
        main.startSize = 0.3f;
        main.startColor = color;
        main.maxParticles = 50;
        main.loop = false;

        var emission = ps.emission;
        emission.enabled = false;

        var burst = new ParticleSystem.Burst(0f, 50);
        emission.SetBursts(new ParticleSystem.Burst[] { burst });

        var shape = ps.shape;
        shape.shapeType = ParticleSystemShapeType.Sphere;
        shape.radius = 0.1f;

        Destroy(burstObj, 2f);
    }
}
