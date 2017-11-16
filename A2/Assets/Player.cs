﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float forceConst = 1750f;
    public float flyLift = 14;
    public bool canShoot;
	public bool canJump;
    public float canFly;
    public int flyBuffDuration = 5;
    float distToGround;
    public float speed;
    public Rigidbody rb;
    public GameObject projectile;
    public float nextFire;
    public float fireRate = 100f;
    public bool faceRight;
    public bool faceLeft;
    public bool canSprint;
    public static int shot = 0;
    Vector3 relativeOffset;
    public bool riding;
	public bool isInAir;
	Transform player;

//    void OnCollisionEnter(Collision player)
//    {
//		
//
//        if (player.transform.tag == "Platform") 
//        {
//            riding = true;
//            Debug.Log("Moonwalking");
//			transform.parent = player.transform;
//		}
//    }
//
//    void OnCollisionExit(Collision player)
//    {
//        if (player.transform.tag == "Platform")
//        {
//            riding = false;
//            transform.parent = null;
//            Debug.Log("I'm off");
//        }
//    }



	//Is on the ground
	bool IsGrounded ()
	{
		return Physics.Raycast (transform.position, Vector3.down, distToGround);
	}

	bool IsInAir ()
	{
		return Physics.Raycast (transform.position, Vector3.down, 5f);
	}

    //If items or goal is touched
    void OnTriggerEnter (Collider other)
	{
		if (other.tag == "Fly Buff")
        {
			if (canFly > 0)
				canFly += flyBuffDuration;
			else
				canFly = flyBuffDuration;
			GameManager.instance.score = GameManager.instance.score + 10;
			other.gameObject.SetActive (false);
		}
		if (other.tag == "Fire Buff") {
			canShoot = true;
			GameManager.instance.score = GameManager.instance.score + 10;
			other.gameObject.SetActive (false);
		}
		if (other.tag == "Goal") {
			print ("You Win!");
			GameManager.instance.score += 500;
		    SceneManager.LoadScene (2);
        }
 	}

	void FixedUpdate ()
	{
		if (canJump == false) {
			//Movement direction and speed during jump
			//float moveHorizontal = Input.GetAxis("Horizontal");
			//float moveVertical = Input.GetAxis("Vertical");
			//Vector3 movement = new Vector3(moveHorizontal, -15f, moveVertical);
			//rb.AddForce(movement * speed);
            
			//Gravity
			rb.AddForce (0, -60f, 0);
		}

        //Jump boolean
        if (canJump)
        {
            canJump = false;
		}
		if (IsGrounded ()) {
			canJump = true;
		}
        
        
		//Fly command
		if (canFly > 0f && Input.GetKey (KeyCode.Space)) {
			rb.AddForce (0, flyLift, 0);
		}
		if (Input.GetKey (KeyCode.A) && canJump == false) {
			//rb.AddForce (Vector3.left * 500f);
			rb.position += 10 * Time.deltaTime * Vector3.left;
		}
		if (Input.GetKey (KeyCode.D) && canJump == false){
			//rb.AddForce (Vector3.right * 500f);
			rb.position += 10 * Time.deltaTime * Vector3.right;
		}
    }
	// Use this for initialization
	void Start ()
	{		
		Time.timeScale = 1;
        distToGround = 0.5f * transform.localScale.y;
		canJump = true;
	}

    // Update is called once per frame
    void Update()
    {
		
        canFly -= Time.deltaTime;

        //Sprint toggle
        if (Input.GetKey(KeyCode.LeftShift) && IsGrounded()) {
            canSprint = true;
        }
        //Stops sprinting if not grounded
        else
            canSprint = false;
        //Jump command
        if (Input.GetKeyDown (KeyCode.Space) && IsGrounded ()) {
			canJump = true;
			rb.AddForce (0, forceConst, 0);
		}
		if (Input.GetKeyUp(KeyCode.Escape))
        {
            Application.Quit();
        }
        //Move Right
        if (Input.GetKey(KeyCode.D))
        {
            //transform.Translate (15 * Time.deltaTime * Vector3.right);
            rb.position += 15 * Time.deltaTime * Vector3.right;
            faceRight = true;
            faceLeft = false;
        }
        //Sprint Right
        if (Input.GetKey(KeyCode.D) && canSprint)
        {
            //transform.Translate (20 * Time.deltaTime * Vector3.right);	
            rb.position += 20 * Time.deltaTime * Vector3.right;
            faceRight = true;
            faceLeft = false;
        }
        //Move Left
        if (Input.GetKey(KeyCode.A))
        {
            //transform.Translate (15 * Time.deltaTime * Vector3.left);
            rb.position += 15 * Time.deltaTime * Vector3.left;
            faceLeft = true;
            faceRight = false;
        }
        //Sprint Left
        if (Input.GetKey(KeyCode.A) && canSprint)
        {
            //transform.Translate (20 * Time.deltaTime * Vector3.left);
            rb.position += 20 * Time.deltaTime * Vector3.left;
            faceLeft = true;
            faceRight = false;
        }
		//Jump
		if (Input.GetKeyDown(KeyCode.Space) && canSprint) 
		{
			if (faceLeft)
			{
				Debug.Log("Tears of Pride!");
				rb.AddForce(Vector3.up * 500f);
				rb.position += 50 * Time.deltaTime * Vector3.left;
				canJump = false;
			}
			else if (faceRight)
			{
				Debug.Log("MIND BULLETS");
				rb.AddForce(Vector3.up * 500f);
				rb.position += 50 * Time.deltaTime * Vector3.right;
				canJump = false;
			}

		}


		//Shoot command
		if (Input.GetKeyDown(KeyCode.W) && canShoot && shot < 2)
		{
			shot++;
			//Calls Projectile Prefab
			GameObject projectileInstance = Instantiate(projectile, transform.position, Quaternion.identity) as GameObject;
			Rigidbody projectileRb = projectileInstance.GetComponent<Rigidbody>();
			nextFire = Time.time + fireRate;
			//Shoots right if facing right
			if (faceRight)
			{
				Debug.Log("Shot Right");
				projectileRb.velocity += 150f * Vector3.right;
			}
			//Shoots left if facing left
			else if (faceLeft)
			{
				Debug.Log("Shot Left");
				projectileRb.velocity += 150f * Vector3.left;
			}
		}
    }
	//Fly buff toggle and timer
	//	IEnumerator canFlyTimer ()
	//	{
	//		canFly = true;
	//		yield return new WaitForSeconds (flyBuffDuration);
	//		canFly = false;
	//	}
}