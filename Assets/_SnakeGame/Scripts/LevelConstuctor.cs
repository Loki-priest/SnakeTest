using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Snake
{
    public class LevelConstuctor : MonoBehaviour
    {
        public Transform wallsContainer;
        public GameObject wallPartPrefab;
        public List<GridPart> walls;
        GridPart tmpWall;


        public void CreateLevel(LevelConfigScriptable cfg)
        {
            RecreateWalls(cfg);
        }

        void RecreateWalls(LevelConfigScriptable cfg)
        {
            for (int i = 0; i < walls.Count; i++)
            {
                Destroy(walls[i].gameObject);
            }
            walls.Clear();

            for (int i = 0; i < cfg.walls.Length; i++)
            {
                tmpWall = GameGrid.Instance.CreatePart(wallPartPrefab, wallsContainer, cfg.walls[i].i, cfg.walls[i].j, 1);//
                walls.Add(tmpWall);
            }
        }


    }
}