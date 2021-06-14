using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class player : MonoBehaviour
{

    //操作キャラクターの切り替え
    public static bool player2Flag = false;//自機フラグ
    public static bool vecpos;
    private bool plsave;//自機フラグの保存用


    //移動
    private Rigidbody2D RB;//操作キャラの重力
    private Rigidbody2D RB2;//操作していないほうのキャラ
    private Rigidbody2D RB3;//磁力選択時
    private float Xspeed;//移動スピード

    //ジャンプ
    public float jump; //ジャンプ量（どれくらいジャンプするか）
    private RaycastHit2D grounded;//ジャンプ判定(ray)
    private bool hahaha;//ジャンプ判定
    private bool jf;//ジャンプフラグ
    private float jumptt;//ジャンプのクールタイム
    private bool jumpFlagN;//ジャンプ判定

    //摩擦
    private bool lmasa;//左摩擦判定
    private bool rmasa;//右摩擦判定
    private RaycastHit2D right;//摩擦判定(ray右側)
    private RaycastHit2D left;//摩擦判定(ray左側)

    //エフェクト
    public GameObject ten;//エフェクト
    public GameObject sen;//エフェクト
    public GameObject ofaten;//エフェクト
    private int jcun;
    private int mcunl;
    private int mcunr;

    //磁力
    public static bool fieldflag;//磁力範囲発動
    public bool osihiki;//斥力か引力か
    public bool hitflag;//磁力の狙い先が子オブジェクトかどうか
    public bool ziryokuf;//磁力発動判定
    public float ZXtt;//磁力発生時間
    private float rad;//磁石をとばす角度計算用
    private Vector2 ziryoku;//磁石をとばす角度計算用
    private GameObject aa;//磁力範囲
    private Vector2 ofa1;//磁力選択キャラの移動用
    private Vector3 aa1;//N_playerの磁力範囲
    private Vector3 aa2;//S_playerの磁力範囲
    private SpriteRenderer aac;//磁力範囲の色

    //アニメーション
    private Animator anim;//N_playerのアニメーション
    private Animator anim2;//S_playerのアニメーション

    //ボックスコライダー
    private BoxCollider2D BC;//操作キャラの当たり判定
    private BoxCollider2D BC2;//操作していないほうのキャラの当たり判定

    //その他
    public static GameObject playno;//操作キャラ取得
    private GameObject playno2;
    private system system;//scene切り替え
    private music music;//SE取得
    private GameObject player2;//S_player
    private Vector3 pos1;//操作キャラの位置
    private GoalScript Goal;//ゴール判定取得
    private Vector2 posi;//操作キャラの移動用
    private Vector2 genzai;//操作キャラのscale変換用




    // Use this for initialization
    void Start()
    {

        //取得
        music = GameObject.FindWithTag("System").GetComponent<music>();
        system = GameObject.FindWithTag("System").GetComponent<system>();//システム系の取得
        player2 = GameObject.FindWithTag("S_player");
        Goal = GameObject.Find("Goal").GetComponent<GoalScript>();
        RB = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        RB2 = player2.GetComponent<Rigidbody2D>();
        anim2 = player2.GetComponent<Animator>();
        BC = GetComponent<BoxCollider2D>();
        BC2 = player2.GetComponent<BoxCollider2D>();
        aa = GameObject.FindWithTag("hani");
        aac = aa.GetComponent<SpriteRenderer>();

        //初期設定
        Xspeed = 3f;
        jumptt = 1f;
        ZXtt = 1f;
        osihiki = false;
        jump = 10f;
        fieldflag = false;
        player2Flag = false;
        aa.SetActive(false);
        hitflag = true;
        ziryokuf = true;
        ziryoku = new Vector2(2, 2);
        aa1 = new Vector3(0, 0, 0);
        genzai = transform.localScale;
        playno = this.gameObject;
        playno2 = player2;
        plsave = player2Flag;
        aa.layer = 2;
        aac.color = new Color(0, 0, 1f, 0.5f);
        anim2.SetBool("idol", false);
        anim2.SetBool("dash", false);
        anim2.SetBool("ziseki_up", false);
        anim2.SetBool("taiki", true);
    }

    // Update is called once per frame
    void Update()
    {
        playno2.transform.rotation = new Quaternion(0, 0, 0, 0);
       
        if (!Goal.gameClear)
        {
            playerb();//自機制御
            janp();//ジャンプ判定
            masatu();//摩擦計算
            idou();//移動制御
            flagens();//磁石計算
        }
        else
            goal();//ゴールした時の動き
        satch();//操作キャラの取得
        syster();//磁力範囲の位置調整
        Chen();//キャラ切り替え
        efe();//エフェクトの位置調整
        pat();
        
        
    }

    void syster()
    {
        jcun = 0;
        mcunl = 0;
        mcunr = 0;
        aa1 = new Vector3(3, 3, 0) * playno.transform.childCount;

        aa.transform.localScale = new Vector3(3, 3, 0) + aa1;
        pos1 = playno.transform.position;
        playno.transform.localScale = genzai;
        genzai = playno.transform.localScale;

        player2.gameObject.layer = 9;
        gameObject.layer = 8;
    }

    void pat()
    {


        aa.transform.position = pos1;
        ten.transform.position = pos1;
        sen.transform.position = pos1;



    }

    void idou()
    {
        if (Input.GetKey(KeyCode.RightArrow) && !fieldflag)
        {

            if (genzai.x < -0)
            {

                childchen(playno);
                genzai.x = genzai.x * -1;

            }
            anim.SetBool("dash", true);
            anim.SetBool("idol", false);
            if (!rmasa)
                posi.x = 1f * Xspeed;
        }

        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            posi.x = 0;
            anim.SetBool("dash", false);
        }


        //←を押してる間左に移動
        if (Input.GetKey(KeyCode.LeftArrow) && !fieldflag)
        {

            if (genzai.x > 0)
            {

                childchen(playno);
                genzai.x = genzai.x*-1;
                

            }
            anim.SetBool("dash", true);
            anim.SetBool("idol", false);
            if (!lmasa)
                posi.x = -1f * Xspeed;
            else posi.x = 0;
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            posi.x = 0;
            anim.SetBool("dash", false);
        }
    }

    void janp()
    {
        grounded = Physics2D.Linecast(
          pos1 - transform.up * 0.65f - transform.right * 0.35f,
          pos1 - transform.up * 0.65f + transform.right * 0.35f
          );


        int count = playno.transform.childCount;
        if (grounded.collider == null || grounded.collider.tag == "hani"  || grounded.collider.gameObject.GetComponent<GoalCheck>()!=null)
        {

            hahaha = false;
            if (count == 1)
            {

                if (playno.GetComponentInChildren<chinull>() != null)
                {
                    chinull cn = playno.GetComponentInChildren<chinull>();
                    if (cn.janp == true)
                    {
                        hahaha = true;
                    }
                    else
                    {
                        hahaha = false;
                    }
                }
            }
            else if (count > 1)
            {

                if (playno.GetComponentsInChildren<chinull>() != null)
                {
                    chinull[] cn = playno.GetComponentsInChildren<chinull>();
                    for (int i = 0; i < cn.Length; i++)
                    {
                        if (cn[i].janp == true)
                        {
                            jcun++;
                        }
                    }
                    if (jcun != 0)
                    {
                        hahaha = true;
                    }
                    else
                    {
                        hahaha = false;
                    }
                }
            }
        }
        else
        {
            if (playno.gameObject.layer == 8)
            {
                if (grounded.collider.tag == "S_yuka")
                {
                    hahaha = false;
                }
            }
            if (playno.gameObject.layer == 9)
            {
                if (grounded.collider.tag == "N_yuka")
                {
                    hahaha = false;
                }
            }
            
            hahaha = true;
        }
        if (hahaha == true)
            jf = true;
        else jf = false;

        //------ -----------------------------------------
        //接地しているときスペースキーでジャンプ
        if (ziryokuf && jf && !fieldflag)
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                posi.y = jump;
                music.SE(8);
                jumpFlagN = false;

            }

            if (jumpFlagN == false)
            {

                jumptt -= 0.05f;

                if (jumptt < 0)
                {

                    jf = false;
                    jumpFlagN = true;
                    jumptt = 1f;
                }
            }
        }
    }

    void masatu()
    {
        right = Physics2D.Linecast(
          pos1 + transform.up * 0.5f + transform.right * 0.41f,
          pos1 - transform.up * 0.45f + transform.right * 0.41f
          );
        left = Physics2D.Linecast(
          pos1 + transform.up * 0.5f - transform.right * 0.41f,
          pos1 - transform.up * 0.45f - transform.right * 0.41f
          );



        int count = playno.transform.childCount;
        if (left.collider == null || left.collider.tag == "hani" || left.transform.parent == playno.transform || left.transform.gameObject.layer == playno.layer || left.transform.gameObject.layer == 15)
        {

            lmasa = false;

            if (count == 1)
            {

                if (playno.GetComponentInChildren<chinull>() != null)
                {
                    chinull cn = playno.GetComponentInChildren<chinull>();
                    if (cn.lmas == true)
                    {

                        lmasa = true;
                    }
                    else
                    {

                        lmasa = false;
                    }
                }
            }
            else if (count > 1)
            {

                if (playno.GetComponentsInChildren<chinull>() != null)
                {
                    chinull[] cn = playno.GetComponentsInChildren<chinull>();
                    for (int i = 0; i < cn.Length; i++)
                    {
                        if (cn[i].lmas == true)
                        {
                            mcunl++;
                        }
                        if (mcunl != 0)
                        {
                            lmasa = true;
                        }
                        else
                        {
                            lmasa = false;
                        }
                    }
                }
            }
        }
        else
        {
           
            if (!player2Flag)
            {

                if (left && left.transform.gameObject.layer == 11)
                {
                   
                    lmasa = false;
                }
                else
                {
                    lmasa = true;
                }
            }
            else
            {

                if (left && left.transform.gameObject.layer == 10)
                {
                    lmasa = false;
                }
                else
                {
                    lmasa = true;
                }
            }
        }
        if (right.collider == null || right.collider.tag == "hani" || right.transform.parent==playno.transform || right.transform.gameObject.layer == playno.layer|| right.transform.gameObject.layer == playno.layer)
        {

           
            rmasa = false;

            if (count == 1)
            {

                if (playno.GetComponentInChildren<chinull>() != null)
                {
                    chinull cn = playno.GetComponentInChildren<chinull>();
                    if (cn.rmas == true)
                    {

                        rmasa = true;
                    }
                    else
                    {
                        rmasa = false;
                    }
                }
            }
            else if (count > 1)
            {

                if (playno.GetComponentsInChildren<chinull>() != null)
                {
                    chinull[] cn = playno.GetComponentsInChildren<chinull>();
                    for (int i = 0; i < cn.Length; i++)
                    {
                        if (cn[i].rmas == true)
                        {
                            mcunr++;
                        }
                        if (mcunr != 0)
                        {
                            rmasa = true;
                        }
                        else
                        {
                            rmasa = false;
                        }
                    }
                }
            }
        }
        else
        {
            if (!player2Flag)
            {

                if (right && right.transform.gameObject.layer == 11)
                {

                    rmasa = false;
                }
                else
                {
                    rmasa = true;
                }
            }
            else
            {

                if (right && right.transform.gameObject.layer == 10)
                {
                    rmasa = false;
                }
                else
                {
                    rmasa = true;
                }
            }
            
        }


        if (jf)
        {
            rmasa = false;
            lmasa = false;
        }
    }


    void goal()
    {
        if (RB != null)
        {
            RB.drag = 3;
            RB.gravityScale = 2;
            posi = RB.velocity;
        }
        BC.size = new Vector2(0.3f, 0.3f);
        BC2.size = new Vector2(0.3f, 0.3f);
        BC.sharedMaterial.friction = 1f;
        BC.enabled = true;

        anim.SetBool("idol", false);
        anim.SetBool("dash", false);
        anim.SetBool("ziseki_up", false);
        anim.SetBool("taiki", true);

        anim2.SetBool("idol", false);
        anim2.SetBool("dash", false);
        anim2.SetBool("ziseki_up", false);
        anim2.SetBool("taiki", true);


        if (RB != null)
        {
            RB.gravityScale = 2f;
            RB.constraints = RigidbodyConstraints2D.FreezeAll;
        }
        if (RB2 != null)
            {
                RB2.gravityScale = 2f;
                RB2.constraints = RigidbodyConstraints2D.FreezeAll;
            }
           

    }

    void playerb()
    {

        if (RB != null)
        {
            RB.drag = 3;
            RB.gravityScale = 2;
            posi = RB.velocity;
        }
        BC.size = new Vector2(0.3f, 0.4f);
        BC2.size = new Vector2(0.3f, 0.3f);
        BC.sharedMaterial.friction = 1f;
        BC.enabled = true;

        anim.SetBool("taiki", false);
        anim.SetBool("idol", true);

		playno.transform.rotation = new Quaternion(0, 0, 0, 0);

        if (RB != null)
            RB.constraints = RigidbodyConstraints2D.FreezeRotation;

    }

    void childchen(GameObject gagagabun)
    {
        foreach (Transform child in gagagabun.transform)
        {
            float chposss = child.transform.localPosition.y;
            float chpos = child.transform.localPosition.x * -1;
            float scl = child.transform.localScale.x * -1;
            child.transform.localPosition = new Vector2(chpos, chposss);
            child.transform.localScale = new Vector2(scl, child.transform.localScale.y);
        }
    }

    void color()
    {
        if (osihiki)
        {
            aac.color = new Color(1f, 0, 0, 0.5f);
        }
        else
        {
            aac.color = new Color(0, 0, 1f, 0.5f);
        }



    }

    void efe()
    {
        if (fieldflag && hani.ofa != null)
        {
            efecter(true);
            ofaten.transform.position = hani.ofa.transform.position;
        }
    }

    void huttobi()
    {
        playno2.transform.rotation = new Quaternion(0, 0, 0, 0);
        anim2.SetBool("taiki", false);
        anim2.SetBool("ziseki", true);
        Vector2 pos= playno.transform.position - playno2.transform.position;
        if (osihiki)
        {
            if (pos.x < 0)
            {
                if (playno2.transform.localScale.x > 0)
                {
                    
                    float a = playno2.transform.localScale.x * -1;
                    playno2.transform.localScale = new Vector2(a, playno2.transform.localScale.y);
                    childchen(playno2);
                }

            }
            else
            {
                if (playno2.transform.localScale.x < 0)
                {
                   
                    float a = playno2.transform.localScale.x * -1;
                    playno2.transform.localScale = new Vector2(a, playno2.transform.localScale.y);
                    childchen(playno2);
                }
            }
        }
        else
        {
            if (pos.x < 0)
            {
                if (playno2.transform.localScale.x < 0)
                {
                   
                    float a = playno2.transform.localScale.x * -1;
                    playno2.transform.localScale = new Vector2(a, playno2.transform.localScale.y);
                    childchen(playno2);
                }
            }
            else
            {
                if (playno2.transform.localScale.x > 0)
                {
                    
                    float a = playno2.transform.localScale.x * -1;
                    playno2.transform.localScale = new Vector2(a, playno2.transform.localScale.y);
                    childchen(playno2);
                }

            }
        }
    }

    void flagens()
    {
        

        if (fieldflag)
        {
            if (ziryokuf)
            {
                if (Input.GetKeyDown(KeyCode.Z))
                {
                    osihiki = !osihiki;
                    music.SE(5);
                    color();
                }
            }
            if (hani.ofa != null)
            {

                if (Input.GetKeyDown(KeyCode.Space))
                {
                    anim2.SetBool("ziseki_up", false);
                    hani.ofa.AddComponent<chid>();
                    aac.enabled = false;
                    ziryokuf = false;
                    music.SE(3);
                    hani.ofa.AddComponent<kabehit>();
                    
                    
                       
                   

                    if (hani.ofa.transform.parent == playno.transform)
                    {

                        if (osihiki)
                        {
                            
                            aac.enabled = true;
                            aa.SetActive(false);
                            fieldflag = false;
                            ZXtt = 0;
                        }
                        else
                        {
                            if (hani.ofa == playno2) {
                                huttobi();
                                
                            }
                            RB3 = hani.ofa.AddComponent<Rigidbody2D>();
                            ofa1 = RB3.velocity;

                        }
                    }
                    else
                    {
                        if (hani.ofa == playno2) huttobi();
                        if (osihiki)
                        {
                            hani.ofa.AddComponent<chilber>();
                        }
                        if (hani.ofa.GetComponent<Rigidbody2D>() != null)
                        {

                            RB3 = hani.ofa.GetComponent<Rigidbody2D>();
                            ofa1 = RB3.velocity;
                        }
                    }
                }
            }
        }
        
       
        //Zを入力したとき
        if (Input.GetKeyDown(KeyCode.X))
        {
            efecter(false);
            music.stop();
            fieldflag = false;
            aa.SetActive(false);
            if(fieldflag) music.SE(2);
        }
        if (ziryokuf && !fieldflag)
        {

            if (Input.GetKeyDown(KeyCode.Z))
            {

                music.SE(6);
                music.SE(7);
                fieldflag = true;
                aa.SetActive(true);

            }
        }


        if (!ziryokuf)
        {
           
            if (hani.ofa != null)
                sankannsu(osihiki);
            ZXtt -= Time.deltaTime;
            if (ZXtt < 0)
            {

                if (hitflag && hani.ofa != null)
                {

                    if (hani.ofa.GetComponent<Rigidbody2D>() == null)
                    {

                        RB3 = hani.ofa.AddComponent<Rigidbody2D>();
                    }
                    else
                    {
                        hani.ofa.GetComponent<Rigidbody2D>().gravityScale = 1;
                    }
                }



                
                
                music.stop();
                efecter(false);
                aac.enabled = true;
                aa.SetActive(false);
                hani.ofa = null;
                ziryokuf = true;
                fieldflag = false;
                BC.sharedMaterial.friction = 0f;
                BC.enabled = true;
                ZXtt = 1f;
                hitflag = true;
                anim2.SetBool("taiki", true);
                anim2.SetBool("ziseki", false);
            }
        }
        //-------------------------------------------------------------------------------------------斥力
        if (RB != null)
            RB.velocity = posi;
        if (!ziryokuf && RB3 != null)
            RB3.velocity = ofa1;
    }

    void efecter(bool set)
    {

        ofaten.SetActive(set);
        ten.SetActive(set);
        sen.SetActive(set);
    }

    //--------------------------
    //falseなら放す　trueなら引く
    //--------------------------

    void sankannsu(bool han)
    {
        if (RB3 != null)
        {
            RB3.constraints = RigidbodyConstraints2D.FreezeRotation;
            RB3.gravityScale = 0.0f;
        }
        RB.constraints = RigidbodyConstraints2D.FreezeAll;
        BC.sharedMaterial.friction = 50f;
        BC.enabled = false;
        BC.enabled = true;

        //三角関数Atan2でp2とp1の方向をとる
        rad = Mathf.Atan2(
            pos1.y - hani.ofa.transform.position.y,//NをSの方向に動かす
            pos1.x - hani.ofa.transform.position.x);

        if (han)
        {
            //p2がp1の方向に動く
            ofa1.x += ziryoku.x * Mathf.Cos(rad);
            ofa1.y += ziryoku.y * Mathf.Sin(rad);
           
        }
        else
        {
            //p2がp1の方向に動く
            ofa1.x -= ziryoku.x * Mathf.Cos(rad);
            ofa1.y -= ziryoku.y * Mathf.Sin(rad);
           
        }
        float ofaa = ofa1.y;
        float ofab = ofa1.x;
        if (ofaa <= 0) ofaa *= -1f;
        if (ofab <= 0) ofab *= -1f;
        if (ofaa > ofab) vecpos = true;
        else vecpos = false;

    }

    void satch()
    {

        //毎フレーム計算
        
        //操作キャラ変更時
        if (plsave != player2Flag)
        {
            
            if (player2Flag)
            {
                
                playno2 = gameObject;
                playno = player2;


            }
            else
            {
                
                playno = this.gameObject;
                playno2 = player2;
            }

            genzai = playno.transform.localScale;
            playno.transform.parent = null;
            RB = playno.GetComponent<Rigidbody2D>();
            if (RB == null)
                RB = playno.AddComponent<Rigidbody2D>();
            anim = playno.GetComponent<Animator>();
            BC = playno.GetComponent<BoxCollider2D>();
            playno.transform.rotation = new Quaternion(0, 0, 0, 0);
            RB2 = playno2.GetComponent<Rigidbody2D>();
            anim2 = playno2.GetComponent<Animator>();
            BC2 = playno2.GetComponent<BoxCollider2D>();
            if (playno.transform.parent == null)
            {
                if (genzai.y != 2.5)
                {
                    if (genzai.x > 0)
                    {
                        genzai = new Vector2(-2.5f, 2.5f);
                    }
                    else
                    {
                        genzai = new Vector2(2.5f, 2.5f);
                    }
                }
            }
            //RB2.constraints =
            //RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
            plsave = player2Flag;



            anim2.SetBool("idol", false);
            anim2.SetBool("dash", false);
            anim2.SetBool("ziseki_up", false);
            anim2.SetBool("taiki", true);

        }
        
    }


    void Chen()
    {
        if (!fieldflag)
        {
            if (Time.timeScale != 0 && ziryokuf)
            {

                system.inputkey();
                //----------------------------------------------------------------------------------プレイヤーの切り替え
                if (Input.GetKeyUp(KeyCode.C))
                {
                    if (player2.transform.parent == null && transform.parent == null)
                    {
                        fieldflag = false;
                        aa.SetActive(false);
                        music.SE(1);
                        player2Flag = !player2Flag;
                    }
                }
            }
        }
    }

    //地面にぶつかった瞬間に呼び出される
    void OnCollisionEnter2D(Collision2D col)
    {

        system.Change(col);

        if (!player2Flag)
        {
            if (!ziryokuf)
            {
                if (hani.ofa != null)
                {
                    if (col.gameObject == hani.ofa)
                    {
                        if (hani.ofa.transform.parent != null)
                        {
                            if (osihiki)
                                ZXtt = 0;
                            hitflag = false;
                        }
                    }
                }
            }
        }
    }
}