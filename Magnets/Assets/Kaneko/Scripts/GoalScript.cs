using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GoalScript : MonoBehaviour {

    int childCount;//子供の数
    int goalCount;//ゴールした数
    private bool n_Goal;//Nがゴールしたか
    private bool s_Goal;//Sがゴールしたか
    private GoalCheck[] childScripts = new GoalCheck[5];

    public bool gameClear;//クリアしたかどうか
    new SpriteRenderer renderer;
    [SerializeField]
    Sprite[] bgImage;
    [SerializeField]
    int loopNo;
    int bgCount;
    float changeTimer;

    Text goalText;//クリアテキスト


    // Use this for initialization
    void Start () {

        gameClear = false;

        n_Goal = false;
        s_Goal = false;

        //ゴールの数を数える
        childCount = transform.childCount;
        //childObject = GetComponentInChildren<GoalCheck[]>();
        
        //子供のゴールチェックを取得
        for (int i = 0; i < childCount; i++)
        {
            string a = i.ToString();
            childScripts[i] = GameObject.Find("GOAL" + a).GetComponent<GoalCheck>();

            //childObject[i] = transform.GetChild(i).gameObject;
            Debug.Log(childScripts[i].name);
        }
        //クリアのテキスト取得
        goalText = GameObject.Find("gameclearText").GetComponent<Text>();

        renderer = GameObject.Find("anime").GetComponent<SpriteRenderer>();
        bgCount = bgImage.Length;

    }
	
	// Update is called once per frame
	void Update () {

		if (gameClear) {
			Goal();
			return;
		}

        //全てゴールしている場合
        if (childCount == goalCount && n_Goal && s_Goal)
        {
            gameClear = true;
			for (int i = 0; i < childCount; i++) {
				childScripts [i].GameClear ();
			}
            return;
        }

        //ゴールしている数を調べる
        CheckCount();
        
	}

    //ゴールしている数を調べる
    void CheckCount()
    {
        goalCount = 0;
        n_Goal = false;
        s_Goal = false;

        //子供の数だけ繰り返す
        for (int i = 0; i < childCount; i++)
        {
            if (childScripts[i].N_OK)//Nが一つだけゴールしている
            {
                if (!n_Goal)
                {
                    n_Goal = true;
                    goalCount++;
                }
            }
            else if (childScripts[i].S_OK)//Sが一つだけゴールしている
            {
                if (!s_Goal)
                {
                    s_Goal = true;
                    goalCount++;
                }
            }
            else if (childScripts[i].OK)//キャラ以外がゴールしている
            {
                goalCount++;
            }
        }
    }

    //全てゴールしている場合
    void Goal()
    {
        goalText.enabled = true;
        

        changeTimer += 0.017f * 6;
        if (changeTimer >= bgCount) changeTimer = loopNo;
        renderer.sprite = bgImage[(int)changeTimer];

      


        if (Input.GetKey(KeyCode.Space))
        {
            SceneManager.LoadScene("stagesentaku");
        }
    }
}
