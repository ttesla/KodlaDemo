//----------------------------------------------
// File: Player.cs
// Copyright © 2019 InsertCoin (www.insertcoin.info)
// Author: Omer Akyol
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace KodlaDemo
{
    public class Player : MonoBehaviour 
    {
        public static Player Instance;

        public Gun TheGun;

        void Awake()
        {
            Instance = this;
        }

        void Update()
        {
            if (Input.GetButton("Fire1"))
            {
                TheGun.PullTrigger();
            }

            if (Input.GetButton("Fire2"))
            {
                TimeManager.Instance.MakeBulletTime();
            }
        }
    }
}
