using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

    private Text gameText;
    public static int score;

    void Awake ()
    {
        
    }

	// Use this for initialization
	void Start () {
        gameText = gameObject.GetComponent<Text>();
        score = 0;
    }
	
	// Update is called once per frame
	void Update ()
    {
        gameText.text = "Score: " + score;
	}
}
