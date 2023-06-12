using Infrastructure.Factory;
using Infrastructure.Services;
using UnityEngine;

namespace Enemy
{
    public class Follow : MonoBehaviour
    {
        private IGameFactory _gameFactory;
        protected Transform _heroTransform;

        protected virtual void Start()
        {
            _gameFactory = AllServices.Container.GetSingle<IGameFactory>();
            
            if (HeroInFactoryExist())
                AssignHeroWhenHeroCreated();
            else
                AssignHeroTransform();
        }

        private bool HeroInFactoryExist() =>
            _gameFactory.HeroGameObject == null;

        private void AssignHeroWhenHeroCreated() =>
            _gameFactory.OnHeroCreated += AssignHeroTransform;

        private void AssignHeroTransform() =>
            _heroTransform = _gameFactory.HeroGameObject.transform;
    
    }
}