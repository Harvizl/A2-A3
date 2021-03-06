﻿using System.Collections;
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
    
    public float playerDistance;
    public bool playerSpotted;
    public bool patrolling;
    public bool backToPatrolling;
    Vector3 originalPosition;
    Vector3 playerPosition;

    public bool scanning;

	void Awake()
    {
        playerTarget = GameObject.FindGameObjectWithTag("Player");
        Vector3 originalPosition = rb.transform.position;
        gameObject.SetActive(true);
    }

    void OnTriggerStay(Collider other)
    {

    }

    void OnTriggerExit(Collider other)
    {

    }

    void EnemyAttack()
    {
        if (enemyShot < 1)
        {
            //Debug.Log("Player in Sight");
            enemyShot++;
            //Spawns enemy bullet
            GameObject projectileInstance = Instantiate(projectile, transform.position, transform.rotation);
            //Declaring bullet's RB
            Rigidbody projectileRb = projectileInstance.GetComponent<Rigidbody>();
            playerPosition = playerTarget.transform.position - transform.position;
            //Direction of bullet
            projectileRb.velocity = Vector3.Lerp(projectileRb.transform.position, playerPosition, 1f);
            //Stuff happens that makes the enemy chase the player
            //Debug.Log("Player Missed");
        }
    }

    void ChasePlayer()
    {
        rb.velocity = Vector3.Lerp(rb.transform.position, playerTarget.transform.position - transform.position, 1f * Time.time);
    }

    void PatrolMode()
    {
       
        rb.MovePosition(new Vector3(originalX + amplitude * Mathf.Sin(frequency * 1f * Mathf.PI * Time.time), originalY + amplitude * Mathf.Sin(frequency * 1f * Mathf.PI * Time.time), transform.position.z));

        RaycastHit patrol;

        if (Physics.Raycast(rb.position, -Vector3.up + -Vector3.right, out patrol, 200f))
        {
            Debug.DrawLine(rb.position, patrol.point, Color.red);
            if (patrol.collider.tag == "Player")
            {
                //print("Found an object - distance: " + patrol.distance);
                playerSpotted = true;
            }
        }

        if (Physics.Raycast(rb.position, -Vector3.up + Vector3.right, out patrol, 200f))
        {
            Debug.DrawLine(rb.position, patrol.point, Color.yellow);
            if (patrol.collider.tag == "Player")
            {
                //print("Found an object - distance: " + patrol.distance);
                playerSpotted = true;
            }
        }
    }

    

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
    }
    


    void FixedUpdate()
    {
        if (patrolling)
        {
            
            PatrolMode();
        }

        if (playerSpotted)
        {
            scanning = false;
            patrolling = false;
            ChasePlayer();
          //EnemyAttack();
        }

        /*if (scanning)
        {
            RaycastHit hit;

            if (Physics.Raycast(rb.position, -Vector3.up + -Vector3.right, out hit, 200f))
            {
                Debug.DrawLine(rb.position, hit.point, Color.red);
                if (hit.collider.tag == "Player")
                {
                    print("Found an object - distance: " + hit.distance);
                    playerSpotted = true;
                }
            }
        }*/

    }
}
