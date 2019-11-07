using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets._2D;

public class healthdown : MonoBehaviour
{
    private bool isColliding = false;

    // Use this for initialization
    void Start()
    {

    }
    HUDscript hud;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isColliding)
        {
            return;
        }
        
        if (collision.tag == "Player")
        {
            isColliding = true;
            hud = GameObject.Find("Main Camera").GetComponent<HUDscript>();
            
            Destroy(this.gameObject);
            //Destroy(collision.gameObject);
            collision.GetComponent<Agent>().hasbeenhit = true;
            hud.changeHealth(-10);
           
        }
    }

    // Update is called once per frame
    void Update()
    {
        isColliding = false;
    }
}
