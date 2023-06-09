﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxScroller : MonoBehaviour
{
    private float lenght, startpos;
    private Camera cam;
    public float parallaxRatio;
    void Start()
    {
        cam = Camera.main;
        lenght = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void FixedUpdate()
    {
        float temp = (cam.transform.position.x * (1 - parallaxRatio));
        float dist = (cam.transform.position.x * parallaxRatio);

        transform.position = new Vector3(startpos + dist,transform.position.y,transform.position.z);

        if(temp > startpos + lenght)
            startpos += lenght;
        else if(temp < startpos - lenght)
            startpos -= lenght;        
    }
}
