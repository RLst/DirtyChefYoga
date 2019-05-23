using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

            m_canvasTimer.GetComponent<Text>().text = m_timer.ToString();
        }

    }
}
