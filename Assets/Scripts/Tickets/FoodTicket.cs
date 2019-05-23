using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DirtyChefYoga
{
    public abstract class FoodTicket : MonoBehaviour
    {
        public TicketSystem m_ticketSystem;

        public float m_expireTime;
        public float m_timer = 0;

        public abstract void MakeFood(int fillingAmount = 3);
    }
}
