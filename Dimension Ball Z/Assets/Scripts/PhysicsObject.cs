using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsObject : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!other.gameObject.CompareTag("Ball"))
        {
            return;
        }

        var otherBody = other.gameObject.GetComponent<Rigidbody2D>();
        var originalSpeed = other.gameObject.GetComponent<BallController>().speed;
        
        
        Debug.Log("a");
        var velocity = otherBody.velocity;

        var reflectedVelocity = Vector2.Reflect(-velocity, other.gameObject.transform.up).normalized;
        reflectedVelocity *= originalSpeed;
        otherBody.velocity = reflectedVelocity;
        
        
        /*
        Vector2 reDirection = otherBody.velocity.normalized;
        reDirection *= originalSpeed;
        
        GetComponent<Rigidbody2D>().velocity = reDirection;
        Debug.Log("collide");*/
    }
}
