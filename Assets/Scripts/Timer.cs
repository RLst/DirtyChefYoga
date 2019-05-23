using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace DirtyChefYoga
{
    public class Timer : MonoBehaviour
    {
        public float m_startTime;
        float m_timer;

        public GameObject m_canvasTimer;

        private void Start()
        {
            m_timer = m_startTime;
        }

        private void Update()
        {
            m_timer -= Time.deltaTime;

            if (m_timer <= 0.0f)
            {
                SceneManager.LoadScene("Game Over");
            }
            else
            {
                string temp = Mathf.FloorToInt(m_timer / 60).ToString();
                temp += ":" + ((int)m_timer % 60).ToString("00");

                m_canvasTimer.GetComponent<Text>().text = temp;//m_timer.ToString();
            }
        }

    }
}
