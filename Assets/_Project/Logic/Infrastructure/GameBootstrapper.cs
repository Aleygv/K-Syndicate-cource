using System;
using UnityEngine;

namespace _Project.Logic.Infrastructure
{
    public class GameBootstrapper : MonoBehaviour
    {
        private Game _game;

        private void Awake()
        {
            _game = new Game();

            DontDestroyOnLoad(this);
        }
    }
}