using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmilController : MonoBehaviour
{
    // Start is called before the first frame update
    public static EmilController instance;
    
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame

    public void activate(GameObject[] objects)
    {
        foreach (var toActivate in objects)
        {
            toActivate.GetComponent<InteractablesController>().activate();
        }
    }
}
