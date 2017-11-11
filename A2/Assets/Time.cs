using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class time : MonoBehaviour {

    private Text timeText;

    private float roundedSeconds;
    private string txtSeconds;
    private string txtMinutes;
    
    public float timer;
    public float gameLength;

    // Use this for initialization
    void Start () {
        timeText = gameObject.GetComponent<Text>();

        gameLength = 60f;
        timer = gameLength;
    }
	
	// Update is called once per frame
	void Update () {

        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            Debug.Log("Time's up");
            //Resets timer
            timer = gameLength;
            Application.LoadLevel("0");
        }
        //Display timer
        roundedSeconds = Mathf.CeilToInt(timer);
        txtSeconds = (roundedSeconds % 100f).ToString("00");
        timeText.text = "Remaining time: " + txtMinutes + txtSeconds;
    }
}

