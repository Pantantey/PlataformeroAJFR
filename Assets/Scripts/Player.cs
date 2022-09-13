using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    private float horizontal;
    public float speed;
    private Rigidbody2D rigidbody2D;


    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal") * speed;
        if (horizontal < 0.0f)
        {
            transform.localScale = new Vector2(-1.0f, 1.0f);
        }else if (horizontal > 0f)
        {
            transform.localScale = new Vector2(1.0f, 1.0f);
        }
    }

    private void FixedUpdate()
    {
        rigidbody2D.velocity = new Vector2(horizontal, rigidbody2D.velocity.y);
    }
}
