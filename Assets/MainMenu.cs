using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    [SerializeField] private GameObject optionsGo;
    [SerializeField] private float tweenDuration;
    [SerializeField] private Ease tweenEase;
    [SerializeField] private bool isTweening;
    public void StartGame()
    {
        SceneManager.LoadScene("Main Game", LoadSceneMode.Single);
    }

    public void OpenOptions()
    {
        if (isTweening)
        {
            return;
        }

        optionsGo.transform.DOKill();
        isTweening = true;
        optionsGo.transform.localScale = Vector3.zero;
        optionsGo.SetActive(true);
        optionsGo.transform.DOScale(1, tweenDuration).SetEase(tweenEase).OnComplete(() => isTweening = false);
    }

    public void CloseOptions()
    {
        if (isTweening)
        {
            return;
        }

        isTweening = true;
        optionsGo.transform.DOScale(0, tweenDuration).SetEase(tweenEase).OnComplete(() => isTweening = false);
        
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ChangeScreenMode(bool fullscreen)
    {
        if (fullscreen)
        {
            Screen.fullScreen = true;
        }
        else
        {
            Screen.fullScreen = false;
        }
    }

    public void UpdateResolution(int index)
    {
        Vector2Int resolution = new Vector2Int(0, 0);
        switch (index)
        {
            case 0:
                resolution.x = 1366;
                resolution.y = 768;
                break;
            case 1:
                resolution.x = 1600;
                resolution.y = 900;
                break;
            case 2:
                resolution.x = 1920;
                resolution.y = 1080;
                break;
            case 3:
                resolution.x = 2560;
                resolution.y = 1440;
                break;
            case 4:
                resolution.x = 3840;
                resolution.y = 2160;
                break;
        }
        Screen.SetResolution(resolution.x, resolution.y, Screen.fullScreen);
        
    }
}
