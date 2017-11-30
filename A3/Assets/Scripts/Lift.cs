using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lift : MonoBehaviour
{

    public float originalY;
    public float amplitude = 35;
    public float frequency = 0.1f;
    public Rigidbody rb;


    // Use this for initialization
    void Start()
    {
        originalY = transform.position.y;
        rb = GetComponent <Rigidbody> ();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.MovePosition (new Vector3 (transform.position.x, (originalY + amplitude * Mathf.Sin(frequency * 0.5f * Mathf.PI * Time.time)), transform.position.z));
    }
}
