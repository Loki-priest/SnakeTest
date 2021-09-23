using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Snake
{

    [System.Serializable]
    public struct GridSlot
    {
        public int i;
        public int j;
    }



    public class GameGrid : MonoBehaviour
    {
        public static GameGrid Instance;

        public int[,] grid = new int[10, 10];
        //0 - свободно
        //1 - стена или хвост
        //2 - еда

        private void Awake()
        {
            Instance = this;
        }

        public void EraseGrid()
        {
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    grid[i, j] = 0;
                }
            }
        }

        public GridPart CreatePart(GameObject prefab, Transform parent, int i, int j, int num)
        {
            grid[i, j] = num;
            GridPart tmp = Instantiate(prefab, parent).GetComponent<GridPart>();
            tmp.gridI = i;
            tmp.gridJ = j;
            tmp.transform.localPosition = new Vector3(-5 + j, 0, 4 - i);

            return tmp;
        }



    }
}