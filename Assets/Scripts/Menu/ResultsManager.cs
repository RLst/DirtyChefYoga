using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class ResultsManager : MonoBehaviour {

    public float playerScore;
    public float highScore;

    public Button menuButton;
    public Button restartButton;
    public Button exitButton;

    public GameObject scoreText;
    public GameObject highscoreText;

    void Start () {
        menuButton.GetComponent<Button>().onClick.AddListener(PressMenu);
        restartButton.GetComponent<Button>().onClick.AddListener(PressRestart);
        exitButton.GetComponent<Button>().onClick.AddListener(PressQuit);

        playerScore = PlayerPrefs.GetFloat("FinalScore");
        highScore = PlayerPrefs.GetFloat("highscore");

        if (playerScore > highScore) {
            PlayerPrefs.SetFloat("highscore", playerScore);
            highScore = playerScore;
        }

        scoreText.GetComponent<TextMeshProUGUI>().text = "Score: " + PlayerPrefs.GetFloat("FinalScore").ToString("C");
        highscoreText.GetComponent<TextMeshProUGUI>().text = "Highscore: " + PlayerPrefs.GetFloat("highscore").ToString("C");
    }

    void PressRestart() {
        SceneManager.LoadScene(1);
    }

    void PressMenu() {
        SceneManager.LoadScene(0);
    }

    void PressQuit() {
        Application.Quit();
    }

}
