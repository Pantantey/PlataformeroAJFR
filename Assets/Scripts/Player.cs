using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    private float horizontal;
    private bool isGrounded;
    public float speed;
    public float jumpForce;
    private Rigidbody2D rigidbody2D;

    private Animator animator;


    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal") * speed;
        if (horizontal < 0.0f)
        {
            transform.localScale = new Vector2(-20.0f, 20.0f);
        }else if (horizontal > 0f)
        {
            transform.localScale = new Vector2(20.0f, 20.0f);
        }

        animator.SetBool("isRunning", horizontal != 0.0f);

        Debug.DrawRay(transform.position, Vector2.down * 0.9f, Color.blue);
        if (Physics2D.Raycast(transform.position, Vector2.down, 0.9f))
        {
            isGrounded = true;
        }
        else isGrounded = false;

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            jump();
        }
    }

    private void jump()
    {
        rigidbody2D.AddForce(Vector2.up * jumpForce);
    }

    private void FixedUpdate()
    {
        rigidbody2D.velocity = new Vector2(horizontal, rigidbody2D.velocity.y);
    }
}
