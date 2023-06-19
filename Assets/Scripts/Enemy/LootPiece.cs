using System;
using System.Collections;
using Infrastructure.Data;
using Infrastructure.Factory;
using Infrastructure.Services.PersistantProgress;
using Logic;
using TMPro;
using UnityEngine;

namespace Enemy
{
    [RequireComponent(typeof(UniqueId))]
    public class LootPiece : MonoBehaviour, ISavedProgressWriter
    {
        [SerializeField] private GameObject _skull;
        [SerializeField] private TextMeshPro _lootText;
        [SerializeField] private GameObject _pickupTextPopup;
        [SerializeField] private float _destroyDelay = 2f;
        
        private Loot _loot;
        private bool _isPickedUp;
        private WorldData _worldData;

        private string _id;
        private IGameFactory _gameFactory;

        private void Start() =>
            _id = GetComponent<UniqueId>().Id;

        public void Construct(IGameFactory _gameFactory, WorldData worldData)
        {
            this._gameFactory = _gameFactory;
            _worldData = worldData;
        }

        public void Initialize(Loot loot) =>
            _loot = loot;

        private void OnTriggerEnter(Collider other) =>
            Pickup();

        private void Pickup()
        {
            if (_isPickedUp)
                return;
            
            _isPickedUp = true;
            
            AddLootToWorldData();
            HideSkull();
            ShowDeathPopup(_loot);
            UnRegisterFromDataWriting();
            RemoveFromUnearnedLootIfContains();
            StartSelfDestroyCoroutine();
        }

        private void RemoveFromUnearnedLootIfContains()
        {
        }

        private void AddLootToWorldData()
        {
            _worldData.LootData.Collect(_loot);
        }

        private void HideSkull() =>
            _skull.SetActive(false);

        private void ShowDeathPopup(Loot loot)
        {
            _lootText.text = $"{loot.Value}";
            _pickupTextPopup.SetActive(true);
        }

        private void UnRegisterFromDataWriting() =>
            _gameFactory.UnRegisterDataUsers(gameObject);

        private void StartSelfDestroyCoroutine() =>
            StartCoroutine(DestroySelf());

        private IEnumerator DestroySelf()
        {
            yield return new WaitForSeconds(_destroyDelay);
            Destroy(gameObject);
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            if (_loot != null)
            {
                LootPieceData data = new LootPieceData()
                {
                    Id = _id,
                    Loot = _loot,
                    Position = transform.position.AsVectorData()
                };
                
                _worldData.UnEarnedLootpieces.LootPieces.Add(data);
            }
        }

        private void OnDestroy()
        {
            UnRegisterFromDataWriting();
        }
    }
}