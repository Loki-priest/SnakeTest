using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Snake
{
    public class GUIController : MonoBehaviour
    {
        public static GUIController Instance;

        public GameObject btnRestart;
        public GameObject btnMenu;

        public GameObject[] btnsLevels;

        public GameObject popupMenu;

        public UILabel scoreTxt;

        private void Awake()
        {
            Instance = this;

            //аналог button.AddListener(
            if (btnRestart) UIEventListener.Get(btnRestart).onClick += (_) => { GameController.Instance.Restart(); };
            if (btnMenu) UIEventListener.Get(btnMenu).onClick += (_) => { GameController.Instance.snake.Die(); /*OpenPopupRestart(true); */};
            
            for (int i = 0; i < btnsLevels.Length; i++)
            {
                int j = i; 
                if (btnsLevels[i]) UIEventListener.Get(btnsLevels[i]).onClick += (_) => { GameController.Instance.StartNew(j); };
            }

        }


        public void OpenPopupRestart(bool f)
        {
            popupMenu.SetActive(f);
        }

        public void UpdateScore(int val)
        {
            scoreTxt.text = val.ToString();
        }


    }
}
