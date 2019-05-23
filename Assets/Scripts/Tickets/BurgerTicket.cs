using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DirtyChefYoga
{
    public class BurgerTicket : FoodTicket
    {
        public BurgerComponent[] m_burgerPieces;

        public override void MakeFood(int fillingAmount = 3)
        {
            m_burgerPieces = new BurgerComponent[fillingAmount + 2];

            m_burgerPieces[0] = BurgerComponent.BOTTOMBUN;
            for (int i = fillingAmount; i > 0; i--)
            {
                m_burgerPieces[i] = (BurgerComponent)Random.Range(1, 5);
            }
            m_burgerPieces[fillingAmount + 1] = BurgerComponent.TOPBUN;
        }
    }

    public enum BurgerComponent
    {
        TOPBUN,
        TOMATO,
        LETTUCE,
        CHEESE,
        PATTY,
        BOTTOMBUN
    }
}