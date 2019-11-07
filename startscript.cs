using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class startscript : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {
       
    }

    // Update is called once per frame
    void OnGUI()
    {


        GUI.Label(new Rect(Screen.width / 2 - 40, 30, 200, 100), "Beer runner");


        GUI.Label(new Rect(Screen.width / 2 - 40, 150, 200, 300), "Jump = high pitch " + "\ncrouch = low pitch");



        if (GUI.Button(new Rect(Screen.width / 2 - 40, 300, 80, 30), "ready?"))
        {
            Application.LoadLevel(1);
            

        }

    }
}

