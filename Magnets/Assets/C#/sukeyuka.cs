using UnityEngine;
using System.Collections;

public class sukeyuka : MonoBehaviour
{


    private RaycastHit2D ray;
    private RaycastHit2D ray2;
    private float time;
    private float time2;
    private bool flag;
    private bool gf;
    private GameObject pl;
    private GameObject pl2;
    private BoxCollider2D BC;
    private BoxCollider2D BCp;
    private BoxCollider2D BC2;
    private player pls;
    LayerMask mask;
    LayerMask mask2;

    // Use this for initialization
    void Start()
    {
        gameObject.layer = 15;
        gameObject.tag = "suke";
        pl = GameObject.FindWithTag("N_player");//自機の位置取得
        pl2 = GameObject.FindWithTag("S_player");//自機の位置取得
        pls = pl.GetComponent<player>();
        BC = GetComponent<BoxCollider2D>();
        BCp = pl.GetComponent<BoxCollider2D>();
        BC2 = pl2.GetComponent<BoxCollider2D>();
        gf=flag = false;
        time = 0.2f;
        time2 = 0.2f;
        mask = 1 << 0 | 1 << 10 | 1 << 11 | 1 << 12 | 1 << 15; 
        mask2 = 1 << 0 | 1 << 10 | 1 << 11 | 1 << 12 | 1 << 15;
    }

    // Update is called once per frame
    void Update()
    {
        if (pl2.GetComponent<kabehit>() == null)
        {

            ray2 = Physics2D.Linecast(
                pl2.transform.position - transform.up * BC2.size.y * 1.625f - transform.right * pl2.transform.localScale.x * 0.16f,
                pl2.transform.position - transform.up * BC2.size.y * 1.625f + transform.right * pl2.transform.localScale.x * 0.16f, mask
                );
        }
        if (pl.GetComponent<kabehit>() == null)
        {
            ray = Physics2D.Linecast(
        pl.transform.position - transform.up * BCp.size.y * 1.625f - transform.right * pl.transform.localScale.x * 0.16f,
        pl.transform.position - transform.up * BCp.size.y * 1.625f + transform.right * pl.transform.localScale.x * 0.16f, mask2
        );
        }
        
        Debug.DrawLine(
        pl.transform.position - transform.up * BCp.size.y * 1.4f - transform.right * pl.transform.localScale.x * 0.16f,
        pl.transform.position - transform.up * BCp.size.y * 1.4f + transform.right * pl.transform.localScale.x * 0.16f);
        
        if (!flag)
        {
            if (ray.collider == BC)
            {
                gameObject.layer = 10;
                if (ray2.collider == BC)
                {
                    gf = true;
                }
            }
            if (ray2.collider == BC)
            {
                gameObject.layer = 11;
                if (ray.collider == BC)
                {
                    gf = true;
                }
            }
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                flag = true;
            }
            if (ray2.collider != BC&& ray.collider != BC)
            {
                time2 -= Time.deltaTime;
                if (time2 <= 0)
                {
                    gameObject.layer = 15;
                    time2 = 0.2f;
                }

            }
            else
            {
                time2 = 0.2f;
            }

        }
        if (gf)
        {
            gameObject.layer = 1;
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                gf = false;
                flag = true;
            }
        }
        if (flag)
        {
            if (!player.fieldflag)
            {
                if (player.player2Flag)
                {
                    if(ray.collider == BC)
                    {
                        gameObject.layer = 15;

                    }else
                    gameObject.layer = 10;
                }
                else
                {
                    if (ray2.collider == BC)
                    {
                        gameObject.layer = 15;

                    }
                    else
                      
                    gameObject.layer = 11;
                }

                time -= Time.deltaTime;
                if (time <= 0)
                {
                    flag = false;
                    time = 0.2f;
                }
            }
            else
            {
                flag = false;
            }
        }
    }
}