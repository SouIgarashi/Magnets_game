using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalCheck : MonoBehaviour {

    public bool OK;//ゴールされている
    public bool N_OK;//Nがゴ－ルしている
    public bool S_OK;//Sがゴールしている
    float reTimer;//ゴール判定を消す時間
    bool resetFlag;//ゴール判定を消す判定

    //ゴールに対応させるものは？
    public bool n_Goal;
    public bool s_Goal;
    public bool fe_Goal;

	GameObject goalObject;

    //全員ゴールした場合の挙動
    GoalScript goalScript;

	// Use this for initialization
	void Start () {
        OK = false;
        N_OK = false;
        S_OK = false;
        resetFlag = false;
        reTimer = 0;
        goalScript = transform.root.gameObject.GetComponent<GoalScript>();
        gameObject.layer = 2;
	}
	
	// Update is called once per frame
	void Update () {

       if (resetFlag) Reset();//ゴール判定が出ている場合
		
	}

    //タイマーでゴール判定を消す
    public void Reset()
    {
        reTimer -= Time.deltaTime;
        if (reTimer <= 0)
        {
            if (OK)
            {
                OK = false;
            }
            else if (N_OK)
            {
                N_OK = false;
            }
            else if (S_OK)
            {
                S_OK = false;
            }
            resetFlag = false;
        }
    }

    //物が触れた瞬間呼び出されるトリガー-----------------------------------------------------
    public void OnTriggerEnter2D(Collider2D collision)
    {
        
        //NかSがゴール地点にたどり着いた場合
        
        
        if (collision.gameObject.tag == "N_player" && !OK && !N_OK && !S_OK && n_Goal)
        {
            N_OK = true;
        }

        if (collision.gameObject.tag == "S_player" && !OK && !N_OK && !S_OK && s_Goal)
        {
            S_OK = true;
        }

        if(collision.gameObject.tag == "zisyaku" && !OK && !N_OK && !S_OK && fe_Goal)
        {
            OK = true;
        }

		if(!goalObject)goalObject = collision.gameObject;
        

    }//--------------------------------------------------------------------------------------

    public void OnTriggerStay2D(Collider2D collision)
    {



		if (collision.gameObject.tag == "N_player" && !OK && !N_OK && !S_OK && n_Goal)
		{
			N_OK = true;
		}

		if (collision.gameObject.tag == "S_player" && !OK && !N_OK && !S_OK && s_Goal)
		{
			S_OK = true;
		}

		if(collision.gameObject.tag == "zisyaku" && !OK && !N_OK && !S_OK && fe_Goal)
		{
			OK = true;
		}

        //物を近付かせる
        //Nがゴール地点で操作キャラ出ない場合
        if (collision.gameObject.tag == "N_player" && player.player2Flag && N_OK)
        {
            collision.transform.position = transform.position;
            //collision.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        }
        else if (collision.gameObject.tag == "S_player" && !player.player2Flag && S_OK)
        {
            collision.transform.position = transform.position;
            //collision.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        }
        else if (collision.gameObject.tag == "zisyaku" && collision.gameObject.transform.root.gameObject == collision.gameObject && OK)
        {
            collision.transform.position = transform.position;
            collision.transform.rotation = Quaternion.Euler(0, 0, 0);
            //collision.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            //collision.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        }

    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        

        
        //物が離れたら判定を消す
        if (OK)
        {
            OK = false;
        }
        else if (N_OK)
        {
            N_OK = false;
        }
        else if (S_OK)
        {
            S_OK = false;
        }
        
		goalObject = null;

    }

	public void GameClear(){
		goalObject.transform.position = transform.position;
		//goalObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
	}
    

}
