using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelController : MonoBehaviour {


    private Rigidbody2D rb;
    private Renderer rend;
    //stav barelu; 0 = základní, 1 = střelený
    private int barrelStatus= 0;

	// Use this for initialization
	void Start () {
        //rb = GetComponent<Rigidbody2D>();
        rend = GetComponent<Renderer>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //při kolizi změn barvu
        if (collision.gameObject.tag == "Fire" & barrelStatus == 0)
        {
            barrelStatus++;
            rend.material.color = Color.cyan;
            GameObject.Find("Robot").GetComponent<PlayerManager>().AddScore();
        }
    }
}
