using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chid : MonoBehaviour {

    

    // Use this for initialization
    void Start () {


    }
	
	// Update is called once per frame
	void Update () {

       
        gravity();
    }

    void gravity()
    {
        if (hani.ofa != this.gameObject)
        {
            if (GetComponent<Rigidbody2D>() != null)
            {

                if (transform.tag != "S_player" && transform.tag != "N_player")
                {
                    GetComponent<Rigidbody2D>().gravityScale = 1;
					GetComponent<Rigidbody2D>().velocity =new Vector3(0,0,0);
                    if (GetComponent<Rigidbody2D>().gravityScale == 1)
                    {
                        this.gameObject.layer = 0;
                        Destroy(GetComponent<chilber>());
                        Destroy(GetComponent<chid>());
                        Destroy(GetComponent<kabehit>());
                    }
                }
                else
                {
                    GetComponent<Rigidbody2D>().gravityScale = 2;
					GetComponent<Rigidbody2D>().velocity = new Vector3(0, 0, 0);
                    if (GetComponent<Rigidbody2D>().gravityScale == 2)
                    {
                        Destroy(GetComponent<chilber>());
                        Destroy(GetComponent<chid>());
                        Destroy(GetComponent<kabehit>());
                    }
                }
            }
        }
    }
}
