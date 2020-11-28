using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalController : MonoBehaviour
{
    // Start is called before the first frame update
    public static PortalController instance;
    public GameObject portal1;
    public GameObject portal2;
    public float timer;

    private void Start()
    {
        instance = this;
        instance.portal1 = portal1;
        instance.portal2 = portal2;
        instance.timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
    }
}
