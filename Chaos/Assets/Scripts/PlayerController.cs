using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float SPEED = 5f;
    private PPRB pprb;

    private void Start()
    {
        pprb = GetComponent<PPRB>();
    }

    void Update()
    {
        float targetXVel = 0;
        if (Input.GetKey(KeyCode.D))
        {
            targetXVel += SPEED;
        }
        if (Input.GetKey(KeyCode.A))
        {
            targetXVel -= SPEED;
        }

        pprb.targetXVelocity = targetXVel;
    }
}
