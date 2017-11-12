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
    public bool onTheGround;
    public float isFalling;
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

    //Game Over if you fall in a hole
    IEnumerator DeathByFall()
    {

        yield return new WaitForSeconds(0.5f);
        
        if (IsGrounded() != true)
        {
            Debug.Log("You Fell");
            Time.timeScale = 0;
        }
        

        //Tried using a Raycast to detect nothing before triggering game over
        /*RaycastHit hit;
        {
            if (Physics.Raycast(transform.position, Vector3.down, out hit, 500f))
            {
                Debug.DrawLine(rb.position, hit.point, Color.green);
                if (hit.collider.tag == null)
                {
                    Debug.Log("You Fell");
                    Time.timeScale = 0;
                }
            }
        }*/

    }

    //Falling counter, triggered when free falling (Level design must be smaller in height before death)
    IEnumerator Falling()
    {
        yield return new WaitForSeconds(0);
        Debug.Log("Falling");
        isFalling += 1 * 0.5f;
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

    // Use this for initialization
    void Start()
    {
        Time.timeScale = 1;
        distToGround = 0.5f * transform.localScale.y;
        canJump = true;
        //isFalling = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //Tried to use booleans and Raycasts to start death via falling through holes
        /*if (onTheGround != true && canFly == false)
        {
            if (canFly == false)
            {
                Debug.Log("Falling...");
                StartCoroutine(DeathByFall());
            }
        }
        RaycastHit hit;
        {
            if (Physics.Raycast(rb.position, Vector3.down, out hit, distToGround))
            {
                if (hit.collider.tag == "Null")
                {
                    StartCoroutine(Falling());
                }
            }
        }*/

        //Float trigger for death via falling through holes
        if (onTheGround != true && canFly == false)
        {
            StartCoroutine(Falling());
            if (isFalling > 100)
            {
                StartCoroutine(DeathByFall());
            }
        }

        //Jump boolean
        if (canJump)
        {
            canJump = false;
            onTheGround = false;
        }
        if (canJump == false)
        {
            //Gravity
            rb.AddForce(0, -60f, 0);
        }
        if (IsGrounded())
        {
            canJump = true;
            onTheGround = true;
            isFalling = 0;
        }

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
        //Move right while in the air (no sprint)
        if (Input.GetKey(KeyCode.D) && canJump == false)
        {
            //Debug.Log("Falling Right");
            transform.Translate(20 * Time.deltaTime * Vector3.right);
            faceRight = true;
            faceLeft = false;
        }
        //Move Left in the air (no sprint)
        if (Input.GetKey(KeyCode.A) && canJump == false)
        {
            //Debug.Log("Falling Left");
            transform.Translate(20 * Time.deltaTime * Vector3.left);
            faceRight = false;
            faceLeft = true;
        }
        //Sprint Jump power (running right)
        if (Input.GetKeyDown(KeyCode.Space) && Input.GetKey(KeyCode.D) && canSprint)
        {
            //Debug.Log("Run Jump Right");
            rb.AddForce(230, 0, 0);
        }
        //Sprint Jump power (running left)
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
        //Hold to aim up
        if (Input.GetKey(KeyCode.W))
        {
            aimUp = true;
        }
        else
            aimUp = false;
        //Hold to aim up-right
        if (Input.GetKey(KeyCode.E))
        {
            aimUpRight = true;
        }
        else
            aimUpRight = false;
        //Hold to aim up-left
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
            //Shoots up
            if (aimUp)
            {
                //Debug.Log("Shot Up");
                projectileRb.velocity += 150f * Vector3.up;
            }
            //Shoots up-right
            else if (aimUpRight)
            {
                //Debug.Log("Shot Up Right");
                projectileRb.velocity += 150f * Vector3.up;
                projectileRb.velocity += 150f * Vector3.right;
            }
            //Shoots up-left
            else if (aimUpLeft)
            {
                //Debug.Log("Shot Up Left");
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

    void FixedUpdate()
    {
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
        //Normal jump while Fly Buff is active and grounded
        if (canFly && IsGrounded())
        {
            if (Input.GetKeyDown(KeyCode.Space))
                rb.AddForce(0, jumpForce, 0);
        }
    }
}
