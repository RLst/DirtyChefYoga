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
    public Button quitButton;

    bool isPaused = false;

    public GameObject eventSystem;

    void Start() {
        resumeButton.GetComponent<Button>().onClick.AddListener(PressResume);
        restartButton.GetComponent<Button>().onClick.AddListener(PressRestart);
        menuButton.GetComponent<Button>().onClick.AddListener(PressMenu);
        quitButton.GetComponent<Button>().onClick.AddListener(PressQuit);
        pausePanel.SetActive(false);
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Joystick1Button7)) {
            isPaused = !isPaused;
            if (isPaused) {
                Time.timeScale = 0.0f;
                pausePanel.SetActive(true);

                // Deselect previous selection and play its deselect transition:
                if (eventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().currentSelectedGameObject != null) {
                    var previous = eventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().currentSelectedGameObject.GetComponent<Selectable>();
                    if (previous != null) {
                        previous.OnDeselect(null);
                        eventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(null);
                    }
                }
                // Select button and play its selection transition:
                eventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(resumeButton.gameObject);
                resumeButton.OnSelect(null);
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

    void PressQuit() {
        Application.Quit();
    }
}
