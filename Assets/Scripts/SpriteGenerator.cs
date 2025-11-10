using UnityEngine;

/// <summary>
/// Utility class to generate simple colored sprites programmatically
/// </summary>
public static class SpriteGenerator
{
    /// <summary>
    /// Creates a simple colored square sprite
    /// </summary>
    public static Sprite CreateSquareSprite(Color color, int size = 32)
    {
        Texture2D texture = new Texture2D(size, size);

        // Fill with color
        for (int y = 0; y < size; y++)
        {
            for (int x = 0; x < size; x++)
            {
                texture.SetPixel(x, y, color);
            }
        }

        texture.Apply();

        return Sprite.Create(texture, new Rect(0, 0, size, size), new Vector2(0.5f, 0.5f), size);
    }

    /// <summary>
    /// Creates a circle sprite (for collectibles)
    /// </summary>
    public static Sprite CreateCircleSprite(Color color, int size = 32)
    {
        Texture2D texture = new Texture2D(size, size);
        int center = size / 2;
        int radius = size / 2;

        // Fill with transparent
        for (int y = 0; y < size; y++)
        {
            for (int x = 0; x < size; x++)
            {
                float distance = Vector2.Distance(new Vector2(x, y), new Vector2(center, center));
                if (distance < radius)
                {
                    texture.SetPixel(x, y, color);
                }
                else
                {
                    texture.SetPixel(x, y, Color.clear);
                }
            }
        }

        texture.Apply();

        return Sprite.Create(texture, new Rect(0, 0, size, size), new Vector2(0.5f, 0.5f), size);
    }

    /// <summary>
    /// Creates a character sprite (player/NPC)
    /// Simple rounded rectangle
    /// </summary>
    public static Sprite CreateCharacterSprite(Color color, int width = 32, int height = 48)
    {
        Texture2D texture = new Texture2D(width, height);

        // Fill with transparent
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                texture.SetPixel(x, y, Color.clear);
            }
        }

        // Draw rounded rectangle (simple character shape)
        for (int y = 4; y < height - 4; y++)
        {
            for (int x = 4; x < width - 4; x++)
            {
                texture.SetPixel(x, y, color);
            }
        }

        // Add simple face (2 eyes, 1 mouth)
        int faceY = height * 2 / 3;
        int eyeSpacing = width / 3;

        // Eyes
        texture.SetPixel(eyeSpacing, faceY, Color.black);
        texture.SetPixel(width - eyeSpacing, faceY, Color.black);

        // Mouth
        int mouthY = faceY - 6;
        for (int x = eyeSpacing; x < width - eyeSpacing; x++)
        {
            texture.SetPixel(x, mouthY, Color.black);
        }

        texture.Apply();

        return Sprite.Create(texture, new Rect(0, 0, width, height), new Vector2(0.5f, 0.5f), 32);
    }

    /// <summary>
    /// Creates a grass/ground tile sprite
    /// </summary>
    public static Sprite CreateGroundTile(Color baseColor, int size = 32)
    {
        Texture2D texture = new Texture2D(size, size);

        // Fill with base color with slight variation
        for (int y = 0; y < size; y++)
        {
            for (int x = 0; x < size; x++)
            {
                float variation = Random.Range(-0.1f, 0.1f);
                Color pixelColor = new Color(
                    Mathf.Clamp01(baseColor.r + variation),
                    Mathf.Clamp01(baseColor.g + variation),
                    Mathf.Clamp01(baseColor.b + variation)
                );
                texture.SetPixel(x, y, pixelColor);
            }
        }

        texture.Apply();

        return Sprite.Create(texture, new Rect(0, 0, size, size), new Vector2(0.5f, 0.5f), 32);
    }
}
