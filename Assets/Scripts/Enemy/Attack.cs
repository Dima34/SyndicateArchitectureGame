using System.Linq;
using Infrastructure;
using Logic;
using UnityEngine;

namespace Enemy
{
    [RequireComponent(typeof(EnemyAnimator))]
    public class Attack : TriggerZoneComponentToToggle
    {
        [SerializeField] private EnemyAnimator _animator;
        [SerializeField] private float _attackCooldown;
        [SerializeField] private float _cleavage;
        [SerializeField] private float _effectiveDistance;
        [SerializeField] private float _damage;

        private Transform _heroTransform;
        private float _timeToNextAttack;
        private bool _isAtacking;
        private Collider[] _hits = new Collider[1];
        private int _layerMask;

        private const float HIT_STARTPOINT_Y_CORRECTION = 0.5f;

        public float AttackCooldown
        {
            get => _attackCooldown;
            set => _attackCooldown = value;
        }

        public float Cleavage
        {
            get => _cleavage;
            set => _cleavage = value;
        }

        public float EffectiveDistance
        {
            get => _effectiveDistance;
            set => _effectiveDistance = value;
        }

        public float Damage
        {
            get => _damage;
            set => _damage = value;
        }

        private bool CanAttack() =>
            _isHeroInZone && CooldownIsUp() && !_isAtacking;

        private bool CooldownIsUp() =>
            _timeToNextAttack <= 0;

        private void Awake()
        {
            _layerMask = 1 << LayerMask.NameToLayer(Constants.PLAYER_LAYERNAME);
        }

        private void Update()
        {
            if (CanAttack())
                StartAttack();
            else
                DecreaseTimeToNextAttack();
        }

        public void Construct(Transform transform) =>
            _heroTransform = transform;


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
                hit.transform.GetComponent<IHealth>()?.TakeDamage(_damage);
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
            new Vector3(transform.position.x, transform.position.y + HIT_STARTPOINT_Y_CORRECTION, transform.position.z) + HitDistance();

        private Vector3 HitDistance() => 
            transform.forward * _effectiveDistance;
    }
}