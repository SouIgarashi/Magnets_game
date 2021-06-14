using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class music : MonoBehaviour {

    private AudioSource SE1;
    private AudioSource SE2;
    private AudioSource SE3;
    private AudioSource SE4;
    private AudioSource SE5;
    private AudioSource SE6;
    private AudioSource SE7;
    private AudioSource SE8;
    private AudioSource SE9;
    private AudioSource SE10;
    private AudioSource SE11;


    // Use this for initialization
    void Start () {
        AudioSource[] audio = GetComponents<AudioSource>();
        SE1 = audio[0];
        SE2 = audio[1];
        SE3 = audio[2];
        SE4 = audio[3];
        SE5 = audio[4];
        SE6 = audio[5];
        SE7 = audio[6];
        SE8 = audio[7];
        SE9 = audio[8];
        SE10 = audio[9];
        SE11 = audio[10];
        SE(9);

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SE(int No)
    {
        switch (No)
        {
            case 1:
                SE1.PlayOneShot(SE1.clip);
                break;
            case 2:
                SE2.PlayOneShot(SE2.clip);
                break;
            case 3:
                SE3.PlayOneShot(SE3.clip);
                break;
            case 4:
                SE4.PlayOneShot(SE4.clip);
                break;
            case 5:
                SE5.PlayOneShot(SE5.clip);
                break;
            case 6:
                SE6.PlayOneShot(SE6.clip);
                break;
            case 7:
                SE7.Play();
                break;
            case 8:
                SE8.PlayOneShot(SE8.clip);
                break;
            case 9:
                SE9.Play();
                break;
            case 10:
                SE10.PlayOneShot(SE10.clip);
                break;
            case 11:
                SE11.PlayOneShot(SE11.clip);
                break;
        }


    }
    public void stop()
    {
        SE7.Stop();
    }

}
