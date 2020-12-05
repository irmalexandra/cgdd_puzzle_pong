using System;
using System.Collections.Generic;
using UnityEngine;

public class BoomerController : MonoBehaviour
{
    private List<GameObject> _pieces;
    public float timer;
    private float _timer;
    private bool _collided;

    private void Start()
    {
        _pieces = new List<GameObject>();
        foreach (Transform child in transform)
        {
            _pieces.Add(child.gameObject);
        }
    }

    private void Update()
    {
        if (_collided)
        {
            if (_timer > 0)
            {
                _timer -= Time.deltaTime;
            }
            else
            {
                Destroy(gameObject);
            }
        }

    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            Destroy(other.gameObject);
            TimeManager.Instance.DoSlowmotion();
            _collided = true;
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            gameObject.GetComponent<Renderer>().enabled = false;
            Vector3 center = transform.GetComponent<Renderer>().bounds.center;
            _timer = timer;
            foreach (GameObject piece in _pieces)
            {
                piece.SetActive(true);
                Vector3 direction = piece.transform.position - center;
                piece.GetComponent<Rigidbody2D>().velocity = direction * 5f;
            }
        }

    }
}

