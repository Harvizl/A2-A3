﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coins : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.right * 250 * Time.deltaTime);
    }
}
