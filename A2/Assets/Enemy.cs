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
    public GameObject projectile;
    public float nextFire;
    public float fireRate = 0.05f;
    public static int enemyShot = 0;
    
    
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
        rb = GetComponent<Rigidbody>();
    }

    //Update is called once per frame
    void Update()
    {
        rb.MovePosition(new Vector3(originalX + amplitude * Mathf.Sin(frequency * 1f * Mathf.PI * Time.time), transform.position.y, transform.position.z));

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