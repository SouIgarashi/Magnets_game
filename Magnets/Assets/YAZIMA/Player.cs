using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
	
	Vector2 Y_Start_posi;
	Vector2 Y_End;
	Vector2 X_Start_posi;
	Vector2 X_End;

	int[] move_rireki = new int[4];

	public LayerMask[] Lay;
	float Lay_length =1.3f;

	int S_to_E = 1;
    bool oriruyo = false;

    //float time =-1;

    public bool lide = false;
    public GameObject ppp; 

    float changeRed = 1.0f;
    float changeGreen = 0.0f;
    float cahngeBlue = 0.0f;
    float cahngeAlpha = 1.0f;

	void Start()
	{

	}


	void Update()
	{

        bool grounded_S_X = Physics2D.Linecast(X_Start_posi, X_End, Lay[5]);
        bool grounded_S_Y = Physics2D.Linecast(Y_Start_posi, Y_End, Lay[5]);
        bool grounded_E_X = Physics2D.Linecast(X_Start_posi, X_End, Lay[4]);
        bool grounded_E_Y = Physics2D.Linecast(Y_Start_posi, Y_End, Lay[4]);

        if (lide && oriruyo==false)
        {
            changeRed = 1.0f;
            changeGreen = 0.0f;
            cahngeBlue = 0.0f;
            cahngeAlpha = 1.0f;

            this.GetComponent<SpriteRenderer>().color = new Color(changeRed, changeGreen, cahngeBlue, cahngeAlpha);
        }

        if (lide && oriruyo)
        {
            Debug.Log("!!!");
            if (Input.GetKey(KeyCode.S))
            {
                changeRed = 1.0f;
                changeGreen = 1.0f;
                cahngeBlue = 1.0f;
                cahngeAlpha = 1.0f;

                Vector2 nowp = this.transform.position;
                nowp.y += 2;
                Instantiate(ppp, nowp, this.transform.rotation);
                oriruyo = false;
                lide = false;

                this.GetComponent<SpriteRenderer>().color = new Color(changeRed, changeGreen, cahngeBlue, cahngeAlpha);

                
            }
        }

		Y_Start_posi = new Vector2 (transform.position.x,transform.position.y+Lay_length);
		Y_End = new Vector2 (transform.position.x,transform.position.y-Lay_length);

		X_Start_posi = new Vector2 (transform.position.x+Lay_length,transform.position.y);
		X_End = new Vector2 (transform.position.x-Lay_length ,transform.position.y);

        if (lide)
        {
            if (S_to_E == 0)
                Use_move_foce();
            if (S_to_E == 2)
            {
                Use_move_revers();
            }

            if (Input.GetKey(KeyCode.Space) && S_to_E == 1 && (grounded_E_X || grounded_E_Y))
            {
                S_to_E = 2;
            }

            if (Input.GetKey(KeyCode.Space) && S_to_E == 1 && (grounded_S_X || grounded_S_Y))
            {
                S_to_E = 0;
            }
        }
	}

	public void Use_move_revers()
	{
		Y_Start_posi = new Vector2 (transform.position.x,transform.position.y+Lay_length);
		Y_End = new Vector2 (transform.position.x,transform.position.y-Lay_length);

		X_Start_posi = new Vector2 (transform.position.x+Lay_length,transform.position.y);
		X_End = new Vector2 (transform.position.x-Lay_length ,transform.position.y);

		bool Y_grounded_R = Physics2D.Linecast(Y_Start_posi,Y_End, Lay[9]);
		bool Y_grounded_L = Physics2D.Linecast(Y_Start_posi,Y_End, Lay[8]);

		bool X_grounded_U = Physics2D.Linecast(X_Start_posi,X_End,Lay[6]);
		bool X_grounded_D = Physics2D.Linecast(X_Start_posi,X_End,Lay[7]);

		bool grounded_end_X = Physics2D.Linecast(X_Start_posi,X_End,Lay[5]);
		bool grounded_end_Y = Physics2D.Linecast(Y_Start_posi,Y_End, Lay[5]);

		if (Y_grounded_R) {
			GetComponent<Rigidbody2D> ().AddForce (Vector2.right * 50);
			Debug.Log ("R");
			move_rireki [0] += 1;
		} else if (Y_grounded_L) {
			GetComponent<Rigidbody2D> ().AddForce (Vector2.left * 50);
			Debug.Log ("L");
			move_rireki [1] += 1;
		} else if (X_grounded_U) {
			GetComponent<Rigidbody2D> ().AddForce (Vector2.up * 50);
			Debug.Log ("U");
			move_rireki [2] += 1;
		} else if (X_grounded_D) {
			GetComponent<Rigidbody2D> ().AddForce (Vector2.down * 80);
			Debug.Log ("D");
			move_rireki [3] += 1;
		}
		if (grounded_end_X ||grounded_end_Y) {
			GetComponent<Rigidbody2D> ().velocity = Vector2.zero;
			S_to_E =1;
            oriruyo = true;
        }

		Debug.DrawLine(Y_Start_posi, Y_End, Color.green, 3, false);
		Debug.DrawLine(X_Start_posi, X_End, Color.red, 3, false);

		Debug.Log (move_rireki [0]);

	}

	public void Use_move_foce()
	{
		Y_Start_posi = new Vector2 (transform.position.x,transform.position.y+Lay_length);
		Y_End = new Vector2 (transform.position.x,transform.position.y-Lay_length);

		X_Start_posi = new Vector2 (transform.position.x+Lay_length,transform.position.y);
		X_End = new Vector2 (transform.position.x-Lay_length ,transform.position.y);

			bool Y_grounded_R = Physics2D.Linecast(Y_Start_posi,Y_End, Lay[0]);
			bool Y_grounded_L = Physics2D.Linecast(Y_Start_posi,Y_End, Lay[1]);

			bool X_grounded_U = Physics2D.Linecast(X_Start_posi,X_End,Lay[2]);
			bool X_grounded_D = Physics2D.Linecast(X_Start_posi,X_End,Lay[3]);

			bool grounded_end_X = Physics2D.Linecast(X_Start_posi,X_End,Lay[4]);
			bool grounded_end_Y = Physics2D.Linecast(Y_Start_posi,Y_End, Lay[4]);

		if (Y_grounded_R) {
			GetComponent<Rigidbody2D> ().AddForce (Vector2.right * 50);

			move_rireki [0] += 1;
		} else if (Y_grounded_L) {
			GetComponent<Rigidbody2D> ().AddForce (Vector2.left * 50);

			move_rireki [1] += 1;
		} else if (X_grounded_U) {
			GetComponent<Rigidbody2D> ().AddForce (Vector2.up * 50);

			move_rireki [2] += 1;
		} else if (X_grounded_D) {
			GetComponent<Rigidbody2D> ().AddForce (Vector2.down * 80);

			move_rireki [3] += 1;
		}
		if (grounded_end_X ||grounded_end_Y) {
			GetComponent<Rigidbody2D> ().velocity = Vector2.zero;
			S_to_E =1;
            oriruyo = true;
        }

		Debug.DrawLine(Y_Start_posi, Y_End, Color.green, 3, false);
		Debug.DrawLine(X_Start_posi, X_End, Color.red, 3, false);

		Debug.Log (move_rireki [0]);
		
	}


}
