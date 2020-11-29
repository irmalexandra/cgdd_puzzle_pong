using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PaddleController : MonoBehaviour
{
    // Start is called before the first frame update
    [Range(5f, 10f)] public float movementSpeed;
    public float upperBound;
    public float lowerBound;
    public Rigidbody2D body;
    private float _verticalMovement;
    private Vector2 _moveDirection;
    
    private void Update()
    {
        ProcessInputs();
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
}

