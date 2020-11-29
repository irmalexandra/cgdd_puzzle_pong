using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;

public class TransferControl : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D ball)
    {
        transform.parent.gameObject.GetComponentInChildren<PaddleController>().active = true;

    }

    private void OnTriggerExit2D(Collider2D ball)
    {
        transform.parent.gameObject.GetComponentInChildren<PaddleController>().active = false;
    }
}
