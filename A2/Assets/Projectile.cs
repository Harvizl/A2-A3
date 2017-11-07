using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    public float fireBallDuration = 0.5f;
    public GameObject bullet;

    void Awake()
    {
        GetComponent<GameObject>();
    }

    void OnTiggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            Debug.Log("Hit");
            other.gameObject.SetActive(true);
            GameManager.instance.score = GameManager.instance.score + 50;
            Player.shot -= 1;
        }
    }

    // Use this for initialization
    void Start()
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



