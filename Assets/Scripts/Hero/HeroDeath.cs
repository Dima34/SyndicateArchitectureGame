using System;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(HeroHealth))]
    public class HeroDeath : MonoBehaviour
    {
        [SerializeField] private HeroHealth _heroHealth;
        [SerializeField] private HeroMove _heroMove;
        [SerializeField] private GameObject _deathFX;
        private bool _isDead = false;

        private void Start() =>
            _heroHealth.OnHealthChanged += OnHealthChanged;

        private void OnDestroy() =>
            _heroHealth.OnHealthChanged -= OnHealthChanged;

        private void OnHealthChanged()
        {
            if (_heroHealth.Current <= 0 && _isDead) 
                Die();
        }

        private void Die()
        {
            DisableMovement();
            PlayDeathFX();
            _isDead = true;
        }

        private void PlayDeathFX()
        {
            Instantiate(_deathFX, transform.position, Quaternion.identity);
        }

        private void DisableMovement()
        {
            _heroMove.enabled = false;
        }
    }
}