using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AstronautMovement : MonoBehaviour
{
    float horizontal;
    float vertical;
    public Rigidbody2D rigidBody;
    public float speed;
    public bool hasJumped = false;
    public bool gravityOn = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        if(horizontal > 0)
        {
            Flip(false);
        } else if (horizontal < 0)
        {
            Flip(true);
        }
        if (Input.GetKeyDown(KeyCode.Space) && !hasJumped)
        {
            Jump();
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            if (gravityOn)
            {
                SwitchGravity(false);
            } else 
            {
                SwitchGravity(true);
            }
        }
    }

    void FixedUpdate() 
    {
        rigidBody.AddForce(new Vector2(horizontal, 0.0f) * speed, ForceMode2D.Force);
        if (!gravityOn)
        {
            rigidBody.AddForce(new Vector2(horizontal, vertical) * (speed / 2.5f), ForceMode2D.Force);
        }
    }

    void Jump()
    {
        var jumpVar = 10.0f;
        if (!gravityOn)
        {
            jumpVar = 5.0f;
        }
        hasJumped = true;
        rigidBody.AddForce(Vector2.up * jumpVar, ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        hasJumped = false;
    }

    void Flip(bool left)
    {
        float flipVar = 180.0f;
        if(left)
        {
            flipVar = 0.0f;
        }
        this.transform.rotation = new Quaternion(0.0f, flipVar, 0.0f, 0.0f);
    }

    void SwitchGravity(bool on)
    {
        if (on) 
        {
            rigidBody.gravityScale = 1.0f;
        } else
        {
            rigidBody.gravityScale = 0.0f;
        }
        gravityOn = on;
    }
}
