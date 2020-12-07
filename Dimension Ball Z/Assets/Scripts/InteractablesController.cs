using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class InteractablesController : MonoBehaviour
{

    public bool toggle;
    public GameObject[] ignorables;
    private bool _toggled;


    private void Start()
    {
        foreach (var item in ignorables)
        {
            Physics2D.IgnoreCollision(item.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }
        
    }


    public void Signal()
    {
        if (toggle)
        {
            Toggle();
        }
        else
        {
            NotToggle();
        }
  
    }


    private void Toggle()
    {
        if (!_toggled)
        {
            _toggled = true;
            Activate();
        }
    }

    private void NotToggle()
    {
        Activate();
    }


    private void Activate()
    {
        if (CompareTag("Door"))
        {
            OpenDoor();
        }
        if (CompareTag("Pear"))
        {
            StartCoroutine(FlashGreen());
        }

        if (CompareTag("Pusher"))
        {
            Push();
        }
        
    }

    private void Push()
    {
        JointMotor2D sliderMotor = gameObject.GetComponent<SliderJoint2D>().motor;
        sliderMotor.motorSpeed *= -1;
        gameObject.GetComponent<SliderJoint2D>().motor = sliderMotor;
    }
    
    private void OpenDoor()
    {
        HingeJoint2D hinge = gameObject.GetComponent<HingeJoint2D>();
        JointMotor2D direction = hinge.motor;
        Rigidbody2D door = gameObject.GetComponent<Rigidbody2D>();
        door.constraints = RigidbodyConstraints2D.None;
        direction.motorSpeed *= -1;
        hinge.motor = direction;
    }

    private IEnumerator FlashGreen()
    {
        Light2D light = GetComponentInChildren<Light2D>();
        Color originalColor = light.color;
        light.color = Color.green;
        yield return new WaitForSeconds(0.3f);
        light.color = originalColor;
    }
}




