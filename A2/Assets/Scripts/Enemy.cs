using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{

    public float originalX;
    public float amplitude = 5f;
    public float frequency = 1f;

//	public float speed = 10f;
//	public float rotationSpeed = 100f;
//	public float translation = Input.GetAxis ("Verticle") * speed;
//	public float rotation = Input.GetAxis ("Horizontol") * rotationSpeed;

    public Rigidbody rb;
    public GameObject projectile;
    public float nextFire;
    public float fireRate = 0.05f;
    public static int enemyShot = 0;
    float distToGround;

    //Is on the ground
    bool IsGrounded()
    {
        return Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.1f);
    }


    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            Time.timeScale = 0;
            Debug.Log("Game Over");
            //SceneManager.LoadScene(4);
        }
    }

    

    //Use this for initialization
    void Start()
    {
        originalX = transform.position.x;
        distToGround = 0.5f * transform.localScale.y;
    }

    //Update is called once per frame
    void Update()
    {
		rb.MovePosition(new Vector3(originalX + amplitude * Mathf.Sin(frequency * 1f * Mathf.PI * Time.time),transform.position.y, transform.position.z));

//		translation *= Time.deltaTime;
//		rotation *= Time.deltaTime;
//		transform.Translate (rotation, 0, 0);
//		transform.Rotate (0, rotation, 0);

         RaycastHit hit;
         {
            if (Physics.Raycast(rb.position, Vector3.up, out hit, 40f))
            {
                Debug.DrawLine(rb.position, hit.point, Color.green);
                if (hit.collider.tag == "Player")
                {
                    StartCoroutine(enemyShootUp());
                }
            }
            else if (Physics.Raycast(rb.position, -Vector3.left, out hit, 40f))
            {
                Debug.DrawLine(rb.position, hit.point, Color.red);
                if (hit.collider.tag == "Player")
                {
                    StartCoroutine(enemyShootRight());
                }
            }
            else if(Physics.Raycast(rb.position, -Vector3.right, out hit, 40f))
            {
                Debug.DrawLine(rb.position, hit.point, Color.blue);
                if (hit.collider.tag == "Player")
                {
                    StartCoroutine(enemyShootLeft());
                }
            }
        }
        if (IsGrounded() == false)
        {
            //Gravity
			rb.AddForce (0, -500, 0);

        }
    }

    

    void FixedUpdate()
    {
        

    }

    IEnumerator enemyShootUp()
    {
        if (enemyShot < 1)
        {
            Debug.Log("UP!");
            enemyShot++;
            yield return new WaitForSeconds(1);
            GameObject projectileInstance = Instantiate(projectile, transform.position, transform.rotation);
            Rigidbody projectileRb = projectileInstance.GetComponent<Rigidbody>();
            nextFire = Time.time + fireRate;
            Debug.Log("Shot Right");
            projectileRb.velocity += 150f * Vector3.up;
        }
    }

    IEnumerator enemyShootRight()
    {
        if (enemyShot < 1)
        {
            Debug.Log("Rawr");
            enemyShot++;
            yield return new WaitForSeconds(1);
            GameObject projectileInstance = Instantiate(projectile, transform.position, transform.rotation);
            Rigidbody projectileRb = projectileInstance.GetComponent<Rigidbody>();
            nextFire = Time.time + fireRate;
            Debug.Log("Shot Right");
            projectileRb.velocity += 150f * Vector3.right;
        }
    }
        IEnumerator enemyShootLeft()
    {
        if (enemyShot < 1)
        {
            Debug.Log("Meow");
            enemyShot++;
            yield return new WaitForSeconds(1);
            GameObject projectileInstance = Instantiate(projectile, transform.position, transform.rotation);
            Rigidbody projectileRb = projectileInstance.GetComponent<Rigidbody>();
            nextFire = Time.time + fireRate;
            Debug.Log("Shot Left");
            projectileRb.velocity += 150f * Vector3.left;
        }
        
    

        
    }

    
        
    

    /*if (Collider.tag == "Player")
    {
        print("Game Over");
yield return new WaitForSeconds(5);
Application.LoadLevel(0);
    }*/




}