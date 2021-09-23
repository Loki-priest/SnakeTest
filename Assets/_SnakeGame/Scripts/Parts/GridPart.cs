using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Snake
{
    public abstract class GridPart : MonoBehaviour
    {
        public int gridI;
        public int gridJ;

        protected virtual void Awake()
        {
            //
        }

    }
}