using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    public GameObject door;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.name == "ButtonFace")
        {
            door.gameObject.GetComponent<DoorController>().activateDoor();
        }
    }
}
