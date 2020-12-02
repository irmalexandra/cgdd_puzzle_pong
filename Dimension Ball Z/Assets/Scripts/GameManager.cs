﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private GameObject[] _dimensions;
    private GameObject _gameOverCanvas;
    public static GameManager Instance;
    public int extraBalls;

    public TimeManager timeManager;



    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        _dimensions = GameObject.FindGameObjectsWithTag("DimensionZone");
        _gameOverCanvas = GameObject.FindWithTag("GameOverMenu");
        _gameOverCanvas.SetActive(false);
        
        Physics2D.IgnoreLayerCollision(8,8, true);
        Physics2D.IgnoreLayerCollision(9,10, true);
        Physics2D.IgnoreLayerCollision(9,9, true);

        timeManager.DoSlowmotion();

    }

    // Update is called once per frame


    public void TriggerGameOverMenu()
    {
        _gameOverCanvas.SetActive(true);
    }
    
    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void SwitchPaddle(PaddleController[] otherPaddles)
    {
        foreach (var dimension in _dimensions)
        {
            var paddlesInDimension = dimension.GetComponentsInChildren<PaddleController>();

            foreach (var dimensionPaddle in paddlesInDimension)
            {
                dimensionPaddle.active = otherPaddles.Contains(dimensionPaddle);
            }
        }
    }
    
}
