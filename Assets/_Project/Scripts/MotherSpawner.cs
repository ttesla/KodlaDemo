//----------------------------------------------
// File: EnemySpawner.cs
// Copyright © 2019 InsertCoin (www.insertcoin.info)
// Author: Omer Akyol
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace KodlaDemo
{
    public class MotherSpawner : MonoBehaviour 
    {
        public GameObject MotherPrefab;

        public float MinDistance;
        public float SpawnRate;
        public float StartSpawning;

        private int mSpawnCount;

        void Start()
        {
            StartCoroutine(SpawnRoutine());
        }

        private IEnumerator SpawnRoutine()
        {
            yield return new WaitForSeconds(StartSpawning);

            while (true)
            {
                SpawnMother();
                yield return new WaitForSeconds(SpawnRate);
            }
        }

        private void SpawnMother()
        {
            var dir = Random.insideUnitSphere;
            dir = new Vector3(dir.x, 0.0f, dir.z).normalized;
            GameObject.Instantiate(MotherPrefab, dir * MinDistance + new Vector3(0.0f, MotherPrefab.transform.localScale.y / 2.0f + 0.1f, 0.0f), Quaternion.identity);
            mSpawnCount++;
        }
    }
}
