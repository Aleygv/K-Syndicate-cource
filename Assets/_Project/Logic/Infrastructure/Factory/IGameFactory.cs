using System;
using System.Collections.Generic;
using _Project.Logic.Data;
using _Project.Logic.Enemy;
using _Project.Logic.Infrastructure.Services;
using _Project.Logic.Infrastructure.Services.PersistentProgress;
using _Project.Logic.StaticData;
using UnityEngine;

namespace _Project.Logic.Infrastructure.Factory
{
    public interface IGameFactory : IService
    {
        List<ISavedProgressReader> ProgressReaders { get; }

        List<ISavedProgress> ProgressWriters { get; }

        event Action<LootPiece> OnLootCreated;

        GameObject CreateHero(GameObject at);

        GameObject CreateHud();

        void CleanUp();

        void CreateSpawner(Vector3 at, string spawnerId, MonsterTypeId monsterTypeId);

        GameObject CreateMonster(MonsterTypeId typeId, Transform parent);

        LootPiece CreateLoot(Vector3Data asVectorData, Loot lootItem);

        void CreateScoreManager();

        void CreateLootPickupTracker();
    }
}