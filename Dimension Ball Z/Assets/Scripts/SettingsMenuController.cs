﻿
using System;
using UnityEngine;
using UnityEngine.UI;


public class SettingsMenuController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject keyboardAndMouse;
    public GameObject keyboardOnly;
    private void Start()
    {
        if (PlayerPrefs.HasKey("Input"))
        {
            if (PlayerPrefs.GetInt("Input") == 1)
            {
                keyboardAndMouse.GetComponent<Button>().interactable = false;
                keyboardOnly.GetComponent<Button>().interactable = true;
            }
            else
            {
                keyboardAndMouse.GetComponent<Button>().interactable = true;
                keyboardOnly.GetComponent<Button>().interactable = false;
            }
        }
    }
}
