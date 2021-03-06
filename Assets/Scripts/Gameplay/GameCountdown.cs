﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace DirtyChefYoga
{
    public class GameCountdown : MonoBehaviour
    {
        public float m_startTime;
        float m_timer;

        public GameObject m_canvasTimer;
        public TicketSystem m_ticketSystem;

        private void Awake()
        {
            m_timer = m_startTime;
            m_ticketSystem = GetComponent<TicketSystem>();
        }

        private void Update()
        {
            m_timer -= Time.deltaTime;

            if (m_timer <= 0.0f)
            {
                EndGame();
            }
            else
            {
                string timeVisual = Mathf.FloorToInt(m_timer / 60f).ToString();
                timeVisual += ":" + ((int)m_timer % 60).ToString("00");

                m_canvasTimer.GetComponent<Text>().text = timeVisual;//m_timer.ToString();
            }
        }

        public void EndGame()
        {
            PlayerPrefs.SetFloat("gameScore", m_ticketSystem.m_CurrentScore);       //Game over score
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
