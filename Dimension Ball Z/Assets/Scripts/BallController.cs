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
    public int nudgeStaminaCost;
    
    void Start()
    {
        startPosition = transform.position;
        body.velocity = direction.normalized * speed;
    }

    // Update is called once per frame
    private void Update()
    {
        if (!GameManager.Instance.IsPaused())
        {
            ProcessInputs(); 
        }
        
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
  
    private void OnCollisionEnter2D(Collision2D other)
    {
        Vector2 reDirection = GetComponent<Rigidbody2D>().velocity;
        

        if (other.gameObject.CompareTag("Paddle"))
        {
            SoundManagerScript.PlaySoundEffect("PaddleHit");
        }
        else
        {
            SoundManagerScript.PlaySoundEffect("BallHit");
            
            if (other.gameObject.CompareTag("ButtonFace"))
            {
                Vector3 otherBoost = other.transform.up * 2f;
                reDirection += new Vector2(otherBoost.x, otherBoost.y);
            }
        }

        reDirection = reDirection.normalized;
        reDirection *= speed;
        GetComponent<Rigidbody2D>().velocity = reDirection;
    }

    private void ProcessInputs()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if (StaminaBar.instance.UseStamina(nudgeStaminaCost))
            {
                Nudge();
            }
        }
    }

    private  void Nudge()
    {
        var nudgeDirection = body.velocity;
        
        var above = transform.position.y <= Camera.main.ScreenToWorldPoint(Input.mousePosition).y;
        var right = transform.position.x <= Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
        
        var goingHorizontal = Mathf.Abs(body.velocity.x) > Mathf.Abs(body.velocity.y);

        if (goingHorizontal)
        {
            if (above)
            {
                nudgeDirection.y += (nudgePower);
            }
            else
            {
                nudgeDirection.y -= nudgePower;
            }
        }
        else
        {
            if (right)
            {
                nudgeDirection.x += nudgePower;
            }
            else
            {
                nudgeDirection.x -= nudgePower;
            }
        }
        body.velocity = nudgeDirection;
    }
}
