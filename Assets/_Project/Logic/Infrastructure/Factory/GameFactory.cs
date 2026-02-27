using System.Collections.Generic;
using _Project.Logic.Data;
using _Project.Logic.Enemy;
using _Project.Logic.Hero;
using _Project.Logic.Infrastructure.AssetManagement;
using _Project.Logic.Infrastructure.Services;
using _Project.Logic.Infrastructure.Services.PersistentProgress;
using _Project.Logic.StaticData;
using _Project.Logic.UI;
using UnityEngine;
using UnityEngine.AI;
using Object = UnityEngine.Object;

namespace _Project.Logic.Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssets _assets;
        private readonly IStaticDataService _staticData;

        public List<ISavedProgressReader> ProgressReaders { get; } = new List<ISavedProgressReader>();

        public List<ISavedProgress> ProgressWriters { get; } = new List<ISavedProgress>();

        private GameObject HeroGameObject { get; set; }


        public GameFactory(IAssets assets, IStaticDataService staticData)
        {
            _assets = assets;
            _staticData = staticData;
        }

        public GameObject CreateHud()
        {
            return InstantiateRegistered(AssetPath.HUDPATH);
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

            var attack = monster.GetComponent<Attack>();
            attack.Construct(HeroGameObject.transform);
            attack.Damage = monsterData.Damage;
            attack.Clevage = monsterData.Clevage;
            attack.EffectiveDistance = monsterData.EffectiveDistance;

            monster.GetComponent<AgentRotateToHero>()?.Construct(HeroGameObject.transform);

            return monster;
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