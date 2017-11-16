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

    public Transform player;
    public float playerDistance;
    public bool playerSpotted;
    public bool patrolling;
    public bool backToPatrolling;
    Vector3 originalPosition;


    void Awake()
    {
        playerTarget = GameObject.FindGameObjectWithTag("Player");
        Vector3 originalPosition = rb.transform.position;
    }
    void OnTriggerStay(Collider other)
    {

    }

    void OnTriggerExit(Collider other)
    {
        
    }

    IEnumerator enemyAttack()
    {
        yield return new WaitForSeconds(0);
        while (enemyShot < 1)
        {
            Debug.Log("Player in Sight");
            enemyShot++;
            Vector3 direction = playerTarget.transform.position - transform.position;
            GameObject projectileInstance = Instantiate(projectile, transform.position, transform.rotation);
            Rigidbody projectileRb = projectileInstance.GetComponent<Rigidbody>();
            projectileRb.velocity = Vector3.Lerp(playerTarget.transform.position, direction, 1f * Time.time);
            //Stuff happens that makes the enemy chase the player
        }
    }

    IEnumerator chasePlayer()
    {
        yield return new WaitForSeconds(0);
        rb.velocity = Vector3.Lerp(player.transform.position, playerTarget.transform.position - transform.position, 1f * Time.time);
    }

    IEnumerator patrolMode()
    {
        yield return new WaitForSeconds(0);
        rb.MovePosition(new Vector3(originalX + amplitude * Mathf.Sin(frequency * 1f * Mathf.PI * Time.time), originalY + amplitude * Mathf.Sin(frequency * 1f * Mathf.PI * Time.time), transform.position.z));
    }

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
        patrolling = true;
        playerSpotted = false;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (patrolling)
        {
            StartCoroutine(patrolMode());
        }

        if (playerSpotted)
        {
            patrolling = false;
            StartCoroutine(chasePlayer());
            StartCoroutine(enemyAttack());
        }

        RaycastHit hit;
        {
			if (Physics.Raycast(rb.position, -Vector3.up + -Vector3.right + -Vector3.left + -Vector3.down, out hit, 200f))
            {
                Debug.DrawLine(rb.position, hit.point);
                if (hit.collider.tag == "Player")
                {
                    //print("Found an object - distance: " + hit.distance);

                    transform.LookAt(player);
                    playerSpotted = true;
                }
                

            }
        }
    }



    void FixedUpdate()
    {


    }
}
