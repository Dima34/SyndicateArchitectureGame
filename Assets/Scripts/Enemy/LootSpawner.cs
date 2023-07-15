using Infrastructure;
using Infrastructure.Data;
using Infrastructure.Factory;
using Infrastructure.Services.Random;
using UnityEngine;

namespace Enemy
{
    public class LootSpawner : MonoBehaviour
    {
        [SerializeField] private EnemyDeath _enemyDeath;
        
        private IGameFactory _factory;
        private IRandomService _randomService;
        private int _floorLayerMask;
        private int _minLoot;
        private int _maxLoot;

        private const float MAX_FLOOR_HIT_DISTANCE = 3f;

        private void Start()
        {
            _floorLayerMask = 1 << LayerMask.GetMask(Constants.FLOOR_LAYER); 
            _enemyDeath.Happend += SpawnLoot;
        }

        public void Construct(IGameFactory factory, IRandomService randomService)
        {
            _factory = factory;
            _randomService = randomService;
        }

        private async void SpawnLoot()
        {
            LootPiece lootGameObject = await _factory.CreateLoot();
            lootGameObject.Initialize(GenerateLoot(), GetSpawnPosition());
        }

        private Vector3 GetSpawnPosition()
        {
            return HitFloor(out RaycastHit hit) 
                ? hit.transform.position 
                : transform.position;
        }

        private Loot GenerateLoot() =>
            new Loot()
            {
                Value = _randomService.Next(_minLoot, _maxLoot)
            };

        private bool HitFloor(out RaycastHit hit)
        {
            var ray = new Ray(transform.position, -transform.up);
            return Physics.Raycast(ray, out hit, MAX_FLOOR_HIT_DISTANCE, _floorLayerMask);
        }

        public void SetLoot(int min, int max)
        {
            _minLoot = min;
            _maxLoot = max;
        }
    }
}