using System;
using System.Threading.Tasks;
using Enemy;
using Infrastructure.Data;
using Infrastructure.Factory;
using Infrastructure.Services.PersistantProgress;
using StaticData;
using UnityEngine;

namespace Logic.EnemySpawners
{
    public class SpawnPoint : MonoBehaviour, ISavedProgress
    {
        [SerializeField] private MonsterTypeId _monsterTypeId;
        // SerializeField for visual working condition test
        [SerializeField] private bool _slain;

        private string _id;
        private IGameFactory _gameFactory;

        public void Construct(IGameFactory factory) =>
            _gameFactory = factory;

        public void Initialize(string id, MonsterTypeId monsterTypeId)
        {
            _id = id;
            _monsterTypeId = monsterTypeId;
        }

        public void LoadProgress(Progress progress)
        {
            if (progress.KillData.ClearedSpawners.Contains(_id))
                _slain = true;
            else
                Spawn();
        }

        private async void Spawn()
        {
            GameObject monster = await _gameFactory.InstantiateMonster(_monsterTypeId, transform);
            monster.GetComponent<EnemyDeath>().Happend += Slain;
        }

        private void Slain() =>
            _slain = true;

        public void UpdateProgress(Progress progress)
        {
            if (_slain)
                progress.KillData.ClearedSpawners.Add(_id);
        }
    }
}