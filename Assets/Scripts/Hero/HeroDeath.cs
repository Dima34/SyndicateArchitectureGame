using Enemy;
using UnityEngine;

namespace Hero
{
    [RequireComponent(typeof(HeroAttack))]
    public class HeroDeath : MortalBase
    {
        [SerializeField] private HeroAttack _heroAttack;
        [SerializeField] private HeroMove _heroMove;
        [SerializeField] private GameObject _deathFX;
        
        private bool _isDead = false;
        
        protected override bool IsDead() =>
            _healthToTrack.CurrentHP <= 0 && !_isDead;

        protected override void Die()
        {
            Debug.LogWarning("Death...");
            DisableAbilities();
            PlayDeathFX();
            _isDead = true;
        }

        private void PlayDeathFX() =>
            Instantiate(_deathFX, transform.position, Quaternion.identity);

        private void DisableAbilities()
        {
            _heroMove.enabled = false;
            _heroAttack.enabled = false;
        }
    }
}