using System.Collections;
using Infrastructure;
using UnityEngine;
using UnityEngine.AI;

namespace Enemy
{
    [RequireComponent(typeof(EnemyAnimator))]
    public class EnemyDeath : MortalBase
    {
        [SerializeField] private EnemyAnimator _enemyAnimator;
        [SerializeField] private GameObject _deathFX;
        [SerializeField] private NavMeshAgent _agent;
        [SerializeField] private AnimateAlongAgent _animateAlongAgent;
        [SerializeField] private AgentMoveToHero _agentMoveToHero;


        protected override bool IsDead() =>
            _healthToTrack.CurrentHP <= 0;

        protected override void Die()
        {
            _enemyAnimator.PlayDeath();
            SpawnDeathVFX();
            DisableMoving();
            StartCoroutine(BodyCollector());
        }

        private void DisableMoving()
        {
            _agent.enabled = false;
            _animateAlongAgent.enabled = false;
            _agentMoveToHero.enabled = false;
        }

        private void SpawnDeathVFX() =>
            Instantiate(_deathFX, transform.position, Quaternion.identity);

        private IEnumerator BodyCollector()
        {
            yield return new WaitForSeconds(Constants.DEAD_BODY_EXIS_TIME);
            Destroy(gameObject);
        }
    }
}