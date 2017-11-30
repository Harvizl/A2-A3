using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MovingCamera : MonoBehaviour
{

    public GameObject player;
    private Vector3 offset;
    public Rigidbody rb;
    public Rigidbody camRb;
    public bool follow;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Finish")
        {
            follow = false;
            camRb.constraints = RigidbodyConstraints.FreezePositionY;
        }
    }

    void Awake()
    {
       
    }

    // Use this for initialization
    void Start()
    {
        follow = true;
        offset = transform.position - player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (follow)
        {
            transform.position = player.transform.position + offset;
        }
    }

    void FixedUpdate()
    {
        
    }

    void LateUpdate()
    {
        
    }

    

}
