using System;
using UnityEngine;

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
    
    /*private void Update()
    {
        if (active)
        {
            ProcessInputs();
        }
        else
        {
            _moveDirection = new Vector2(0, 0);
        }
    }*/

    private void FixedUpdate()
    {
        if (!active) return;
        if (transform.position.y < lowerBound && transform.position.y > upperBound);
            _mousePosition = Input.mousePosition;
            _mousePosition = Camera.main.ScreenToWorldPoint(_mousePosition);
            var paddleLocation = transform.position;
            var step = speed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(paddleLocation, new Vector2(paddleLocation.x, _mousePosition.y), step);
        
        /*Move();*/
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
            if (!Input.GetKey(KeyCode.Mouse2)) return;
            Debug.Log("Hello!");
            _mousePosition = Input.mousePosition;
            _mousePosition = Camera.main.ScreenToWorldPoint(_mousePosition);
            transform.position = Vector2.Lerp(transform.position, _mousePosition, 2.0f);
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

