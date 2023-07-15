using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using StaticData;
using StaticData.Windows;
using UI.Services.Windows;
using UnityEngine;

namespace Infrastructure.Services.StaticData
{
    public class StaticDataService : IStaticDataService
    {
        private Dictionary<MonsterTypeId, MonsterStaticData> _monsters;
        private Dictionary<string, LevelStaticData> _levels;
        private HeroStaticData _hero;
        private Dictionary<WindowID, WindowConfig> _windows;

        public void LoadStaticData()
        {
            LoadHero();
            LoadMonsters();
            LoadLevels();
            LoadWindowsData();
        }
        
        private void LoadMonsters() =>
            _monsters = TryLoadAll<MonsterStaticData>(StaticDataPath.STATICDATA_MONSTERS_PATH)?.ToDictionary(x => x.TypeId);
        
        private void LoadLevels() =>
            _levels = TryLoadAll<LevelStaticData>(StaticDataPath.STATICDATA_LEVELS_PATH)
                .ToDictionary(x => x.LevelKey);
        
        private void LoadWindowsData() =>
            _windows = TryLoad<WindowsStaticData>(StaticDataPath.STATICDATA_WINDOWS_PATH)
                .Configs
                .ToDictionary(x=> x.WindowID, x=>x);

        public void LoadHero() =>
            _hero = TryLoad<HeroStaticData>(StaticDataPath.STATICDATA_HERO_PATH);

        public MonsterStaticData ForMonster(MonsterTypeId monsterTypeId) =>
            _monsters.TryGetValue(monsterTypeId, out MonsterStaticData staticData) ? staticData : null;

        public LevelStaticData ForLevel(string sceneKey) =>
            _levels.TryGetValue(sceneKey, out LevelStaticData staticData) ? staticData : null;

        public WindowConfig ForWindow(WindowID windowID) =>
            _windows.TryGetValue(windowID, out WindowConfig windowConfig) ? windowConfig : null;

        public HeroStaticData Hero() =>
            _hero;

        private static T[] TryLoadAll<T>(string path) where T : UnityEngine.Object
        {
            T[] loadedResources = Resources.LoadAll<T>(path);
            
            if (loadedResources.Length == 0)
                throw new BadPathException($"Can`t find resources in path \"{path}\"");

            return loadedResources;
        }
        
        private static T TryLoad<T>(string path) where T : UnityEngine.Object
        {
            T loadedResource = Resources.Load<T>(path);
            
            if (loadedResource == null)
                throw new BadPathException($"Can`t find resource in path \"{path}\"");

            return loadedResource;
        }
    }
}