using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets._2D;

public class AI_Controller : MonoBehaviour
{
    public bool AIisrunning;
    public Agent myagent;
    public int agent_index;
    // Start is called before the first frame update
    void Start()
    {
        AIisrunning = false;
        //myagent = new Agent();
       
        if (!AIisrunning)
        {
            myagent.CreatenewAgent();
           // myagent.initAgent();
            AIisrunning = true;
            // myagent.
            agent_index = myagent.roundnr;


        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        agent_index = myagent.roundnr;

       // if (myagent.InGround)
        //{
            myagent.getcurrAction();
        //}
       //myagent.getcurrAction();
        if (myagent.hasbeenhit)
        {
           // myagent.initAgent();
           // Application.LoadLevel(Application.loadedLevel);
        }

    }
}
