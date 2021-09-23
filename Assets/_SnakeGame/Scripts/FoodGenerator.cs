using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Snake
{
    public class FoodGenerator : MonoBehaviour
    {
        public Transform foodContainer;
        public GameObject foodPrefab;
        GridPart tmpFood;
        public List<GridSlot> freeSlots;


        public void EraseFood()
        {
            if (tmpFood != null)
                Destroy(tmpFood.gameObject);
        }


        //оптимизнуть: составить список свободных ячеек и среди них рандомить
        public void SpawnFood()//если нет места, то не спавнить! 
        {
            freeSlots.Clear();
            for (int ii = 0; ii < 10; ii++)
            {
                for (int jj = 0; jj < 10; jj++)
                {
                    if (GameGrid.Instance.grid[ii, jj] == 0)
                    {
                        GridSlot gs = new GridSlot();
                        gs.i = ii;
                        gs.j = jj;
                        freeSlots.Add(gs);
                    }
                }
            }

            if (freeSlots.Count == 0) return; //не спавним, т.к. места нет (хотя это невозможно на практике)

            GridSlot tmpSlot = freeSlots[Random.Range(0, freeSlots.Count)];
            int i = tmpSlot.i;
            int j = tmpSlot.j;

            tmpFood = GameGrid.Instance.CreatePart(foodPrefab, foodContainer, i, j, 2);//



            //старый способ: плох тем, что если мало места, рандом может часто себя вызывать
            /*
            int i = Random.Range(0, 10);
            int j = Random.Range(0, 10);
            if (GameGrid.Instance.grid[i, j] == 0) //оптимизнуть
            {
                tmpFood = GameGrid.Instance.CreatePart(foodPrefab, foodContainer, i, j, 2);//
            }
            else
            {
                SpawnFood();
            }
            */
        }





    }
}
