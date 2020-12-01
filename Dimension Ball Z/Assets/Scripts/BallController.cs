using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class BallController : MonoBehaviour
{
    // Start is called before the first frame update
    public Rigidbody2D body;
    public Vector2 direction;
    public float speed;
    public float currentMagnitude;
    public Vector2 levelBounds;
    public Vector2 startPosition;

    
    
    public float nudgePower;
    public float nudgeCooldown = 5f;
    private bool nudgeUsed;
    private float nudgeCooldownTimer;
    private bool nudgePressed;
    
    void Start()
    {
        startPosition = transform.position;
        body.velocity = direction.normalized * speed;
    }

    // Update is called once per frame
    private void Update()
    {
        ProcessInputs();
        currentMagnitude = body.velocity.magnitude;
        if (!(transform.position.x < -levelBounds.x) && !(transform.position.x > levelBounds.x) &&
            !(transform.position.y < -levelBounds.y) && !(transform.position.y > levelBounds.y)) return;
        if (GameManager.Instance.extraBalls > 0)
            GameManager.Instance.extraBalls--;
        if (GameManager.Instance.extraBalls != 0)
            transform.position = startPosition;
        

        if (GameManager.Instance.extraBalls == 0)
        {
            GameManager.Instance.TriggerGameOverMenu();
            
        }
        
    }
    

    private void FixedUpdate()
    {

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

    private void ProcessInputs()
    {
        if (Input.GetButton("Jump"))
        {
            Nudge();
        }
    }

    private  void Nudge()
    {
        Debug.Log("Nudging Ball");

        var nudgeDirection = body.velocity;
        
        var above = transform.position.y <= Camera.main.ScreenToWorldPoint(Input.mousePosition).y;
        var right = transform.position.x <= Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
        
        var goingHorizontal = Mathf.Abs(body.velocity.x) > Mathf.Abs(body.velocity.y);

        if (goingHorizontal)
        {
            if (above)
            {
                Debug.Log("Adding to Y");
                nudgeDirection.y += (nudgePower);
            }
            else
            {
                Debug.Log("Subtracting from Y");
                nudgeDirection.y -= nudgePower;
            }
        }
        else
        {
            if (right)
            {
                Debug.Log("Adding to X");
                nudgeDirection.x += nudgePower;
            }
            else
            {
                Debug.Log("Subtracting from X");
                nudgeDirection.x -= nudgePower;
            }
        }

        body.velocity = nudgeDirection;
    }
}
