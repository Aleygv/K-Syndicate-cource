using _Project.Logic.Data;
using _Project.Logic.Infrastructure.Factory;
using UnityEngine;
using Random = System.Random;

namespace _Project.Logic.Enemy
{
    public class LootSpawner : MonoBehaviour
    {
        public EnemyDeath EnemyDeath;
        private IGameFactory _factory;
        private int _lootMin;
        private int _lootMax;
        private Random _random;
        private LootPickupTracker _pickupTracker;

        public void Construct(IGameFactory gameFactory, Random random, LootPickupTracker lootPickupTracker)
        {
            _factory = gameFactory;
            _random = random;
            _pickupTracker = lootPickupTracker;
        }

        private void Start()
        {
            EnemyDeath.Happend += SpawnLoot;
            SpawnRemainingLoot();
        }

        private void SpawnLoot()
        {
            LootPiece loot = _factory.CreateLoot();
            loot.transform.position = transform.position;

            var lootItem = GenerateLoot();
            loot.Initialize(lootItem);
        }

        private void SpawnRemainingLoot()
        {
            foreach (LootSavedData lootSavedData in _pickupTracker.GetRemainingLootData)
            {
                var instance = _factory.CreateLoot();
                var lootPiece = instance.GetComponent<LootPiece>();
                var lootItem = GenerateLoot();
                lootPiece.Initialize(lootItem);
                lootPiece.SetId(lootSavedData.Id);
                lootPiece.SetPosition(lootSavedData.Position);
            }
        }

        private Loot GenerateLoot()
        {
            return new Loot()
            {
                Value = _random.Next(_lootMin, _lootMax),
            };
        }

        public void SetLoot(int min, int max)
        {
            _lootMin = min;
            _lootMax = max;
        }
    }
}