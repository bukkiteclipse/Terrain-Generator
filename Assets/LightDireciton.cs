﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightDireciton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.LookAt(new Vector3(0, 0, 0));
    }

    // Update is called once per frame
    void Update()
    {
        //transform.LookAt(new Vector3(0, 0, 0));
    }
}
