using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PPPhysics : MonoBehaviour
{
    [Range(0, 5)]
    public float G;
    public static List<PPRB> pprbs = new List<PPRB>();
    private Vector2 gravity;

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
            pprb.transform.position += new Vector3(movement.x, movement.y, 0);
        }
    }
}
