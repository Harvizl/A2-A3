using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MovingCamera : MonoBehaviour {

	public GameObject player;
	private Vector3 offset;
    


   

       

    void Awake()
    {
        
    }

    // Use this for initialization
    void Start ()
	{
		offset = transform.position - player.transform.position;
	}

    // Update is called once per frame
    void Update()
    {
        
    }

    void LateUpdate ()
	{
		transform.position = player.transform.position + offset;
	}
}
