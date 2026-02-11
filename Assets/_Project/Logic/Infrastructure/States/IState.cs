namespace _Project.Logic.Infrastructure.States
{
    public interface IState : IExitableState
    {
        void Enter();
    }

    public interface IExitableState
    {
        void Exit();
    }

    public interface IPayLoadedState<TPayload> : IExitableState
    {
        void Enter(TPayload payload);
    }
}