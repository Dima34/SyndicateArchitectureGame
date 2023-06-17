using Infrastructure;
using Infrastructure.Factory;
using UnityEngine;

namespace Enemy
{
    public class LootSpawner : MonoBehaviour
    {
        [SerializeField] private EnemyDeath _enemyDeath;
        
        private IGameFactory _factory;
        private int _floorLayerMask;
        private const float MAX_FLOOR_HIT_DISTANCE = 3f;

        private void Start()
        {
            _floorLayerMask = 1 << LayerMask.GetMask(Constants.FLOOR_LAYER); 
            
            _enemyDeath.Happend += SpawnLoot;
        }

        public void Construct(IGameFactory factory) =>
            _factory = factory;

        private void SpawnLoot()
        {
            var lootGameObject = _factory.CreateLoot();
            lootGameObject.transform.position = GetSpawnPosition();
        }

        private Vector3 GetSpawnPosition()
        {
            return HitFloor(out RaycastHit hit) 
                ? hit.transform.position 
                : transform.position;
        }

        private bool HitFloor(out RaycastHit hit)
        {
            var ray = new Ray(transform.position, -transform.up);
            return Physics.Raycast(ray, out hit, MAX_FLOOR_HIT_DISTANCE, _floorLayerMask);
        }
    }
}