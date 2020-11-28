using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Portal : MonoBehaviour
{
    private GameObject ball;

    void Start()
    {
        ball = GameObject.FindGameObjectWithTag("Ball");

    }
    
    private void OnTriggerEnter2D(Collider2D ball)
    {
        Debug.Log("in portal thing for some reason");
        var destination = gameObject;
        var portal1 = PortalController.instance.portal1; 
        var portal2 = PortalController.instance.portal2;
        if (!ball.CompareTag("Ball")) return; // Makes sure the object entering the portal is a ball.
        if (!(PortalController.instance.timer < 0.0001f)) return;

        if (portal1.GetComponent<Rigidbody2D>() == GetComponent<Rigidbody2D>())
        {
            ball.transform.position = portal2.transform.position;
        }
        else if (portal2.GetComponent<Rigidbody2D>() == GetComponent<Rigidbody2D>())
        {

            ball.transform.position = portal1.transform.position;
        }

        PortalController.instance.timer = 0.1f;


    }


}
