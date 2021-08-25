using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PPCircleCollider : MonoBehaviour
{
    public Vector2Int offset;
    public int radius;

    [Range (0,1)]
    public float friction;
    [Range (0,1)]
    public float bounciness;

    private float PPU = 32;

    private void OnDrawGizmos()
    {
        Vector3 worldOffset = new Vector3(offset.x, offset.y, 0);
        Gizmos.DrawWireSphere(transform.position + worldOffset / PPU, radius/ PPU);
    }
}
