using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chilber : MonoBehaviour {
    private RaycastHit2D ray;
    
    private GameObject player1;
    private GameObject player2;
    
    private player pl;
    private bool flag;
    private float time;
    private bool hit;
    private int x;
    private int x2;
    private int y;
	private float timer;


    // Use this for initialization
    void Start() {
        player1 = GameObject.FindWithTag("N_player");
        player2 = GameObject.FindWithTag("S_player");
    
        pl = player1.GetComponent<player>();
		timer = 0.3f;
        flag = false;
        hit = false;
        time = 0.2f;
    }

    // Update is called once per frame
    void Update()
    {



        if (flag)
        {
            gameObject.AddComponent<chinull>();
            chchch();
            Destroy(GetComponent<chilber>());
			timer = -Time.deltaTime;
			if (timer <= 0) {
				Destroy(GetComponent<kabehit>());
			}
        }

    }



    void OnCollisionEnter2D(Collision2D col)
    {
        ray = Physics2D.Linecast(
                transform.position, col.transform.position
                );
        if (col.gameObject == player1 || col.gameObject == player2)
        {

            transform.parent = col.transform;

            Destroy(GetComponent<Rigidbody2D>());
            pl.hitflag = false;
            if (transform.parent == col.transform)
                flag = true;
        }
       

    }
    void chchch()
    {

        foreach (Transform child in player.playno.transform)
        {
            if (child.transform.localPosition.y == 0)
            {
                if (child.transform.localPosition.x < 0)
                {
                    x++;
                    if (child.transform.gameObject == this.gameObject)
                    {
                        x--;
                    }
                }
                else
                {
                    x2++;
                    if (child.transform.gameObject == this.gameObject)
                    {
                        x2--;
                    }
                }
            }
            else if (child.transform.localPosition.x == 0)
            {
                y++;
                if (child.transform.gameObject == this.gameObject)
                {
                    y--;
                }
            }

        }

        
    float xx = ray.normal.x;
    float yy = ray.normal.y;
        if (xx< 0) xx *= -1;
        if (yy< 0) yy *= -1;
        if (yy == xx) yy += 0.1f;
        if (yy<xx) transform.localPosition = new Vector2(transform.localPosition.x, 0);
       
        else
        {
            if (transform.localPosition.y < 0)
            {

                transform.localPosition = new Vector2(0, y * 0.36f + 0.36f);
            }
            else
                transform.localPosition = new Vector2(0, y * 0.36f + 0.36f);

        }

        transform.localRotation = new Quaternion(0, 0, 0, 0);


       
    }

}
