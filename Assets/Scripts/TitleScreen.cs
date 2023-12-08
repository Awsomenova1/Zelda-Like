using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleScreen : MonoBehaviour {
    [SerializeField] private Button startBtn;    
    [SerializeField] private Button howToPlayBtn;    
    [SerializeField] private Button creditsBtn;    
    [SerializeField] private Button quitBtn;    
    [SerializeField] private Button howToPlayBack;    
    [SerializeField] private Button creditsBack;    
    [SerializeField] private GameObject howToPlay;
    [SerializeField] private GameObject title;
    [SerializeField] private GameObject credits;

    void OnEnable() {
        startBtn.onClick.AddListener(StartClicked);
        howToPlayBtn.onClick.AddListener(HowToPlayClicked);
        creditsBtn.onClick.AddListener(CreditsClicked);
        quitBtn.onClick.AddListener(QuitClicked);

        howToPlayBack.onClick.AddListener(HowToPlayBackClicked);
        creditsBack.onClick.AddListener(CreditsBackClicked);
    }

    void OnDisable() {
        startBtn.onClick.RemoveAllListeners();
        howToPlayBtn.onClick.RemoveAllListeners();
        creditsBtn.onClick.RemoveAllListeners();
        quitBtn.onClick.RemoveAllListeners();
        howToPlayBack.onClick.RemoveAllListeners();
        creditsBack.onClick.RemoveAllListeners();
    }

    void StartClicked() {
        SceneManager.LoadScene(1);
    }

    void HowToPlayClicked() {
        title.SetActive(false);
        howToPlay.SetActive(true);
    }

    void CreditsClicked() {
        title.SetActive(false);
        credits.SetActive(true);
    }

    void HowToPlayBackClicked() {
        howToPlay.SetActive(false);
        title.SetActive(true);
    }

    void CreditsBackClicked() {
        credits.SetActive(false);
        title.SetActive(true);
    }

    void QuitClicked() {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
        Application.Quit();
    }
}
