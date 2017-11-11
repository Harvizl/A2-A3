using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float jumpForce = 1750f;
    public float flyLift = 100f;
    public bool faceLeft;
    public bool faceRight;
    public bool aimUp;
    public bool aimUpLeft;
    public bool aimUpRight;
    public bool canSprint;
    public bool canShoot;
    public bool canJump;
    public bool canFly;
    public float flyTimer;
    public int flyBuffDuration = 5;
    float distToGround;
    public float speed;
    public Rigidbody rb;
    public GameObject projectile;
    public float nextFire;
    public float fireRate = 100f;
    public static int shot = 0;

    //Is on the ground
    bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, distToGround);
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
        }
        if (other.tag == "Goal")
        {
            print("You Win!");
            GameManager.instance.score += 500;
            SceneManager.LoadScene(2);
        }
    }

    void FixedUpdate()
    {
        //Jump boolean
        if (canJump)
        {
            canJump = false;
        }
        if (canJump == false)
        {
            //Gravity
            rb.AddForce(0, -60f, 0);
        }
        if (IsGrounded())
        {
            canJump = true;
        }

        //Fly command
        if (canFly)
        {
            if (flyTimer > 0f && Input.GetKey(KeyCode.Space))
            {
                Debug.Log("Mom, I'm flying!");
                rb.AddForce(0, flyLift, 0);
            }
            if (Input.GetKey(KeyCode.A) && flyTimer > 0)
            {
                //rb.AddForce (Vector3.left * 500f);
                rb.position += 10 * Time.deltaTime * Vector3.left;
                Debug.Log("Flying Left");
            }
            if (Input.GetKey(KeyCode.D) && flyTimer > 0)
            {
                //rb.AddForce (Vector3.right * 500f);
                rb.position += 10 * Time.deltaTime * Vector3.right;
                Debug.Log("Flying Right");
            }
        }
        if (canFly && IsGrounded())
        {
            if (Input.GetKeyDown(KeyCode.Space))
                rb.AddForce(0, jumpForce, 0);
        }
    }

    // Use this for initialization
    void Start()
    {
        Time.timeScale = 1;
        distToGround = 0.5f * transform.localScale.y;
        canJump = true;
    }

    // Update is called once per frame
    void Update()
    {
        //Flytimer constant countdown
        flyTimer -= Time.deltaTime;
        if (flyTimer < 0)
        {
            canFly = false;
        }

        if (Input.GetKeyUp(KeyCode.Escape))
        {
            Application.Quit();
        }
        //Sprint toggle
        if (Input.GetKey(KeyCode.LeftShift) && IsGrounded())
        {
            canSprint = true;
        }
        //Stops sprinting if not grounded
        else
            canSprint = false;
        //Keeps Fly bool false
        if (canFly && Input.GetKey(KeyCode.S))
        {
            print("Come Down Son");
            rb.position += 15 * Time.deltaTime * Vector3.down;
        }

        //Jump command
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            canJump = true;
            rb.AddForce(0, jumpForce, 0);
        }
        if (Input.GetKey(KeyCode.D) && canJump == false)
        {
            //Debug.Log("Falling Right");
            transform.Translate(20 * Time.deltaTime * Vector3.right);
            faceRight = true;
            faceLeft = false;
        }
        if (Input.GetKey(KeyCode.A) && canJump == false)
        {
            //Debug.Log("Falling Left");
            transform.Translate(20 * Time.deltaTime * Vector3.left);
            faceRight = false;
            faceLeft = true;
        }
        if (Input.GetKeyDown(KeyCode.Space) && Input.GetKey(KeyCode.D) && canSprint)
        {
            //Debug.Log("Run Jump Right");
            rb.AddForce(230, 0, 0);
        }
        if (Input.GetKeyDown(KeyCode.Space) && Input.GetKey(KeyCode.A) && canSprint)
        {
            //Debug.Log("Run Jump Left");
            rb.AddForce(-230, 0, 0);
        }
        //Move Right
        if (Input.GetKey(KeyCode.D) && IsGrounded())
        {
            //transform.Translate (15 * Time.deltaTime * Vector3.right);
            rb.position += 15 * Time.deltaTime * Vector3.right;
            faceRight = true;
            faceLeft = false;
        }
        //Sprint Right
        if (Input.GetKey(KeyCode.D) && canSprint && IsGrounded())
        {
            //transform.Translate (20 * Time.deltaTime * Vector3.right);	
            rb.position += 20 * Time.deltaTime * Vector3.right;
            faceRight = true;
            faceLeft = false;
        }
        //Move Left
        if (Input.GetKey(KeyCode.A) && IsGrounded())
        {
            //transform.Translate (15 * Time.deltaTime * Vector3.left);
            rb.position += 15 * Time.deltaTime * Vector3.left;
            faceLeft = true;
            faceRight = false;
        }
        //Sprint Left
        if (Input.GetKey(KeyCode.A) && canSprint && IsGrounded())
        {
            //transform.Translate (20 * Time.deltaTime * Vector3.left);
            rb.position += 20 * Time.deltaTime * Vector3.left;
            faceLeft = true;
            faceRight = false;
        }
        if (Input.GetKey(KeyCode.W))
        {
            aimUp = true;
        }
        else
            aimUp = false;
        if (Input.GetKey(KeyCode.E))
        {
            aimUpRight = true;
        }
        else
            aimUpRight = false;
        if (Input.GetKey(KeyCode.Q))
        {
            aimUpLeft = true;
        }
        else
            aimUpLeft = false;
        //Shoot command
        if (Input.GetKeyDown(KeyCode.KeypadEnter) && canShoot && shot < 2)
        {
            shot++;
            //Calls Projectile Prefab
            GameObject projectileInstance = Instantiate(projectile, transform.position, Quaternion.identity) as GameObject;
            Rigidbody projectileRb = projectileInstance.GetComponent<Rigidbody>();
            nextFire = Time.time + fireRate;
            //Aims up
            if (aimUp)
            {
                Debug.Log("Shot Up");
                projectileRb.velocity += 150f * Vector3.up;
            }
            else if (aimUpRight)
            {
                Debug.Log("Shot Up Right");
                projectileRb.velocity += 150f * Vector3.up;
                projectileRb.velocity += 150f * Vector3.right;
            }
            else if (aimUpLeft)
            {
                Debug.Log("Shot Up Left");
                projectileRb.velocity += 150f * Vector3.up;
                projectileRb.velocity += 150f * Vector3.left;
            }
            //Shoots right if facing right
            else if (faceRight)
            {
                //Debug.Log("Shot Right");
                projectileRb.velocity += 150f * Vector3.right;
            }
            //Shoots left if facing left
            else if (faceLeft)
            {
                //Debug.Log("Shot Left");
                projectileRb.velocity += 150f * Vector3.left;
            }
            
        }
    }
}
