using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PPCircleCollider : MonoBehaviour
{
    public Vector2Int offsetPx;
    public float radius;

    [Range (0,1)]
    public float friction;
    [Range (0,1)]
    public float bounciness;

    private float PPU = 32;

    private void OnDrawGizmos()
    {
        Vector3 worldOffset = new Vector3(offsetPx.x, offsetPx.y, 0);
        Gizmos.DrawWireSphere(transform.position + worldOffset / PPU, radius);
    }
}
