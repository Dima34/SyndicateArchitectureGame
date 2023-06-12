using System.Linq;
using Infrastructure.Factory;
using Infrastructure.Services;
using Logic;
using Player;
using UnityEngine;

namespace Enemy
{
    [RequireComponent(typeof(EnemyAnimator))]
    public class Attack : TriggerZoneComponentToToggle
    {
        [SerializeField] private EnemyAnimator _animator;
        [SerializeField] private float _attackCooldown = 3f;
        [SerializeField] private float _cleavage = 1f;
        [SerializeField] private float _effectiveDistance;
        [SerializeField] private float _damage = 13;

        private IGameFactory _gameFactory;
        private Transform _heroTransform;
        private float _timeToNextAttack;
        private bool _isAtacking;
        private Collider[] _hits = new Collider[1];
        private int _layerMask;

        private const float HIT_STARTPOINT_Y_CORRECTION = 0.5f;
        
        private void Awake()
        {
            _gameFactory = AllServices.Container.GetSingle<IGameFactory>();
            _gameFactory.OnHeroCreated += RegisterHeroTransform;
            _layerMask = 1 << LayerMask.NameToLayer("Player");
        }

        private void Update()
        {
            if (CanAttack())
                StartAttack();
            else
                DecreaseTimeToNextAttack();
        }

        private void RegisterHeroTransform() =>
            _heroTransform = _gameFactory.HeroGameObject.transform;

        private bool CanAttack() =>
            _isHeroInZone &&  CooldownIsUp() && !_isAtacking;

        private bool CooldownIsUp() =>
            _timeToNextAttack <= 0;

        private void StartAttack()
        {
            transform.LookAt(_heroTransform);
            _animator.PlayAttack();

            _isAtacking = true;
        }

        private void DecreaseTimeToNextAttack() =>
            _timeToNextAttack -= Time.deltaTime;

        private void OnAttackEnded()
        {
            _timeToNextAttack = _attackCooldown;
            _isAtacking = false;
        }

        private void OnAttack()
        {
            if (Hit(out Collider hit)) 
                hit.transform.GetComponent<HeroHealth>().TakeDamage(_damage);
        }

        private bool Hit(out Collider hit)
        {
            int hitsCount = Physics.OverlapSphereNonAlloc(HitStartPoint(), _cleavage, _hits, _layerMask);

            hit = _hits.FirstOrDefault();
#if UNITY_EDITOR
            PhysicsDebug.DebugLineSpere(HitStartPoint(), _cleavage, 1f);
#endif

            return hitsCount > 0;
        }

        private Vector3 HitStartPoint() =>
            new Vector3(transform.position.x, transform.position.y + HIT_STARTPOINT_Y_CORRECTION, transform.position.z) + EffectiveDistance();

        private Vector3 EffectiveDistance() => 
            transform.forward * _effectiveDistance;

    }
}