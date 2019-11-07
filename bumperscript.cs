using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bumperscript : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collision)
    {
       if(collision.tag == "Player")
        {
            Application.LoadLevel(2);
            return;

        }
        if (collision.gameObject.transform.parent)
        {
            Destroy(collision.gameObject.transform.parent.gameObject);

        }

        else
        {
            Destroy(collision.gameObject);
        }
    }
}

