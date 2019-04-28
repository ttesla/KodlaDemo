//----------------------------------------------
// File: Shootable.cs
// Copyright © 2019 InsertCoin (www.insertcoin.info)
// Author: Omer Akyol
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace KodlaDemo
{
    public class Shootable : MonoBehaviour 
    {
        public float Health;
        public AudioClip[] HitSfx;
        public AudioSource ASource;

        public bool IsDead { get; private set; }

        public virtual void Hit(float damage)
        {
            ASource.PlayOneShot(HitSfx[Random.Range(0, HitSfx.Length)]);

            Health -= damage;

            if(!IsDead && Health < 0)
            {
                Die();
            }
        }

        public virtual void Die()
        {
            IsDead = true;
        }
    }
}
