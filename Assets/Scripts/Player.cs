using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int life = 2;
    public float speed;
    public float jumpForce;
    public float cooldownTime = 0.5f;
    public GameObject bulletPrefab;
    public GameObject lifesPanel;


    public float horizontal;
    private bool isGrounded;
    private bool isInCooldown;
    private Vector2 initialPosition;
    private float lastShoot;
    private GameObject destinyWarp;

    private Rigidbody2D bulletRigidbody;
    private new Rigidbody2D rigidbody2D;
    private Animator animator;
    public Vector2 respawnPoint;



    // Start is called before the first frame update
    void Start()
    {
        bulletRigidbody = GetComponent<Rigidbody2D>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        initialPosition = transform.position;
        respawnPoint = initialPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale > 0)
        {
            /*if (isGrounded)
        {
            respawnPoint = transform.position;
        }*/

            horizontal = Input.GetAxis("Horizontal") * speed;
            if (horizontal < 0.0f)
            {
                transform.localScale = new Vector2(-1.0f, 1.0f);
            }
            else if (horizontal > 0f)
            {
                transform.localScale = new Vector2(1.0f, 1.0f);
            }

            animator.SetBool("isRunning", horizontal != 0.0f);
            animator.SetBool("isJumping", !isGrounded);


            if (Input.GetKeyDown(KeyCode.Space) && Time.time > lastShoot + 0.5f)
            {
                shoot();
                lastShoot = Time.time;
            }

            if (Input.GetKeyDown(KeyCode.W) && isGrounded)
            {
                jump();
            }

            if (Input.GetKeyDown(KeyCode.M) && destinyWarp)
            {
                transform.position = destinyWarp.transform.position;
            }

            deathOnFall();
        }
        

    }

    public void Death()
    {
        transform.position = initialPosition;
        respawnPoint = initialPosition;
        if (life <= 0)
        {
            life = 2;
            for (int i = 0; i< lifesPanel.transform.childCount; i++)
            {
                lifesPanel.transform.GetChild(i).gameObject.SetActive(true);
            }
        }
    }

    public void Hit(float knockback, GameObject enemy)
    {
        if (!isInCooldown)
        {
            StartCoroutine(Cooldown());
            if (life > 0)
            {
                lifesPanel.transform.GetChild(life).gameObject.SetActive(false);
                life -= 1;
                if (enemy)
                {
                    Vector2 difference = (transform.position - enemy.transform.position);
                    float knockbackDirection = difference.x >= 0 ? 1 : -1;   //si force.x es mayor a 0 entonces =1 y sino es = -1
                    rigidbody2D.velocity = new Vector2(knockbackDirection * knockback, knockback/2);
                }
            }
            else
            {
                Death();
            }
        }
        
    }

    IEnumerator Cooldown()
    {
        isInCooldown = true;
        yield return new WaitForSeconds(cooldownTime);
        isInCooldown = false;
    }

    private void deathOnFall()
    {
        if (transform.position.y < -10)
        {
            transform.position = respawnPoint;
            Hit(0, null);
        }
    }

    public void shoot()
    {
        Vector3 direction;
        if (transform.localScale.x > 0) direction = Vector3.right;
        else direction = Vector3.left;
        GameObject bullet = Instantiate(bulletPrefab, transform.position +direction *0.1f, Quaternion.identity);
        bullet.GetComponent<Bullet>().setDirection(direction);
    }

    private void jump()
    {
        rigidbody2D.AddForce(Vector2.up * jumpForce);
    }

    private void FixedUpdate()
    {
        //caminar si no está stun
        if (!isInCooldown)
        {
            bulletRigidbody.velocity = new Vector2(horizontal, bulletRigidbody.velocity.y);
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.name == "Tilemap") isGrounded = true;

        if (collider.name == "PointA" || collider.name == "PointB")
        {
            GameObject warp = collider.transform.parent.gameObject;
            if (collider.name == "PointA")
            {
                destinyWarp = warp.transform.Find("PointB").gameObject;

            }
            else
            {
                destinyWarp = warp.transform.Find("PointA").gameObject;
            }
        }
        
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if(collider.name == "Tilemap") isGrounded = false;
        if (collider.name == "PointA" || collider.name == "PointB")
        {
            destinyWarp = null;
        }
    }

}
