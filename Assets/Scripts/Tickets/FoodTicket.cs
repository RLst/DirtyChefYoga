using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DirtyChefYoga
{
    public abstract class FoodTicket : MonoBehaviour
    {
        public abstract void MakeFood(int fillingAmount = 3);
    }
}
