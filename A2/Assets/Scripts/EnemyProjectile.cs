﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour {

    public float fireBallDuration = 1f;
    public GameObject enemyBullet;

    // Use this for initialization
    void Start () {
		
	}

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        StartCoroutine(numberOfFireballs());
    }

    IEnumerator numberOfFireballs()
    {
        yield return new WaitForSeconds(fireBallDuration);
        Debug.Log("Player Missed");
        Destroy(enemyBullet.gameObject);
        Enemy.enemyShot -= 1;
        Enemy2.enemyShot -= 1;
    }

    
        
    


    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Player Hit");
            Destroy(gameObject);
            Enemy.enemyShot -= 1;
            Enemy2.enemyShot -= 1;
        }
    }
}