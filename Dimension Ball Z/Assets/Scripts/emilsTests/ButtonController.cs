using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    public GameObject door;
    public bool openOnly;
    private bool _open;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.name == "ButtonFace")
        {
            if (openOnly)
            {
                OpenOnly();
            }
            else
            {
                Toggle();
            }
            
        }
    }
    
    private void Toggle()
    {
        door.gameObject.GetComponent<DoorController>().activateDoor();
    }

    private void OpenOnly()
    {
        if (!_open)
        {
            door.gameObject.GetComponent<DoorController>().activateDoor();
            _open = true;
        }
    }
    
}
