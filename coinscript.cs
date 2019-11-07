using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coinscript : MonoBehaviour {
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

            hud.changeMoney(50);

        }
    }

    // Update is called once per frame
    void Update()
    {
        isColliding = false;
    }
}
