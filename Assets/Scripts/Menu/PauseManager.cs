using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour {

    public GameObject pausePanel;

    public Button resumeButton;
    public Button restartButton;
    public Button menuButton;

    bool isPaused = false;

    void Start() {
        resumeButton.GetComponent<Button>().onClick.AddListener(PressResume);
        restartButton.GetComponent<Button>().onClick.AddListener(PressRestart);
        menuButton.GetComponent<Button>().onClick.AddListener(PressMenu);
        pausePanel.SetActive(false);
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            isPaused = !isPaused;
            if (isPaused) {
                Time.timeScale = 0.0f;
                pausePanel.SetActive(true);
            } else {
                Time.timeScale = 1.0f;
                pausePanel.SetActive(false);
            }
        }
    }

    void PressResume() {
        Time.timeScale = 1.0f;
        isPaused = false;
        pausePanel.SetActive(false);
    }

    void PressRestart() {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(1);
    }

    void PressMenu() {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(0);
    }
}
