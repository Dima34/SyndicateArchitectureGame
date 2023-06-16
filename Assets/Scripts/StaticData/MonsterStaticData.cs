using UnityEngine;

namespace StaticData
{
    [CreateAssetMenu(fileName = "MonsterData", menuName = "StaticData/Monster", order = 0)]
    public class MonsterStaticData : ScriptableObject
    {
        [SerializeField] private MonsterTypeId _monsterTypeId;

        [Range(1, 100)] [SerializeField] private float _hp;
        [Range(1, 30)] [SerializeField] private float _damage;

        [Range(0.5f, 1)] [SerializeField] private float _EffectiveDistance;
        [Range(0.5f, 1)] [SerializeField] private float _cleverage;

        [Range(0.5f, 10)] [SerializeField] private float _speed;

        [SerializeField] private GameObject _prefab;
        [SerializeField] private float _attackCooldown;

        public MonsterTypeId TypeId
        {
            get => _monsterTypeId;
            set => _monsterTypeId = value;
        }

        public float Hp
        {
            get => _hp;
            set => _hp = value;
        }

        public float Damage
        {
            get => _damage;
            set => _damage = value;
        }

        public float EffectiveDistance
        {
            get => _EffectiveDistance;
            set => _EffectiveDistance = value;
        }

        public float Cleverage
        {
            get => _cleverage;
            set => _cleverage = value;
        }

        public GameObject Prefab
        {
            get => _prefab;
            set => _prefab = value;
        }

        public float Speed
        {
            get => _speed;
            set => _speed = value;
        }

        public float AttackCooldown
        {
            get => _attackCooldown;
            set => _attackCooldown = value;
        }
    }
}