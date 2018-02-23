using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour {

    public float speedX;
    public float jumpSpeedY;
    public GameObject firePrefab;
    public Transform fireSpawn;
    public int score = 0;

    private Animator anim;
    private Rigidbody2D rb;

    bool facingRight, jumping, walking;
    float speed;

    // Use this for initialization
    void Start() {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        facingRight = true;
    }

    // Update is called once per frame
    void Update() {
        //flip if necessary
        Flip();

        //jump and fall
        HandleJumpandFall();

        //move the player
        MovePlayer(speed);

        //walk left
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            WalkLeft();
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            WalkStop();
        }

        //walk right
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            WalkRight();
        }
        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            WalkStop();
        }

        //jump
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Jump();
        }

        //fire
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Fire();
        }

    }

    void Flip()
    {
        if ((speed > 0 && !facingRight) || (speed < 0 && facingRight))
        {
            facingRight = !facingRight;

            Vector3 temp = transform.localScale;
            temp.x *= -1;
            transform.localScale = temp;
        }
    }

    void MovePlayer (float playerSpeed)
    {
        if (!jumping)
        {
            if (playerSpeed == 0)
                //stop
                anim.SetInteger("State", 0);
            else
                //walk
                anim.SetInteger("State", 1);
        }
        else
            anim.SetInteger("State", 3);

        rb.velocity = new Vector3(speed, rb.velocity.y, 0);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        jumping = false;
        anim.SetInteger("State", 4);
        if (collision.gameObject.tag == "Spike")
         {
            anim.SetInteger("State", 5);
         }
    }

    public void WalkLeft()
    {
        speed = -speedX;
    }
    public void WalkRight()
    {
        speed = speedX;
    }
    public void WalkStop()
    {
        speed = 0;
    }
    public void Jump()
    {
        jumping = true;
        rb.AddForce(new Vector2(rb.velocity.x, jumpSpeedY));
    }
    public void Fire()
    {
        // Create the Bullet from the Bullet Prefab
        var bullet = (GameObject)Instantiate(
            firePrefab,
            fireSpawn.position,
            fireSpawn.rotation);

        //kam letí střela: 1 = doprava, -1 = doleva
        int multiplier = facingRight ? 1 : -1;

        //nasměruj střelu, je třeba přes pomocný vektor (viz tutoriál Unity)
        Vector3 temp = bullet.transform.localScale;
        temp.x = Mathf.Abs(temp.x);
        temp.x *= multiplier;
        bullet.transform.localScale = temp;

        //dej střele energii
        bullet.GetComponent<Rigidbody2D>().AddForce(new Vector2(bullet.GetComponent<FireManager>().speedX * multiplier, 0));

        anim.SetInteger("State", 6);

        // Destroy the bullet after 2 seconds
        Destroy(bullet, 2.0f);

    }

    void HandleJumpandFall()
    {
        if(jumping)
        {
            if(rb.velocity.y>0)
            {
                //jde nahoru
                anim.SetInteger("State", 3);
            }
            else
            {
                //jde dolu
                anim.SetInteger("State", 4);
            }
        }
    }

    //přidej score
    public void AddScore()
    {
        score++;
        CountScore();
    }

    //spočítej score a nahraj další level
    void CountScore()
    {
        if(score==6)
        {
            SceneManager.LoadScene("2nd floor");
        }
    }
}
