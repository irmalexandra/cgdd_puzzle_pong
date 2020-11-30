using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    public GameObject[] toActivate;
    public bool openOnly;
    private bool _open;
    private GameObject _buttonFace;
    private Vector3 _originalPosition;

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

    private void Start()
    {
        _buttonFace = GameObject.Find("ButtonFace");
        _originalPosition = _buttonFace.transform.position;
    }

    

    private void Toggle()
    {
        EmilController.instance.activate(toActivate);
    }

    private void OpenOnly()
    {
        if (!_open)
        {
            EmilController.instance.activate(toActivate);
            _open = true;
        }
    }
    
}
