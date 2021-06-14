using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zisyaku : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (transform.parent == null)
        {
            if (GetComponent<Rigidbody2D>() != null)
                GetComponent<Rigidbody2D>().gravityScale = 1;
        }
        else
        {
        }
	}
}
