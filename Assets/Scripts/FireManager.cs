using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireManager : MonoBehaviour {

    public float speedX;

    private Collider2D collid;

    // Use this for initialization
    void Start () {
        //GetComponent<Renderer>().enabled = false;
    }

    // Update is called once per frame
    void Update () {
		
	}
/*
    public void Fire(bool directionRight, Rigidbody2D player)
    {
        //collider hráče, pro umístění střely
        collid = player.GetComponent<Collider2D>();

        //kam letí střela: 1 = doprava, -1 = doleva
        int multiplier = directionRight ? 1 : -1;

        //nasměruj střelu, je třeba přes pomocný vektor (viz tutoriál Unity)
        Vector3 temp = transform.localScale;
        temp.x = Mathf.Abs(temp.x);
        temp.x *= multiplier;
        transform.localScale = temp;

        //umísti střelu na pozici hráče, k jeho levému/pravému boku
        transform.position = player.position + new Vector2(collid.bounds.size.x * multiplier, 0);

        //dej střele energii
        GetComponent<Rigidbody2D>().AddForce(new Vector2(speedX * multiplier, 0));

        //ukaž střelu
        GetComponent<Renderer>().enabled = true;

        //za vteřinu střelu schovej
        Invoke("HideFire", duration);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //při kolizi zmizí
        HideFire();
    }

    void HideFire()
    {
        //zastav střelu
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;

        //schovej střelu
        GetComponent<Renderer>().enabled = false;
    }
*/
}
