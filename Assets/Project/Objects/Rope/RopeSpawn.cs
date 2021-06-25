using System.Collections;

using System.Collections.Generic;

using UnityEngine;

namespace Platformer_Assignment {

    //https://www.youtube.com/watch?v=pKSUhsyrj_4
    public class RopeSpawn : MonoBehaviour
    {

        [SerializeField]
        private GameObject partPrefab, parentObject;

       // private int length = 2;

        //[SerializeField]
        //private float partDistance = 0.21f;

        void Awake()
        {
            Spawn();
        }

        public void Spawn()
        {

        }

    }
}