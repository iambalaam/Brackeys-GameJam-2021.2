using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class PPCollisions : MonoBehaviour
{
    private SpriteRenderer renderer;

    // Unity connected
    public Sprite foreground;

    public void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
        renderer.sprite = foreground;
    }

    public Vector2Int World2Pixel(Vector3 worldPos)
    {
        float ppu = foreground.pixelsPerUnit;

        var worldOffset = (worldPos - transform.position) * ppu;
        var spriteOffset = new Vector2Int(foreground.texture.width / 2, foreground.texture.height / 2);

        return new Vector2Int((int)(worldOffset.x), (int)(worldOffset.y)) + spriteOffset;
    }

    public bool PointCollision(Vector2Int pixel)
    {
        Color c = foreground.texture.GetPixel(pixel.x, pixel.y);
        return c.a != 0; // Not transparent
    }

    private float Hypot(int a, int b)
    {
        return Mathf.Sqrt(Mathf.Pow(a, 2) + Mathf.Pow(b, 2));
    }

    public Vector2Int? CircleCollision(Vector2Int center, int radius)
    {
        for (int height = radius; height >= -radius; height--)
        {
            int y = center.y - height;

            if (PointCollision(new Vector2Int(center.x, y))) return new Vector2Int(0, -height);


            for (int width = 1; Hypot(height, width) < radius + 0.5f; width++)
            {
                int x1 = center.x + width;
                int x2 = center.x - width;
                if (PointCollision(new Vector2Int(x1, y))) return new Vector2Int(width, -height);
                if (PointCollision(new Vector2Int(x2, y))) return new Vector2Int(-width, -height);
            }
        }
        return null;
    }

    private void SetTexture(SpriteRenderer r, Texture2D t)
    {
        // Let's see if we can optimise this later and avoid uneccessary instantiations...
        r.sprite = Sprite.Create(t, new Rect(0, 0, t.width, t.height), Vector2.one * 0.5f);
    }
}
