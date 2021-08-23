using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class PixelDestroyer : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Texture2D texture;
    public void Start()
    {
        Texture2D initTexture = Resources.Load<Texture2D>("Spaceship_Test");
        texture = Instantiate(initTexture);

        spriteRenderer = GetComponent<SpriteRenderer>();
        SetTexture(spriteRenderer, texture);
    }

    public void Update()
    {
        int x = (int)Input.mousePosition.x;
        int y = (int)Input.mousePosition.y;
        texture.SetPixel(x, y, Color.clear);
        texture.Apply();
        SetTexture(spriteRenderer, texture);
    }

    private void SetTexture(Renderer r, Texture2D t) {
        // Let's see if we can optimise this later and avoid uneccessary instantiations...
        spriteRenderer.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.one * 0.5f);
    }
}
