//----------------------------------------------
// File: Crate.cs
// Copyright © 2019 InsertCoin (www.insertcoin.info)
// Author: Omer Akyol
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace KodlaDemo
{
    public class Crate : Shootable 
    {
        public GameObject CrackedParent;

        private bool mIsCracked;
       

        public override void Hit(float damage)
        {
            base.Hit(damage);
        }

        public override void Die()
        {
            base.Die();

            // Detach from parent and make it active, delete the original one.
            CrackedParent.transform.parent = null;
            CrackedParent.SetActive(true);
            GameObject.Destroy(gameObject);
            mIsCracked = true;
        }
    }
}
