namespace _Project.Logic.Infrastructure.States
{
    public interface IState : IExitableState
    {
        void Enter();
    }

    public interface IExitableState : IUpdatableState
    {
        void Exit();
    }

    public interface IUpdatableState
    {
        void Update();
    }

    public interface IPayLoadedState<TPayload> : IExitableState
    {
        void Enter(TPayload payload);
    }
}