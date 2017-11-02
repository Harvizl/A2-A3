using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
	public float forceConst = 7f;
	public float flyLift = 14;
	public bool canShoot;
	private bool canJump;
	public float canFly;
	public int flyBuffDuration = 5;
	private Rigidbody selfRigidbody;
	float distToGround;
	public float speed;
	public Rigidbody rb;
	public GameObject projectile;
	public Rigidbody Projectile;
	public float nextFire;
	public float fireRate = 0.1f;
	public float bulletSpeed = 30f;
	public bool faceRight;
	public bool faceLeft;
	public bool canSprint;
	public static int shot = 0;
    
	Coroutine canFlyCoroutine;

    //Is on the ground
    bool IsGrounded ()
	{
		return Physics.Raycast (transform.position, Vector3.down, distToGround + 0.1f);
	}

	//If items or goal is touched
	void OnTriggerEnter (Collider other)
	{
		if (other.tag == "Capsule") {
//			if (canFlyCoroutine != null) {
//				StopCoroutine (canFlyCoroutine);
//			}
//			canFlyCoroutine = StartCoroutine (canFlyTimer());

			if (canFly > 0)
				canFly += flyBuffDuration;
			else
				canFly = flyBuffDuration;
            GameManager.instance.score = GameManager.instance.score + 10;
            other.gameObject.SetActive (false);
		}
		if (other.tag == "Fire") {
			canShoot = true;
            GameManager.instance.score = GameManager.instance.score + 10;
            other.gameObject.SetActive (false);
		}
       	if (other.tag == "Goal")
		{
			print ("You Win!");
            GameManager.instance.score += 500;
            //GameManager.instance.score += GameManager.instance.gameLength;
            //Time.timeScale = 0;
            //StartCoroutine(DelayedRestart());
            SceneManager.LoadScene(2);
            
        }
    }
	//Restarts level after 3 seconds if killed or reaches goal
	/*IEnumerator DelayedRestart()
	{
		yield return new WaitForSecondsRealtime (1);
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}*/

    void FixedUpdate ()
	{
		//Movement direction and speed during jump
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);

		rb.AddForce (movement * speed);      
	}
	// Use this for initialization
	void Start ()
	{		
		Time.timeScale = 1;
		selfRigidbody = GetComponent<Rigidbody> ();	
		distToGround = 0.5f * transform.localScale.y;
    }
		
	// Update is called once per frame
	void Update ()
	{
		canFly -= Time.deltaTime;

        //Sprint toggle
		if (Input.GetKey (KeyCode.LeftShift) && IsGrounded ()) {
			canSprint = true;
		}
        //Stops sprinting if not grounded
        else
			canSprint = false;
        //Jump boolean
		if (canJump) {
			canJump = false;
			selfRigidbody.AddForce (0, forceConst, 0, ForceMode.Impulse);
		}
        //Move right command
		if (Input.GetKey (KeyCode.D)) {
			transform.Translate (15 * Time.deltaTime * Vector3.right);
			faceRight = true;
			faceLeft = false;
		}
        //Sprint right command
		if (Input.GetKey (KeyCode.D) && canSprint) {
			transform.Translate (20 * Time.deltaTime * Vector3.right);	
		}
        //Move left command
		if (Input.GetKey (KeyCode.A)) {
			transform.Translate (15 * Time.deltaTime * Vector3.left);
			faceLeft = true;
			faceRight = false;
		}
        //Sprint left command
		if (Input.GetKey (KeyCode.A) && canSprint) {
			transform.Translate (20 * Time.deltaTime * Vector3.left);	
		}
        //Jump command
		if (Input.GetKey (KeyCode.Space) && IsGrounded ()) {
			canJump = true;
		}
        //Fly command
		if (canFly > 0f && Input.GetKey (KeyCode.Space)) {
			selfRigidbody.AddForce (0, flyLift, 0);
		}
        //Shoot command
		if (Input.GetKeyDown (KeyCode.W) && canShoot && shot < 2) {	
			shot++;
			GameObject projectileInstance = Instantiate (projectile, transform.position, transform.rotation);
			Rigidbody projectileRb = projectileInstance.GetComponent<Rigidbody> ();
			nextFire = Time.time + fireRate;
            //Shoots right if facing right
			if (faceRight) {
				projectileRb.velocity += 75f * Vector3.right;
			}
            //Shoots left if facing left
            else if (faceLeft) {
				projectileRb.velocity += 75f * Vector3.left;
			}
            if (Input.GetKeyUp (KeyCode.Escape))
            {
                Application.Quit();
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