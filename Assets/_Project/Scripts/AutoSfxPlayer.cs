//----------------------------------------------
// File: AutoSfxPlayer.cs
// Copyright © 2019 InsertCoin (www.insertcoin.info)
// Author: Omer Akyol
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace KodlaDemo
{
    public class AutoSfxPlayer : MonoBehaviour 
    {
        public bool PlayOnHit;
        public bool PlayWithDelay;
        public float Delay;
        public AudioClip[] Clips;
        public float VolumeFactor;
        public int MaxPlayCount;

        private AudioSource mAudioSource;
        private int mPlayCount;

        void Awake()
        {
            mAudioSource = GetComponent<AudioSource>();    
        }

        void Start()
        {
            if (PlayWithDelay)
            {
                Invoke("Play", Delay);
            }    
        }

        void Update()
        {
            TimeManager.Instance.AdjustAudioSource(mAudioSource);
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (PlayOnHit)
            {
                Play();
            }
        }

        void Play()
        {
            if(mPlayCount < MaxPlayCount)
            {
                mAudioSource.PlayOneShot(Clips[Random.Range(0, Clips.Length)], VolumeFactor);
                mPlayCount++;
            }
        }
    }
}
