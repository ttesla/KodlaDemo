//----------------------------------------------
// File: CameraShake.cs
// Copyright © 2019 InsertCoin (www.insertcoin.info)
// Author: Omer Akyol
//----------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KodlaDemo
{
    public enum ShakeType
    {
        Random,
        Vertical,
    }

    public class CameraShake : MonoBehaviour
    {
        public static CameraShake Instance;

        private Coroutine mRandomRoutine;
        private Coroutine mVerticalRoutine;
        private Vector3 LocalDefaultPos;

        void Awake()
        {
            Instance = this;
            LocalDefaultPos = transform.localPosition;
        }

#if UNITY_EDITOR
        void Update()
        {
            // Small and short
            //if (Input.GetKeyDown(KeyCode.S))
            //{
            //    ShakeIt(0.1f, 0.25f, ShakeType.Random);
            //}

            //// Big and long
            //if (Input.GetKeyDown(KeyCode.D))
            //{
            //    ShakeIt(0.15f, 0.10f, ShakeType.Random);
            //}

            //// Big and long in Y dimension only
            //if (Input.GetKeyDown(KeyCode.F))
            //{
            //    ShakeIt(0.1f, 0.15f, ShakeType.Vertical);
            //}
        }
#endif

        public void ShakeIt(float intensity, float decaySpeed, ShakeType shakeType, float delay = 0)
        {
            if (shakeType == ShakeType.Random)
            {
                //if(mRandomRoutine != null)
                //{
                //    transform.localPosition = LocalDefaultPos;
                //    StopCoroutine(mRandomRoutine);
                //}

                mRandomRoutine = StartCoroutine(RandomShakeRoutine(intensity, decaySpeed, delay));
            }
            else if (shakeType == ShakeType.Vertical)
            {
                //if(mVerticalRoutine != null)
                //{
                //    transform.localPosition = LocalDefaultPos;
                //    StopCoroutine(mVerticalRoutine);
                //}

                mVerticalRoutine = StartCoroutine(VerticalShakeRoutine(intensity, decaySpeed, delay));
            }
        }

        private IEnumerator RandomShakeRoutine(float intensity, float decaySpeed, float delay)
        {
            if (delay > 0)
            {
                yield return new WaitForSeconds(delay);
            }

            while (intensity > 0.0)
            {
                intensity -= decaySpeed * Time.deltaTime;
                transform.localPosition = LocalDefaultPos + Random.insideUnitSphere * intensity;

                yield return new WaitForEndOfFrame();
            }

            transform.localPosition = LocalDefaultPos;
        }

        private IEnumerator VerticalShakeRoutine(float intensity, float decaySpeed, float delay)
        {
            if(delay > 0)
            {
                yield return new WaitForSeconds(delay);
            }

            while (intensity > 0.0)
            {
                intensity -= decaySpeed * Time.deltaTime;
                transform.localPosition = LocalDefaultPos + new Vector3(0, Random.Range(0.0f, 1.0f) * intensity, 0);
                //transform.localPosition = defaultPos + new Vector3(0, down * intensity, 0);

                yield return new WaitForEndOfFrame();
            }

            transform.localPosition = LocalDefaultPos;
        }
    }
}
