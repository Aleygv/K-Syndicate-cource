using _Project.Logic.Data;

namespace _Project.Logic.Infrastructure.Services.SaveLoad
{
    public interface ISaveLoadService : IService
    {
        void SaveProgress();

        PlayerProgress LoadProgress();
    }
}