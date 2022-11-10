using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingEnemy : Enemy
{
    public float speed = 5f;
    public float direction = 1f;
    //public float directionTimeChange = 4f;

    private new Rigidbody2D rigidbody2D;

    private GameObject wallL;
    private GameObject wallR;


    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        wallL = transform.parent.Find("WallL").gameObject;
        wallR = transform.parent.Find("WallR").gameObject;

        //StartCoroutine(DirectionChange());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        rigidbody2D.velocity = new Vector2(direction * speed, rigidbody2D.velocity.y);
    }

    /* *******************si quisiera que funcione x tiempo
    IEnumerator DirectionChange()
    {
        while (true)
        {
            yield return new WaitForSeconds(directionTimeChange);
            Turn();
        }
    }
    */

    private new void OnTriggerEnter2D(Collider2D collider)
    {
        base.OnTriggerEnter2D(collider);
        if (collider.gameObject == wallL || collider.gameObject == wallR)
        {
            Turn();
        }
    }

    private void Turn()
    {
        direction = direction * -1;
        transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
    }

}
