using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    public GameObject[] toActivate;
    public bool hold;
    private bool _open;

    private void OnCollisionEnter2D(Collision2D other)
    {
        
        if (other.gameObject.name == "ButtonFace")
        {
            
            foreach (var interactableObject in toActivate)
            {
                interactableObject.GetComponent<InteractablesController>().Signal();
            }

        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (hold && other.gameObject.name == "ButtonFace")
        {
            foreach (var interactableObject in toActivate)
            {
                interactableObject.GetComponent<InteractablesController>().Signal();
            }
        }
    }
}