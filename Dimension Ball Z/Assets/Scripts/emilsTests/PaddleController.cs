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
        float moveY = Input.GetAxisRaw("Vertical");

        _moveDirection = new Vector2(0, moveY);   
    }

    void Move()
    {
        body.velocity = new Vector2(0, _moveDirection.y * movementSpeed);
    }
}

