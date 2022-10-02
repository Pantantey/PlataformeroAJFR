using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public float speed = 1f;
    public bool friendBullet = true;
    public float bulletTime = 0.8f;

    private new Rigidbody2D rigidbody2D;
    private Vector2 direction;


    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        StartCoroutine(BulletTime());
    }

    private void FixedUpdate()
    {
        rigidbody2D.velocity = direction * speed;
    }

    public void setDirection(Vector2 dir)
    {
        direction = dir;
    }

    public void DestroyBullet()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy enemy = collision.GetComponent<Enemy>();
        if (enemy != null && friendBullet)
        {
            enemy.Hit();
            DestroyBullet();
        }
    }

    IEnumerator BulletTime()
    {
        yield return new WaitForSeconds(bulletTime);
        Destroy(gameObject);
    }


}
