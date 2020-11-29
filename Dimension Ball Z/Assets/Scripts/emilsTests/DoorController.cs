using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{

    public void activateDoor()
    {
        JointMotor2D direction = GetComponent<HingeJoint2D>().motor;
        direction.motorSpeed *= -1;
        GetComponent<HingeJoint2D>().motor = direction;
    }
}
