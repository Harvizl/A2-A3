using System.Collections;
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
    private Rigidbody selfRigidbody;
    float distToGround;
	public float speed;
	public Rigidbody rb;
	public GameObject projectile;
	public float nextFire;
	public float fireRate = 0.1f;
	public float bulletSpeed = 30f;
	public bool faceRight;
	public bool faceLeft;
	public bool canSprint;
	public static int shot = 0;
    Vector3 relativeOffset;
    Transform player;
    public bool riding;
    
	Coroutine canFlyCoroutine;

	//Is on the ground
	bool IsGrounded ()
	{
		return Physics.Raycast (transform.position, Vector3.down, distToGround + 0.01f);
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

    void LateUpdate()
    {
        if (player && riding)
        {
            player.transform.position = this.transform.position + relativeOffset;
        }
    }

    void OnCollisionEnter(Collision player)
    {
        if (player.transform.tag == "Platform")
        {
            transform.parent = player.transform;
            relativeOffset = player.transform.position - this.transform.position;
        }
    }

    void OnCollisionExit (Collision player)
    {
        if(player.transform.tag == "Platform")
        {
            transform.parent = null;
        }
    }

	void FixedUpdate ()
	{
        if (canJump == false)
        {
            //Movement direction and speed during jump
            //float moveHorizontal = Input.GetAxis("Horizontal");
            //float moveVertical = Input.GetAxis("Vertical");
            //Vector3 movement = new Vector3(moveHorizontal, -15f, moveVertical);
            //rb.AddForce(movement * speed);
            
            //Gravity
            rb.AddForce(0, -60f, 0);
        }
        //Jump boolean
        if (canJump)
        {
            canJump = false;
            rb.AddForce(0, forceConst, 0);
        }
        if (Input.GetKeyDown(KeyCode.Space) && canSprint)
        {
            Debug.Log("Tears of Pride!");
            canJump = false;
            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");
            Vector3 movement = new Vector3(moveHorizontal, forceConst, moveVertical);
            rb.AddForceAtPosition(movement, rb.position, ForceMode.Force);
        }
    }
	// Use this for initialization
	void Start ()
	{		
		Time.timeScale = 1;
        selfRigidbody = GetComponent<Rigidbody>();
        distToGround = 0.5f * transform.localScale.y;
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
		}
		//Fly command
		if (canFly > 0f && Input.GetKey (KeyCode.Space)) {
			rb.AddForce (0, flyLift, 0);
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
        //Shoot command
        if (Input.GetKeyDown(KeyCode.W) && canShoot && shot < 2)
        {
            shot++;
            //Calls Projectile Prefab
            GameObject projectileInstance = Instantiate(projectile, transform.position, transform.rotation);
            Rigidbody projectileRb = projectileInstance.GetComponent<Rigidbody>();
            nextFire = Time.time + fireRate;
            //Shoots right if facing right
            if (faceRight)
            {
                Debug.Log("Shot Right");
                projectileRb.velocity += 75f * Vector3.right;
            }
            //Shoots left if facing left
            else if (faceLeft)
            {
                Debug.Log("Shot Left");
                projectileRb.velocity += 75f * Vector3.left;
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