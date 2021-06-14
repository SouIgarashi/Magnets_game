using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hani : MonoBehaviour
{
    private GameObject[] hit = new GameObject[100];
    private int i = 0;
    public static GameObject ofa;
    private player play;
    private GameObject ofasave;
    private float savey;
    private float savex;
    private music music;
    
    

    // Use this for initialization
    void Start()
    {
        ofa = null;
        ofasave = null;
        GameObject[] ads = GameObject.FindGameObjectsWithTag("zisyaku");
        music = GameObject.FindWithTag("System").GetComponent<music>();
        play = GameObject.FindWithTag("N_player").GetComponent<player>();//自機の位置取得
        for(int i = 0; i < ads.Length; i++)
        {
            ads[i].AddComponent<zisyaku>();
        }

    }
    
    // Update is called once per frame
    void FixedUpdate()
    {

        if (ofa != ofasave) {
            ofasave.gameObject.layer = senta.save;
            Destroy(ofasave.GetComponent<senta>());
            ofasave = ofa;
        }
       
        if (ofa != null && play.ziryokuf)
        {
            if(ofa.GetComponent<senta>()==null)
            ofa.AddComponent<senta>();
            ofasave = ofa;

            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                music.SE(4);
                senta.pos = 4;
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                music.SE(4);
                senta.pos = 2;
            }
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                music.SE(4);
                senta.pos = 3;
            }
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                music.SE(4);
                senta.pos = 1;
            }

            if (senta.kari != null)
            {
                for(int i = 0; i < 10; i++)
                {
                  
                    if (hit[i] == senta.kari)
                    {
                        ofa = hit[i];
                        break;
                    }
                }
            }
         
          

          

        }
        if (ofa == null)
        {

            for (int j = 0; j < 10; j++)
            {
                hit[j] = null;
            }
            i = 0;
            ofasave = null;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject != player.playno)
        {
            if (col.gameObject.tag == "S_player" || col.gameObject.tag == "N_player" || col.gameObject.tag == "zisyaku")
            {
                if (col.gameObject != null)
                {
                    hit[i] = col.gameObject;
                    i++;
                    ofa = hit[0];


                }
                else
                {
                    ofa = null;
                }
                ofasave = ofa;
              
            }
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {

        if (ofa != null)
        {
            if (ofa.transform.parent == null)
            {
                if (col.tag == ofa.tag)
                {
                    player pl = GameObject.FindWithTag("N_player").GetComponent<player>();
					pl.ZXtt = 0;
                    ofa = null;
                    for (int j = 0; j < 10; j++)
                    {
                        hit[j] = null;
                    }
                }
            }
        }
        i = 0;
       
    }

}

