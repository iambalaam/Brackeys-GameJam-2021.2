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
    private float skin = 0.00005f;

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
            Vector2 velocity = pprb.velocity;
            velocity += gravity * Time.fixedDeltaTime;

            // Check if pprb has agency
            if (pprb.targetXVelocity != 0) { velocity.x = pprb.targetXVelocity; }

            Vector2 movement = velocity * Time.fixedDeltaTime;
            Vector3 newPosition = pprb.transform.position + new Vector3(movement.x, movement.y, 0);

            var collider = pprb.GetComponent<PPCircleCollider>();
            if (collider != null)
            {
                var maybeCollision = collisions.CircleCollision(newPosition, collider.radius);
                if (maybeCollision.HasValue)
                {
                    var collision = maybeCollision.Value;

                    // Normal and tangent for collision force
                    Vector3 v3Movement = new Vector3(movement.x, movement.y, 0);
                    Vector3 normal = collision.normalized;
                    Vector3 tangent = Vector3.Cross(normal, Vector3.back);

                    /*
                    // Bounciness and friction
                    Vector3 bounce = -normal * collider.bounciness * Mathf.Abs(Vector3.Dot(v3Movement, normal));
                    Vector3 slide = tangent * (1 - collider.friction) * Vector3.Dot(v3Movement, tangent);
                    pprb.velocity = (bounce + slide) * PPU;
                    */

                    // Apply impulse against collision and recalculate position
                    var impulse = new Vector2(collision.x, collision.y) / Time.fixedDeltaTime;
                    velocity -= impulse * (1 + collider.bounciness);
                    velocity *= 0.9f;
                    movement = velocity * Time.fixedDeltaTime;
                    newPosition = pprb.transform.position + new Vector3(movement.x, movement.y, 0);
                }
            }

            // Set position and velocity
            pprb.velocity = velocity;
            pprb.transform.position = newPosition;

        }
    }
}