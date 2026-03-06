using System;
using _Project.Logic.Enemy;
using _Project.Logic.Infrastructure.Services;
using _Project.Logic.Infrastructure.Services.PersistentProgress;
using _Project.Logic.Infrastructure.Services.SaveLoad;
using UnityEngine;

namespace _Project.Logic.Data
{
    public class ScoreManager : MonoBehaviour, ISavedProgress
    {

        private int _localScore;
        private PlayerProgress _progress;
        private ISaveLoadService _saveLoadService;

        private void Awake()
        {
            _saveLoadService = AllServices.Container.Single<ISaveLoadService>();
        }

        public void LoadProgress(PlayerProgress progress)
        {
            _progress = progress;
            _localScore = progress.WorldData.LootData.Collected;
            Debug.Log("Load");
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            _progress.WorldData.LootData.Collected = _localScore;
            Debug.Log("Update");
        }

        public void AddScore(int value)
        {
            _localScore += value;
        }

        private void OnApplicationQuit()
        {
            _saveLoadService.SaveProgress(this);
        }
    }
}