using UnityEditor.Animations;

namespace Enemy
{
    public interface IAnimationStateReader
    {
        void EnterState(int stateHash);
        void ExitState(int stateHash);
        AnimatorState State { get; }
    }
}