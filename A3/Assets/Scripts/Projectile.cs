using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
	public Player2 player;
    public float fireBallDuration = 1f;

	float aliveTimer = 0f;

	public AudioClip hurt;


    // Update is called once per frame
    void Update()
    {
		aliveTimer += Time.deltaTime;
		if (aliveTimer > fireBallDuration)
		{
			player.shot--;
			Destroy (gameObject);
		}
    }

    void OnTriggerEnter (Collider other)
    {
        if (other.tag == "Enemy")
        {
			AudioSource audio = GetComponent<AudioSource> ();
			audio.PlayOneShot (hurt);
            Debug.Log("Hit");
            other.gameObject.SetActive(false);
			Destroy(gameObject, 0.01f);
            GameManager.instance.score = GameManager.instance.score + 50;
			player.shot--;
        }
        if (other.tag == "Wall")
        {
            Destroy(gameObject);
            other.gameObject.SetActive(false);
            GameManager.instance.score = GameManager.instance.score + 10;
			player.shot--;
        }
    }
}



