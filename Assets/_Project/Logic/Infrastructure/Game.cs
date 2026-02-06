using _Project.Logic.Services.Input;
using UnityEngine;

namespace _Project.Logic.Infrastructure
{
    public class Game
    {
        public readonly GameStateMachine _stateMachine;
        public static IInputService InputService;

        public Game(ICoroutineRunner coroutineRunner, LoadingCurtain curtain)
        {
            _stateMachine = new GameStateMachine(new SceneLoader(coroutineRunner), curtain);
        }
    }
}