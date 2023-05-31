using System;
using Infrastructure;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.AI;

namespace Enemy
{
    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(EnemyAnimator))]
    public class AnimateAlongAgent : MonoBehaviour
    {
        private NavMeshAgent _navMeshAgent;
        private EnemyAnimator _enemyAnimator;

        private void Start()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _enemyAnimator = GetComponent<EnemyAnimator>();
        }

        private void Update()
        {
            var agentMovementSpeed = _navMeshAgent.velocity.magnitude;
            if(ShouldMove(agentMovementSpeed))
                _enemyAnimator.Move(agentMovementSpeed);
            else
                _enemyAnimator.StopMoving();
        }

        private bool ShouldMove(float agentMovementSpeed)
        {
            var isMoving = agentMovementSpeed > Constants.MINIMAL_SPEED;
            var isOnDestination = _navMeshAgent.remainingDistance > _navMeshAgent.radius;
            
            return isMoving && isOnDestination;
        }
    }
}