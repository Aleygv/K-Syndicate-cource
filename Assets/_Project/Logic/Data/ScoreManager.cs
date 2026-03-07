using _Project.Logic.Infrastructure.Services.PersistentProgress;
using UnityEngine;

namespace _Project.Logic.Data
{
    public class ScoreManager : MonoBehaviour, ISavedProgress
    {
        private int _localScore;
        private PlayerProgress _progress;

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
    }
}