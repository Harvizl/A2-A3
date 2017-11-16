using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player2 : MonoBehaviour {

	public bool faceLeft;
	public bool faceRight;
	public bool aimUp;
	public bool aimUpLeft;
	public bool aimUpRight;
	public bool canFly;
	public bool canJump;
	public bool canShoot;
	public bool isGrounded;

	public bool walkLeft;
	public bool walkRight;
	public bool canSprintLeft;
	public bool canSprintRight;

	public float flyLift = 100f;
	public float jumpForce = 1200f;
	public float flyTimer;
	public float nextFire;
	public float fireRate = 100f;
	public float maxSpeed = 2f;
	public float maxWalkSpeed = 1f;
	public float gravity = 30f;
	public float slowdown = 2f;

	float distToGround;
	public Rigidbody rb;
	public GameObject projectile;

	public static int shot = 0;

	public int flyBuffDUration = 5;

	bool IsGrounded()
	{
		return Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.1f);
	}

	//If items or goal is touched
//	void OnTriggerEnter(Collider other)
//	{
//		if (other.tag == "Fly Buff")
//		{
//			canFly = true;
//			if (flyTimer > 0)
//				flyTimer += flyBuffDuration;
//			else
//				flyTimer = flyBuffDuration;
//			GameManager.instance.score = GameManager.instance.score + 10;
//			other.gameObject.SetActive(false);
//		}
//		if (other.tag == "Fire Buff")
//		{
//			canShoot = true;
//			GameManager.instance.score = GameManager.instance.score + 10;
//			other.gameObject.SetActive(false);
//		}
//		if (other.tag == "Goal")
//		{
//			print("You Win!");
//			GameManager.instance.score += 500;
//			SceneManager.LoadScene(2);
//		}
//		if (other.tag == "Death Plane")
//		{
//			print("You Fell");
//			StartCoroutine(DeathByFall());
//		}
//		if (other.tag == "Ground")
//		{
//			rb.constraints = RigidbodyConstraints.FreezePositionY;
//		}
//		else
//			rb.constraints = RigidbodyConstraints.None | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY |
//				RigidbodyConstraints.FreezeRotationZ;
//	}

	// Use this for initialization
	void Start () 
	{
		Time.timeScale = 1;
		distToGround = 0.5f * transform.localScale.y;
	}
	
	// Update is called once per frame
	void Update () 
	{
		//Gravity
		//rb.AddForce(0, -60f, 0);

		isGrounded = IsGrounded();

		canSprintLeft = false;
		canSprintRight = false;
		walkLeft = false;
		walkRight = false;

		//Speed Limit
		if (Input.GetKey (KeyCode.A)) {
			walkLeft = true;
			if (Input.GetKey (KeyCode.LeftShift))
				canSprintLeft = true;
			else
				canSprintLeft = false;
		}
		if (Input.GetKey (KeyCode.D)) {
			walkRight = true;
			if (Input.GetKey (KeyCode.LeftShift))
				canSprintRight = true;
			else
				canSprintRight = false;
		}

		if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
		{
		    canJump = true;

		}	



	}

	void FixedUpdate ()
	{
		isGrounded = IsGrounded();

		//Jump
		if (canJump)
		{
//			rb.AddForce (0, jumpForce, 0);
			rb.velocity += 75f * Vector3.up;
			canJump = false;
		}

		//Move Left
		if (walkLeft)
		{
			rb.velocity += 1f * Vector3.left;
		}
		//Move Right
		if (walkRight)
		{
			rb.velocity += 1f * Vector3.right;
		}

		if (walkLeft || walkRight && !(canSprintLeft || canSprintRight)) {
			rb.velocity = new Vector3 (Mathf.Clamp (rb.velocity.x, -maxWalkSpeed, maxWalkSpeed), rb.velocity.y, rb.velocity.z);
		}

		//Sprint Left
		if (canSprintLeft)
		{
			rb.velocity += 5f * Vector3.left;

		}
		//Sprint Right
		if (canSprintRight)
		{
			rb.velocity += 5f * Vector3.right;

		}

		if (canSprintLeft || canSprintRight || walkLeft || walkRight) {
			rb.velocity = new Vector3 (Mathf.Clamp (rb.velocity.x, -maxSpeed, maxSpeed), rb.velocity.y, rb.velocity.z);
		}


		if (isGrounded == false)
		{
			rb.velocity += 5f * Vector3.down;
		}


		//Slows player down
		if (!(canSprintLeft || canSprintRight || walkLeft || walkRight)) {
			if (rb.velocity.x > 0) {
				rb.velocity = new Vector3 (Mathf.Max (rb.velocity.x - slowdown, 0f), rb.velocity.y, rb.velocity.z);
			}
			if (rb.velocity.x < 0) {
				rb.velocity = new Vector3 (Mathf.Min (rb.velocity.x + slowdown, 0f), rb.velocity.y, rb.velocity.z);
			}
		}
	}
}
