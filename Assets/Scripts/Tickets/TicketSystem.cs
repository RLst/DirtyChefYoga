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
        [Range(0, 100)]
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
        public List<FoodTicket> tickets;

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
            foreach (FoodTicket f in tickets)
            {
                f.m_timer += Time.deltaTime;
                f.gameObject.transform.GetChild(0).GetChild(0).GetComponent<Image>().fillAmount = 1 - (f.m_timer / f.m_expireTime);
                // Debug.Log(f.m_timer / f.m_expireTime);
                if (f.m_timer >= f.m_expireTime)
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
            if (tickets.Count >= m_maxNumberOfTickets)
            {
                return;
            }

            GameObject newTicket = null;

            if (Random.Range(1, 100) < m_burgerChance)
            {
                newTicket = Instantiate(m_burgerTicketPrefab, m_ticketPanel.transform);      //adds a new ticket

                tickets.Add(newTicket.AddComponent<BurgerTicket>());        //adds the burger component to the ticket 
                tickets[tickets.Count - 1].m_expireTime = m_ticketExpireTimer;

                //calculate ticket place in canvas
                newTicket.transform.position = new Vector3((newTicket.transform.localScale.x * newTicket.GetComponent<RectTransform>().rect.width + 3) * tickets.Count, newTicket.transform.position.y, newTicket.transform.position.z);

                tickets[tickets.Count - 1].MakeFood();      //sets up the burger

                FoodTicket food = tickets[tickets.Count - 1];

                //loops through all children except first (first is timer) to add burger component image to ticket in canvas
                for (int i = ((BurgerTicket)food).fillings.Length - 1; i >= 0; i--)
                {

                    switch (((BurgerTicket)food).fillings[i])     //adds a sprite of the burger to the slip
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

                tickets.Add(newTicket.AddComponent<FryTicket>());       //adds fry component to ticket
                tickets[tickets.Count - 1].m_expireTime = m_ticketExpireTimer;

                //calculate ticket place
                newTicket.transform.position = new Vector3((newTicket.transform.localScale.x * newTicket.GetComponent<RectTransform>().rect.width + 3) * tickets.Count, newTicket.transform.position.y, newTicket.transform.position.z);

                tickets[tickets.Count - 1].MakeFood();      //sets up the fries (im pretty sure this function is actually just empty for now, might get improved for use of sauces)

                FoodTicket food = tickets[tickets.Count - 1];

                Instantiate(m_friesPrefab, food.transform.GetChild(1));

            }
        }

        public void RemoveTicket(int ticket = 0)
        {
            GameObject temp = tickets[ticket].gameObject;
            tickets.RemoveAt(ticket);
            Destroy(temp);

            //update positions
            UpdatePositions();
        }

        public void RemoveTicket(FoodTicket foodTicket)
        {
            tickets.Remove(foodTicket);
            Destroy(foodTicket.gameObject);

            //update positions
            UpdatePositions();
        }

        public void UpdatePositions()
        {
            for (int i = 0; i < tickets.Count; i++)
            {
                tickets[i].transform.position = new Vector3((tickets[i].transform.localScale.x * tickets[i].GetComponent<RectTransform>().rect.width + 3) * (i + 1), tickets[i].transform.position.y, tickets[i].transform.position.z);
            }
        }

        public void CheckTicket(Order order)
        {
            /* Pseudocode
            Incorrect order if there aren't any current tickets

            for each ticket
                if it's a fryTicket
                    Check order are french fries && has only one ingredient && that one ingredient is a fries item
                        CorrectOrderFound(order)
                
                else if it's a burger ticket
                    check that the burger order
                    for each burger


            CorrectOrderFound


            if ticket found
            
             */

            //check if fries or burger 
            //if fries check if cooked
            //if burger check the ingredients
            //also check to make sure the arent any more pieces to the burger ticket

            if (tickets.Count == 0)      //error check for no tickets available currently
            {
                OnIncorrectOrder.Invoke();
                return;
            }

            for (int t = 0; t < tickets.Count; t++)
            {
                //Fries
                if (tickets[t] is FryTicket)
                {
                    if (!(order.ingredients.Count == 1 && order.ingredients[0] is Fries))
                    {
                        if (t == tickets.Count-1) OnIncorrectOrder.Invoke();
                        return;
                    }
                }
                //Burgers
                else if (tickets[t] is BurgerTicket)
                {
                    //Make sure the burger has the right amount of layers
                    if (order.ingredients.Count != ((BurgerTicket)tickets[0]).fillings.Length)       //if there are more or less burger ingredients than in the ticket it is wrong
                    {
                        Debug.Log("Wrong amount of fillings");
                        OnIncorrectOrder.Invoke();
                        return;
                    }

                    //Check each ingredient
                    for (int i = 0; i < order.ingredients.Count; i++)
                    {
                        //TODO: check to see if there are more ingredients than the ticket supports then return to stop any errors
                        //check to see if the burger and ticket ingredient isnt the same
                        if (order.ingredients[i] is BottomBun)
                        {
                            if (!(((BurgerTicket)tickets[t]).fillings[i] == BurgerComponent.BOTTOMBUN))
                            {
                                // OnIncorrectOrder.Invoke();
                                break;
                            }
                        }
                        else if (order.ingredients[i] is Patty && order.ingredients[i].cookStatus == CookStatus.Cooked) //Make sure patty is cooked properly
                        {
                            if (!(((BurgerTicket)tickets[t]).fillings[i] == BurgerComponent.PATTY))
                            {
                                // OnIncorrectOrder.Invoke();
                                break;
                            }
                        }
                        else if (order.ingredients[i] is Cheese)
                        {
                            if (!(((BurgerTicket)tickets[t]).fillings[i] == BurgerComponent.CHEESE))
                            {
                                // OnIncorrectOrder.Invoke();
                                break;
                            }
                        }
                        else if (order.ingredients[i] is Tomatoes)
                        {
                            if (!(((BurgerTicket)tickets[t]).fillings[i] == BurgerComponent.TOMATO))
                            {
                                // OnIncorrectOrder.Invoke();
                                break;
                            }
                        }
                        else if (order.ingredients[i] is Lettuce)
                        {
                            if (!(((BurgerTicket)tickets[t]).fillings[i] == BurgerComponent.LETTUCE))
                            {
                                // OnIncorrectOrder.Invoke();
                                break;
                            }
                        }
                        else if (order.ingredients[i] is TopBun)
                        {
                            if (!(((BurgerTicket)tickets[t]).fillings[i] == BurgerComponent.TOPBUN))
                            {
                                // OnIncorrectOrder.Invoke();
                                break;
                            }
                        }
                    }
                }   //End of Ticket checking logic

                //If you mae it here means correct order?
                onCorrectOrder(t);
                return;
            }

            void onCorrectOrder(int t)
            {
                //CORRECT ORDER! Everything checks out
                AddScore(order.totalScoreValue);
                RemoveTicket(tickets[t]);
                OnCorrectOrder.Invoke();
            }
        }


        public void AddScore(float increment)
        {
            m_CurrentScore += increment;
        }
    }
}
