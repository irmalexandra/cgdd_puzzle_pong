using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    private bool _backgroundStarted;
    
    private void Update()
    {
        if (_backgroundStarted) return;
        GameManager.Instance.StartLevel();
        _backgroundStarted = true;
    }

    public void PlayTutorial()
    {
        SceneManager.LoadScene("PaddleTutorial");
    }
    public void ShowTutorialI()
    {
        SceneManager.LoadScene("Tutorial");
    }

    // Start is called before the first frame update
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void BallHell()
    {

        SceneManager.LoadScene("BallHell");
    }

    public void PortalInsanity()
    {
        SceneManager.LoadScene("PortalInsanity");
    }

    public void Quit()
    {
        Application.Quit();
    }


    

}

