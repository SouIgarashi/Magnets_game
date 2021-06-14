using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kabehit : MonoBehaviour {
    private RaycastHit2D up;
    private RaycastHit2D donw;
    private RaycastHit2D riri;
    private RaycastHit2D lili;
    private music music;
    private player pl;
    private Animator ani;
    // Use this for initialization
    void Start () {
        music = GameObject.FindWithTag("System").GetComponent<music>();
        pl  = GameObject.FindWithTag("N_player").GetComponent<player>();
        if (gameObject.tag=="S_player"|| gameObject.tag == "N_player")
        {
            ani = GetComponent<Animator>();
        }
        else
        {
            gameObject.layer = 14;
        }
		
    }
	
	// Update is called once per frame
	void FixedUpdate() {
        hitch();
       
    }

    void hitch()
    {
        if (gameObject.transform.parent != player.playno.gameObject)
        {

            LayerMask mask = ~(1 << 2 | 1 << 8 | 1 << 9 | 1 << 14 | 1 << 15);
            if (gameObject.layer == 8)
            {
                mask = ~(1 << 2 | 1 << 8  | 1 << 11 | 1 << 14 | 1 << 15);
            }
            else if (gameObject.layer == 9)
            {
                mask = ~(1 << 2 | 1 << 9 | 1 << 10 | 1 << 14 | 1 << 15);
            }
            Vector3 pos = transform.position - player.playno.transform.position;

            if (pos.x > 1 || pos.x < -1)
            {
                float xs = pos.x;
                if (xs < 0) xs *= -1;
                xs *= 2;
                pos /= xs;
            }
            else if (pos.y > 1 || pos.y < -1)
            {
                float ys = pos.y;
                if (ys < 0) ys *= -1;
                ys *= 2;
                pos /= ys;
            }
            Vector3 righter = transform.right * transform.localScale.x * 0.16f;
            Vector3 uper = transform.up * transform.localScale.y * 0.16f;
            Vector3 rights = Vector3.zero;
            Vector3 rightend = Vector3.zero;
            Vector3 lefts = Vector3.zero;
            Vector3 leftend = Vector3.zero;
            Vector3 donws = Vector3.zero;
            Vector3 donwend = Vector3.zero;
            Vector3 ups = Vector3.zero;
            Vector3 upend = Vector3.zero;

            if (player.vecpos)
            {
                rights = transform.position + transform.right * transform.localScale.x * 0.15f + transform.up * transform.localScale.y * 0.17f;
                rightend = transform.position - transform.right * transform.localScale.x * 0.15f + transform.up * transform.localScale.y * 0.17f;
                lefts = transform.position + transform.right * transform.localScale.x * 0.15f - transform.up * transform.localScale.y * 0.17f;
                leftend = transform.position - transform.right * transform.localScale.x * 0.15f - transform.up * transform.localScale.y * 0.17f;
                
                
                if (pl.osihiki)
                {
                    donws = transform.position - righter;
                    donwend = player.playno.transform.position - righter;
                    ups = transform.position + righter;
                    upend = player.playno.transform.position + righter;

                }
                else
                {

                    donws = transform.position + pos - righter;
                    donwend = transform.position - righter;
                    ups = transform.position + pos + righter;
                    upend = transform.position + righter;


                }
            }
            else
            {
                rights = transform.position + transform.up * transform.localScale.y * 0.15f + transform.right * transform.localScale.x * 0.17f;
                rightend = transform.position - transform.up * transform.localScale.y * 0.15f + transform.right * transform.localScale.x * 0.17f;
                lefts = transform.position + transform.up * transform.localScale.y * 0.15f - transform.right * transform.localScale.x * 0.17f;
                leftend = transform.position - transform.up * transform.localScale.y * 0.15f - transform.right * transform.localScale.x * 0.17f;

                if (pl.osihiki)
                {
                    donws = transform.position - uper;
                    donwend = player.playno.transform.position - uper;
                    ups = transform.position + uper;
                    upend = player.playno.transform.position + uper;


                }
                else
                {
                    
                    donws = transform.position + pos - uper;
                    donwend = transform.position - uper;
                    ups = transform.position + pos + uper;
                    upend = transform.position + uper;


                }
            }


            donw = Physics2D.Linecast(
                donws,
               donwend, mask
              );
            up = Physics2D.Linecast(
               ups,
               upend, mask
              );
            Debug.DrawLine(
                donws,
               donwend);
            Debug.DrawLine(
                ups,
               upend);
            Debug.DrawLine(
                rights,
               rightend);
            Debug.DrawLine(
                lefts,
               leftend);
            riri = Physics2D.Linecast(
                rights,
               rightend);
            lili = Physics2D.Linecast(
               lefts,
               leftend);

            Debug.Log(up.collider + "↑");
            Debug.Log(donw.collider + "↓");
            Debug.Log(riri.collider + "→");
            Debug.Log(lili.collider + "←");
            if (up.collider == riri.collider && riri.collider != null || donw.collider == riri.collider && riri.collider != null || up.collider == lili.collider && lili.collider != null || donw.collider == lili.collider && lili.collider != null)
            {
              
                    if (gameObject.tag == "S_player" || gameObject.tag == "N_player")

                        ani.SetBool("ziseki_up", true);

                    music.SE(11);
                    pl.ZXtt = 0;
              
            }

        }
    }
}
