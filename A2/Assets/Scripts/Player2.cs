using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player2 : MonoBehaviour
{
    public bool pause;
    public bool faceLeft;
    public bool faceRight;
    public bool aimUp;
    public bool aimUpLeft;
    public bool aimUpRight;
    public bool canFly;
    public bool flying;
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
    public float maxSpeed = 30f;
    public float maxWalkSpeed = 20f;
    public float slowdown = 2f;


    float distToGround;
    public Rigidbody rb;
    public GameObject lift;
    public GameObject projectile;
    public GameObject gameObjectEnemy2;
    //public Component Enemy2;

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
        GameObject SpawnEnemy2 = Instantiate(gameObjectEnemy2, new Vector3(340, 35, 0), Quaternion.identity);
        SpawnEnemy2.GetComponent<Enemy2>().patrolling = true;
    }

    void SpawnLift()
    {
        GameObject SpawnLift = Instantiate(lift, new Vector3(76, 50, 0), Quaternion.identity);
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Time.timeScale = 0;
            print("Game Over");
            //Load game over screen
        }
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
            //print("You Win!");
            GameManager.instance.score += 500;
            SceneManager.LoadScene(2);
        }
        if (other.tag == "Death Plane")
        {
            //print("You Fell");
            StartCoroutine(DeathByFall());

        }
        if (other.tag == "Ground")
        {
            rb.constraints = RigidbodyConstraints.FreezePositionY;
        }
        else
            rb.constraints = RigidbodyConstraints.None | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY |
                RigidbodyConstraints.FreezeRotationZ;

        

        if (other.gameObject.tag == "Key")
        {
            GameManager.instance.score = GameManager.instance.score + 50;
            SpawnLift();
            other.gameObject.SetActive(false);
        }
        if (other.tag == "Coin")
        {
            print("Real Money Please");
            Destroy(other.gameObject);
            GameManager.instance.score = GameManager.instance.score + 10;
        }
    }

    

    // Use this for initialization
    void Start()
    {
        canShoot = false;
        Time.timeScale = 1;
        distToGround = 0.5f * transform.localScale.y;
    }

    // Update is called once per frame
    void Update()
    {
        //Gravity
        //rb.AddForce(0, -60f, 0);

        isGrounded = IsGrounded();

        canSprintLeft = false;
        canSprintRight = false;
        walkLeft = false;
        walkRight = false;

        flyTimer -= Time.deltaTime;
        if (flyTimer < 0)
        {
            canFly = false;
        }

        RaycastHit stomp;
        {
            if (Physics.Raycast(transform.position, Vector3.down, out stomp, distToGround + 0.1f))
            {
                if (stomp.collider.tag == "Enemy")
                {
                    print("stomped");
                    GameManager.instance.score = GameManager.instance.score + 50;
                    Destroy(stomp.transform.gameObject);
                    if (Input.GetKey(KeyCode.Space))
                    {
                        print("Boost");
                        rb.velocity += 120f * Vector3.up;
                    }
                }
            }
        }

        if (Input.GetKeyUp(KeyCode.P))
        {
            pause = true;
        }
        if (Time.timeScale == 0 && Input.GetKeyUp(KeyCode.P))
        {
            Time.timeScale = 1;
            pause = false;
        }

        //Speed Limit
        if (Input.GetKey(KeyCode.A))
        {
            walkLeft = true;
            faceLeft = true;
            faceRight = false;
            if (Input.GetKey(KeyCode.LeftShift))
                canSprintLeft = true;
            else
                canSprintLeft = false;
        }
        if (Input.GetKey(KeyCode.D))
        {
            walkRight = true;
            faceRight = true;
            faceLeft = false;
            if (Input.GetKey(KeyCode.LeftShift))
                canSprintRight = true;
            else
                canSprintRight = false;
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            canJump = true;
        }

        if (Input.GetKeyDown(KeyCode.KeypadEnter) && canShoot)
        {
            shoot = true;
            shot++;
        }
        else
        {
            shoot = false;
        }

        if (Input.GetKey(KeyCode.Space) && canFly)
        {
            flying = true;
        }
        else
        {
            flying = false;
        }

    }

    void FixedUpdate()
    {
        isGrounded = IsGrounded();

        if (pause)
        {
            Time.timeScale = 0;
        }
        

        //Jump
        if (canJump)
        {
            //rb.AddForce (0, jumpForce, 0);
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

        if (walkLeft || walkRight && !(canSprintLeft || canSprintRight))
        {
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
            rb.velocity += 4f * Vector3.down;
        }

        //Slows player down when not pressing direction
        if (!(canSprintLeft || canSprintRight || walkLeft || walkRight))
        {
            if (rb.velocity.x > 0)
            {
                rb.velocity = new Vector3(Mathf.Max(rb.velocity.x - slowdown, 0f), rb.velocity.y, rb.velocity.z);
            }
            if (rb.velocity.x < 0)
            {
                rb.velocity = new Vector3(Mathf.Min(rb.velocity.x + slowdown, 0f), rb.velocity.y, rb.velocity.z);
            }
        }

        if (shoot && shot < 1)
        {
            //Calls Projectile Prefab
            GameObject projectileInstance = Instantiate(projectile, transform.position, Quaternion.identity) as GameObject;
            Rigidbody projectileRb = projectileInstance.GetComponent<Rigidbody>();
            if (faceLeft)
            {
                projectileRb.velocity += 150f * Vector3.left;
            }
            else if (faceRight)
            {
                projectileRb.velocity += 150f * Vector3.right;
            }
        }

        if (flying && isGrounded == false)
        {
            rb.AddForce(0, 250, 0);
        }
    }
}
