using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour {
    [SerializeField] private Button continueBtn; 
    [SerializeField] private Button quitBtn; 

    void Start() {
        continueBtn.onClick.AddListener(ContinueClicked);
        quitBtn.onClick.AddListener(QuitClicked);
    }

    public void ContinueClicked() {
        Debug.Log("Continue");
        gameObject.SetActive(false);
    }

    public void QuitClicked() {
        UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }
}
