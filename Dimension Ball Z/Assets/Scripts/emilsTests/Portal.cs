using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Portal : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject ball;

    void Start()
    {
        ball = GameObject.FindGameObjectWithTag("Ball");

    }

    // Update is called once per frame
   
    

    private void OnTriggerEnter2D(Collider2D ball)
    {
        var destination = gameObject;
        var portal1 = PortalController.instance.portal1; 
        var portal2 = PortalController.instance.portal2;
        
        if (!(PortalController.instance.timer < 0.0001f)) return;

        if (portal1.GetComponent<Rigidbody2D>() == GetComponent<Rigidbody2D>())
        {
            ball.transform.position = portal2.transform.position;
        }
        else if(portal2.GetComponent<Rigidbody2D>() == GetComponent<Rigidbody2D>())
        {
            
            ball.transform.position = portal1.transform.position;
        }

        PortalController.instance.timer = 0.1f;


    }


}
