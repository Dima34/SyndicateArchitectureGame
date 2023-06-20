using System.Collections.Generic;
using System.Linq;
using Infrastructure;
using UnityEngine;

namespace StaticData
{
    public class StaticDataService : IStaticDataService
    {
        private Dictionary<MonsterTypeId, MonsterStaticData> _monsters;
        private Dictionary<string, LevelStaticData> _levels;
        
        public void LoadMonsters()
        {
            _monsters = Resources.LoadAll<MonsterStaticData>(AssetPath.STATICDATA_MONSTERS_PATH)
                .ToDictionary(x => x.TypeId);
            
            _levels = Resources.LoadAll<LevelStaticData>(AssetPath.STATICDATA_LEVELS_PATH)
                .ToDictionary(x => x.LevelKey);
        }

        public MonsterStaticData ForMonster(MonsterTypeId monsterTypeId) =>
            _monsters.TryGetValue(monsterTypeId, out MonsterStaticData staticData) ? staticData : null;

        public LevelStaticData ForLevel(string sceneKey) =>
            _levels.TryGetValue(sceneKey, out LevelStaticData staticData) ? staticData : null;
    }
}