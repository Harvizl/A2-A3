﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

	public float fireBallDuration = 1f;


	void OnTriggerEnter (Collider other)
	{
		if (other.tag == "Enemy") {
			other.gameObject.SetActive (false);
            GameManager.instance.score = GameManager.instance.score + 50;
            Destroy (gameObject);
			Player.shot -= 1;
			}

	}

	// Use this for initialization
	void Awake () {
        StartCoroutine(numberOfFireballs());
    }
	
	// Update is called once per frame
	void Update () 
	{
		
}
	IEnumerator numberOfFireballs (){
		yield return new WaitForSeconds (fireBallDuration);
		Player.shot -= 1;
		}
	}



