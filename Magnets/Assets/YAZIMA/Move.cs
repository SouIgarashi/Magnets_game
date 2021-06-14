using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour {

    Rigidbody2D RB2;
    bool notteru = false;
    Player P;

	// Use this for initialization
	void Start () {

        RB2 = GetComponent<Rigidbody2D>();
        P = GameObject.Find("Player").GetComponent<Player>();
	}
	
	// Update is called once per frame
	void Update () {
        
            if (Input.GetKey(KeyCode.D))
            {
                RB2.AddForce(transform.right * 20);
            }
        if (notteru)
        {
            {
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    P.lide = true;
                    Destroy(gameObject);
                }

            }
        }
	}

    void OnCollisionEnter2D(Collision2D c)
    {
        if (c.gameObject.tag == "oppai")
            notteru = true;
        
    }

    void OnCollisionExit2D(Collision2D c)
    {
        if (c.gameObject.tag == "oppai")
            notteru = false;

    }

}
