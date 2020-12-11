using System;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    [CanBeNull] public GameObject gameName;
    [CanBeNull] public GameObject inputMenu;
    [CanBeNull] public GameObject mainMenu;
    [CanBeNull] public TextMeshProUGUI levelInputField;
    private bool _backgroundStarted;
    
    private void Update()
    {
        if (_backgroundStarted) return;
        GameManager.Instance.StartLevel();
        _backgroundStarted = true;
    }

    private void Start()
    {
        if (PlayerPrefs.HasKey("Input"))
        {
            if (gameName)
            {
                gameName.gameObject.SetActive(true);
            }

            if (mainMenu)
            {
                mainMenu.gameObject.SetActive(true);
            }
            if (inputMenu)
            {
                inputMenu.gameObject.SetActive(false);
            }
        }

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
        SceneManager.LoadScene("Portal_Insanity");
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void UserInputs(bool mouse)
    {
        if (mouse)
        {
            PlayerPrefs.SetInt("Input", 1);
        }
        else
        {
            PlayerPrefs.SetInt("Input", 0);
        }
    }

    public void SelectLevel()
    {
        int.TryParse(levelInputField.text.Replace("\u200b", ""), out int sceneIndex);
        if (sceneIndex != 0)
        {
            SceneManager.LoadScene(sceneIndex);
        }

    }
}

