using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorMove : MonoBehaviour {

    [SerializeField]
    Vector2 move;//動く距離

    [SerializeField]
    float timerScale;//何秒で動くか

    [SerializeField]
    float stopTimer;//動作切り替えの停止時間

    bool stopFlag;//切り替え時の停止
    float stopTime;//停止中実際に動かす時間

    Vector3 startPosition;//スタートの位置
    Vector3 endPosition;//ゴールの位置

    float moveTimer;//0~1で動く時間
    bool moveChange;//動作切り替え　true=進む　false=戻る

	// Use this for initialization
	void Start () {
        moveTimer = 0;
        moveChange = true;

        stopTime = 0;
        stopFlag = false;

        startPosition = transform.position;
        endPosition = transform.position + new Vector3(move.x, move.y, 0);
	}
	
	// Update is called once per frame
	void Update () {

        //動作の切り替え指示
        if (moveTimer < 0) { moveChange = true; moveTimer = 0; stopFlag = true; }
        else if (moveTimer > 1) { moveChange = false; moveTimer = 1; stopFlag = true; }

        //切り替え直後は停止
        if (stopFlag) stop();
        else
        {
            if (moveChange) moveTimer += Time.deltaTime / timerScale;
            else moveTimer -= Time.deltaTime / timerScale;

            transform.position = Vector3.Lerp(startPosition, endPosition, moveTimer);
        }
	}

    //一時停止
    void stop()
    {
        stopTime += Time.deltaTime;
        if (stopTime >= stopTimer)
        {
            stopTime = 0;
            stopFlag = false;
        }
    }
}
