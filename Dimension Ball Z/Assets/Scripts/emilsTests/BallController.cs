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
    void Start()
    {
        body.velocity = direction.normalized * speed;
    }

    // Update is called once per frame




    private void OnCollisionExit2D(Collision2D other)
    {
        Vector2 thing = GetComponent<Rigidbody2D>().velocity.normalized;
        thing *= speed;
        GetComponent<Rigidbody2D>().velocity = thing;

    }
}
