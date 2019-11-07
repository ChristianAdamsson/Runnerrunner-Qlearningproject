using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDscript : MonoBehaviour {

    public float playerScore = 0;
    public float playerHealth = 100;
    int playerMoney = 0;
    public Slider healthSlider;
    public Image damageImage;
    public float flashSpeed = 2f;
    public Color flashColour = new Color(1f, 0f, 0f, 0.1f);
    public Color healthColour = new Color(0f, 1f, 0f, 0.1f);
    //public Agent test;
    

    AudioSource playerAudio;

    private void Awake()
    {
        Debug.Log(PlayerPrefs.GetInt("Highscore"));
        playerAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update () {
        playerScore += Time.deltaTime;
        if(playerMoney >= 200 && Input.GetKeyDown(KeyCode.S) && playerHealth < 100)
        {
            playerHealth = 100;
            playerMoney = playerMoney - 200;
            healthSlider.value = playerHealth;
        }
        //damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);

    }
  
    public void changeHealth(int amount)
    {
        
        playerHealth += amount;
        
        if (amount < 0)
        {
            //damageImage.color = flashColour;

            //playerAudio.Play();
        }
        else
        {
            //damageImage.color = healthColour;
            
        }

       

        //healthSlider.value = playerHealth;
        
        
        if (playerHealth <= 100)
        {
            
            if(PlayerPrefs.GetInt("Highscore") < (int)playerScore)
            {


                PlayerPrefs.SetInt("highscore", (int)playerScore);
            }
            //Application.LoadLevel(2);
            //return;
        }

        if (playerHealth >= 100)
        {
            playerHealth = 100;
            playerScore += 3;
        }
        

        }
    public void changeScore(int amount)
    {
        playerScore += amount;

    }
    public void changeMoney(int amount)
    {
        playerMoney += amount;

    }

    void OnDisable()
    {
        PlayerPrefs.SetInt("Score", (int)playerScore);

    }
    void OnGUI()
    {
        
       
        GUI.Label(new Rect(Screen.width / 2 - 40, 30, 100, 30), "Score: " + (int)(playerScore));

        GUI.Label(new Rect(10, 30, 100, 30), "health: " + (int)(playerHealth));

        //GUI.Label(new Rect(Screen.width - 100, 30, 100, 30), "Money: " + (int)(playerMoney));
        



    }
}
