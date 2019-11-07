using System;
using UnityEngine;
//using UnityEngine.microphone;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.Audio;
using System.Collections.Generic;
using UnityEngine.UI;

namespace UnityStandardAssets._2D
{
    [RequireComponent(typeof (PlatformerCharacter2D))]
    public class Platformer2DUserControl : MonoBehaviour
    {
        private PlatformerCharacter2D m_Character;
        public bool m_Jump;
        public Transform sightStart, sightEnd, sightEnd2;
        //sightEnd2;
        public bool spotted1, spotted2;
        public float res;
        RaycastHit2D myray;
        public float distancex;
        public float distancey;

         private float time;
        public int distancetonextobj;
        /* public int qSamples = 1024;  // array size
         float refValue = 0.1f; // RMS value for 0 dB
         //float threshold = 0.001f;      // minimum amplitude to extract pitch
         float pitchValue; // sound pitch - Hz


          float[] spectrum; // audio spectrum
          float[] fSample;
          int samplerate;

             */





        private void Awake()
        {
            m_Character = GetComponent<PlatformerCharacter2D>();
           
            //spotted2 = false;
            spotted1 = false;
            /* spectrum = new float[qSamples];
             samplerate = AudioSettings.outputSampleRate;

             AudioSource audio = GetComponent<AudioSource>();
             audio.clip = Microphone.Start(null, true, 1, 22050);
             audio.loop = true;
             while (!(Microphone.GetPosition(null) > 0)) { }
             Debug.Log("start playing... position is " + Microphone.GetPosition(null));
             audio.Play();
             */
        }
      


        private void Update()
        {

           
        }

        void Raycasting()
        {
            Debug.DrawLine(sightStart.position, sightEnd.position, Color.green);
            
            Debug.DrawLine(sightStart.position, sightEnd2.position, Color.red);
            //spotted1 = Physics2D.Linecast(sightStart.position, sightEnd.position, 1 << LayerMask.NameToLayer("obstacles"));
           
            LayerMask test3 = Physics2D.AllLayers;
            
             spotted1 = Physics2D.Linecast(sightStart.position, sightEnd.position);
            spotted2 = Physics2D.Linecast(sightStart.position, sightEnd2.position);
           // myray = Physics2D.Raycast(sightStart.position, sightEnd.position);
            //Physics2D.Raycast(sightStart.position, sightEnd.position, )
            
            
            if (spotted1)
            {
                //res = 0;
                myray = Physics2D.Linecast(sightStart.position, sightEnd.position);

                distancex = myray.collider.transform.position.x - gameObject.transform.position.x;
                //distancey = myray.collider.transform.position.y - gameObject.transform.position.y;
                distancey = 0;
                //Debug.Log("testar distance2" + distancex);
                //Physics2D.Linecast(sightStart.position, sightEnd.position, test3, res);
            }
            else if(spotted2){
                RaycastHit2D higherray = Physics2D.Linecast(sightStart.position, sightEnd2.position);
                distancex = higherray.collider.transform.position.x - gameObject.transform.position.x;
                distancey = higherray.collider.transform.position.y - gameObject.transform.position.y;

            }
            else
            {
                distancex = 0;
                distancey = 0;
            }
            spotted1 = false;
            spotted2 = false;
           // spotted2 = Physics2D.Linecast(sightStart.position, sightEnd2.position);

          /*  if (!m_Jump)
            {


                m_Character.Move(1f, spotted2, spotted1);
                // Read the jump input in Update so button presses aren't missed.
                m_Jump = spotted1;

            }
            */


        }


        private void FixedUpdate()
        {
            Raycasting();
            // Analyze();


            //time = Time.timeSinceLevelLoad;
           if (!m_Jump)
            {
                m_Jump = CrossPlatformInputManager.GetButtonDown("Jump");
            }
            //{
                /* if (pitchValue > 400)
                 {
                     m_Jump = true;
                 }
                 */
                /* if (spotted1)
                 {
                     m_Jump = spotted1;
                 }
                 else
                 {
                     m_Jump = false;

                 }
                 // Read the jump input in Update so button presses aren't missed.

                 */
             //   m_Jump = CrossPlatformInputManager.GetButtonDown("Jump");
            //}
        
            bool crouch = false;
            time += Time.deltaTime;
            // Read the inputs.
             crouch = Input.GetKey(KeyCode.LeftControl);
            /* if (spotted2)
             {
                  crouch = spotted2;
             }
             */
            // bool crouch = false;
            /*    if (pitchValue < 400 && pitchValue > 100)
             {
                 crouch = true;
             }

             */
            m_Character.Move(1, crouch, m_Jump);
            /*
            // make the player run faster
            if (time < 5)
            {
                m_Character.Move(1, crouch, m_Jump);
                //m_Jump = false;
            }
            else if (time < 15 || time < 30)
            {
                m_Character.Move(2f, crouch, m_Jump);
               // m_Jump = false;
            }
            else if (time < 50 || time < 100)
            {
                m_Character.Move(5f, crouch, m_Jump);
                //m_Jump = false;
            }
            else
            {
                m_Character.Move(10f, crouch, m_Jump);
                //m_Jump = false;
            }
            */
           
           // m_Jump = false;
        }
    }
}


