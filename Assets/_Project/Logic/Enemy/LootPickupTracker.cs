using System.Collections.Generic;
using System.Linq;
using _Project.Logic.Data;
using _Project.Logic.Infrastructure.Factory;
using _Project.Logic.Infrastructure.Services;
using _Project.Logic.Infrastructure.Services.PersistentProgress;
using UnityEngine;

namespace _Project.Logic.Enemy
{
    public class LootPickupTracker : ISavedProgress, IService
    {
        private readonly IGameFactory _factory;
        private List<LootPiece> _remainingLootPiece = new List<LootPiece>();
        private List<LootSavedData> _savedLootData;
        private LootPiece _currentLootPiece;

        public LootPickupTracker(IGameFactory factory)
        {
            _factory = factory;
            _factory.OnLootCreated += OnLootCreate;
        }

        public void SpawnRemainingLoot()
        {
            if (_savedLootData == null)
                return;

            foreach (LootSavedData savedData in _savedLootData)
            {
                var lootPiece = _factory.CreateLoot(savedData.Position, savedData.Loot);
                lootPiece.transform.position = savedData.Position.AsUnityVector();
                lootPiece.Initialize(savedData.Loot);

                _remainingLootPiece.Add(lootPiece);
                lootPiece.OnLootCollected += LootCollected;
            }

            _savedLootData.Clear();
        }

        private void OnLootCreate(LootPiece lootPiece)
        {
            _remainingLootPiece.Add(lootPiece);
            lootPiece.OnLootCollected += LootCollected;
        }

        private void LootCollected(LootPiece piece)
        {
            piece.OnLootCollected -= LootCollected;
            _remainingLootPiece.Remove(piece);
        }

        public void LoadProgress(PlayerProgress progress)
        {
            Debug.Log($"[Tracker] LoadProgress called! Data: {progress.WorldData.LootData.RemainingLoot?.Count ?? 0}");

            _savedLootData = progress.WorldData.LootData.RemainingLoot;
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            progress.WorldData.LootData.RemainingLoot =
                _remainingLootPiece.Where(p => p != null).Select(p => p.GetLootSavedData()).ToList();
            Debug.Log("loot updated");
        }

        public void Dispose()
        {
            _factory.OnLootCreated -= OnLootCreate;
            Debug.Log("Dispose LootPickupTracker");
        }
    }
}