using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class ResultsManager : MonoBehaviour {

    public float playerScore;
    public float highScore;

    public GameObject scoreText;
    public GameObject highscoreText;

    public Button restartButton;
    public Button menuButton;
    public Button exitButton;

    void Start () {
        restartButton.GetComponent<Button>().onClick.AddListener(PressRestart);
        menuButton.GetComponent<Button>().onClick.AddListener(PressMenu);
        exitButton.GetComponent<Button>().onClick.AddListener(PressExit);

        playerScore = PlayerPrefs.GetFloat("score");
        highScore = PlayerPrefs.GetFloat("highscore");

        if (playerScore > highScore) {
            PlayerPrefs.SetFloat("highscore", playerScore);
            highScore = playerScore;
        }

        scoreText.GetComponent<TextMeshProUGUI>().text = "Time: " + playerScore;
        highscoreText.GetComponent<TextMeshProUGUI>().text = "Highscore: " + highScore;
    }

    void PressRestart() {
        SceneManager.LoadScene(1);
    }

    void PressMenu() {
        SceneManager.LoadScene(0);
    }

    void PressExit() {
        Application.Quit();
    }

}
