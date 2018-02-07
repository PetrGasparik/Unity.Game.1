using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {

    public float speedX;
    public float jumpSpeedY;

    private Animator anim;
    private Rigidbody2D rb;
    private GameObject fire;

    bool facingRight, jumping, walking;
    float speed;

    // Use this for initialization
    void Start() {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        fire = GameObject.Find("Fire");
        //schováme výstřel
        fire.GetComponent<Renderer>().enabled = false;

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
        //        if (collision.gameObject.tag == "Ground")
        //        {
            jumping = false;
            anim.SetInteger("State", 0);
        //}
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
        fire.GetComponent<Renderer>().enabled = true;
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
}
