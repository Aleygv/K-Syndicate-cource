namespace _Project.Logic.Infrastructure.States
{
    public interface IDisposabler : ICoroutineRunner
    {
        void OnApplicationQuit();
    }
}