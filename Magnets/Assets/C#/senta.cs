using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class senta : MonoBehaviour
{

    private float time;
    public static int pos;
    private GameObject aa;
    private CircleCollider2D acc;
    public static GameObject kari;
    private Vector2 posi;
    public static int save;

    // Use this for initialization
    void Start()
    {
        aa = GameObject.FindWithTag("hani");
        acc = aa.GetComponent<CircleCollider2D>();
        save = gameObject.layer;
        if (gameObject.tag == "S_player" || gameObject.tag == "N_player")
        { }
        else
        {
            if (player.fieldflag)
                gameObject.layer = 16;
        }
    }

    // Update is called once per frame
    public void Update()
    {

        if (hani.ofa != gameObject)
        {
            Destroy(GetComponent<senta>());
        }

        time += Time.deltaTime * 20f;
        switch (pos)
        {
            case 0:
                time = 0;
                posi = new Vector2(0, 0);
                break;
            case 1:
                posi = transform.position + transform.right * time;
                break;
            case 2:
                posi = transform.position - transform.right * time;
                break;
            case 3:
                posi = transform.position + transform.up * time;
                break;
            case 4:
                posi = transform.position - transform.up * time;
                break;
        }

        Collider2D[] targets = Physics2D.OverlapCircleAll(posi, time);
        foreach (Collider2D obj in targets)
        {
            if (obj.gameObject != player.playno)
            {
                if (obj.gameObject != hani.ofa)
                {
                    if (obj.tag == "zisyaku" || obj.tag == "N_player" || obj.tag == "S_player")
                    {
                        kari = obj.gameObject;
                        gameObject.layer = save;
                        pos = 0;
                        break;
                    }
                }
            }
        }
    }
}
   

