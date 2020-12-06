using System;
using UnityEngine;

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

