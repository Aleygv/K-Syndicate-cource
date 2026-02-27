using System.Collections.Generic;
using System.Linq;
using _Project.Logic.Infrastructure.Services;
using UnityEngine;

namespace _Project.Logic.StaticData
{
    public class StaticDataService : IStaticDataService
    {
        private Dictionary<MonsterTypeId,MonsterStaticData> _monsters;
        private HeroStaticData _hero;

        public void LoadMonsters()
        {
            _monsters = Resources.LoadAll<MonsterStaticData>("StaticData/Monsters").ToDictionary(x => x.MonsterTypeId, x => x);
        }

        public MonsterStaticData ForMonster(MonsterTypeId typeId)
        {
            return _monsters.TryGetValue(typeId, out MonsterStaticData staticData) ? staticData : null;
        }

        public void LoadHero()
        {
            _hero = Resources.Load<HeroStaticData>("StaticData/Hero/HeroData");
        }

        public HeroStaticData ForHero()
        {
            return _hero;
        }

        public void Dispose()
        {
            // TODO release managed resources here
        }
    }
}