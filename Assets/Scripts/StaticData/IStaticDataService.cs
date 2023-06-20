using Infrastructure.Services;

namespace StaticData
{
    public interface IStaticDataService : IService
    {
        void LoadMonsters();
        MonsterStaticData ForMonster(MonsterTypeId monsterTypeId);
        LevelStaticData ForLevel(string sceneKey);
    }
}