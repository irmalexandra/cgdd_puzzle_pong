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
    public Vector2 levelBounds;
    public Vector2 startPosition;
    
    void Start()
    {
        startPosition = transform.position;
        body.velocity = direction.normalized * speed;
    }

    // Update is called once per frame
    private void Update()
    {
        currentMagnitude = body.velocity.magnitude;
        if (!(transform.position.x < -levelBounds.x) && !(transform.position.x > levelBounds.x) &&
            !(transform.position.y < -levelBounds.y) && !(transform.position.y > levelBounds.y)) return;
        transform.position = startPosition;
        //body.velocity = Vector2.zero;
        GameManager.Instance.extraBalls--;
    }


    private void OnCollisionEnter2D(Collision2D other)
    {
        Vector2 reDirection = GetComponent<Rigidbody2D>().velocity.normalized;
        reDirection *= speed;
        GetComponent<Rigidbody2D>().velocity = reDirection;

        if (other.gameObject.CompareTag("Paddle"))
        {
            SoundManagerScript.PlaySoundEffect("PaddleHit");
        }
        else
        {
            SoundManagerScript.PlaySoundEffect("BallHit");
        }

    }
}
