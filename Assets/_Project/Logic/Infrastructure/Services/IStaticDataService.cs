using _Project.Logic.StaticData;

namespace _Project.Logic.Infrastructure.Services
{
    public interface IStaticDataService : IService
    {
        void Load();

        void LoadHero();

        MonsterStaticData ForMonster(MonsterTypeId typeId);

        HeroStaticData ForHero();

        LevelStaticData ForLevel(string sceneKey);
    }
}