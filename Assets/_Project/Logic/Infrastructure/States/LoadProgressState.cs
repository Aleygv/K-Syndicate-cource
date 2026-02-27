using _Project.Logic.Data;
using _Project.Logic.Infrastructure.Services;
using _Project.Logic.Infrastructure.Services.PersistentProgress;
using _Project.Logic.Infrastructure.Services.SaveLoad;
using _Project.Logic.StaticData;

namespace _Project.Logic.Infrastructure.States
{
    public class LoadProgressState : IState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly IPersistentProgressService _progressService;
        private ISaveLoadService _saveLoadService;
        private IStaticDataService _staticData;

        public LoadProgressState(GameStateMachine gameStateMachine, IPersistentProgressService progressService,
            ISaveLoadService saveLoadService, IStaticDataService staticData)
        {
            _gameStateMachine = gameStateMachine;
            _progressService = progressService;
            _saveLoadService = saveLoadService;
            _staticData = staticData;
        }

        public void Enter()
        {
            LoadProgressOrInitNew();
            _gameStateMachine.Enter<LoadLevelState, string>(_progressService.Progress.WorldData.PositionOnLevel.Level);
        }

        public void Exit()
        {
        }

        public void Update()
        {
        }

        private void LoadProgressOrInitNew()
        {
            _progressService.Progress = _saveLoadService.LoadProgress() ?? NewProgress();
        }

        private PlayerProgress NewProgress()
        {
            var progress = new PlayerProgress(initialLevel: "Main");

            HeroStaticData heroData = _staticData.ForHero();

            progress.HeroState.MaxHP = heroData.Hp;
            progress.HeroStats.Damage = heroData.Damage;
            progress.HeroStats.DamageRadius = heroData.DamageRadius;
            progress.HeroState.ResetHP();

            return progress;
        }
    }
}