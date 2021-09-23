using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Snake
{
    public class FoodPart : GridPart
    {
        public void EatMe()
        {
            Destroy(gameObject);
        }
    }
}
