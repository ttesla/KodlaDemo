//----------------------------------------------
// File: Projectile.cs
// Copyright © 2019 InsertCoin (www.insertcoin.info)
// Author: Omer Akyol
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace KodlaDemo
{
    public class Projectile : MonoBehaviour 
    {
        private const float NoHitDisableTime = 5.0f;

        private float mNoHitDisableTimer;
        private bool mIsActive = false;
        private Vector3 mDirection;
        private Vector3 mStartPosition;
        private Vector3 mTargetPosition;
        private float mSpeed;
        private float mDistanceToTarget;
        private float mMoveValue;

        public void Fire(Vector3 startPos, Vector3 targetPos, float speed)
        {
            mStartPosition    = startPos;
            mTargetPosition   = targetPos;
            var diffVector    = targetPos - startPos;
            mDistanceToTarget = diffVector.magnitude;
            mDirection        = diffVector.normalized;
            transform.forward = mDirection;
            mSpeed            = speed;
            mIsActive         = true;
        }

        // Update is called once per frame
        void Update()
        {
            if (mIsActive) // Only update stuff if we're alive
            {
                mNoHitDisableTimer += Time.deltaTime; // Increase disappear timer
                if (mNoHitDisableTimer > NoHitDisableTime) // If we're alive too long, get rekt
                {
                    Disable();
                }

                mMoveValue += (1.0f / mDistanceToTarget * mSpeed) * Time.deltaTime;

                if (mMoveValue > 1)
                {
                    mMoveValue = 1;
                    Hit();
                }

                transform.position = Vector3.Lerp(mTargetPosition, mTargetPosition, mMoveValue);
            }
        }

        void Disable(float destroyTime = 0.0f)
        {
            mIsActive = false;
            GameObject.Destroy(gameObject, destroyTime);
        }

        void Hit()
        {
            Disable();
        }
    }
}
