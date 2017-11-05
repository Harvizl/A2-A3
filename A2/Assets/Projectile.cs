using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    public float fireBallDuration = 0.5f;
    public GameObject bullet;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            other.gameObject.SetActive(false);
            GameManager.instance.score = GameManager.instance.score + 50;
            Destroy(gameObject);
            Player.shot -= 1;
        }
    }

    // Use this for initialization
    void Awake()
    {

    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(numberOfFireballs());
    }
    
    IEnumerator numberOfFireballs()
    {
        yield return new WaitForSeconds(fireBallDuration);
        Destroy(bullet.gameObject);
        Player.shot -= 1;
    }
}



