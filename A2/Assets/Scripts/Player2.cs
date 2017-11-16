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
    public bool shoot;
	public bool canShoot;
	public bool isGrounded;
	public bool walkLeft;
	public bool walkRight;
	public bool canSprintLeft;
	public bool canSprintRight;

	public float flyLift = 100f;
    public float flyBuffDuration = 5;
	public float jumpForce = 1200f;
	public float flyTimer;
	public float nextFire;
	public float fireRate = 100f;
	public float maxSpeed = 5f;
	public float maxWalkSpeed = 1f;
	public float gravity = 30f;
	public float slowdown = 2f;

	float distToGround;
	public Rigidbody rb;
	public GameObject projectile;
    public GameObject enemy2;

	public static int shot = 0;

	public int flyBuffDUration = 5;

	bool IsGrounded()
	{
		return Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.1f);
	}

    IEnumerator DeathByFall()
    {
        yield return new WaitForSeconds(2);
        Time.timeScale = 0;
    }

    void SpawnEnemy2()
    {
        GameObject enemy2Spawn = Instantiate(enemy2, new Vector3(336, 18, 0), Quaternion.identity) as GameObject;
    }

    //If items or goal is touched
    void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Fly Buff")
		{
			canFly = true;
			if (flyTimer > 0)
				flyTimer += flyBuffDuration;
			else
				flyTimer = flyBuffDuration;
			GameManager.instance.score = GameManager.instance.score + 10;
			other.gameObject.SetActive(false);
		}
    	if (other.tag == "Fire Buff")
		{
		    canShoot = true;
			GameManager.instance.score = GameManager.instance.score + 10;
			other.gameObject.SetActive(false);
            SpawnEnemy2();

        }
		if (other.tag == "Goal")
		{
			print("You Win!");
			GameManager.instance.score += 500;
			SceneManager.LoadScene(2);
		}
		if (other.tag == "Death Plane")
		{
			print("You Fell");
			StartCoroutine(DeathByFall());
		}
		if (other.tag == "Ground")
		{
			rb.constraints = RigidbodyConstraints.FreezePositionY;
		}
		else
			rb.constraints = RigidbodyConstraints.None | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY |
				RigidbodyConstraints.FreezeRotationZ;
	}

	// Use this for initialization
	void Start () 
	{
		Time.timeScale = 1;
		distToGround = 0.5f * transform.localScale.y;
        canShoot = false;
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
            faceLeft = true;
            faceRight = false;
            if (Input.GetKey (KeyCode.LeftShift))
				canSprintLeft = true;
			else
				canSprintLeft = false;
		}
		if (Input.GetKey (KeyCode.D)) {
			walkRight = true;
            faceRight = true;
            faceLeft = false;
            if (Input.GetKey (KeyCode.LeftShift))
				canSprintRight = true;
			else
				canSprintRight = false;
        }

		if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
		{
		    canJump = true;
        }

        if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            shoot = true;
        }
        else
        {
            shoot = false;
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
            rb.velocity = new Vector3(Mathf.Clamp(rb.velocity.x, -maxWalkSpeed, maxWalkSpeed), rb.velocity.y, rb.velocity.z);
        }
		//Move Right
		if (walkRight)
		{
            rb.velocity += 1f * Vector3.right;
            rb.velocity = new Vector3(Mathf.Clamp(rb.velocity.x, -maxWalkSpeed, maxWalkSpeed), rb.velocity.y, rb.velocity.z);

        }

		if (walkLeft || walkRight && !(canSprintLeft || canSprintRight)) {
			//rb.velocity = new Vector3 (Mathf.Clamp (rb.velocity.x, -maxWalkSpeed, maxWalkSpeed), rb.velocity.y, rb.velocity.z);
		}

		//Sprint Left
		if (canSprintLeft)
		{
			rb.velocity += 6f * Vector3.left;
            rb.velocity = new Vector3(Mathf.Clamp(rb.velocity.x, -maxSpeed, maxSpeed), rb.velocity.y, rb.velocity.z);

        }
		//Sprint Right
		if (canSprintRight)
		{
			rb.velocity += 6f * Vector3.right;
            rb.velocity = new Vector3(Mathf.Clamp(rb.velocity.x, -maxSpeed, maxSpeed), rb.velocity.y, rb.velocity.z);
        }

        //Gravity
		if (isGrounded == false)
		{
			rb.velocity += 5f * Vector3.down;
		}

        //Slows player down when not pressing direction
		if (!(canSprintLeft || canSprintRight || walkLeft || walkRight)) {
			if (rb.velocity.x > 0) {
				rb.velocity = new Vector3 (Mathf.Max (rb.velocity.x - slowdown, 0f), rb.velocity.y, rb.velocity.z);
			}
			if (rb.velocity.x < 0) {
				rb.velocity = new Vector3 (Mathf.Min (rb.velocity.x + slowdown, 0f), rb.velocity.y, rb.velocity.z);
			}
		}

        if (shoot && shot < 2)
        {
            shot++;
            //Calls Projectile Prefab
            GameObject projectileInstance = Instantiate(projectile, transform.position, Quaternion.identity) as GameObject;
            Rigidbody projectileRb = projectileInstance.GetComponent<Rigidbody>();
            nextFire = Time.time + fireRate;
            if (faceLeft)
            {
                projectileRb.velocity += 150f * Vector3.left;
            }
            else if (faceRight)
            {
                projectileRb.velocity += 150f * Vector3.right;
            }
        }
	}
}
