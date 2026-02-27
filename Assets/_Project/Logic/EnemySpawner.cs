using _Project.Logic.Data;
using _Project.Logic.Enemy;
using _Project.Logic.Infrastructure.Factory;
using _Project.Logic.Infrastructure.Services;
using _Project.Logic.Infrastructure.Services.PersistentProgress;
using _Project.Logic.StaticData;
using UnityEngine;

namespace _Project.Logic
{
    public class EnemySpawner : MonoBehaviour, ISavedProgress
    {
        public MonsterTypeId MonsterTypeId;
        private string _id;

        [SerializeField] private bool _slain;
        private IGameFactory _factory;
        private EnemyDeath _enemyDeath;

        public bool Slain => _slain;

        private void Awake()
        {
            _id = GetComponent<UniqueId>().Id;
            _factory = AllServices.Container.Single<IGameFactory>();
        }

        public void LoadProgress(PlayerProgress progress)
        {
            if (progress.KillData.ClearedSpawners.Contains(_id))
            {
                _slain = true;
            }
            else
            {
                Spawn();
            }
        }

        private void Spawn()
        {
            var monster = _factory.CreateMonster(MonsterTypeId, transform);
            _enemyDeath = monster.GetComponent<EnemyDeath>();
            _enemyDeath.Happend += Slay;
        }

        private void Slay()
        {
            if (_enemyDeath != null)
            {
                _enemyDeath.Happend -= Slay;
            }

            _slain = true;
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            if (Slain)
            {
                progress.KillData.ClearedSpawners.Add(_id);
            }
        }
    }
}