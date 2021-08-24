using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PPPhysics : MonoBehaviour
{
    [Range(0, 5)]
    public float G;
    public static List<PPRB> pprbs = new List<PPRB>();
    private Vector2 gravity;
    private PPCollider collisions;

    private void Start()
    {
        collisions = GetComponent<PPCollider>();
    }

    public void Update()
    {
        gravity = Vector2.down * G;
    }

    // Physics Loop
    public void FixedUpdate()
    {
        foreach (PPRB pprb in pprbs)
        {
            pprb.velocity += gravity * Time.fixedDeltaTime;
            Vector2 movement = pprb.velocity * Time.fixedDeltaTime;

            Vector3 newPosition = pprb.transform.position + new Vector3(movement.x, movement.y, 0);

            var collider = pprb.GetComponent<PPCircleCollider>();
            if (collider != null)
            {
                var colliderCenter = collisions.World2Pixel(newPosition) + collider.offset;
                if (collisions.CircleCollision(colliderCenter, collider.radius))
                {
                    // Stop
                    pprb.velocity = Vector3.zero;
                    return;
                }
            }

            pprb.transform.position = newPosition;
            
        }
    }
}
