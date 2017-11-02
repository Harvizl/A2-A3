using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{

    public float originalX;
    public float amplitude = 5f;
    public float frequency = 1f;
    public Rigidbody rb;
    
	void OnCollisionEnter (Collision other)
	{
		if (other.gameObject.tag == "Player")
		{
			Time.timeScale = 0;
            SceneManager.LoadScene(4);
		}
	}

	//Use this for initialization
	void Start ()
	{
        originalX = transform.position.x;
        rb = GetComponent<Rigidbody>();
    }
	
	//Update is called once per frame
	void Update ()
	{
        rb.MovePosition(new Vector3(originalX + amplitude * Mathf.Sin(frequency * 1f * Mathf.PI * Time.time), transform.position.y, transform.position.z));
    }

	

    /*if (Collider.tag == "Player")
    {
        print("Game Over");
yield return new WaitForSeconds(5);
Application.LoadLevel(0);
    }*/




}