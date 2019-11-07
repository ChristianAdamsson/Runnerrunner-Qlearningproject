using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace UnityStandardAssets._2D
{
    [RequireComponent(typeof(PlatformerCharacter2D))]
    public class Agent : MonoBehaviour
    {
        public int roundnr = 0;
        public float highscore;
        public bool canjump;
        public bool shouldstandup;
        public float time;
        private List<Agent> myagents;
        public Agent curr_agent;
        public int NrofTrainingRounds = 1000;
        public int NrofLearningRounds = 100000;
        public const int NR_OF_Actions = 3;
        public bool isExploring = true;
        public bool isLearning = true;
        public float MinRate = 0.1f;
        public float ExplorationRate = 0.4f;
        public float LearningRate = 0.5f;
        int agentIndex = 0;
        float gamma = 0.99f;
        int newstate, currState = 0;
        public const int NrOfFeatures = 3;
        public float[][] Qtable;
        public int totstates;
        public float lastaction = 1;

        public float[][] visitedState;
        public int[] features;
        Platformer2DUserControl playercontrol;
        PlatformerCharacter2D mychar;
        public bool InGround;
        public int distanceinx;
        public int distanceiny;
        public bool hasbeenhit;
        GameObject realplayer;
        public int testint;

        HUDscript playerinfo;
        public float score; 

        // Start is called before the first frame update
        void Start()
        {
            totstates = NrOfFeatures * NrOfFeatures;
            playerinfo = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<HUDscript>();
            playercontrol = GetComponent<Platformer2DUserControl>();
            mychar = GetComponent<PlatformerCharacter2D>();
             CreatenewAgent();
            //curr_agent = this;
            testint = 0;
            hasbeenhit = false;
            realplayer = GameObject.FindGameObjectWithTag("Player");
            myagents = new List<Agent>();
            canjump = true;
            //Mathf.Pow(NrOfFeatures, 2);
        }
        public void CreatenewAgent()
        {
            Debug.Log("vi kom in i createnewAgent");
            roundnr = 0;
            highscore = 0;

            Qtable = new float[totstates][];
            for (int state = 0; state < totstates; state++)
            {
                Qtable[state] = new float[NR_OF_Actions];
                for (int action = 0; action < NR_OF_Actions; action++)
                {
                    Qtable[state][action] = 0;
                    //Qtable.Add(state, action);
                }
            }
            Debug.Log("qtable lenght: " + Qtable.Length);
            visitedState = new float[totstates][];
            for (int state = 0; state < totstates; state++)
            {
                visitedState[state] = new float[NR_OF_Actions];
                for (int action = 0; action < NR_OF_Actions; action++)
                {
                    visitedState[state][action] = 0;
                }
            }
            /* features = new int[3] { 1, 2, 3 };
             //features.SetValue(1, )
             Debug.Log("vi bör sätta värden i features");
              features[0] = 1;
              features[1] = 2;
             features[2] = 3;
             lastaction = 1;
             calculateFeatures();
             currState = getState(features);
             roundnr++;
             initAgent();
             */

            //curr_agent: curr_agent.roundnr = roundnr; curr_agent.highscore = highscore; curr_agent.agentIndex = myagents.Count; curr_agent.Qtable = Qtable;

            //myagents.Add(curr_agent);
            /*curr_agent =;
            curr_agent.roundnr = this.roundnr;
            curr_agent.highscore = this.highscore;
            curr_agent.agentIndex = myagents.Count;
            curr_agent.Qtable = Qtable;
            myagents.Add(curr_agent);
            */
            initAgent();
           // curr_agent = this;
            //features = new int[3] { 1, 1, 1 };


        }
        public void initAgent()
        {


            //curr_agent = GetComponent<Agent>();
            //myagents.Add(curr_agent);
            
            
            features = new int[NrOfFeatures] { 1, 1 , 1};
            //features.SetValue(1, )
            
           // features[0] = 1;
           // features[1] = 2;
            //features[2] = 3;
            lastaction = 1;
            calculateFeatures();
            currState = getState(features);
            roundnr++;
            
        }


        public void getcurrAction()
        {
            // Evaluate the new state
            calculateFeatures();
            newstate = getState(features);

            // update qTable for the last action since we now know if the 
            // last action was bad or good (died or survived)
           /* Debug.Log("currstate: " + currState);
            Debug.Log("newstate: " + newstate);
            Debug.Log("lastaction: " + lastaction);
            Debug.Log("qtable lenght: " + Qtable.Length);
            */
            updateQtable(currState, lastaction, newstate);
            //Debug.Log("we should reload the scene");
            string debugmessage = "";
            if (hasbeenhit)
             {
               // Debug.Log("we should reload the scene");
                for (int state = 0; state < totstates; state++)
                {
                    //Debug.Log("Q[1]" + Qtable[1]);
                    //Qtable[state] = new float[NR_OF_Actions];
                    for (int action = 0; action < NR_OF_Actions; action++)
                    {
                        //Qtable[state][action] = 0;
                        // Debug.Log("Q-värde:" + Qtable[0][0] + "  ");
                        //Qtable.Add(state, action);
                        debugmessage += "-  " + Qtable[state][action];
                    }
                    debugmessage += "new line";
                    
                }
                Debug.Log(debugmessage);
                //Debug.Log("Qtable: " + Qtable[][1] + "Qtable2: " + Qtable[4][0]);
                // Application.LoadLevel(Application.loadedLevel);
                playerinfo.playerHealth = 100;
                playerinfo.playerScore = 0;
                hasbeenhit = false;
                
                roundnr++;
                //UnityEngine.SceneManagement.SceneManager.LoadScene(0);
            }
             
             

            // Update current state to the new state
            currState = newstate;

            // Choose and do action according to the new state
            float actionIdx = decideonAction(currState);
            
            DoAction(actionIdx);

            // Update the old action to the new action
            lastaction = actionIdx;

            //if (StateMain.getGameScore() > best_score)
           /* if ((int)playerinfo.playerScore > highscore)
            * 
            {
                highscore = (int)playerinfo.playerScore;
            }
            */
            if(playerinfo.playerScore > highscore)
            {
                highscore = playerinfo.playerScore;
            }
        }

        public float decideonAction(int state)
        {
            float actionid = 0;

            if(!InGround || transform.position.y > 0)
            {
                return 0;
            }
            //Debug.Log("vi printar rand");
            if (isExploring)
            {
                float rand = Random.Range(0f, 1f);
               // Debug.Log("vi printar rand" + rand);
                if (rand < ExplorationRate)
                {
                    //act randomly
                    actionid = Mathf.Floor(Random.Range(0f, 1f) * NR_OF_Actions);
                    //Debug.Log("vi printar actionid" + actionid);
                }
                //else act according to the policy
                else
                {
                    float[] qstate = Qtable[state];
                    //actionid = (int)qstate[(int)Mathf.Max(qstate)];
                    // float maxind = qstate[(int)Mathf.Max(qstate)];
                    //actionid = qstate[(int)Mathf.Max(qstate)];

                    float newstatemax = Mathf.Max(qstate);

                    for (int i = 0; i < qstate.Length; ++i)
                    {
                        if (qstate[i] == newstatemax)
                        {
                            actionid = i;
                        }
                    }
                    //Debug.Log("vi printar qstate max" + qstate[(int)Mathf.Max(qstate)]);
                    //needs fixing
                    //actionid = Mathf.Floor(actionid);
                   // actionid = (int)Mathf.Max(Qtable[state]);
                    Debug.Log("vi printar actionid nmr2: " + actionid);
                }
                while (!InGround && actionid == 1) 
                 {
                    ; //do nothing
                 }
               //  while (!shouldstandup)
                //{
                  //  ;
                //}
            
            
            if (roundnr > NrofTrainingRounds)
            {
                if(ExplorationRate > MinRate)
                {
                    ExplorationRate = ExplorationRate - 0.001f;
                }
            }
        }
            else
            {
                    float[] qstate = Qtable[state];
                    // Debug.Log("qstatemax: " + qstate[Mathf.Max(qstate)]);

                    // actionid = qstate[(int)Mathf.Max(qstate)];
                   // actionid = (int)Mathf.Max(Qtable[state]);
                   
                    float newstatemax = Mathf.Max(qstate);

                    for (int i = 0; i < qstate.Length; ++i)
                    {
                        if (qstate[i] == newstatemax)
                        {
                            actionid = i;
                        }
                    }
                }
            
            
            if (actionid < 0)
            {
                return 0;
            }
          /*  if (actionid > 1)
            {
                return 0;
            }
            */
            Debug.Log("action id före return: " + actionid);
           // Mathf.Floor(actionid);
           // Debug.Log("actionid 4 :" + actionid);
            return actionid;
        }

        public void DoAction(float actionid2)
        {
            Debug.Log("actionid2 : " + actionid2);
            int actionid = (int)actionid2;
            switch (actionid)
            {
                case 1:
                    Debug.Log("vi kom in i case 0 = jump");
                    //playercontrol.m_Jump = true;
                    if (canjump)
                    {
                        mychar.Move(1, false, true);
                        canjump = false;
                        Invoke("jumpagain", 0.2f);
                    }
                  
                    break;
                case 0:
                    playercontrol.m_Jump = false;
                    break;
                case 2:
                    Debug.Log("vi kom in i case 2 = dodge");
                    shouldstandup = false;
                    mychar.Move(1, true, false);
                   // canjump = false;
                    //Invoke("jumpagain", 0.4f);
                    Invoke("standagain", 0.7f);
                    
                    break;
                case 3:
                    break;

                    
            }
        }
       void jumpagain()
        {
            canjump = true;
        }
        void standagain()
        {
            shouldstandup = true;
        }
        public float giveReward()
        {
            if(hasbeenhit)
            {
                return -10;
            }
            float reward = 0.1f;
           /* if(realplayer.transform.position.y <= 0)
            {
                return reward + ;
            }
            */
            Debug.Log("reward" + reward);
            return reward;
            //return realplayer.transform.position.y/10;
        }

        public void calculateFeatures()
        {
            int id = 0;
            //Debug.Log("testar distance" + (int)playercontrol.distancex);
            //features[0] = (int)playercontrol.distancex;
            features[0] = distanceinx;
            features[2] = distanceiny;

            if (InGround)
            {
                features[1] = 1;
            }
            else
            {
                features[1] = 0;
            }
          
        }
        public int getState(int[] features)
        {
            int stateIndex = 0;
            if (features[0] < 6 && features[0] > 0) stateIndex += 1;          
            if (features[1] == 1) stateIndex += 2;
            if (features[2] > 0) stateIndex += 4;


            return stateIndex;
        }
        public void updateQtable(float Qstateid, float actionId, float newQstateId)
        {
            if (isLearning)
            { 
                    float[] qState = Qtable[(int)Qstateid];
                    float[] qnewstate = Qtable[(int)newQstateId];

                    //float maxqNew = qnewstate[(int)Mathf.Max(qnewstate)];
                    float maxqNew = 1;
                     maxqNew = Mathf.Max(qnewstate);

                  /*  for (int i = 0; i < qnewstate.Length; ++i)
                    {
                        if (qnewstate[i] == newstatemax)
                        {
                            maxqNew = i;
                        }
                    }
                    */
                   // Debug.Log("maxqnew: " + maxqNew);
                    float oldvalue = (1 - LearningRate) * qState[(int)actionId];
                    float newValue = LearningRate * (giveReward() + gamma * maxqNew);
                    // Debug.Log("oldvalue: " + oldvalue + "newValue : " + newValue);
                    qState[(int)actionId] = (oldvalue + newValue);

                    if (visitedState[(int)Qstateid][(int)actionId] > NrofLearningRounds)
                    {
                        if (LearningRate > MinRate)
                        {
                            LearningRate = LearningRate - 0.00001f;
                        }
                    }
                    visitedState[(int)Qstateid][(int)actionId]++;

                    
                }


        }
        public void savecurragent()
        {
            int currid = myagents.LastIndexOf(curr_agent);
            myagents[currid].roundnr = roundnr;
            myagents[currid].highscore = highscore;
            myagents[currid].Qtable = Qtable;
            string jsontext = JsonUtility.ToJson(myagents);
            string filedata = "agents = [" + jsontext + "]";
            
            

        }
        // Update is called once per frame
        void FixedUpdate()
        {
            time += Time.time;
            InGround = GetComponent<PlatformerCharacter2D>().m_Grounded;
            distanceinx = (int)playercontrol.distancex;
            /*if(distanceinx <= 0)
            {
                distanceinx = 14;
            }
            */
            distanceiny = (int)playercontrol.distancey;
            testint++;
            
            //calculateFeatures();
            //getcurrAction();
            //decideonAction();

            // getcurrAction();
            // int testaction = decideonAction(getState(features));
            // DoAction(testaction);
        }
    }
}
