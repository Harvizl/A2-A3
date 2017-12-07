using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MovingCamera : MonoBehaviour
{
    public GameObject player;
    private Vector3 offset;
//    public Rigidbody rb;
//    public Rigidbody camRb;
//    public bool follow;

//    void OnTriggerEnter(Collider other)
//    {
//        if (other.tag == "Finish")
//        {
//            follow = false;
//            camRb.constraints = RigidbodyConstraints.FreezePositionY;
//        }
//    }
//
//    void Awake()
//    {
//       
//    }

    // Use this for initialization
    void Start()
    {
//        follow = true;
        offset = transform.position - player.transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
//        if (follow)
//        {
            transform.position = player.transform.position + offset;
//        }

		//transform.position.y = Mathf.Clamp (transform.position.y, -13, 1000);

		transform.position = new Vector3 (transform.position.x, Mathf.Max(transform.position.y, -13.0f), transform.position.z);
    }

//    void FixedUpdate()
//    {
//        
//    }
//
//    void LateUpdate()
//    {
//        
//    }
}
