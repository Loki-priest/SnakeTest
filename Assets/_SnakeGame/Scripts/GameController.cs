using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Snake
{
    public class GameController : MonoBehaviour
    {
        public static GameController Instance;

        public LevelConstuctor levelConstuctor;
        public FoodGenerator foodGenerator;
        public Snake snake;

        public LevelConfigScriptable[] levels;
        LevelConfigScriptable currentLevel;


        public int score
        {
            get { return m_score; }
            set {
                m_score = value;
                GUIController.Instance.UpdateScore(value);
            }
        }
        int m_score;



        private void Awake()
        {
            Instance = this;

            snake.AfterDie += GameOver;
            snake.AfterEat += AddScore;
        }


        private void Start()
        {
            StartNew(PlayerPrefs.GetInt("LastLevel", 0));
        }

        public void AddScore()
        {
            score++;
            snake.tickTime *= currentLevel.tickReduceMultiplier;//0.9f;

            foodGenerator.SpawnFood();
        }


        public void GameOver()
        {
            GUIController.Instance.OpenPopupRestart(true);
        }

        public void Restart()
        {
            GameGrid.Instance.EraseGrid();
            foodGenerator.EraseFood();

            levelConstuctor.CreateLevel(currentLevel);
            snake.StartMe(currentLevel);
            snake.Resurrect();

            foodGenerator.SpawnFood();

            score = 0;
            GUIController.Instance.OpenPopupRestart(false);
        }


        public void StartNew(int i)
        {
            PlayerPrefs.SetInt("LastLevel", i);
            currentLevel = levels[i];
            Restart();
        }



        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                Restart();
                return;
            }
        }




    }
}