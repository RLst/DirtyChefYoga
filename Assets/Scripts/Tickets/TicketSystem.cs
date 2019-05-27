using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using UnityEngine.Events;

namespace DirtyChefYoga
{
    public class TicketSystem : MonoBehaviour
    {
        [Range(0,100)]
        public float m_burgerChance;

        public float m_spawnTime = 10.0f;
        private float m_timer;

        public float m_ticketExpireTimer;

        public int m_maxNumberOfTickets = 5;

        public float m_foodScoreAmount;
        public float m_CurrentScore;

        public GameObject m_scoreCanvas;
        private Text m_scoreText;


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

		[SerializeField] UnityEvent OnIncorrectOrder, OnCorrectOrder;

        private void Awake()
        {
            m_scoreText = m_scoreCanvas?.GetComponent<Text>();
            m_timer = m_spawnTime;
            SpawnTicket();
        }

        private void Update()
        {
            m_timer -= Time.deltaTime;

            m_scoreText.text = m_CurrentScore.ToString("0.00");

            if (m_timer <= 0)
            {
                m_timer += m_spawnTime;
                SpawnTicket();
            }

            FoodTicket temp = null;
            foreach(FoodTicket f in m_foodItems)
            {
                f.m_timer += Time.deltaTime;
                f.gameObject.transform.GetChild(0).GetChild(0).GetComponent<Image>().fillAmount =  1 - (f.m_timer / f.m_expireTime);
                // Debug.Log(f.m_timer / f.m_expireTime);
                if(f.m_timer >= f.m_expireTime)
                {
                    temp = f;
                }
            }
            if (temp != null)
            {
                RemoveTicket(temp);
            }
        }

        public void SpawnTicket()
        {
            if(m_foodItems.Count >= m_maxNumberOfTickets)
            {
                return;
            }

            GameObject newTicket = null;

            if (Random.Range(1, 100) < m_burgerChance)
            {
                newTicket = Instantiate(m_burgerTicketPrefab, m_ticketPanel.transform);      //adds a new ticket

                m_foodItems.Add(newTicket.AddComponent<BurgerTicket>());        //adds the burger component to the ticket 
                m_foodItems[m_foodItems.Count-1].m_expireTime = m_ticketExpireTimer;
                
                //calculate ticket place in canvas
                newTicket.transform.position = new Vector3((newTicket.transform.localScale.x * newTicket.GetComponent<RectTransform>().rect.width + 3) * m_foodItems.Count, newTicket.transform.position.y, newTicket.transform.position.z);

                m_foodItems[m_foodItems.Count - 1].MakeFood();      //sets up the burger

                FoodTicket food = m_foodItems[m_foodItems.Count - 1];

                //loops through all children except first (first is timer) to add burger component image to ticket in canvas
                for (int i = ((BurgerTicket)food).m_burgerPieces.Length - 1; i >= 0; i--)
                {

                    switch (((BurgerTicket)food).m_burgerPieces[i])     //adds a sprite of the burger to the slip
                    {
                        case BurgerComponent.BOTTOMBUN:
                            Instantiate(m_bottomBunPrefab, food.transform.GetChild(i + 1));
                            break;
                        case BurgerComponent.PATTY:
                            Instantiate(m_pattyPrefab, food.transform.GetChild(i + 1));
                            break;
                        case BurgerComponent.CHEESE:
                            Instantiate(m_cheesePrefab, food.transform.GetChild(i + 1));
                            break;
                        case BurgerComponent.LETTUCE:
                            Instantiate(m_lettucePrefab, food.transform.GetChild(i + 1));
                            break;
                        case BurgerComponent.TOMATO:
                            Instantiate(m_tomatoPrefab, food.transform.GetChild(i + 1));
                            break;
                        case BurgerComponent.TOPBUN:
                            Instantiate(m_topBunPrefab, food.transform.GetChild(i + 1));
                            break;
                    }
                }
            }
            else
            {
                newTicket = Instantiate(m_friesTicketPrefab, m_ticketPanel.transform);       //adds new ticket

                m_foodItems.Add(newTicket.AddComponent<FryTicket>());       //adds fry component to ticket
                m_foodItems[m_foodItems.Count - 1].m_expireTime = m_ticketExpireTimer;

                //calculate ticket place
                newTicket.transform.position = new Vector3((newTicket.transform.localScale.x * newTicket.GetComponent<RectTransform>().rect.width + 3) * m_foodItems.Count, newTicket.transform.position.y, newTicket.transform.position.z);

                m_foodItems[m_foodItems.Count - 1].MakeFood();      //sets up the fries (im pretty sure this function is actually just empty for now, might get improved for use of sauces)

                FoodTicket food = m_foodItems[m_foodItems.Count - 1];

                Instantiate(m_friesPrefab, food.transform.GetChild(1));

            }
        }

