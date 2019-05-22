using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DirtyChefYoga
{
    public class BurgerTicket : FoodTicket
    {
        public BurgerComponent[] m_burgerPieces;



        //private void Awake()
        //{
        //    if(m_burgerPieces.Length == 0)
        //    {
        //        MakeFood();
        //    }
        //}

        public override void MakeFood(int fillingAmount = 3)
        {
            m_burgerPieces = new BurgerComponent[fillingAmount + 2];

            m_burgerPieces[0] = BurgerComponent.TOPBUN;
            for (int i = 1; i <= fillingAmount; i++)
            {
                m_burgerPieces[i] = (BurgerComponent)Random.Range(1, 5);
            }
            m_burgerPieces[m_burgerPieces.Length - 1] = BurgerComponent.BOTTOMBUN;
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