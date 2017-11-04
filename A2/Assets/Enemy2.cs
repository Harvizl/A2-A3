﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy2 : MonoBehaviour {

    public float originalX;
    public float originalY;
    public float amplitude = 5f;
    public float frequency = 1f;
    public Rigidbody rb;

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            Time.timeScale = 0;
            SceneManager.LoadScene(4);
        }
    }

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
        //rb.MovePosition(new Vector3 (transform.position.x, originalY + amplitude * Mathf.Sin(frequency * 1f * Mathf.PI * Time.time), transform.position.z));
    }
}