using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour {

	public float originalX;
	public float amplitude = 10;
	public float frequency = 0.5f;
	public Rigidbody rb;


	// Use this for initialization
	void Start () {
		originalX = transform.position.x;
		rb = GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update () {
//		transform.position = new Vector3 (originalX + amplitude * Mathf.Sin(frequency * 1.0f * Mathf.PI * Time.time), transform.position.y, transform.position.z);
		rb.MovePosition (new Vector3 (originalX + amplitude * Mathf.Sin(frequency * 1.0f * Mathf.PI * Time.time), transform.position.y, transform.position.z));
	}
}
