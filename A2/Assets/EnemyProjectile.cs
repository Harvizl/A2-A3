using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour {

    public float fireBallDuration = 100f;
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
        Destroy(enemyBullet.gameObject);
        Enemy.enemyShot -= 1;
    }


    void OnTriggerEnter(Collider other)
    {
        
        if (other.tag == "Player")
        {
            Destroy(gameObject);
            Enemy.enemyShot -= 1;
            Time.timeScale = 0;
            Debug.Log("Game Over");
        }


    }
}