        public void RemoveTicket(int ticket = 0)
        {
            GameObject temp = m_foodItems[ticket].gameObject;
            m_foodItems.RemoveAt(ticket);
            Destroy(temp);

            //update positions
            UpdatePositions();
        }

        public void RemoveTicket(FoodTicket foodTicket)
        {
            m_foodItems.Remove(foodTicket);
            Destroy(foodTicket.gameObject);

            //update positions
            UpdatePositions();
        }

        public void UpdatePositions()
        {
            for (int i = 0; i < m_foodItems.Count; i++)
            {
                m_foodItems[i].transform.position = new Vector3((m_foodItems[i].transform.localScale.x * m_foodItems[i].GetComponent<RectTransform>().rect.width + 3) * (i + 1), m_foodItems[i].transform.position.y, m_foodItems[i].transform.position.z);
            }
        }

        public void CheckTicket(Order itemToCheck)
        {
            //check if fries or burger 
            //if fries check if cooked
            //if burger check the ingredients
            //also check to make sure the arent any more pieces to the burger ticket

            if(m_foodItems.Count == 0)      //error check for no tickets available currently
            {
				OnIncorrectOrder.Invoke();
                // m_incorrectOrderSound.GetComponent<AudioSource>().Play();
                return;
            }

            if(m_foodItems[0].GetType() == typeof(FryTicket))
            {
                if(!(itemToCheck.ingredients.Count == 1 && itemToCheck.ingredients[0].GetType() == typeof(Fries)))
                {
					OnIncorrectOrder.Invoke();
                    // m_incorrectOrderSound.GetComponent<AudioSource>().Play();
                    return;
                }
            }
            else
            {
                if(itemToCheck.ingredients.Count != ((BurgerTicket)m_foodItems[0]).m_burgerPieces.Length)       //if there are more or less burger ingredients than in the ticket it is wrong
                {
                    OnIncorrectOrder.Invoke();
                    return;
                }
                for (int i = 0; i < itemToCheck.ingredients.Count; i++)
                {
                    //TODO: check to see if there are more ingredients than the ticket supports then return to stop any errors
                    //check to see if the burger and ticket ingredient isnt the same
                    if (itemToCheck.ingredients[i] is BottomBun)
                    {
                        if (!(((BurgerTicket)m_foodItems[0]).m_burgerPieces[i] == BurgerComponent.BOTTOMBUN))
                        {
                            OnIncorrectOrder.Invoke();
                            return;
                        }
                    }
                    else if (itemToCheck.ingredients[i] is Patty && itemToCheck.ingredients[i].cookStatus == CookStatus.Cooked)	//Make sure patty is cooked properly
                    {
                        if (!(((BurgerTicket)m_foodItems[0]).m_burgerPieces[i] == BurgerComponent.PATTY))
                        {
                            OnIncorrectOrder.Invoke();
                            return;
                        }
                    }
                    else if (itemToCheck.ingredients[i] is Cheese)
                    {
                        if (!(((BurgerTicket)m_foodItems[0]).m_burgerPieces[i] == BurgerComponent.CHEESE))
                        {
                            OnIncorrectOrder.Invoke();
                            return;
                        }
                    }
                    else if (itemToCheck.ingredients[i] is Tomatoes)
                    {
                        if (!(((BurgerTicket)m_foodItems[0]).m_burgerPieces[i] == BurgerComponent.TOMATO))
                        {
                            OnIncorrectOrder.Invoke();
                            return;
                        }
                    }
                    else if (itemToCheck.ingredients[i] is Lettuce)
                    {
                        if (!(((BurgerTicket)m_foodItems[0]).m_burgerPieces[i] == BurgerComponent.LETTUCE))
                        {
                            OnIncorrectOrder.Invoke();
                            return;
                        }
                    }
                    else if (itemToCheck.ingredients[i] is TopBun)
                    {
                        if (!(((BurgerTicket)m_foodItems[0]).m_burgerPieces[i] == BurgerComponent.TOPBUN))
                        {
                            OnIncorrectOrder.Invoke();
                            return;
                        }
                    }
                }
            }

            m_CurrentScore += m_foodScoreAmount;
			OnCorrectOrder.Invoke();
        }
    }
}
