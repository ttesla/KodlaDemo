//----------------------------------------------
// File: Enemy.cs
// Copyright © 2019 InsertCoin (www.insertcoin.info)
// Author: Omer Akyol
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace KodlaDemo
{
    public enum EnemyType
    {
        Basic,
        Medium,
        Mother
    }

    public class Enemy : Shootable 
    {
        public Color HitColor;
        public float MinMoveSpeed;
        public float MaxMoveSpeed;
        public float MaxVelocity;
        public float AttackDistance;
        public float JumpSpeed;
        public EnemyType EnemyT;
        public GameObject DeathParticlePrefab;
        public GameObject GibPrefab;
        public int GibAmount;

        private Color mDefaultColor;
        private Material mMaterial;
        private Coroutine mHitRoutine;
        private Rigidbody mRigidbody;
        private float mMoveSpeed;
        private Renderer mRenderer;
        private Collider mCollider;
        private Transform mPlayerTransform;
        private float mJumpTimer;

        void Awake()
        {
            mRenderer        = GetComponent<Renderer>();
            mMaterial        = mRenderer.material;
            mDefaultColor    = mMaterial.color;
            mMoveSpeed       = Random.Range(MinMoveSpeed, MaxMoveSpeed);
            mRigidbody       = GetComponent<Rigidbody>();
            mPlayerTransform = Player.Instance.transform;
            mCollider        = GetComponent<Collider>();
        }

        void FixedUpdate()
        {
            if (!IsDead)
            {
                AiFixedUpdate();
            }
        }

        private void AiFixedUpdate()
        {
            var posDif   = mPlayerTransform.position - transform.position;
            var distance = posDif.magnitude;
            var dir      = posDif.normalized;
            
            // AI move
            dir = new Vector3(dir.x, 0.0f, dir.z); // Zero out Y direction
            mRigidbody.AddForce(dir * mMoveSpeed * Time.fixedDeltaTime);

            // AI jump except mother
            if(EnemyT != EnemyType.Mother)
            {
                if (mJumpTimer > 3.0f && distance < AttackDistance)
                {
                    mRigidbody.AddForce(Vector3.up * JumpSpeed, ForceMode.Impulse);
                    mJumpTimer = 0.0f;
                }

                mJumpTimer += Time.fixedDeltaTime;
            }

            // Clamp speed
            mRigidbody.velocity = Vector3.ClampMagnitude(mRigidbody.velocity, MaxVelocity);
        }

        public override void Hit(float damage)
        {
            base.Hit(damage);

            if (DemoController.EnableHitImpact)
            {
                if (mHitRoutine != null)
                {
                    StopCoroutine(mHitRoutine);
                }

                mHitRoutine = StartCoroutine(HitRoutine());
            }

            if (DemoController.EnableHitKickBack)
            {
                mRigidbody.velocity = mRigidbody.velocity / 4.0f;
            }
        }

        public override void Die()
        {
            base.Die();

            if (DemoController.EnableCameraShake)
            {
                CameraShake.Instance.ShakeIt(0.1f, 0.25f, ShakeType.Random);
            }

            if (DemoController.EnableDeathAnim)
            {
                StartCoroutine(DieRoutine());
            }
            else
            {
                // Direct destroy if death anim is not active
                GameObject.Destroy(gameObject);
            }
        }

        /// <summary>
        /// Hit flash!
        /// </summary>
        private IEnumerator HitRoutine()
        {
            float t = 0;

            mMaterial.color = HitColor;
            // Stay as flash color for a while
            yield return new WaitForSeconds(0.10f);

            // Fade out to default color
            while (t < 1.0f)
            {
                mMaterial.color = Color.Lerp(HitColor, mDefaultColor, t);
                t += Time.deltaTime * 5.0f;
                yield return new WaitForEndOfFrame();
            }

            mMaterial.color = mDefaultColor;
        }

        private IEnumerator DieRoutine()
        {
            // Disable these to make gibs scatter perfect
            mRenderer.enabled = false;
            mCollider.enabled = false;

            GameObject.Instantiate(DeathParticlePrefab, transform.position, Quaternion.identity);

            for (int i = 0; i < GibAmount; i++)
            {
                var obj = GameObject.Instantiate(GibPrefab, transform.position + Random.insideUnitSphere, Random.rotation);
                obj.GetComponent<Rigidbody>().AddExplosionForce(1.5f, transform.position, 4.0f, 0.5f, ForceMode.Impulse);
                yield return new WaitForEndOfFrame();
            }

            GameObject.Destroy(gameObject);
        }
    }
}
