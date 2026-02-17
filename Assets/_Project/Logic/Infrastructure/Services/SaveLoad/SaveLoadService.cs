using _Project.Logic.Data;
using _Project.Logic.Infrastructure.Factory;
using _Project.Logic.Infrastructure.Services.PersistentProgress;
using UnityEngine;

namespace _Project.Logic.Infrastructure.Services.SaveLoad
{
    public class SaveLoadService : ISaveLoadService
    {
        private const string PROGRESSKEY = "Progress";

        private readonly IPersistentProgressService _progressService;
        private readonly IGameFactory _gameFactory;

        public SaveLoadService(IPersistentProgressService progressService, IGameFactory gameFactory)
        {
            _progressService = progressService;
            _gameFactory = gameFactory;
        }

        public void SaveProgress()
        {
            foreach (ISavedProgress progressWriter in _gameFactory.ProgressWriters)
            {
                progressWriter.UpdateProgress(_progressService.Progress);
            }

            PlayerPrefs.SetString(PROGRESSKEY, _progressService.Progress.ToJson());
        }

        public PlayerProgress LoadProgress()
        {
            return PlayerPrefs.GetString(PROGRESSKEY)?.ToDeserialized<PlayerProgress>();
        }

        public void Dispose()
        {
            _progressService?.Dispose();
            _gameFactory?.Dispose();
        }
    }
}