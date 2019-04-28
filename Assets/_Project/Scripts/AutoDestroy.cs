//----------------------------------------------
// File: AutoDestroy.cs
// Copyright © 2019 InsertCoin (www.insertcoin.info)
// Author: Omer Akyol
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace KodlaDemo
{
    /// <summary>
    /// Don't ever use this script anywhere :)
    /// </summary>
    public class AutoDestroy : MonoBehaviour 
    {
        public float Delay;
        public bool DisableOnly;

        private float mTimer;
        private Collider mCollider;
        private Rigidbody mRigidbody;

        void Start()
        {
            mCollider  = GetComponent<Collider>();
            mRigidbody = GetComponent<Rigidbody>();
            //if (!DisableOnly)
            //{
            //    GameObject.Destroy(gameObject, Delay);
            //}
        }

        private void OnDisable()
        {
            mTimer = 0.0f;
        }

        private void Update()
        {
            if (mTimer > Delay)
            {
                if (DisableOnly)
                {
                    gameObject.SetActive(false);
                }
                else
                {
                    if(mCollider != null)
                    {
                        mCollider.enabled = false;
                    }

                    if(mRigidbody != null)
                    {
                        mRigidbody.isKinematic = true;
                    }

                    // Hide under carpet then destroy!
                    transform.position -= Vector3.up * Time.deltaTime;
                }
            }

            // If still not destroyed after 2 times delay, force destroy
            if(!DisableOnly && mTimer > Delay * 2.0f)
            {
                GameObject.Destroy(gameObject);
                return;
            }

            if (transform.position.y < -5.0f)
            {
                // Anyting fell under carpet must be destroyed! 
                GameObject.Destroy(gameObject);
            }

            mTimer += Time.deltaTime;
        }
    }
}
