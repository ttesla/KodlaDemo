//----------------------------------------------
// File: ExplosiveBarrel.cs
// Copyright © 2019 InsertCoin (www.insertcoin.info)
// Author: Omer Akyol
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace KodlaDemo
{
    public class ExplosiveBarrel : Shootable 
    {
        public float ExpRadius;
        public float ExpForce;
        public float UpForce;
        public float DamageFactor;
        public GameObject ExplosionParticle;

        private int mId;

        public override void Hit(float damage)
        {
            base.Hit(damage);
        }

        public override void Die()
        {
            base.Die();
            mId = gameObject.GetInstanceID();
            ExplosionParticle.transform.parent = null;
            ExplosionParticle.SetActive(true);
            StartCoroutine(ExplodeRoutine());

            if (DemoController.EnableCameraShake)
            {
                CameraShake.Instance.ShakeIt(0.5f, 0.75f, ShakeType.Random);
            }
        }

        private IEnumerator ExplodeRoutine()
        {
            yield return new WaitForEndOfFrame();

            Vector3 expPos = transform.position;
            var colliders  = Physics.OverlapSphere(expPos, ExpRadius);

            foreach (var col in colliders)
            {
                var rigidbody = col.GetComponent<Rigidbody>();

                if (rigidbody != null && rigidbody.GetInstanceID() != mId)
                {
                    rigidbody.AddExplosionForce(ExpForce, expPos, ExpRadius, UpForce, ForceMode.Impulse);

                    if (TagsAndLayers.IsShootable(rigidbody.tag))
                    {
                        var mag = ExpRadius / ((rigidbody.transform.position - expPos).magnitude + 0.01f);
                        rigidbody.GetComponent<Shootable>().Hit(mag * DamageFactor);
                    }
                }
            }

            GameObject.Destroy(gameObject);
        }
    }
}
