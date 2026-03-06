using System.Collections.Generic;
using System.Linq;
using _Project.Logic.Data;
using _Project.Logic.Infrastructure.Services;
using _Project.Logic.Infrastructure.Services.PersistentProgress;
using _Project.Logic.Infrastructure.Services.SaveLoad;
using UnityEngine;

namespace _Project.Logic.Enemy
{
    public class LootPickupTracker : MonoBehaviour, ISavedProgress
    {
        private List<LootSavedData> _remainingLootData = new List<LootSavedData>();
        private ISaveLoadService _saveLoadService;

        private void Awake()
        {
            _saveLoadService = AllServices.Container.Single<ISaveLoadService>();
        }

        public void LoadProgress(PlayerProgress progress)
        {
            _remainingLootData = new List<LootSavedData>(progress.WorldData.LootData.RemainingLoot);
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            progress.WorldData.LootData.RemainingLoot = _remainingLootData;
        }

        public List<LootSavedData> GetRemainingLootData => _remainingLootData;

        private void FindAllNotCollectedLoot()
        {
            _remainingLootData.Clear();

            LootPiece[] lootPieces = FindObjectsByType<LootPiece>(FindObjectsSortMode.None);
            foreach (LootPiece piece in lootPieces)
            {
                _remainingLootData.Add(new LootSavedData(piece.UniqueId.Id, piece.transform.position.AsVectorData()));
            }
        }

        private void OnApplicationQuit()
        {
            FindAllNotCollectedLoot();
            _saveLoadService.SaveProgress(this);
        }
    }
}