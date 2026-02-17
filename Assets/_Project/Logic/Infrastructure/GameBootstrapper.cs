using System;
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
            LoadingCurtain curtain = CreateCurtain();

            _game = new Game(this, curtain);
            _game._stateMachine.Enter<BootstrapState>();

            DontDestroyOnLoad(this);
        }

        private void Update()
        {
            _game._stateMachine.Update();
        }

        private LoadingCurtain CreateCurtain()
        {
            var curtain = Instantiate(CurtainPrefab);
            return curtain;
        }
    }
}