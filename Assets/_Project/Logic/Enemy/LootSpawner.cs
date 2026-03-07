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

        public void Construct(IGameFactory gameFactory, Random random)
        {
            _factory = gameFactory;
            _random = random;
            // _pickupTracker = lootPickupTracker;
        }

        private void Start()
        {
            EnemyDeath.Happend += SpawnLoot;
            // SpawnRemainingLoot();
        }

        private void SpawnLoot()
        {
            var lootItem = GenerateLoot();

            LootPiece loot = _factory.CreateLoot(transform.position.AsVectorData(), lootItem);
            loot.transform.position = transform.position;

            loot.Initialize(lootItem);
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