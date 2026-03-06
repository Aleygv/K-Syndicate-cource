using System.Collections.Generic;
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

        GameObject CreateHero(GameObject at);

        GameObject CreateHud();

        void CleanUp();

        void Register(ISavedProgressReader progressReader);

        GameObject CreateMonster(MonsterTypeId typeId, Transform parent);

        LootPiece CreateLoot();

        void CreateScoreManager();

        void CreateLootPickupTracker();
    }
}