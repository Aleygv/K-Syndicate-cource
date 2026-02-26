using System;

namespace _Project.Logic.Data
{
    [Serializable]
    public class PlayerProgress
    {
        public State HeroState;
        public WorldData WorldData;
        public Stats HeroStats;
        public KillData KillData;

        public PlayerProgress(string initialLevel)
        {
            HeroState = new State();
            WorldData = new WorldData(initialLevel);
            HeroStats = new Stats();
            KillData = new KillData();
        }
    }
}