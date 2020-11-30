using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    // Start is called before the first frame update
    public Rigidbody2D body;
    public Vector2 direction;
    public float speed;
    public float currentMagnitude;
    void Start()
    {
        body.velocity = direction.normalized * speed;
    }

    // Update is called once per frame
    private void Update()
    {
        currentMagnitude = body.velocity.magnitude;
    }


    private void OnCollisionEnter2D(Collision2D other)
    {
        Vector2 reDirection = GetComponent<Rigidbody2D>().velocity.normalized;
        reDirection *= speed;
        GetComponent<Rigidbody2D>().velocity = reDirection;

    }
}
