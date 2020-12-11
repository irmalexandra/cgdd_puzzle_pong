using System.Linq;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class GameManager : MonoBehaviour
{
    private GameObject[] _dimensions;
    public GameObject GameOverCanvas;
    public GameObject PauseCanvas;
    public GameObject LevelStartCanvas;
    public GameObject LevelCompleteCanvas;
    public static GameManager Instance;
    public int extraBalls;
    private bool _levelStarted;
    private bool _paused;
    public  bool locked;
    public bool DisablePauseMenu;



    void Start()
    {
        Instance = this;
        _dimensions = GameObject.FindGameObjectsWithTag("DimensionZone");
        
        Physics2D.IgnoreLayerCollision(8, 8, true);
        Physics2D.IgnoreLayerCollision(9, 10, true);
        Physics2D.IgnoreLayerCollision(9, 9, true);
        Physics2D.IgnoreLayerCollision(11, 11, true);
        Physics2D.IgnoreLayerCollision(11, 9, true);
        Physics2D.IgnoreLayerCollision(13, 14, true);
        
        Time.timeScale = 0;
    }
    
    private void Update()
    {
        ProcessInputs();
    }

    public void ProcessInputs()
    {
        if (!DisablePauseMenu)
        {
            if (!_levelStarted)
            {
                if (Input.GetButtonDown("Submit"))
                {
                    StartLevel();
                }
            }
            else
            {
                if (Input.GetButtonDown("Cancel"))
                {
                    if (!IsPaused())
                    {
                        Pause();
                        LockMouse();
                    }
                    else
                    {
                        Resume();
                        LockMouse();
                    }
                
                }
            }
        }
        
        
    }

    public void TriggerMenuButtonSoundEffect()
    {
        SoundManager.PlayMenuButtonSoundEffect();
    }


    public void TriggerGameOverMenu()
    {
        GameOverCanvas.SetActive(true);
        LockMouse();
    }

    public void TriggerLevelCompleteMenu()
    {
        TimeManager.Instance.Pause();
        LevelCompleteCanvas.SetActive(true);
        LockMouse();
    }

    public void RestartLevel()
    {
        ScoreTracking.ResetScore();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ReturnToMenu()
    {
        ScoreTracking.ResetScore();
        SceneManager.LoadScene(0);
    }

    public void NextLevel()
    {
        ScoreTracking.ResetScore();
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
                if (dimensionPaddle.active)
                {
                    if (!dimensionPaddle.flashing)
                    {
                        dimensionPaddle.GetComponentInChildren<Light2D>().intensity = 0.5f;
                    }
                    
                }
                else
                {
                    dimensionPaddle.GetComponentInChildren<Light2D>().intensity = 0.1f;
                }
            }
        }
    }

    public void ChangeInput(bool mouse)
    {
        PlayerPrefs.SetInt("Input", mouse ? 1 : 0);
    }

    public bool IsPaused()
    {
        return TimeManager.Instance.GetPaused();
    }

    public void Resume()
    {
        TimeManager.Instance.Resume();
        PauseCanvas.SetActive(false);

    }

    public void Pause()
    {
        TimeManager.Instance.Pause();
        PauseCanvas.SetActive(true);
        
        
    }

    public void LockMouse()
    {
        if (locked)
        {
            Cursor.lockState = CursorLockMode.None;
                locked = !locked;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            locked = !locked;
        }

    }

    public void StartLevel()
    {
        TimeManager.Instance.Resume();
        GameObject.FindGameObjectWithTag("LevelStartMenu").SetActive(false);
        _levelStarted = true;
        LevelTimer.StartTimer();
        LockMouse();
    }
    
}
