using System;
using System.Linq.Expressions;
using UnityEditor.Animations;
using UnityEngine;

namespace Enemy
{
    [RequireComponent(typeof(Animator))]
    public class EnemyAnimator : MonoBehaviour, IAnimationStateReader
    {
        private Animator _animator;

        private static readonly int Attack_01 = Animator.StringToHash("Attack_01");
        private static readonly int Speed = Animator.StringToHash("Speed");
        private static readonly int IsMoving = Animator.StringToHash("IsMoving");
        private static readonly int Hit = Animator.StringToHash("Hit");
        private static readonly int Die = Animator.StringToHash("Die");

        private readonly int _dieStateHash = Animator.StringToHash("die");
        private readonly int _idleStateHash = Animator.StringToHash("idle");
        private readonly int _attackStateHash = Animator.StringToHash("attack01");
        private readonly int _moveStateHash = Animator.StringToHash("Move");

        public event Action<AnimatorState> StateEntered;
        public event Action<AnimatorState> StateExited;

        public AnimatorState State { get; set; }

        private void Awake() =>
            _animator = GetComponent<Animator>();

        public void PlayDeath() =>
            _animator.SetTrigger(Die);

        public void PlayHit() =>
            _animator.SetTrigger(Hit);

        public void Move(float speed)
        {
            _animator.SetBool(IsMoving, true);
            _animator.SetFloat(Speed, speed);
        }

        public void StopMoving() =>
            _animator.SetBool(IsMoving, false);

        public void PlayAttack() =>
            _animator.SetTrigger(Attack_01);

        public void EnterState(int stateHash)
        {
            State = StateFor(stateHash);
            StateEntered?.Invoke(State);
        }

        public void ExitState(int stateHash)
        {
            AnimatorState stateToExit = StateFor(stateHash);
            StateExited?.Invoke(stateToExit);
        }

            
        private AnimatorState StateFor(int stateHash)
        {
            return new AnimatorState();
        }
    }
}
