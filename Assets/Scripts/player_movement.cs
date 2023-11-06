using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_movement : MonoBehaviour
{
    private Rigidbody2D body;
    [SerializeField] private float speed = 10; //SerializedFiled enables changing it from engine
    private Animator anim;
    private bool grounded;

    private void Awake()
    {
        // Take references for rigid body and animator from object
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        float horizontal_input = Input.GetAxis("Horizontal");

        body.velocity = new Vector2(horizontal_input * speed, body.velocity.y);


        // fliping player based on moving left/right
        if(horizontal_input > 0.01f) // is moving right?
        {
            transform.localScale = Vector3.one;
        }
        else if (horizontal_input < - 0.01f)  // is moving left?
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        if (Input.GetKey(KeyCode.Space) && grounded)
        {
            jump();
        }

        // setting animator params
        anim.SetBool("run", horizontal_input != 0);
        anim.SetBool("grounded", grounded);

    }

    private void jump()
    {
        body.velocity = new Vector2(body.velocity.x, speed);
        grounded = false;

        anim.SetTrigger("jump");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
         if(collision.gameObject.tag == "ground")
        {
            grounded = true;
        }
    }

}
