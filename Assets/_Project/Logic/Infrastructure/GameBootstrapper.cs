using System.Collections.Generic;
using UnityEngine;

namespace _Project.Logic.Infrastructure
{
    public class GameBootstrapper : MonoBehaviour, ICoroutineRunner
    {
        public LoadingCurtain Curtain;

        private Game _game;

        private void Awake()
        {
            _game = new Game(this, Curtain);
            _game._stateMachine.Enter<BootstrapState>();

            DontDestroyOnLoad(this);
        }
    }
}