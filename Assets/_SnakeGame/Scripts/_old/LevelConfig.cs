using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Snake
{
    public class LevelConfig : MonoBehaviour
    {
        public int index = 0;
        public float startTick = 0.5f;
        public float tickReduceMultiplier = 0.9f;
        public Direction snakeDirection;
        public GridSlot[] snake;
        public GridSlot[] walls;
    }
}
