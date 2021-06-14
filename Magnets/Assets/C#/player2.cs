using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class player2 : MonoBehaviour {

    private GameObject sys;
    private system system;
    private GameObject playe;
    private player play;
    // Use this for initialization
    void Start () {

        sys = GameObject.FindWithTag("System");//自機の位置取得
        system = sys.GetComponent<system>();
        playe = GameObject.FindWithTag("N_player");//自機の位置取得
        play = playe.GetComponent<player>();

    }

    void Update()
    {
        


    }

    void OnCollisionEnter2D(Collision2D col)
    {

        system.Change(col);

        if (player.player2Flag)
        {
            
            if (!play.ziryokuf)
            {
                if (hani.ofa != null)
                {
                    if (col.gameObject == hani.ofa)
                    {
                        if (play.osihiki)
                            play.ZXtt = 0;
                        play.hitflag = false;
                    }
                }
            }
        }
    }
}
