using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class PPCollisions : MonoBehaviour
{
    // Unity connected
    public Sprite foreground;

    private SpriteRenderer renderer;
    private float ppu;

    public void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
        renderer.sprite = foreground;
        ppu = foreground.pixelsPerUnit;
    }

    public Vector2Int World2Pixel(Vector3 worldPos)
    {
        var worldOffset = (worldPos - transform.position) * ppu;
        var spriteOffset = new Vector2Int(foreground.texture.width / 2, foreground.texture.height / 2);
        return new Vector2Int((int)(worldOffset.x), (int)(worldOffset.y)) + spriteOffset;
    }

    public Vector3 Pixel2World(Vector2Int pixelPos)
    {
        var pixelOffset = new Vector3((float)pixelPos.x, (float)pixelPos.y, 0) / ppu;
        var spriteOffset = new Vector3(foreground.texture.width / 2, foreground.texture.height / 2, 0) / ppu;
        return transform.position + pixelOffset - spriteOffset;
    }

    public bool PointCollision(Vector2Int pixel)
    {
        Color c = foreground.texture.GetPixel(pixel.x, pixel.y);
        return c.a != 0; // Not transparent
    }

    private float Hypot(float a, float b)
    {
        return Mathf.Sqrt(Mathf.Pow(a, 2) + Mathf.Pow(b, 2));
    }

    /*
     * Check a circle for collision
     * Return a Vector2 in the direction of collision with magnitude of collision depth
     */
    public Vector3? CircleCollision(Vector3 center, float radius)
    {

        Vector2Int pixelCenter = World2Pixel(center);
        int pixelRadius = (int)(radius * ppu);
        float skinnedRadius = radius + 0.5f / ppu;

        List<Vector2Int> pixelCollisions = new List<Vector2Int>();

        for (int height = -pixelRadius; height <= pixelRadius; height++)
        {
            // check middle
            Vector2Int centerPixel = new Vector2Int(pixelCenter.x, pixelCenter.y + height);
            if (PointCollision(centerPixel)) { pixelCollisions.Add(centerPixel); }

            for (int width = 1; Hypot(width, height) < pixelRadius; width++)
            {
                Vector2Int leftPixel = new Vector2Int(pixelCenter.x - width, pixelCenter.y + height);
                Vector2Int rightPixel = new Vector2Int(pixelCenter.x + width, pixelCenter.y + height);
                if (PointCollision(leftPixel)) { pixelCollisions.Add(leftPixel); }
                if (PointCollision(rightPixel)) { pixelCollisions.Add(rightPixel); }
            }
        }

        // No collisions
        if (pixelCollisions.Count == 0) return null;

        Debug.Log(Pixel2World(new Vector2Int(129, 159)));

        // Get deepest collision
        Vector3 deepestCollision = Vector3.zero;
        for (int i = 0; i < pixelCollisions.Count; i++)
        {
            var collisionFromCenter = Pixel2World(pixelCollisions[i]) - center;
            var collisionWithDepth = (skinnedRadius - collisionFromCenter.magnitude) * collisionFromCenter.normalized;
            if (collisionWithDepth.magnitude > deepestCollision.magnitude)
            {
                deepestCollision = collisionWithDepth;
            }
        }

        return deepestCollision;
    }

    private void SetTexture(SpriteRenderer r, Texture2D t)
    {
        // Let's see if we can optimise this later and avoid uneccessary instantiations...
        r.sprite = Sprite.Create(t, new Rect(0, 0, t.width, t.height), Vector2.one * 0.5f);
    }
}
