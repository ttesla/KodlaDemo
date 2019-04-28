//----------------------------------------------
// File: Mother.cs
// Copyright © 2019 InsertCoin (www.insertcoin.info)
// Author: Omer Akyol
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace KodlaDemo
{
    public class Mother : MonoBehaviour 
    {
        public Transform SpawnPos;
        public GameObject BasicPrefab;
        public GameObject MediumPrefab;
        public float SpawnRate;

        private int mSpawnCount;

        void Start()
        {
            StartCoroutine(SpawnRoutine());
        }

        private IEnumerator SpawnRoutine()
        {
            while (true)
            {
                SpawnEnemy();
                yield return new WaitForSeconds(SpawnRate);
            }
        }

        private void SpawnEnemy()
        {
            if(mSpawnCount % 5 == 0)
            {
                GameObject.Instantiate(MediumPrefab, SpawnPos.position, Quaternion.identity);
            }
            else
            {
                GameObject.Instantiate(BasicPrefab, SpawnPos.position, Quaternion.identity);
            }

            mSpawnCount++;
        }
    }
}
