using System;
using Infrastructure;
using Infrastructure.Factory;
using Infrastructure.Services;
using UnityEngine;
using UnityEngine.AI;

namespace Enemy
{
    public class AgentMoveToPlayer : MonoBehaviour
    {
        public NavMeshAgent _agent;
        private Transform _heroTransform;
        private IGameFactory _gameFactory;

        private void Start()
        {
            _gameFactory = AllServices.Container.GetSingle<IGameFactory>();
            
            if (_gameFactory.HeroGameObject == null)
                _gameFactory.HeroCreated += HeroCreated;
            else
                RegisterHeroTransform();
        }

        private void HeroCreated() =>
            RegisterHeroTransform();

        private void RegisterHeroTransform() =>
            _heroTransform = _gameFactory.HeroGameObject.transform;

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