using System;
using System.Collections;
using UnityEngine;

namespace _Project.Logic
{
    public class LoadingCurtain : MonoBehaviour, IDisposable
    {
        public CanvasGroup Curtain;

        private void Awake()
        {
            DontDestroyOnLoad(this);
        }

        public void Show()
        {
            gameObject.SetActive(true);
            Curtain.alpha = 1;
        }

        public void Hide()
        {
            gameObject.SetActive(true);
            StartCoroutine(FadeIn());
        }

        private IEnumerator FadeIn()
        {
            while (Curtain.alpha > 0)
            {
                Curtain.alpha -= 0.03f;
                yield return new WaitForSeconds(0.04f);
            }

            gameObject.SetActive(false);
        }

        public void Dispose()
        {
            Debug.Log("Dispose from Curtain");
        }
    }
}
