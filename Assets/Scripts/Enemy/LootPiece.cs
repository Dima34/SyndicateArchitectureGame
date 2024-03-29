using System.Collections;
using Infrastructure.Data;
using Infrastructure.Factory;
using Infrastructure.States;
using Logic;
using TMPro;
using UnityEngine;

namespace Enemy
{
    [RequireComponent(typeof(UniqueId))]
    public class LootPiece : MonoBehaviour
    {
        [SerializeField] private GameObject _skull;
        [SerializeField] private TextMeshPro _lootText;
        [SerializeField] private GameObject _pickupTextPopup;
        [SerializeField] private float _destroyDelay = 2f;
        
        private Loot _loot;
        private bool _isPickedUp;
        private string _id;
        private IGameFactory _gameFactory;
        private IUnearnedLootService _lootService;
        private CollectedPointsData _collectedPoints;

        private void Awake() =>
            _id = GetComponent<UniqueId>().Id;

        public void Construct(IUnearnedLootService lootService, CollectedPointsData collectedPoints)
        {
            _lootService = lootService;
            _collectedPoints = collectedPoints;
        }

        public void Initialize(Loot loot, Vector3 position)
        {
            _loot = loot;
            transform.position = position;
            RegisterOnLootService(_id, position, loot);
        }

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
            StartSelfDestroyCoroutine();
        }

        private void RegisterOnLootService(string id, Vector3 position, Loot loot) =>
            _lootService.Add(id, position, loot);

        private void AddLootToWorldData()
        {
            _collectedPoints.Collect(_loot);
        }

        private void HideSkull() =>
            _skull.SetActive(false);

        private void ShowDeathPopup(Loot loot)
        {
            _lootText.text = $"{loot.Value}";
            _pickupTextPopup.SetActive(true);
        }

        private void StartSelfDestroyCoroutine() =>
            StartCoroutine(DestroySelf());

        private IEnumerator DestroySelf()
        {
            yield return new WaitForSeconds(_destroyDelay);
            Destroy(gameObject);
        }
    }
}