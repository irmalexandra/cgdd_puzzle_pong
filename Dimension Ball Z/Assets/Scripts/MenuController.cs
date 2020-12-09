﻿using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public GameObject gameName;
    public GameObject inputMenu;
    public GameObject mainMenu;
    public TextMeshProUGUI levelInputField;
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
            gameName.gameObject.SetActive(true);
            mainMenu.gameObject.SetActive(true);
            inputMenu.gameObject.SetActive(false);
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

