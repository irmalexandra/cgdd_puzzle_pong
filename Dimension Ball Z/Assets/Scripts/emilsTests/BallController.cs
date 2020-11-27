using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    // Start is called before the first frame update
    public Rigidbody2D body;
    public Vector2 direction;
    public float impulse;
    void Start()
    {
        body.velocity = direction.normalized * impulse;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
