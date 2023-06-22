using StaticData;
using StaticData.Windows;
using UI.Services.Windows;

namespace Infrastructure.Services.StaticData
{
    public interface IStaticDataService : IService
    {
        MonsterStaticData ForMonster(MonsterTypeId monsterTypeId);
        LevelStaticData ForLevel(string sceneKey);
        HeroStaticData Hero();
        WindowConfig ForWindow(WindowID windowID);
        void LoadStaticData();
    }
}