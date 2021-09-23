using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace Snake
{
    public class Snake : MonoBehaviour
    {
        [HideInInspector]
        public float tickTime = 0.5f; // сколько секунд между шагами
        float timer = 0.0f;
        bool isAlive = false;

        [HideInInspector]
        public Direction currentDirection;
        [HideInInspector]
        public Direction nextDirection;

        public Transform bodyContainer;
        public GameObject bodyPartPrefab;
        GridPart tmpPart;

        public List<GridPart> body;
        GridPart head;
        GridPart tail;
        public Transform face;




        public event DieEventHandler AfterDie;
        public delegate void DieEventHandler();

        public event EatEventHandler AfterEat;
        public delegate void EatEventHandler();



        private void Awake()
        {
            timer = tickTime;
        }



        public void StartMe(LevelConfigScriptable cfg)
        {
            //тело
            for (int i = 0; i < body.Count; i++)
            {
                Destroy(body[i].gameObject);
            }
            body.Clear();

            for (int i = 0; i < cfg.snake.Length; i++)
            {
                tmpPart = GameGrid.Instance.CreatePart(bodyPartPrefab, bodyContainer, cfg.snake[i].i, cfg.snake[i].j, 1);
                body.Add(tmpPart);
            }

            head = body[body.Count - 1];
            tail = body[0];


            //направление
            currentDirection = cfg.snakeDirection;
            nextDirection = currentDirection;

            face.position = head.transform.position; 
            switch (currentDirection) //
            {
                case Direction.up:
                    face.localEulerAngles = Vector3.zero;
                    break;

                case Direction.down:
                    face.localEulerAngles = Vector3.up * 180f;
                    break;

                case Direction.right:
                    face.localEulerAngles = Vector3.up * 90f;
                    break;

                case Direction.left:
                    face.localEulerAngles = Vector3.up * -90f;
                    break;
            }


            //"скорость"
            tickTime = cfg.startTick;
            timer = tickTime;
        }


        public void Die()
        {
            isAlive = false;//Debug.Log("DIE");
            AfterDie();
        }

        public void Resurrect()
        {
            isAlive = true;
        }




        private void MoveSnake()
        {
            timer -= Time.deltaTime;
            if (timer <= 0.0f)
            {
                //свап сегмента змеи в конец списка. конец хвоста будет головой после перетрансформа
                body.Remove(tail);
                body.Add(tail);

                //освободить место в таблице, где был конец хвоста
                GameGrid.Instance.grid[tail.gridI, tail.gridJ] = 0;

                //запомнить i j хвоста на случай, если туда понадобится после поедания его же пересоздать
                int oldI = tail.gridI; 
                int oldJ = tail.gridJ;

                //выбор позиции
                //+примечание: если делать как в старых играх на телефонах, что мы телепортируемся из правого края в левый, то:
                //1) убрать первое условие
                //2) перенести его ниже и менять индексы по типу if(>9)=0 и if(<0)=9
                //3) вместо +transform использовать transform.localPosition = new Vector3(-5 + j, 0, 4 - i);
                currentDirection = nextDirection;
                switch (currentDirection) 
                {
                    case Direction.up:
                        tail.gridI = head.gridI - 1;
                        tail.gridJ = head.gridJ;
                        if (tail.gridI < 0 || GameGrid.Instance.grid[tail.gridI, tail.gridJ] == 1)
                        {
                            Die();
                            return;
                        }

                        tail.transform.position = head.transform.position + head.transform.forward;
                        face.localEulerAngles = Vector3.zero;
                        break;

                    case Direction.down:
                        tail.gridI = head.gridI + 1;
                        tail.gridJ = head.gridJ;
                        if (tail.gridI > 9 || GameGrid.Instance.grid[tail.gridI, tail.gridJ] == 1)
                        {
                            Die();
                            return;
                        }

                        tail.transform.position = head.transform.position - head.transform.forward;
                        face.localEulerAngles = Vector3.up * 180f;
                        break;

                    case Direction.right:
                        tail.gridI = head.gridI;
                        tail.gridJ = head.gridJ + 1;
                        if (tail.gridJ > 9 || GameGrid.Instance.grid[tail.gridI, tail.gridJ] == 1)
                        {
                            Die();
                            return;
                        }

                        tail.transform.position = head.transform.position + head.transform.right;
                        face.localEulerAngles = Vector3.up * 90f;
                        break;

                    case Direction.left:
                        tail.gridI = head.gridI;
                        tail.gridJ = head.gridJ - 1;
                        if (tail.gridJ < 0 || GameGrid.Instance.grid[tail.gridI, tail.gridJ] == 1)
                        {
                            Die();
                            return;
                        }

                        tail.transform.position = head.transform.position - head.transform.right;
                        face.localEulerAngles = Vector3.up * -90f;
                        break;
                }

                //запомним, что мы наткнулись на еду
                bool isFood = false;
                if (GameGrid.Instance.grid[tail.gridI, tail.gridJ] == 2)
                {
                    isFood = true;
                }

                //заняли место под новую голову
                GameGrid.Instance.grid[tail.gridI, tail.gridJ] = 1;
                head = tail;
                tail = body[0];
                face.position = head.transform.position;

                //вспомнили, что здесь была еда, значит добавляем хвост по старым индексам
                if (isFood)
                {
                    tmpPart = GameGrid.Instance.CreatePart(bodyPartPrefab, bodyContainer, oldI, oldJ, 1);//
                    body.Add(tmpPart);
                    tail = tmpPart;

                    AfterEat();
                }

                //ждем следующий шаг
                timer = tickTime;
            }

        }


        FoodPart tmpFood;
        private void OnTriggerEnter(Collider other)
        {
            if (tmpFood = other.GetComponent<FoodPart>())
            {
                tmpFood.EatMe();
            }
        }



        void Update()
        {
            if (isAlive)
            {
                MoveSnake();
            }
        }


    }

    public enum Direction
    {
        up,
        down,
        right,
        left
    }
}