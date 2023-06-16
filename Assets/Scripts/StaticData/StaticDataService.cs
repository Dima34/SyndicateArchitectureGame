using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace StaticData
{
    public class StaticDataService : IStaticDataService
    {
        private Dictionary<MonsterTypeId, MonsterStaticData> _monsters;

        public void LoadMonsters()
        {
            _monsters = Resources.LoadAll<MonsterStaticData>("StaticData/Monsters")
                .ToDictionary(x => x.TypeId);
        }

        public MonsterStaticData ForMonster(MonsterTypeId monsterTypeId) =>
            _monsters.TryGetValue(monsterTypeId, out MonsterStaticData staticData) ? staticData : null;
    }
}