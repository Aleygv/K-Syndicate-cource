using System;
using UnityEngine;

namespace _Project.Logic.Infrastructure
{
    public class GameRunner : MonoBehaviour
    {
        public GameBootstrapper BootstrapperPrefab;

        private void Awake()
        {
#pragma warning disable CS0618 // Type or member is obsolete
            var bootstrapper = FindObjectOfType<GameBootstrapper>();
#pragma warning restore CS0618 // Type or member is obsolete

            if (bootstrapper == null)
            {
                Instantiate(BootstrapperPrefab);
            }
        }
    }
}