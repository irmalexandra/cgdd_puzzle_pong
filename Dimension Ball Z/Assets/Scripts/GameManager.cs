using System.Linq;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.SceneManagement;

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
    private GameObject[] _paddles;
    public bool locked;
    public bool disableSlowmotion;
    public bool DisablePauseMenu;
    public bool StartInBeginning;

    private bool _shouldBeLocked;


    void Start()
    {
        Instance = this;
        _dimensions = GameObject.FindGameObjectsWithTag("DimensionZone");
        _paddles = GameObject.FindGameObjectsWithTag("Paddle");
        Physics2D.IgnoreLayerCollision(8, 8, true);
        Physics2D.IgnoreLayerCollision(9, 10, true);
        Physics2D.IgnoreLayerCollision(9, 9, true);
        Physics2D.IgnoreLayerCollision(11, 11, true);
        Physics2D.IgnoreLayerCollision(11, 9, true);
        Physics2D.IgnoreLayerCollision(13, 14, true);
        if (PlayerPrefs.GetInt("Slowmotion") == 1)
        {
            DisableSlowMo(true);
        }
        else
        {
            DisableSlowMo(false);
        }

        if (!StartInBeginning)
        {
            Time.timeScale = 0;

        }
    }
    
    private void Update()
    {
        if (DisablePauseMenu) return;
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
                    if (!IsPaused()) Pause();
                    else Resume();
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
        TimeManager.Instance.Pause();
        GameOverCanvas.SetActive(true);
        LockMouse();
        DisablePauseMenu = true;
    }

    public void TriggerLevelCompleteMenu()
    {
        TimeManager.Instance.Pause();
        LevelCompleteCanvas.SetActive(true);
        LockMouse();
        DisablePauseMenu = true;
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
        foreach (GameObject paddle in _paddles)
        {
            paddle.GetComponent<PaddleController>().UpdateInput(mouse);
        }
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
        LockMouse();
    }

    public void Pause()
    {
        TimeManager.Instance.Pause();
        PauseCanvas.SetActive(true);
        LockMouse();
    }

    public void LockMouse()
    {
        if (locked)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            locked = !locked;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            locked = !locked;
        }
    }

    public void DisableSlowMo(bool on)
    {
        disableSlowmotion = on;
        SlowMotionTrigger.DisableSlowmotion(on);
        if(on)
        {
            PlayerPrefs.SetInt("Slowmotion", 1);
            
        }
        else
        {
            PlayerPrefs.SetInt("Slowmotion", 0);
        }
    }

    public void StartLevel()
    {
        Resume();
        
        GameObject.FindGameObjectWithTag("LevelStartMenu").SetActive(false);
        _levelStarted = true;
        LevelTimer.StartTimer();
        
    }


    
}
