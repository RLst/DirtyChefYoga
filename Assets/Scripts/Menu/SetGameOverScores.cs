using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace DirtyChefYoga
{
    public class SetGameOverScores : MonoBehaviour
    {
        [SerializeField] Text hiScoreText;
        [SerializeField] Text scoreText;

        void Start()
        {
            //If the current score is higher than the highscore then overwrite it
            var hiScore = PlayerPrefs.GetFloat("hiScore", 0);
            var gameScore = PlayerPrefs.GetFloat("gameScore", 0);   //Should be modified from the main gameplay scene
            if (gameScore > hiScore)
                hiScore = gameScore;

            //Display the scores
            hiScoreText.text = "HighScore: " + hiScore;
            scoreText.text = "Score: " + gameScore;

            //Finally update the player prefs
            PlayerPrefs.SetFloat("hiScore", hiScore);
        }
    }
}