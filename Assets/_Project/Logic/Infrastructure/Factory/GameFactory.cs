using System.Collections.Generic;
using _Project.Logic.Data;
using _Project.Logic.Enemy;
using _Project.Logic.Infrastructure.AssetManagement;
using _Project.Logic.Infrastructure.Services;
using _Project.Logic.Infrastructure.Services.PersistentProgress;
using _Project.Logic.StaticData;
using _Project.Logic.UI;
using UnityEngine;
using UnityEngine.AI;
using Object = UnityEngine.Object;
using Random = System.Random;

namespace _Project.Logic.Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssets _assets;
        private readonly IStaticDataService _staticData;
        private readonly Random _randomService;
        private readonly IPersistentProgressService _progressService;
        private ScoreManager _scoreManager;
        private LootPickupTracker _pickupTracker;

        public List<ISavedProgressReader> ProgressReaders { get; } = new List<ISavedProgressReader>();

        public List<ISavedProgress> ProgressWriters { get; } = new List<ISavedProgress>();

        private GameObject HeroGameObject { get; set; }


        public GameFactory(IAssets assets, IStaticDataService staticData, Random randomService,
            IPersistentProgressService progressService)
        {
            _assets = assets;
            _staticData = staticData;
            _randomService = randomService;
            _progressService = progressService;
        }

        public GameObject CreateHud()
        {
            GameObject hud = InstantiateRegistered(AssetPath.HUDPATH);

            hud.GetComponentInChildren<LootCounter>().Construct(_progressService.Progress.WorldData);

            return hud;
        }

        public GameObject CreateHero(GameObject at)
        {
            HeroGameObject = InstantiateRegistered(AssetPath.HEROPATH, at.transform.position);
            return HeroGameObject;
        }

        public GameObject CreateMonster(MonsterTypeId typeId, Transform parent)
        {
            MonsterStaticData monsterData = _staticData.ForMonster(typeId);
            GameObject monster = Object.Instantiate(monsterData.Prefab, parent.position, Quaternion.identity, parent);

            IHealth health = monster.GetComponent<IHealth>();
            health.Current = monsterData.Hp;
            health.Max = monsterData.Hp;

            monster.GetComponent<ActorUI>().Construct(health);
            monster.GetComponent<AgentMoveToPlayer>().Construct(HeroGameObject.transform);
            monster.GetComponent<NavMeshAgent>().speed = monsterData.MoveSpeed;

            LootSpawner lootSpawner = monster.GetComponentInChildren<LootSpawner>();
            lootSpawner.SetLoot(monsterData.MinLoot, monsterData.MaxLoot);
            lootSpawner.Construct(this, _randomService, _pickupTracker);

            var attack = monster.GetComponent<Attack>();
            attack.Construct(HeroGameObject.transform);
            attack.Damage = monsterData.Damage;
            attack.Clevage = monsterData.Clevage;
            attack.EffectiveDistance = monsterData.EffectiveDistance;

            monster.GetComponent<AgentRotateToHero>()?.Construct(HeroGameObject.transform);

            return monster;
        }

        public LootPiece CreateLoot()
        {
            LootPiece lootPiece = InstantiateRegistered(AssetPath.LOOT).GetComponent<LootPiece>();

            if (_progressService != null)
            {
                lootPiece.Construct(_progressService.Progress.WorldData, _scoreManager, _pickupTracker);
            }

            return lootPiece;
        }

        public void CreateScoreManager()
        {
            GameObject scoreManager = InstantiateRegistered(AssetPath.SCOREMANAGER);
            _scoreManager = scoreManager.GetComponent<ScoreManager>();
        }

        public void CreateLootPickupTracker()
        {
            GameObject pickupTracker = InstantiateRegistered(AssetPath.LOOTPICKUPTRACKER);
            _pickupTracker = pickupTracker.GetComponent<LootPickupTracker>();
        }

        public void CleanUp()
        {
            ProgressReaders.Clear();
            ProgressWriters.Clear();
        }

        private GameObject InstantiateRegistered(string prefabPath, Vector3 position)
        {
            GameObject gameObject = _assets.Instantiate(prefabPath, at: position);
            RegisterProgressWatchers(gameObject);
            return gameObject;
        }

        private GameObject InstantiateRegistered(string prefabPath)
        {
            GameObject gameObject = _assets.Instantiate(prefabPath);
            RegisterProgressWatchers(gameObject);
            return gameObject;
        }

        private void RegisterProgressWatchers(GameObject gameObject)
        {
            foreach (ISavedProgressReader progressReader in gameObject.GetComponentsInChildren<ISavedProgressReader>())
            {
                Register(progressReader);
            }
        }

        public void Register(ISavedProgressReader progressReader)
        {
            if (progressReader is ISavedProgress progressWriter)
            {
                ProgressWriters.Add(progressWriter);
            }

            ProgressReaders.Add(progressReader);
        }

        public void Dispose()
        {
            Debug.Log("Dispose from GameFactory");
        }
    }
}