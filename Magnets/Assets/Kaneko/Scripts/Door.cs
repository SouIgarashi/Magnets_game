using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {

    public bool openStay;
    
    Vector2 pivot = new Vector2(0.5f, 0.5f);
    Animator animator;
    new Collider2D collider;

    // Use this for initialization
    void Start () {
        animator = GetComponent<Animator>();
        collider = GetComponent<BoxCollider2D>();
	}
	
	// Update is called once per frame
	void Update () {

	}
    
    public void Open()
    {
        animator.SetBool("moveDoor", true);
        collider.isTrigger = true;
    }

    public void Close()
    {
        animator.SetBool("moveDoor", false);
        if (!openStay)
        {
            collider.isTrigger = false;
        }
    }

}
