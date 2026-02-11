using System.Collections.Generic;
using _Project.Logic.Infrastructure.States;
using UnityEngine;

namespace _Project.Logic.Infrastructure
{
    public class GameBootstrapper : MonoBehaviour, ICoroutineRunner
    {
        public LoadingCurtain CurtainPrefab;

        private Game _game;

        private void Awake()
        {
            _game = new Game(this, CurtainPrefab);
            _game._stateMachine.Enter<BootstrapState>();

            DontDestroyOnLoad(this);
        }
    }
}