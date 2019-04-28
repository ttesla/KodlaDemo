//----------------------------------------------
// File: DemoController.cs
// Copyright © 2019 InsertCoin (www.insertcoin.info)
// Author: Omer Akyol
//----------------------------------------------

using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

namespace KodlaDemo
{
    public class DemoController : MonoBehaviour
    {
        public AudioMixer Mixer;

        // Let settings remain in this session, (we have a scene restart)
        public static bool EnableHitImpact;
        public static bool EnableHitKickBack;
        public static bool EnableBulletImpact;
        public static bool EnableDeathAnim;
        public static bool EnableGunKick;
        public static bool EnableMuzzleFlash;
        public static bool EnableShells;
        public static bool EnableScatteredSpray;
        public static bool EnableCameraShake;
        public static bool EnableSfx = true;


        private static bool mFogEnabled = true;

        private void Awake()
        {
        //    ToggleFog();
        }

        private void SetAll(bool enable)
        {
            EnableHitImpact      = enable;
            EnableHitKickBack    = enable;
            EnableBulletImpact   = enable;
            EnableDeathAnim      = enable;
            EnableGunKick        = enable;
            EnableMuzzleFlash    = enable;
            EnableShells         = enable;
            EnableScatteredSpray = enable;
            EnableSfx            = enable;
            EnableCameraShake    = enable;
            SetEnableUpdateSfx();
        }

        private void ToggleFog()
        {
            mFogEnabled            = !mFogEnabled;
            RenderSettings.fog     = mFogEnabled;
            Camera.main.clearFlags = mFogEnabled ? CameraClearFlags.SolidColor : CameraClearFlags.Skybox;
            UIController.Instance.SetMessageText("Sis", mFogEnabled);
        }

        private void ToggleSound()
        {
            EnableSfx = !EnableSfx;
            SetEnableUpdateSfx();
        }

        private void SetEnableUpdateSfx()
        {
            float volume = EnableSfx ? 0.0f : -80.0f;
            Mixer.SetFloat("SfxVol", volume);
            UIController.Instance.SetMessageText("Ses", EnableSfx);
        }

        private void ResetLevel()
        {
            // Reload scene
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                EnableBulletImpact = !EnableBulletImpact;
                UIController.Instance.SetMessageText("Mermi Efekti", EnableBulletImpact);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                EnableHitImpact = !EnableHitImpact;
                UIController.Instance.SetMessageText("Vurma Efekti", EnableHitImpact);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                EnableHitKickBack = !EnableHitKickBack;
                UIController.Instance.SetMessageText("Vuruş Tepmesi", EnableHitKickBack);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                EnableDeathAnim = !EnableDeathAnim;
                UIController.Instance.SetMessageText("Ölme Efekti", EnableDeathAnim);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                EnableGunKick = !EnableGunKick;
                UIController.Instance.SetMessageText("Silah Tepmesi", EnableGunKick);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha6))
            {
                EnableShells = !EnableShells;
                UIController.Instance.SetMessageText("Boş Kovanlar", EnableShells);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha7))
            {
                EnableMuzzleFlash = !EnableMuzzleFlash;
                UIController.Instance.SetMessageText("Namlu Alevi", EnableMuzzleFlash);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha8))
            {
                EnableScatteredSpray = !EnableScatteredSpray;
                UIController.Instance.SetMessageText("Mermi Saçılması", EnableScatteredSpray);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha9))
            {
                EnableCameraShake = !EnableCameraShake;
                UIController.Instance.SetMessageText("Ekran Sallanması", EnableCameraShake);
            }

            else if (Input.GetKeyDown(KeyCode.F1))
            {
                ResetLevel();
            }

            else if (Input.GetKeyDown(KeyCode.F2))
            {
                SetAll(true);
                UIController.Instance.SetMessageText("Tüm Efektler", true);

            }
            else if (Input.GetKeyDown(KeyCode.F3))
            {
                SetAll(false);
                UIController.Instance.SetMessageText("Tüm Efektler", false);

            }
            else if (Input.GetKeyDown(KeyCode.F4))
            {
                ToggleSound();
            }

            else if (Input.GetKeyDown(KeyCode.F5))
            {
                ToggleFog();
            }

            else if (Input.GetKeyDown(KeyCode.Escape))
            {
                Application.Quit();
            }
        }
    }
}
