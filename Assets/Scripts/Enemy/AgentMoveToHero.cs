using Infrastructure;
using UnityEngine;
using UnityEngine.AI;

namespace Enemy
{
    public class AgentMoveToHero : Follow
    {
        public NavMeshAgent _agent;

        private void Update()
        {
            var heroTransformNotInitialized = _heroTransform == null;
            if(heroTransformNotInitialized)
                return;
            
            var heroPosition = _heroTransform.position;
            var distanceToHero = Vector3.Distance(_agent.transform.position, heroPosition);

            if(distanceToHero > Constants.MINIMAL_DISTANSE_TO_PLAYER)
                _agent.destination = heroPosition;
        }
    }
}