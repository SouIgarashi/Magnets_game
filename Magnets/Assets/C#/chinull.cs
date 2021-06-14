using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chinull : MonoBehaviour {

    private RaycastHit2D hit;
    private RaycastHit2D riri;
    private RaycastHit2D lili;
    private GameObject pl;
    private GameObject pl2;
    
    public bool janp;
    

    public bool rmas;
    public bool lmas;

	// Use this for initialization
	void Start () {

        pl = GameObject.FindWithTag("N_player");//自機の位置取得
        pl2 = GameObject.FindWithTag("S_player");//自機の位置取得
        Destroy(GetComponent<senta>());
    }
	
	// Update is called once per frame
	void Update () {


      

        if (transform.parent == null)
        {
            gameObject.AddComponent<Rigidbody2D>();
            if (GetComponent<Rigidbody2D>()!=null)
            Destroy(GetComponent<chinull>());
            gameObject.layer = 0;

        }
        nuuuu();
        unnnn();

    }


    void unnnn()
    {
       
        if (pl2.transform.parent == pl.transform)
        {
            if (pl2.transform.localPosition.y == 0)
            {
                if (pl2.transform.localPosition.x < 0)
                {
                    if (Input.GetKeyDown("c"))
                    {
                        if (pl2.transform.localPosition.x > transform.localPosition.x)
                        {
                            gameObject.AddComponent<Rigidbody2D>();
                        }
                    }
                }
                else if (pl2.transform.localPosition.x > 0)
                {
                    if (Input.GetKeyDown("c"))
                    {
                        if (pl2.transform.localPosition.x < transform.localPosition.x)
                        {
                            gameObject.AddComponent<Rigidbody2D>();
                        }
                    }
                }
            }
            else
            {
             
                if (Input.GetKeyDown("c"))
                {
                    Debug.Log("aa");
                    if (pl2.transform.localPosition.y < transform.localPosition.y)
                    {

                        gameObject.AddComponent<Rigidbody2D>();
                    }
                }
            }
        }

        if (pl.transform.parent == pl2.transform)
        {
            if (pl.transform.localPosition.y == 0)
            {
                if (pl.transform.localPosition.x < 0)
                {
                    if (Input.GetKeyDown("c"))
                    {
                        if (pl.transform.localPosition.x > transform.localPosition.x)
                        {
                            gameObject.AddComponent<Rigidbody2D>();
                        }
                    }
                }
                else if (pl.transform.localPosition.x > 0)
                {
                    if (Input.GetKeyDown("c"))
                    {
                        if (pl.transform.localPosition.x < transform.localPosition.x)
                        {

                            gameObject.AddComponent<Rigidbody2D>();
                        }
                    }
                }
            }
            else
            {
                if (Input.GetKeyDown("c"))
                {
                    if (pl.transform.localPosition.y < transform.localPosition.y)
                    {
                        gameObject.AddComponent<Rigidbody2D>();
                    }
                }
            }
        }


    }

    void nuuuu()
    {
        if (transform.parent != null)
        {
            masa();
            ray();
            if (gameObject.tag != "S_player" && gameObject.tag != "N_player")
            {
                if(!player.fieldflag&&transform.parent==player.playno.transform)
                gameObject.layer = transform.parent.transform.gameObject.layer;
            }
            if (GetComponent<Rigidbody2D>() != null)
            {
                if (transform.parent.transform.childCount >= 2)
                {
                    if (transform.localPosition.x < 0)
                    {
                        foreach (Transform child in transform.parent.transform)
                        {
                            if (transform.localPosition.x > child.localPosition.x)
                            {
                                child.transform.parent = null;
                            }
                        }
                    }
                    else
                    {
                        foreach (Transform child in transform.parent.transform)
                        {
                            if (transform.localPosition.x < child.localPosition.x)
                            {
                                child.transform.parent = null;
                            }
                        }
                    }
                    if (transform.localPosition.y > 0)
                    {
                        foreach (Transform child in transform.parent.transform)
                        {
                            if (transform.localPosition.y < child.localPosition.y)
                            {
                                child.transform.parent = null;
                            }
                        }
                    }

                }
                transform.parent = null;


            }
        }

    
    }

    void masa()
    {
        riri = Physics2D.Linecast(
           transform.position + transform.up * 0.45f + transform.right * 0.49f,
           transform.position - transform.up * 0.45f + transform.right * 0.49f
           );
        lili = Physics2D.Linecast(
           transform.position + transform.up * 0.45f - transform.right * 0.49f,
           transform.position - transform.up * 0.45f - transform.right * 0.49f
           );
        if (riri.collider == null || riri.collider.tag == "hani" || riri.collider.gameObject.layer == gameObject.layer || riri.collider.gameObject.layer == transform.parent.gameObject.layer || riri.transform.gameObject.layer == 15 || riri.collider.transform.parent == transform.parent)
        {
            rmas = false;
        }
        else
        {
            if (gameObject.layer == 8)
            {
                if (riri && riri.transform.gameObject.layer == 11)
                {

                    rmas = false;
                }
                else
                {
                    rmas = true;
                }
            }
            else if (gameObject.layer == 9)
            {
                if (riri && riri.transform.gameObject.layer == 11)
                {

                    rmas = false;
                }
                else
                {
                    rmas = true;
                }
            }
            else
            {
                rmas = true;
            }

        }

        if (lili.collider == null || lili.collider.tag == "hani" || lili.collider.gameObject.layer == gameObject.layer || lili.collider.gameObject.layer == transform.parent.gameObject.layer || lili.transform.gameObject.layer == 15 || lili.collider.transform.parent == transform.parent)
        {
            lmas = false;
        }
        else
        {
            if (gameObject.layer == 8)
            {
                if (lili && lili.transform.gameObject.layer == 11)
                {

                    lmas = false;
                }
                else
                {

                    lmas = true;
                }

            }
            else if (gameObject.layer == 9)
            {
                if (lili && lili.transform.gameObject.layer == 11)
                {

                    lmas = false;
                }
                else
                {
                   
                    lmas = true;
                }
            }
            else
            {
                
                lmas = true;
            }

        }
        


    }
    
    void ray()
    {
        hit = Physics2D.Linecast(
            transform.position - transform.up * 0.55f - transform.right * 0.35f,
            transform.position - transform.up * 0.55f + transform.right * 0.35f
            );
        

        if (hit.collider == null || hit.collider.tag == "hani"|| hit.collider.gameObject.layer == transform.parent.gameObject.layer||hit.transform.parent!=null || hit.collider.tag == "S_yuka" || hit.collider.tag == "N_yuka" || hit.collider.gameObject.GetComponent<GoalCheck>() != null)
        {
            janp = false;

        }
        else
        {
            if (gameObject.layer == 8)
            {
                if (hit.collider.tag == "S_yuka")
                {
                    janp = false;
                }
            }
            if (gameObject.layer == 9)
            {
                if (hit.collider.tag == "N_yuka")
                {
                    janp = false;
                }
            }

            
            janp = true;
        }
      

    }
    
}
