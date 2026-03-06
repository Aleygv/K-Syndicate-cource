using System;
using _Project.Logic.Hero;
using _Project.Logic.Infrastructure.Factory;
using _Project.Logic.Infrastructure.Services.PersistentProgress;
using _Project.Logic.UI;
using UnityEngine;

namespace _Project.Logic.Infrastructure.States
{
    public class LoadLevelState : IPayLoadedState<string>
    {
        private const string INITIALPOINTTAG = "InitialPoint";
        private const string ENEMYSPAWNER = "EnemySpawner";
        private readonly GameStateMachine _gameStateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly LoadingCurtain _curtain;
        private readonly IGameFactory _gameFactory;
        private readonly IPersistentProgressService _progressService;

        public LoadLevelState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, LoadingCurtain curtain,
            IGameFactory gameFactory, IPersistentProgressService progressService)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _curtain = curtain;
            _gameFactory = gameFactory;
            _progressService = progressService;
        }

        public void Enter(string sceneName)
        {
            _curtain.Show();
            _gameFactory.CleanUp();
            _sceneLoader.Load(sceneName, OnLoaded);
        }

        public void Exit()
        {
            _curtain.Hide();
            _curtain?.Dispose();
        }

        public void Update()
        {
        }

        private void OnLoaded()
        {
            InitGameWorld();
            InformProgressReaders();

            _gameStateMachine.Enter<GameLoopState>();
        }

        private void InformProgressReaders()
        {
            foreach (ISavedProgressReader progressReader in _gameFactory.ProgressReaders)
            {
                progressReader.LoadProgress(_progressService.Progress);
            }
        }

        private void InitGameWorld()
        {
            InitSpawners();

            GameObject hero = _gameFactory.CreateHero(at: GameObject.FindWithTag(INITIALPOINTTAG));

            InitHud(hero);

            CameraFollow(hero);

            InitSaveManagers();
        }

        private void InitSpawners()
        {
            foreach (GameObject spawnerObject in GameObject.FindGameObjectsWithTag(ENEMYSPAWNER))
            {
                var spawner = spawnerObject.GetComponent<EnemySpawner>();
                _gameFactory.Register(spawner);
            }
        }

        private void InitHud(GameObject hero)
        {
            GameObject hud = _gameFactory.CreateHud();

            hud.GetComponentInChildren<ActorUI>().Construct(hero.GetComponentInChildren<HeroHealth>());
        }

        private void InitSaveManagers()
        {
            _gameFactory.CreateLootPickupTracker();
            _gameFactory.CreateScoreManager();
        }

        private static void CameraFollow(GameObject hero)
        {
            Camera.main.GetComponent<CameraFollow>().Follow(hero);
        }
    }
}