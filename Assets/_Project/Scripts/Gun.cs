//----------------------------------------------
// File: Gun.cs
// Copyright © 2019 InsertCoin (www.insertcoin.info)
// Author: Omer Akyol
//----------------------------------------------

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace KodlaDemo
{
    public class Gun : MonoBehaviour 
    {
        public float RateOfFireDelay; 
        public Transform MuzzlePosition;
        public Transform ShellOutPosition;
        public GameObject HitDecalPrefab;
        public GameObject ShellPrefab;
        public GameObject MuzzleFlashPrefab;
        public Transform MuzzleFlashPool;
        public float GunKickBackDist; // Basic Z direction only kick back
        public float KickBackSpeed;
        public float MinDamage;
        public float MaxDamage;
        public AudioClip FireSfx1;
        public AudioClip FireSfx2;

        private float mGunTimer = 0;
        private List<GameObject> mMuzzleFlashes;
        private int mMuzzleIndex = 0;
        private Vector3 mDefaultPos;
        private Vector3 mKickBackPos;
        private Coroutine mGunKickRoutine;
        protected AudioSource pAudioSource;

        protected void Init()
        {
            pAudioSource = GetComponent<AudioSource>();
            CalculateKickBackPos();
            FillMuzzleFlashPool();    
        }

        void Update()
        {
            mGunTimer += Time.deltaTime;

            TimeManager.Instance.AdjustAudioSource(pAudioSource);
        }

        public void PullTrigger()
        {
            // Don't shoot if next bullet is not ready ;)
            if(mGunTimer < RateOfFireDelay)
            {
                return;
            }

            if (DemoController.EnableMuzzleFlash)
            {
                MakeMuzzleFlash();
            }

            if (DemoController.EnableGunKick)
            {
                if(mGunKickRoutine != null)
                {
                    StopCoroutine(mGunKickRoutine);
                }

                mGunKickRoutine = StartCoroutine(GunKickRoutine());
            }

            if (DemoController.EnableSfx)
            {
                if (DemoController.EnableMuzzleFlash)
                {
                    pAudioSource.PlayOneShot(FireSfx1, 0.5f);
                }
                else
                {
                    pAudioSource.PlayOneShot(FireSfx2, 0.5f);
                }
            }

            mGunTimer = 0.0f;
            Fire();
        }

        protected virtual void Fire()
        {
            // Override this in your gun class
        }

        private void MakeMuzzleFlash()
        {
            // Make current muzzle flash from the pool
            // they became DeActive themselves
            mMuzzleFlashes[mMuzzleIndex].SetActive(true);
            mMuzzleIndex++;

            if (mMuzzleIndex >= mMuzzleFlashes.Count)
            {
                mMuzzleIndex = 0;
            }
        }

        private IEnumerator GunKickRoutine()
        {
            float t = 0;

            transform.localPosition = mKickBackPos;

            while (t < 1.0f)
            {
                transform.localPosition = Vector3.Lerp(mKickBackPos, mDefaultPos, t);
                t += KickBackSpeed * Time.deltaTime;

                yield return new WaitForEndOfFrame();
            }

            transform.localPosition = mDefaultPos;
        }

        private void CalculateKickBackPos()
        {
            mDefaultPos  = transform.localPosition;
            mKickBackPos = mDefaultPos - new Vector3(0.0f, 0.0f, GunKickBackDist);
        }

        private void FillMuzzleFlashPool()
        {
            int poolSize   = (int)(1.0f / RateOfFireDelay) + 1;
            mMuzzleFlashes = new List<GameObject>();

            for (int i = 0; i < poolSize; i++)
            {
                var obj = GameObject.Instantiate(MuzzleFlashPrefab, MuzzleFlashPool);
                mMuzzleFlashes.Add(obj);
            }
        }
    }
}
