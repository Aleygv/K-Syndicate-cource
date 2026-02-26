using System;
using _Project.Logic.Data;
using _Project.Logic.Infrastructure.Services.PersistentProgress;
using _Project.Logic.StaticData;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Project.Logic
{
    public class EnemySpawner : MonoBehaviour, ISavedProgress
    {
        public MonsterTypeId MonsterTypeId;
        private string _id;

        public bool Slain;

        private void Awake()
        {
            _id = GetComponent<UniqueId>().Id;
        }

        public void LoadProgress(PlayerProgress progress)
        {
            if (progress.KillData.ClearedSpawners.Contains(_id))
            {
                Slain = true;
            }
            else
            {
                Spawn();
            }
        }

        private void Spawn()
        {

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