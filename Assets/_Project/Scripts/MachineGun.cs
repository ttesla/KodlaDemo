//----------------------------------------------
// File: MachineGun.cs
// Copyright © 2019 InsertCoin (www.insertcoin.info)
// Author: Omer Akyol
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace KodlaDemo
{
    public class MachineGun : Gun 
    {
        public float MaxDistance;
        public GameObject Coil;
        public LayerMask MaskLayer;
        
        private Camera mMain;

        void Start()
        {
            Init();
            mMain = Camera.main; 
        }

        protected override void Fire()
        {
            Vector2 randomScatter = Vector2.zero;

            if (DemoController.EnableScatteredSpray)
            {
                randomScatter = Random.insideUnitCircle * 0.02f;
            }

            if (DemoController.EnableShells)
            {
                FireEmptyShell();
            }
            
            var ray  = mMain.ViewportPointToRay(new Vector3(0.5f + randomScatter.x, 0.5f + randomScatter.y * mMain.aspect, 0f));

            RaycastHit hitInfo;

            if (Physics.Raycast(ray, out hitInfo, MaxDistance, MaskLayer))
            {
                if (DemoController.EnableBulletImpact)
                {
                    CreateHitDecal(hitInfo);
                }

                //Debug.Log("Hit:" + hitInfo.transform.name);
                
                if (TagsAndLayers.IsShootable(hitInfo.collider.tag))
                {
                    var damage = Random.Range(MinDamage, MaxDamage);
                    hitInfo.collider.gameObject.GetComponent<Shootable>().Hit(damage);
                }

                // Hit Kick Back 
                if (DemoController.EnableHitKickBack)
                {
                    // It affects everything which have rigidbody
                    if(hitInfo.rigidbody != null)
                    {
                        hitInfo.rigidbody.AddForceAtPosition(-hitInfo.normal * 1.0f, hitInfo.point, ForceMode.Impulse);
                    }
                }
            }
        }

        private void FireEmptyShell()
        {
            var lookDir        = Quaternion.LookRotation(-ShellOutPosition.right);
            var shell          = GameObject.Instantiate(ShellPrefab, ShellOutPosition.position, lookDir);
            var shellRigidBody = shell.GetComponent<Rigidbody>();
            var rndForce = Random.Range(0.5f, 0.75f);
            shellRigidBody.AddForce(ShellOutPosition.forward * rndForce, ForceMode.Impulse);
            shellRigidBody.AddTorque(Random.insideUnitSphere * 2.0f);
        }

        private void CreateHitDecal(RaycastHit hitInfo)
        {
            var hitPoint = hitInfo.point;
            var decal = GameObject.Instantiate(HitDecalPrefab, hitPoint, Quaternion.identity);
            decal.transform.forward = hitInfo.normal;
            decal.transform.SetParent(hitInfo.collider.gameObject.transform);
        }
    }
}
