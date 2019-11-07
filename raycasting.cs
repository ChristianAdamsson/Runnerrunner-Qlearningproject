using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.Audio;
using System.Collections.Generic;
using UnityEngine.UI;

namespace UnityStandardAssets._2D
{
    [RequireComponent(typeof(PlatformerCharacter2D))]
    public class raycasting : MonoBehaviour
    {
        //Physics2D.Raycast myray;
        //Raycas
        public Transform sightStart, sightEnd, sightEnd2;
        public bool spotted1, spotted2;
        private PlatformerCharacter2D m_Character;
        public bool m_Jump;

        // Start is called before the first frame update
        void Start()
        {
            m_Character = GetComponent<PlatformerCharacter2D>();
            spotted2 = false;
            spotted1 = false;
        }

        // Update is called once per frame
        void Update()
        {
            Raycasting();
            Behaviours();
        }

        void Raycasting()
        {
            Debug.DrawLine(sightStart.position, sightEnd.position, Color.green);
            Debug.DrawLine(sightStart.position, sightEnd2.position, Color.red);
            //spotted1 = Physics2D.Linecast(sightStart.position, sightEnd.position, 1 << LayerMask.NameToLayer("obstacles"));
            spotted1 = Physics2D.Linecast(sightStart.position, sightEnd.position);
            spotted2 = Physics2D.Linecast(sightStart.position, sightEnd2.position);

            if (!m_Jump)
            {


                m_Character.Move(1f, spotted2, spotted1);
                // Read the jump input in Update so button presses aren't missed.
                m_Jump = spotted1;

            }
            

        }
        void Behaviours()
        {

        }
    }
}
