using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DirtyChefYoga
{
    public class BurgerTicket : FoodTicket
    {
        public BurgerComponent[] fillings;

        public override void MakeFood(int fillingAmount = 3)
        {
            fillings = new BurgerComponent[fillingAmount + 2];

            fillings[0] = BurgerComponent.BOTTOMBUN;
            for (int i = fillingAmount; i > 0; i--)
            {
                fillings[i] = (BurgerComponent)Random.Range(1, (int)BurgerComponent.COUNT-1);
            }
            fillings[fillingAmount + 1] = BurgerComponent.TOPBUN;
        }
    }

    public enum BurgerComponent
    {
        TOPBUN,
        TOMATO,
        LETTUCE,
        CHEESE,
        PATTY,
        BOTTOMBUN,
        COUNT
    }
}