using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractablesController : MonoBehaviour
{
    
    public void activate()
    {
        if (CompareTag("Door"))
        {
            openDoor();
        }
    }



    private void openDoor()
    {
        JointMotor2D direction = GetComponent<HingeJoint2D>().motor;
        direction.motorSpeed *= -1;
        GetComponent<HingeJoint2D>().motor = direction;
    }
}




