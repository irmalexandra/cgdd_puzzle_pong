using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PaddleController : MonoBehaviour
{
    // Start is called before the first frame update
    [Range(0.01f,0.1f)] public float movementSpeed;

    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 position = transform.position;
        if (Input.GetKey("w"))
        {
            position.y += movementSpeed;

        }       
        else if (Input.GetKey("s"))
        {
            position.y -= movementSpeed;
        }
        transform.position = position;

    }

    private void OnCollisionEnter(Collision other)
    {
        Destroy(other.gameObject);
        
    }
}
