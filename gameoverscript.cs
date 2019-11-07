using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameoverscript : MonoBehaviour {
    int score = 0;
	// Use this for initialization
	void Start () {
        score = PlayerPrefs.GetInt("Score");
	}
	
	
	void OnGUI ()
    {


        GUI.Label(new Rect(Screen.width / 2 - 40, 80, 80, 30), "Game over");


        GUI.Label(new Rect(Screen.width / 2 - 40, 200, 80, 30), "Score: " + score);


        if (GUI.Button(new Rect(Screen.width / 2 - 40, 300, 80, 30), "try again?"))
        {
            Application.LoadLevel(1);

        }

	}
}
