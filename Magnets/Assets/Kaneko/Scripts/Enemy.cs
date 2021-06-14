using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour {

	[SerializeField]
	Vector2 move;//動く距離

	[SerializeField]
	float timerScale;//何秒で動くか

	[SerializeField]
	float stopTimer;//動作切り替えの停止時間

	bool stopFlag;//切り替え時の停止
	float stopTime;//停止中実際に動かす時間

	Vector3 startPosition;
	Vector3 endPosition;

	float moveTimer;//0~1で動く時間
	bool moveChange;//動作切り替え　true=進む　false=戻る
	Vector2 scl;
	private Animator anim;
	private float time;
	private bool daiflag;
    private GoalScript Goal;

    Animator animator;

    // Use this for initialization
    void Start ()
    { time = 0.4f;
        daiflag = false;
        anim = GetComponent<Animator>();
        moveTimer = 0;
        moveChange = true;

        stopTime = 0;
        stopFlag = false;
        scl = transform.localScale;
        Goal = GameObject.Find("Goal").GetComponent<GoalScript>();
        startPosition = transform.position;
        endPosition = transform.position + new Vector3(move.x, move.y, 0);
		animator = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
        if (!Goal.gameClear)
        {
            on();
        }
        else
        {
            animator.SetInteger("enemyCon", 0);
        }
		
    }

    void on()
    {
        if (!daiflag)
        {
            //動作の切り替え指示
            if (moveTimer < 0) { moveChange = true; moveTimer = 0; stopFlag = true; }
            else if (moveTimer > 1) { moveChange = false; moveTimer = 1; stopFlag = true; }

            //切り替え直後は停止
            if (stopFlag) stop();
            else
            {
                if (moveChange) { moveTimer += Time.deltaTime / timerScale; animator.SetInteger("enemyCon", 2); }
                else { moveTimer -= Time.deltaTime / timerScale; animator.SetInteger("enemyCon", 1); }

                transform.position = Vector3.Lerp(startPosition, endPosition, moveTimer);
            }
        }
        else
        {
            time -= Time.deltaTime;
            if (time <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

    //一時停止
    void stop()
    {
        animator.SetInteger("enemyCon", 0);
        stopTime += Time.deltaTime;
        if (stopTime >= stopTimer)
        {
            stopTime = 0;
            stopFlag = false;
        }
    }

    //主人公との当たり時
    private void OnCollisionEnter2D(Collision2D collision) {
		if (collision.gameObject.GetComponent<kabehit>() != null) {
			daiflag = true;
			GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
			animator.SetBool("dai", true);

		}
		else if (collision.gameObject.tag == "S_player" || collision.gameObject.tag == "N_player") {
			SceneManager.LoadScene("stagesentaku");
		}
    }
}
