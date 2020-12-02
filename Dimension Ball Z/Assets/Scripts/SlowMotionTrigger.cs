using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowMotionTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            Debug.Log("Triggering Slowmotion");
            GameManager.Instance.timeManager.DoSlowmotion();
        }
    }
}
