using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PPPhysics : MonoBehaviour
{
    [Range(0, 5)]
    public float G;
    public static List<PPRB> pprbs = new List<PPRB>();
    private Vector2 gravity;
    private PPCollisions collisions;
    private int PPU = 32;

    private void Start()
    {
        collisions = GetComponent<PPCollisions>();
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
                var collision = collisions.CircleCollision(colliderCenter, collider.radius);
                if (collision.HasValue)
                {

                    // Handle collision
                    Vector3 v3Movement = new Vector3(movement.x, movement.y, 0);
                    Vector3 normal = new Vector3(collision.Value.x, collision.Value.y, 0).normalized;
                    Vector3 tangent = Vector3.Cross(normal, Vector3.forward);

                    Vector3 bounce = -normal * collider.bounciness * Vector3.Dot(v3Movement, normal);
                    Vector3 slide = tangent * (1 - collider.friction) * Vector3.Dot(v3Movement, tangent);

                    pprb.velocity = (bounce + slide) * PPU;

                    break;
                }
            }

            pprb.transform.position = newPosition;
            
        }
    }
}
