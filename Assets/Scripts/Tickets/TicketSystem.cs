using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DirtyChefYoga
{
    public class TicketSystem : MonoBehaviour
    {
        public float m_spawnTime = 10.0f;
        private float m_timer;

        public int m_maxNumberOfTickets = 5;

        public GameObject m_ticketPanel;
        public List<FoodTicket> m_foodItems;

        public GameObject m_burgerTicketPrefab;
        public GameObject m_friesTicketPrefab;

        public GameObject m_topBunPrefab;
        public GameObject m_tomatoPrefab;
        public GameObject m_lettucePrefab;
        public GameObject m_cheesePrefab;
        public GameObject m_pattyPrefab;
        public GameObject m_bottomBunPrefab;

        public GameObject m_friesPrefab;

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
            if(m_foodItems.Count >= m_maxNumberOfTickets)
            {
                return;
            }

            if (Random.Range(1, 3) == 1)
            {
                GameObject newTicket = Instantiate(m_burgerTicketPrefab, m_ticketPanel.transform);      //adds a new ticket

                m_foodItems.Add(newTicket.AddComponent<BurgerTicket>());        //adds the burger component to the ticket 

                //calculate ticket place in canvas
                newTicket.transform.position = new Vector3((newTicket.transform.localScale.x * newTicket.GetComponent<RectTransform>().rect.width + 3) * m_foodItems.Count, newTicket.transform.position.y, newTicket.transform.position.z);


                m_foodItems[m_foodItems.Count - 1].MakeFood();      //sets up the burger

                FoodTicket food = m_foodItems[m_foodItems.Count - 1];

                for (int i = ((BurgerTicket)food).m_burgerPieces.Length - 1; i >= 0; i--)
                {

                    switch (((BurgerTicket)food).m_burgerPieces[i])     //adds a sprite of the burger to the slip
                    {
                        case BurgerComponent.BOTTOMBUN:
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
            }
            else
            {
                GameObject newTicket = Instantiate(m_friesTicketPrefab, m_ticketPanel.transform);       //adds new ticket

                m_foodItems.Add(newTicket.AddComponent<FryTicket>());       //adds fry component to ticket

                //calculate ticket place
                newTicket.transform.position = new Vector3((newTicket.transform.localScale.x * newTicket.GetComponent<RectTransform>().rect.width + 3) * m_foodItems.Count, newTicket.transform.position.y, newTicket.transform.position.z);

                m_foodItems[m_foodItems.Count - 1].MakeFood();      //sets up the fries (im pretty sure this function is actually just empty for now, might get improved for use of sauces)

                FoodTicket food = m_foodItems[m_foodItems.Count - 1];

                Instantiate(m_friesPrefab, food.transform.GetChild(0));
            }
        }

        public void RemoveTicket(int ticket = 0)
        {
            GameObject temp = m_foodItems[ticket].gameObject;
            m_foodItems.RemoveAt(ticket);
            Destroy(temp);

            //update positions
        }

        public void UpdatePositions()
        {
            for (int i = 0; i < m_foodItems.Count; i++)
            {
                m_foodItems[i].transform.position = new Vector3((m_foodItems[i].transform.localScale.x * m_foodItems[i].GetComponent<RectTransform>().rect.width + 3) * (i + 1), m_foodItems[i].transform.position.y, m_foodItems[i].transform.position.z);
            }
        }
    }
}
