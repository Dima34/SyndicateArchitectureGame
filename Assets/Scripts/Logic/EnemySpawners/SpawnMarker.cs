using StaticData;
using UnityEngine;

namespace Logic.EnemySpawners
{
    public class SpawnMarker : MonoBehaviour
    {
        [SerializeField] private MonsterTypeId monsterMonsterTypeId;
        
        public MonsterTypeId MonsterTypeId
        {
            get => monsterMonsterTypeId;
            set => monsterMonsterTypeId = value;
        }
    }
}