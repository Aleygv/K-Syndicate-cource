using _Project.Logic.Data;
using _Project.Logic.Infrastructure.Services.PersistentProgress;

namespace _Project.Logic.Infrastructure.Services.SaveLoad
{
    public interface ISaveLoadService : IService
    {
        void SaveProgress();

        void SaveProgress(ISavedProgress currentSaver);

        PlayerProgress LoadProgress();
    }
}