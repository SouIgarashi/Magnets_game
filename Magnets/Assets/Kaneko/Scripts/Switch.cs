using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Switch : MonoBehaviour {

    new SpriteRenderer renderer;
    BoxCollider2D col;
    public Sprite Defo;
    public Sprite Push;

    public bool openStay;

    public GameObject moveObject;
    Door door;

    private GameObject _child;//横判定の子オブジェクト

    public bool push;//踏まれてるかどうか
    
    void Start () {
        _child = transform.Find("Side").gameObject;
        col = GetComponent<BoxCollider2D>();
        renderer = GetComponent<SpriteRenderer>();
        door = moveObject.GetComponent<Door>();
        push = false;
	}
	
	void Update () {

        SpriteChange();//画像の切り替え

        ChaildChange();
        if (push)
        {
            door.Open();
        }
        else
        {
            door.Close();
        }


    }

    //横判定のつけ消し
    void ChaildChange()
    {
        if (push) _child.SetActive(false);
        else _child.SetActive(true);
    }

    //画像切り替え
    void SpriteChange()
    {
        if (push && renderer.sprite != Push) renderer.sprite = Push;
        else if (!push && renderer.sprite != Defo) renderer.sprite = Defo;
    }
    
    //踏まれたら動き出す-----------------------------------------------------
    private void OnCollisionEnter2D(Collision2D obj)
    {
        if (obj.gameObject.tag == "N_player" || obj.gameObject.tag == "S_player")
        {
            col.isTrigger = true;
            push = true;
            if (openStay) Destroy(gameObject,0.5f);
        }
    }

    public void OnCollisionStay2D(Collision2D obj)
    {
        if (obj.gameObject.tag == "N_player" || obj.gameObject.tag == "S_player")
        {
            col.isTrigger = true;
            push = true;

        }
    }

    public void OnCollisionExit2D(Collision2D obj)
    {
        if (obj.gameObject.tag == "N_player" || obj.gameObject.tag == "S_player")
        {
            push = false;
            col.isTrigger = false;
        }
    }

    //踏まれていたら動く-----------------------------------------------------
    public void OnTriggerEnter2D(Collider2D obj)
    {
        if (obj.gameObject.tag == "N_player" || obj.gameObject.tag == "S_player")
        {
            col.isTrigger = true;
            push = true;
        }
    }

    public void OnTriggerStay2D(Collider2D obj)
    {
        if (obj.gameObject.tag == "N_player" || obj.gameObject.tag == "S_player")
        {
            col.isTrigger = true;
            push = true;
        }
    }

    public void OnTriggerExit2D(Collider2D obj)
    {
        if (obj.gameObject.tag == "N_player" || obj.gameObject.tag == "S_player")
        {
            push = false;
            col.isTrigger = false;
        }
    }

    }
