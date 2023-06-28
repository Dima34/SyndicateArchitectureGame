using UnityEngine;

namespace Enemy
{
    public interface IAnimationStateReader
    {
        void EnterState(int stateHash);
        void ExitState(int stateHash);
        AnimatorStateInfo State { get; }
    }
}