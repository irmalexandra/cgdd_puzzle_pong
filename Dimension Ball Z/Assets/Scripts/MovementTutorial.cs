﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementTutorial : MonoBehaviour
{
    private TMPro.TextMeshProUGUI _textBox;

    private void Start()
    {
        _textBox = GameObject.FindGameObjectWithTag("TutorialTopMid").GetComponent<TMPro.TextMeshProUGUI>();
        Debug.Log(_textBox);
    }

    private void Update()
    {
        if (ScoreTracking.CheckScore())
        {
            _textBox.text = "Good job!!! Now bounce the ball into the Green Win Portal to complete the level!";
        }
    }
}
