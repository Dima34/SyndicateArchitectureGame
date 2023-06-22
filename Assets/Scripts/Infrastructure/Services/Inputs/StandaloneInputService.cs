using UnityEngine;

namespace Services.Inputs
{
    public class StandaloneInputService : InputService
    {
        public override Vector2 Axis => GetActiveInputAxis();
        public override bool IsAttackButtonUp() => IsSimpleInputFireClicked() || IsUnityFireButtonCliked();

        private Vector2 GetActiveInputAxis()
        {
            var axis = GetSimpleInputAxis();

            if (axis == Vector2.zero)
                axis = GetUnityAxis();

            return axis;
        }
    }
}