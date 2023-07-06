using Enemy;
using Infrastructure;
using Infrastructure.Data;
using Infrastructure.Services.PersistantProgress;
using Logic;
using Services.Inputs;
using UnityEngine;

namespace Hero
{
    [RequireComponent(typeof(CharacterController))]
    [RequireComponent(typeof(HeroAnimator))]
    public class HeroAttack : MonoBehaviour, ISavedProgressReader
    {
        [SerializeField] private HeroAnimator _heroAnimator;
        
        private IInputService _inputService;
        private int _layerMask;
        // Physics.overlap dont recreate hit array. It will find as many object as array lenght
        private Collider[] _hits;
        private int _attackDamage;
        private HeroStats _heroStats;

        private void Awake() =>
            _layerMask = 1 << LayerMask.NameToLayer(Constants.HIITBLE_LAYERNAME);

        public void Construct(IInputService inputService) =>
            _inputService = inputService;

        private void Update()
        {
            if (_inputService.IsAttackButtonUp())
                Attack();
        }

        public void LoadProgress(Progress progress)
        {
            _heroStats = progress.HeroStats;
            _hits = new Collider[_heroStats.HitObjectPerHit];
        }

        private void Attack() =>
            _heroAnimator.PlayAttack();

        private void OnAttack()
        {
            for (int i = 0; i < Hit(); i++)
            {
                var hitObjectHealth = _hits[i].transform.parent.GetComponent<IHealth>();
                hitObjectHealth?.TakeDamage(_heroStats.Damage);
            }
        }

        private int Hit()
        {
#if UNITY_EDITOR
            PhysicsDebug.DebugLineSpere(AttackStartPos(), _heroStats.HitRadius, 0.5f);
#endif
            return Physics.OverlapSphereNonAlloc(AttackStartPos(), _heroStats.HitRadius, _hits, _layerMask);
        }

        private Vector3 AttackStartPos()
        {
            var forwardHitOffset = _heroStats.HitForwardOffset * transform.forward;
            return new Vector3(transform.position.x, transform.position.y, transform.position.z) + forwardHitOffset;
        }
    }
}