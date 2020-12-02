using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private GameObject[] _dimensions;
    private GameObject _gameOverCanvas;
<<<<<<< HEAD
    private GameObject _pauseCanvas;
=======
    private GameObject _levelCompleteCanvas;
>>>>>>> c477972f77567845b9f9294b0f9a531d03c8987c
    public static GameManager Instance;
    public int extraBalls;

    private bool _levelStarted;
    private bool _paused;

    public TimeManager timeManager;



    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        _dimensions = GameObject.FindGameObjectsWithTag("DimensionZone");
        
        _gameOverCanvas = GameObject.FindWithTag("GameOverMenu");
        _pauseCanvas = GameObject.FindWithTag("PauseMenu");
        _gameOverCanvas.SetActive(false);
        _pauseCanvas.SetActive(false);
        
<<<<<<< HEAD
        Physics2D.IgnoreLayerCollision(8,8, true);
        Physics2D.IgnoreLayerCollision(9,10, true);
        Physics2D.IgnoreLayerCollision(9,9, true);
        
        timeManager.Pause();
    }
    
    private void Update()
    {
        ProcessInputs();
=======
        _levelCompleteCanvas = GameObject.FindWithTag("LevelCompleteMenu");
        _levelCompleteCanvas.SetActive(false);

        Physics2D.IgnoreLayerCollision(8, 8, true);
        Physics2D.IgnoreLayerCollision(9, 10, true);
        Physics2D.IgnoreLayerCollision(9, 9, true);

        timeManager.DoSlowmotion();

>>>>>>> c477972f77567845b9f9294b0f9a531d03c8987c
    }

    void ProcessInputs()
    {
        if (!_levelStarted)
        {
            if (Input.GetButtonDown("Jump"))
            {
                Resume();
            }
        }
        else
        {
            if (Input.GetButtonDown("Cancel"))
            {
                if (!IsPaused())
                {
                    Pause();
                }
                else
                {
                    Resume();
                }
                
            }
        }
        
    }
    // Update is called once per frame


    public void TriggerGameOverMenu()
    {
        _gameOverCanvas.SetActive(true);
    }

    public void TriggerLevelCompleteMenu()
    {
        Debug.Log("in trigger level complete menu");
        _levelCompleteCanvas.SetActive(true);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
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

    public bool IsPaused()
    {
        return timeManager.GetPaused();
    }

    public void Resume()
    {
        Debug.Log("Triggering Resume");
        if (!_levelStarted)
        {
            timeManager.Resume();
            GameObject.FindGameObjectWithTag("LevelStartMenu").SetActive(false);
            _levelStarted = true;
            
        }
        else
        {
            timeManager.Resume();
            _pauseCanvas.SetActive(false);
        }
        
        
        
    }

    public void Pause()
    {
        timeManager.Pause();
        _pauseCanvas.SetActive(true);
    }
    
}
