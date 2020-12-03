using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private GameObject[] _dimensions;
    public static GameObject GameOverCanvas;
    public static GameObject PauseCanvas;
    public static GameObject LevelStartCanvas;
    public static GameObject LevelCompleteCanvas;
    
    public static GameManager Instance;
    public int extraBalls;

    private bool _levelStarted;
    private bool _paused;
   
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        _dimensions = GameObject.FindGameObjectsWithTag("DimensionZone");
        
        GameOverCanvas = GameObject.FindWithTag("GameOverMenu");
        PauseCanvas = GameObject.FindWithTag("PauseMenu");
        LevelStartCanvas = GameObject.FindWithTag("LevelStartMenu");
        LevelCompleteCanvas = GameObject.FindWithTag("LevelCompleteMenu");
        GameOverCanvas.SetActive(false);
        PauseCanvas.SetActive(false);
        LevelCompleteCanvas.SetActive(false);
        
        Physics2D.IgnoreLayerCollision(8,8, true);
        Physics2D.IgnoreLayerCollision(9,10, true);
        Physics2D.IgnoreLayerCollision(9,9, true);
        
        Time.timeScale = 0;
    }
    
    private void Update()
    {
        ProcessInputs();

        Physics2D.IgnoreLayerCollision(8, 8, true);
        Physics2D.IgnoreLayerCollision(9, 10, true);
        Physics2D.IgnoreLayerCollision(9, 9, true);
        
    }

    void ProcessInputs()
    {
        if (!_levelStarted)
        {
            if (Input.GetButtonDown("Jump"))
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
        GameOverCanvas.SetActive(true);
    }

    public void TriggerLevelCompleteMenu()
    {
        LevelCompleteCanvas.SetActive(true);
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
        return TimeManager.Instance.GetPaused();
    }

    public void Resume()
    {
        TimeManager.Instance.Resume();
        Debug.Log(PauseCanvas);
        PauseCanvas.SetActive(false);
    
    }

    public void Pause()
    {
        TimeManager.Instance.Pause();
        Debug.Log(PauseCanvas);
        PauseCanvas.SetActive(true);
    }

    private void StartLevel()
    {
        TimeManager.Instance.Resume();
        GameObject.FindGameObjectWithTag("LevelStartMenu").SetActive(false);
        _levelStarted = true;
    }
    
}
