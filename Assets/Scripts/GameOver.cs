using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour {
    [SerializeField] private PlayerStatistics stats;
    [SerializeField] private GameObject player;
    [SerializeField] private CanvasGroup group;
    [SerializeField] private Button continueBtn; 
    [SerializeField] private Button quitBtn; 

    void OnEnable() {
        stats.PlayerDied.AddListener(OnPlayerDied);
        continueBtn.onClick.AddListener(OnContinueClicked);
        quitBtn.onClick.AddListener(OnQuitClicked);
    }

    void OnDisable() {
        continueBtn.onClick.RemoveAllListeners();
        quitBtn.onClick.RemoveAllListeners();
        stats.PlayerDied.RemoveAllListeners();
    }

    public void SetUIVisible(bool state) {
        group.alpha = state == true ? 1 : 0; 
        group.interactable = state;
        group.blocksRaycasts = state;
    }

    public void OnPlayerDied() {
        SetUIVisible(true);
    }

    public void OnContinueClicked() {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); 
    }

    public void OnQuitClicked() {
        UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }
}
