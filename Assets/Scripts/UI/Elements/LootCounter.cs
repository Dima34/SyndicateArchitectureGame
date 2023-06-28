using Infrastructure.Data;
using TMPro;
using UnityEngine;

namespace UI.Elements
{
    public class LootCounter : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _counter;

        private CollectedPointsData _collectedPointsData;

        private void Start() =>
            UpdateCounter();

        public void Construct(CollectedPointsData collectedPointsData)
        {
            _collectedPointsData = collectedPointsData;
            _collectedPointsData.OnChanged += UpdateCounter;
        }

        private void UpdateCounter() =>
            _counter.text = $"{_collectedPointsData.PointsCollected.ToString()}";
    }
}