using Enemy;
using Infrastructure.Data;
using Infrastructure.Factory;
using Infrastructure.Services;
using Infrastructure.Services.PersistantProgress;
using StaticData;
using UnityEngine;

namespace Logic
{
    [RequireComponent(typeof(UniqueId))]
    public class EnemySpawner : MonoBehaviour, ISavedProgress
    {
        [SerializeField] private MonsterTypeId _monsterTypeId;
        // SerializeField for visual working condition test
        [SerializeField] private bool _slain;

        private string _id;
        private IGameFactory _gameFactory;
        
        private void Awake()
        {
            AssingGameFactory();
            _id = GetComponent<UniqueId>().Id;
        }

        private void AssingGameFactory() =>
            _gameFactory = AllServices.Container.GetSingle<IGameFactory>();

        public void LoadProgress(PlayerProgress progress)
        {
            if (progress.KillData.ClearedSpawners.Contains(_id))
                _slain = true;
            else
                Spawn();
        }

        private void Spawn()
        {
            GameObject monster = _gameFactory.InstantiateMonster(_monsterTypeId, transform);
            monster.GetComponent<EnemyDeath>().Happend += Slain;
        }

        private void Slain() =>
            _slain = true;

        public void UpdateProgress(PlayerProgress progress)
        {
            if(_slain)
                progress.KillData.ClearedSpawners.Add(_id);
        }
    }
}