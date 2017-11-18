using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour {

    
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
        NumberOfFireballs();
    }

     void NumberOfFireballs()
    {
        Debug.Log("Player Missed");
        Destroy(gameObject, 2);
        Enemy.enemyShot -= 1;
        Enemy2.enemyShot -= 1;
    }

   
    
        
    


    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Destroy(gameObject, 0);
            Debug.Log("Player Hit");
            Enemy.enemyShot -= 1;
            Enemy2.enemyShot -= 1;
        }
    }
}
