using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTransition : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag("Ball")) return;
        var camera = GameObject.FindWithTag("MainCamera");
        var newPosition = transform.parent.position;
        newPosition.z = -10;
        camera.transform.position = newPosition;
    }
}
