﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShatteringDoor : MonoBehaviour {
    private int speed = 20;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.left * speed * Time.deltaTime, Space.World);

    }
}
