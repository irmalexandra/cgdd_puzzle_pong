using System.Linq;
using UnityEngine;

public class PhysicsObject : MonoBehaviour
{
    public GameObject[] toAvoid;
    public float timer;
    private float _timer;
    private Vector3 _originalPosition;
    private Quaternion _originalRotation;


    private void Start()
    {
        _timer = timer;
        _originalPosition = transform.position;
        _originalRotation = transform.rotation;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (toAvoid.Contains(other.gameObject))
        {
            _timer = timer;
        }
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (toAvoid.Contains(other.gameObject))
        {
            if (_timer > 0)
            {
                _timer -= Time.deltaTime;
            }
            else
            {
                transform.position = _originalPosition;
                transform.rotation = _originalRotation;
            }
        }

    }
}
