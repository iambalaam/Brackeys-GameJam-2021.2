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
    public Animator animatorController;
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
            animatorController.SetBool("Running", true);
            Flip(false);
        } else if (horizontal < 0)
        {
            animatorController.SetBool("Running", true);
            Flip(true);
        } else 
        {
            animatorController.SetBool("Running", false);
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
        if (!gravityOn)
        {
            rigidBody.AddForce(new Vector2(horizontal, vertical) * (speed / 25f), ForceMode2D.Impulse);
        } else 
        {
            transform.position += (new Vector3(horizontal, 0.0f, 0.0f) * Time.deltaTime * speed);
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
        float flipVar = 0.0f;
        if(left)
        {
            flipVar = 180.0f;
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
