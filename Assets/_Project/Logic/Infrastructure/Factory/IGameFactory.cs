using System;
using System.Collections.Generic;
using _Project.Logic.Infrastructure.Services;
using _Project.Logic.Infrastructure.Services.PersistentProgress;
using UnityEngine;

namespace _Project.Logic.Infrastructure.Factory
{
    public interface IGameFactory : IService
    {
        event Action HeroCreated;

        GameObject HeroGameObject { get; }

        List<ISavedProgressReader> ProgressReaders { get; }

        List<ISavedProgress> ProgressWriters { get; }

        GameObject CreateHero(GameObject at);

        void CreateHud();

        void CleanUp();
    }
}