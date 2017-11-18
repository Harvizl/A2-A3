using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    public float fireBallDuration = 2f;
    public GameObject bullet;

    void Awake()
    {
        GetComponent<GameObject>();
    }

    

    // Use this for initialization
    void Start()
    {

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
        Destroy(bullet.gameObject);
        Player2.shot -= 1;
    }

    void OnTriggerEnter (Collider other)
    {
        if (other.tag == "Enemy")
        {
            Destroy(gameObject);
            Debug.Log("Hit");
            other.gameObject.SetActive(false);
            GameManager.instance.score = GameManager.instance.score + 50;
            Player2.shot -= 1;
        }
        if (other.tag == "Wall")
        {
            Destroy(gameObject);
            other.gameObject.SetActive(false);
            GameManager.instance.score = GameManager.instance.score + 10;
            Player2.shot -= 1;
        }
    }
}



