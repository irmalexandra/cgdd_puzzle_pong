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

    private void Update()
    {
        if (_buttonFace.transform.position.y > _originalPosition.y)
        {
            _buttonFace.transform.position = _originalPosition;
        }
    }

    private void Toggle()
    {
        foreach (var gameobject in toActivate)
        {
            gameobject.gameObject.GetComponent<DoorController>().activate();
        }
    }

    private void OpenOnly()
    {
        if (!_open)
        {
            foreach (var gameobject in toActivate)
            {
                gameobject.gameObject.GetComponent<DoorController>().activate();
            }
            _open = true;
        }
    }
    
}
