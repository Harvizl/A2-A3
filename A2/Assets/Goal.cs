using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour {

	public float originalY;
	public float amplitude = 3.5f;
	public float frequency = 1f;
	public Rigidbody rb;

	// Use this for initialization
	void Start () 
	{
		originalY = transform.position.y;
		rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
		rb.MovePosition(new Vector3 (transform.position.x, originalY + amplitude * Mathf.Sin(frequency * 1f * Mathf.PI * Time.time), transform.position.z));
	}
}
