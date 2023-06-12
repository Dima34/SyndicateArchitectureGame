using System;
using Player;
using UnityEngine;

namespace UI
{
    public class ActorUI : MonoBehaviour
    {
        [SerializeField] private HpBar _hpBar;

        private HeroHealth _heroHealth;

        private void UpdateHpBar()
        {
            _hpBar.SetValue(_heroHealth.Current, _heroHealth.Max);
        }

        public void Construct(HeroHealth heroHealth)
        {
            _heroHealth = heroHealth;
            _heroHealth.OnHealthChanged += UpdateHpBar;
        }

        private void OnDestroy() =>
            _heroHealth.OnHealthChanged -= UpdateHpBar;
    }
}