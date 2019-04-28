//----------------------------------------------
// File: UIController.cs
// Copyright © 2019 InsertCoin (www.insertcoin.info)
// Author: Omer Akyol
//----------------------------------------------

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace KodlaDemo
{
    public class UIController : MonoBehaviour 
    {
        public static UIController Instance;

        public Text MessageText;

        private void Awake()
        {
            Instance = this;
        }

        public void SetMessageText(string text, bool isActive)
        {
            string status       = isActive ? "<color=#00ff00ff>AKTİF</color>" : "<color=#ff0000ff>KAPALI</color>";
            MessageText.text    = text + ": " + status;
            MessageText.enabled = true;
            StopCoroutine("FadeoutMessageText");
            StartCoroutine(FadeoutMessageText());
        }

        private IEnumerator FadeoutMessageText()
        {
            yield return new WaitForSeconds(5.0f);
            //float t = 0;

            //var defColor    = new Color(MessageText.color.r, MessageText.color.g, MessageText.color.b, 1.0f);
            //var targetColor = new Color(MessageText.color.r, MessageText.color.g, MessageText.color.b, 0.0f);

            //while (t < 1.0f)
            //{
            //    MessageText.color = Color.Lerp(defColor, targetColor, t);
            //    t += Time.deltaTime;
            //    yield return new WaitForEndOfFrame();
            //}

            MessageText.enabled = false;
        }
    }
}
