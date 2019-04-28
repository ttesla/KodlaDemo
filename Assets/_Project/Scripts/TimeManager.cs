//----------------------------------------------
// File: TimeManager.cs
// Copyright © 2019 InsertCoin (www.insertcoin.info)
// Author: Omer Akyol
//----------------------------------------------

using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

namespace KodlaDemo
{
    public class TimeManager : MonoBehaviour 
    {
        public static TimeManager Instance;

        public float BulletTimeFactor;
        public float BulletTimeDuration;
        public float MinBulletTimeAudioPitch;

        private Coroutine mBulletTimeRoutine;
        private List<AudioSource> mAudioSourceList;

        void Awake()
        {
            Instance = this;    
        }

        /// <summary>
        ///  Bullet time audio adjuster
        /// </summary>
        public void AdjustAudioSource(AudioSource source)
        {
            source.pitch = Mathf.Clamp(Time.timeScale, MinBulletTimeAudioPitch, 1.0f);
        }

        public void MakeBulletTime()
        {
            if(mBulletTimeRoutine != null)
            {
                StopCoroutine(mBulletTimeRoutine);
            }

            mBulletTimeRoutine = StartCoroutine(BulletTimeRoutine());
        }

        private IEnumerator BulletTimeRoutine()
        {
            float t = 0;

            while(t < 1.0f)
            {
                Time.timeScale = Mathf.Lerp(1.0f, BulletTimeFactor, t);
                t += Time.unscaledDeltaTime * 5.0f;

                yield return new WaitForEndOfFrame();
            }

            Time.timeScale = BulletTimeFactor;

            yield return new WaitForSecondsRealtime(BulletTimeDuration);

            t = 0;

            while (t < 1.0f)
            {
                Time.timeScale = Mathf.Lerp(BulletTimeFactor, 1.0f, t);
                t += Time.unscaledDeltaTime * 5.0f;

                yield return new WaitForEndOfFrame();
            }

            Time.timeScale = 1.0f;
        }
    }
}
