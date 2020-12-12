﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelStartController : MonoBehaviour
{
    public TMPro.TextMeshProUGUI levelNumberText;
    private int _levelNumber;
    void Start()
    {
        _levelNumber = SceneManager.GetActiveScene().buildIndex - 1;
        levelNumberText.text = "Level " + _levelNumber;
    }
}
