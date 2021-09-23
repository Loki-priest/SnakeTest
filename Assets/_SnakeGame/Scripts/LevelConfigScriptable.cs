using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Snake
{
    [CreateAssetMenu]
    public class LevelConfigScriptable : ScriptableObject
    {
        public int index = 0;
        public float startTick = 0.5f;
        public float tickReduceMultiplier = 0.9f;
        public Direction snakeDirection;
        public GridSlot[] snake;
        public GridSlot[] walls;
    }
}