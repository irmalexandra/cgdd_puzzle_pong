﻿using System.Collections;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class PaddleController : MonoBehaviour
{
    // Start is called before the first frame update
    [Range(5f, 10f)] public float movementSpeed;
    public float upperBound;
    public float lowerBound;
    public Rigidbody2D body;
    public float speed;
    private Vector3 _mousePosition;
    private float _verticalMovement;
    private Vector2 _moveDirection;
    public bool active = false;

    public bool scoreSystemInPlay;
    public Light2D freeFormLight;
    private float _originalIntensity;
    public bool flashing;

    private void Start()
    {
        
        _originalIntensity = freeFormLight.intensity;
    }

    private void FixedUpdate()
    {
        

        if (PlayerPrefs.GetInt("Input") == 1)
        {
            if (!active) return;
            _mousePosition = Input.mousePosition;
            _mousePosition = Camera.main.ScreenToWorldPoint(_mousePosition);
            var paddleLocation = transform.position;
            var step = speed * Time.deltaTime;
            if (transform.position.y <= upperBound && transform.position.y >= lowerBound)
            {
                transform.position = 
                    Vector2.MoveTowards(paddleLocation, new Vector2(paddleLocation.x, _mousePosition.y), step);
            }
            else
            {
                if (transform.position.y >= upperBound && _mousePosition.y <= upperBound)
                {
                    transform.position =
                        Vector2.MoveTowards(paddleLocation, new Vector2(paddleLocation.x, upperBound), step);
                }
                else if (transform.position.y <= lowerBound && _mousePosition.y >= lowerBound)
                {
                    transform.position =
                        Vector2.MoveTowards(paddleLocation, new Vector2(paddleLocation.x, lowerBound), step);
                }
            }
        }
        else
        {
            if (active)
            {
                ProcessInputs();
                Move();
            }
            else
            {
                body.velocity = new Vector2(0, 0);
            }
            
        }
        
        
        
        
        
    }

    void ProcessInputs()
    {
        Vector2 direction = new Vector2(0, 0);
        if (Input.GetKey(KeyCode.DownArrow))
        {
            direction = new Vector2(0, -1);
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            direction = new Vector2(0, 1);
        }
        
        
        _moveDirection = direction;
    }

    void Move()
    {
        if ((_moveDirection.y < 0 && transform.position.y < lowerBound) 
            || (_moveDirection.y > 0 && transform.position.y > upperBound))
        {
            body.velocity = new Vector2(0,0);
        }
        else
        {
            Vector2 newPosition = new Vector2(0, _moveDirection.y * movementSpeed);
            body.velocity = newPosition;
        }
    }
    
    
    private IEnumerator Flash()
    {
        flashing = true;
        freeFormLight.intensity += 2;
        yield return new WaitForSeconds(0.1f);
        freeFormLight.intensity = _originalIntensity;
        flashing = false;

    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        StartCoroutine(Flash());
        
        if (!scoreSystemInPlay) return;
        var side = transform.parent.name;
        switch (side)
        {
            case "LeftSide":
                ScoreTracking.AddScore(true);
                break;
            case "RightSide":
                ScoreTracking.AddScore(false);
                break;
        }
    }
}

/*

public class PaddleController : MonoBehaviour
{
    // Start is called before the first frame update
    [Range(5f, 10f)] public float movementSpeed;
    public float upperBound;
    public float lowerBound;
    public Rigidbody2D body;
    private float _verticalMovement;
    private Vector2 _moveDirection;
    public bool active = false;

    public bool scoreSystemInPlay;
    
    private void Update()
    {
        if (active)
        {
            ProcessInputs();
        }
        else
        {
            _moveDirection = new Vector2(0, 0);
        }
    }

    private void FixedUpdate()
    {
        Move();
    }

    void ProcessInputs()
    {
        Vector2 direction = new Vector2(0, Input.GetAxisRaw("Vertical"));
        _moveDirection = direction;
    }

    void Move()
    {
        if ((_moveDirection.y < 0 && transform.position.y < lowerBound) 
            || (_moveDirection.y > 0 && transform.position.y > upperBound))
        {
            body.velocity = new Vector2(0,0);
        }
        else
        {
            Vector2 newPosition = new Vector2(0, _moveDirection.y * movementSpeed);
            body.velocity = newPosition;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!scoreSystemInPlay) return;
        var side = transform.parent.name;
        switch (side)
        {
            case "LeftSide":
                ScoreTracking.AddScore(true);
                break;
            case "RightSide":
                ScoreTracking.AddScore(false);
                break;
        }
    }
}
*/
