using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Snake
{
    public class Navigator : MonoBehaviour
    {
        public Snake snake;

        public GameObject btnUp;
        public GameObject btnDown;
        public GameObject btnLeft;
        public GameObject btnRight;


        private void Awake()
        { 
            //аналог button.AddListener(
            if (btnUp) UIEventListener.Get(btnUp).onClick += (_) => { ChooseUp(); };
            if (btnDown) UIEventListener.Get(btnDown).onClick += (_) => { ChooseDown(); };
            if (btnLeft) UIEventListener.Get(btnLeft).onClick += (_) => { ChooseLeft(); };
            if (btnRight) UIEventListener.Get(btnRight).onClick += (_) => { ChooseRight(); };
        }


        void ChooseUp()
        {
            if (snake.currentDirection != Direction.down)
                snake.nextDirection = Direction.up;
        }

        void ChooseDown()
        {
            if (snake.currentDirection != Direction.up)
                snake.nextDirection = Direction.down;
        }

        void ChooseRight()
        {
            if (snake.currentDirection != Direction.left)
                snake.nextDirection = Direction.right;
        }

        void ChooseLeft()
        {
            if (snake.currentDirection != Direction.right)
                snake.nextDirection = Direction.left;
        }






        void Update()
        {
            if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
            {
                //вверх
                ChooseUp();
                return;
            }

            if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
            {
                //вниз
                ChooseDown();
                return;
            }

            if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
            {
                //вправо
                ChooseRight();
                return;
            }

            if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
            {
                //влево
                ChooseLeft();
                return;
            }




        }
    }
}
