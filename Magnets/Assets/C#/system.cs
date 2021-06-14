using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class system : MonoBehaviour {
    private music music;//SE取得
    private GameObject ohinai;

    // Use this for initialization
    void Start () {
        music = GameObject.FindWithTag("System").GetComponent<music>();
        if (GameObject.FindWithTag("otitatoki") != null)
        {
            ohinai = GameObject.FindWithTag("otitatoki");
            ohinai.layer = 2;
        }
    }
	
	// Update is called once per frame
	void Update () {

      

	}

    public void Change(Collision2D OBJ)
    {

        if (OBJ.gameObject.tag == "sceneover")
        {
            scene scene = OBJ.gameObject.GetComponent<scene>();
            SceneManager.LoadScene("main"+scene.sceneno);
            
        }


        if (OBJ.gameObject.tag == "Start!!")
        {
            SceneManager.LoadScene("stagesentaku");
             
        }
        if (OBJ.gameObject.tag == "Exit!!")
        {
            Application.Quit();
             
        }

        if (OBJ.gameObject.tag == "hari")
        {
            SceneManager.LoadScene("stagesentaku");
             
        }
        if (OBJ.gameObject.tag == "otitatoki")
        {
            SceneManager.LoadScene("stagesentaku");
             
        }
    }
    public void inputkey()
    {
        if (Input.GetKey(KeyCode.Alpha1))
        {
             
            SceneManager.LoadScene("main1");
        }
        if (Input.GetKey(KeyCode.Alpha2))
        {
             
            SceneManager.LoadScene("main2");
        }
        if (Input.GetKey(KeyCode.Alpha3))
        {
             
            SceneManager.LoadScene("main3");
        }
        if (Input.GetKey(KeyCode.Alpha4))
        {
             
            SceneManager.LoadScene("main4");
        }
        if (Input.GetKey(KeyCode.Alpha5))
        {
             
            SceneManager.LoadScene("main5");
        }
        if (Input.GetKey(KeyCode.Alpha6))
        {
             
            SceneManager.LoadScene("main6");
        }
        if (Input.GetKey(KeyCode.Alpha7))
        {
             
            SceneManager.LoadScene("main7");
        }
        if (Input.GetKey(KeyCode.Alpha8))
        {
             
            SceneManager.LoadScene("main8");
        }
        if (Input.GetKey(KeyCode.Alpha9))
        {
             
            SceneManager.LoadScene("main9");
        }
        if (Input.GetKey(KeyCode.Alpha0))
        {
             
            SceneManager.LoadScene("main10");
        }
        if (Input.GetKey(KeyCode.F1))
        {
             
            SceneManager.LoadScene("main11");
        }
        if (Input.GetKey(KeyCode.F2))
        {
             
            SceneManager.LoadScene("main12");
        }
        if (Input.GetKey(KeyCode.F3))
        {

            SceneManager.LoadScene("main13");
        }
        if (Input.GetKey(KeyCode.F4))
        {

            SceneManager.LoadScene("main14");
        }
        if (Input.GetKey(KeyCode.F5))
        {

            SceneManager.LoadScene("main15");
        }
        if (Input.GetKey(KeyCode.F6))
        {

            SceneManager.LoadScene("main16");
        }


        if (Input.GetKey(KeyCode.Q))
        {//-------------------------------------------アプリケーション終了

            if (Input.GetKey(KeyCode.A))
            {
                if (Input.GetKey(KeyCode.Z))
                {
                    SceneManager.LoadScene("main00");
                }
            }
        }
        if (Input.GetKey(KeyCode.F11))
        {
             
            SceneManager.LoadScene("stagesentaku");
        }
        if (Input.GetKey(KeyCode.F12))
        {
             
            SceneManager.LoadScene("title");
        }

        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }//---------------------------------------------------------------------------------------------------------------------デバッグ用

    }
    public void ExitGame()
    {
        Application.Quit();
    }

    public void SceneLoad()
    {
         
        Application.LoadLevel("stagesentaku");
    }

}
