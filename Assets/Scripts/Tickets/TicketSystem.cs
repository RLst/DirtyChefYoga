using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DirtyChefYoga
{
    public class TicketSystem : MonoBehaviour
    {
        public float m_spawnTime = 10.0f;
        private float m_timer;

        public GameObject m_ticketPanel;
        public List<FoodTicket> m_burgers;

        public GameObject m_burgerTicketPrefab;

        public GameObject m_topBunPrefab;
        public GameObject m_tomatoPrefab;
        public GameObject m_lettucePrefab;
        public GameObject m_cheesePrefab;
        public GameObject m_pattyPrefab;
        public GameObject m_bottomBunPrefab;

        private void Awake()
        {
            m_timer = m_spawnTime;
            SpawnTicket();
        }

        private void Update()
        {
            m_timer -= Time.deltaTime;

            if (m_timer <= 0)
            {
                m_timer += m_spawnTime;
                SpawnTicket();
            }
        }

        public void SpawnTicket()
        {
            GameObject newTicket = Instantiate(m_burgerTicketPrefab, m_ticketPanel.transform);

            m_burgers.Add(newTicket.AddComponent<BurgerTicket>());
            //JM:STARTHERE, need to get the tickets to spawn next to eachother
            newTicket.transform.position = new Vector3((newTicket.transform.localScale.x + 3) * m_burgers.Count, newTicket.transform.position.y, newTicket.transform.position.z);

            //m_burgers.Add(m_ticketPanel.transform.GetChild(0).gameObject.AddComponent<BurgerTicket>());
            m_burgers[m_burgers.Count - 1].MakeFood();

            FoodTicket food = m_ticketPanel.transform.GetChild(0).GetComponent<BurgerTicket>();

            food = m_burgers[m_burgers.Count - 1];

            for (int i = ((BurgerTicket)food).m_burgerPieces.Length - 1; i >= 0; i--)
            {

                switch (((BurgerTicket)food).m_burgerPieces[i])
                {
                    case BurgerComponent.BOTTOMBUN:
                        //food.transform.GetChild(i).transform(m_bottomBunPrefab);
                        Instantiate(m_bottomBunPrefab, food.transform.GetChild(i));
                        break;
                    case BurgerComponent.PATTY:
                        Instantiate(m_pattyPrefab, food.transform.GetChild(i));
                        break;
                    case BurgerComponent.CHEESE:
                        Instantiate(m_cheesePrefab, food.transform.GetChild(i));
                        break;
                    case BurgerComponent.LETTUCE:
                        Instantiate(m_lettucePrefab, food.transform.GetChild(i));
                        break;
                    case BurgerComponent.TOMATO:
                        Instantiate(m_tomatoPrefab, food.transform.GetChild(i));
                        break;
                    case BurgerComponent.TOPBUN:
                        Instantiate(m_topBunPrefab, food.transform.GetChild(i));
                        break;
                }
            }
                
           // temp.transform.GetChild(0).Equals();
        }
    }
}
