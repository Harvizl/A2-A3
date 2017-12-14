using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{

    public Text timeText;
    public int gameLength;

    public Text scoreText;
    public int score;
    

    // Use this for initialization
    void Start()
    {
		
    }
	void Awake()
	{
		InvokeRepeating("Updatescore", 0, 1);
		gameLength = 300;
	}

    void Updatescore()
    {
        if (gameLength > 0)
        {
            gameLength -= 1;
        }
    }
    // Update is called once per frame
    void Update()
    {
        //gameLength -= Time.deltaTime;

        timeText.text = "Timer: " + gameLength.ToString("0");
        scoreText.text = "Score: " + score;
        if (gameLength == 0)
        {
            Debug.Log ("Time's up!");
            Time.timeScale = 0;
            //Call time's up
			//GameObject timesUpScreen = GetComponent<>
        }
	}

    


    //	void StartGame()
    //	{
    //		if (State != GameState.restarting)
    //		{
    //			PlayerController player = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerController> ();
    //			player.Unfreeze ();
    //
    //			wonText.enabled = false;
    //			lostText.enabled = false;
    //
    //		}
    //	}
    
}

