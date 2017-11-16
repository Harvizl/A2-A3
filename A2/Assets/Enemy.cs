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

    bool EnemySightsLeft()
    {
        return Physics.Raycast(transform.position, Vector3.left, 40f);
    }

    bool EnemySightsRight()
    {
        return Physics.Raycast(transform.position, Vector3.right, 40f);
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
        rb = GetComponent<Rigidbody>();
    }

    //Update is called once per frame
    void Update()
    {
        rb.MovePosition(new Vector3(originalX + amplitude * Mathf.Sin(frequency * 1f * Mathf.PI * Time.time), transform.position.y, transform.position.z));
    }

    void FixedUpdate()
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
    }

    
        
    

    /*if (Collider.tag == "Player")
    {
        print("Game Over");
yield return new WaitForSeconds(5);
Application.LoadLevel(0);
    }*/




}