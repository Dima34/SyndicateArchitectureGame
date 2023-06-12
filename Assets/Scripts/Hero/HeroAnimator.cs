using System;
using UnityEditor.Animations;
using UnityEngine;

namespace Enemy
{
    [RequireComponent(typeof(Animator))]
    public class HeroAnimator : MonoBehaviour, IAnimationStateReader
    {
        private Animator _animator;

        private static readonly int Attack = Animator.StringToHash("Attack");
        private static readonly int Speed = Animator.StringToHash("Speed");
        private static readonly int IsMoving = Animator.StringToHash("IsMoving");

        private readonly int _idleStateHash = Animator.StringToHash("Idle");
        private readonly int _attackStateHash = Animator.StringToHash("Attack");
        private readonly int _moveStateHash = Animator.StringToHash("Move");

        public event Action<AnimatorState> StateEntered;
        public event Action<AnimatorState> StateExited;

        public AnimatorState State { get; set; }

        private void Awake() =>
            _animator = GetComponent<Animator>();

        public void Move(float speed)
        {
            _animator.SetBool(IsMoving, true);
            _animator.SetFloat(Speed, speed);
        }

        public void StopMoving() =>
            _animator.SetBool(IsMoving, false);

        public void PlayAttack() =>
            _animator.SetTrigger(Attack);

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
