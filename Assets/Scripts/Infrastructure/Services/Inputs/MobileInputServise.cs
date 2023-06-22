using UnityEngine;

namespace Services.Inputs
{
    public class MobileInputServise : InputService
    {
        public override Vector2 Axis => GetSimpleInputAxis();
        public override bool IsAttackButtonUp() => IsUnityFireButtonCliked();
    }
}