using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy2 : MonoBehaviour
{

    public float originalX;
    public float originalY;
    public float amplitude = 5f;
    public float frequency = 1f;
    public Rigidbody rb;
    public GameObject projectile;
    public float fireRate = 1f;
    public static int enemyShot = 0;
    public GameObject playerTarget;
    public SphereCollider col;


    void Awake()
    {
        playerTarget = GameObject.FindGameObjectWithTag("Player");

    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            StartCoroutine(enemyAttack());
        }
    }
    IEnumerator enemyAttack()
    {
        yield return new WaitForSeconds(0);
        while (enemyShot < 1)
        {
            Debug.Log("Player in Sight");
            Vector3 direction = playerTarget.transform.position - transform.position;
            enemyShot = 1;
            GameObject projectileInstance = Instantiate(projectile, transform.position, transform.rotation);
            Rigidbody projectileRb = projectileInstance.GetComponent<Rigidbody>();
            projectileRb.velocity = Vector3.Lerp(projectileRb.transform.position, direction, fireRate * Time.time);
            //Stuff happens that makes the enemy chase the player
        }
    }
    /*bool EnemySightsLeft()
    {
        return Physics.Raycast(transform.position, Vector3.left, 40f);
    }

    bool EnemySightsRight()
    {
        return Physics.Raycast(transform.position, Vector3.right, 40f);
    }*/

    /* void OnCollisionEnter(Collision other)
     {
         if (other.gameObject.tag == "Player")
         {
             Time.timeScale = 0;
             Debug.Log("Game Over");
             //SceneManager.LoadScene(4);
         }
     }*/

    // Use this for initialization
    void Start()
    {
        originalX = transform.position.x;
        originalY = transform.position.y;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.MovePosition(new Vector3(originalX + amplitude * Mathf.Sin(frequency * 1f * Mathf.PI * Time.time), originalY + amplitude * Mathf.Sin(frequency * 1f * Mathf.PI * Time.time), transform.position.z));
    }

    /*void FixedUpdate()
    {
        StartCoroutine(enemyShootLeft());
        StartCoroutine(enemyShootRight());

    }

    IEnumerator enemyShootLeft()
    {
        if (EnemySightsLeft() && enemyShot < 1)
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

    IEnumerator enemyShootRight()
    {
        if (EnemySightsRight() && enemyShot < 1)
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
    }*/
}
