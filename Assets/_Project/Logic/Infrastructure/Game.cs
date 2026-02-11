using _Project.Logic.Infrastructure.Services;
using _Project.Logic.Infrastructure.States;


namespace _Project.Logic.Infrastructure
{
    public class Game
    {
        public readonly GameStateMachine _stateMachine;

        public Game(ICoroutineRunner coroutineRunner, LoadingCurtain curtain)
        {
            _stateMachine = new GameStateMachine(new SceneLoader(coroutineRunner), curtain, new AllServices());
        }
    }
}