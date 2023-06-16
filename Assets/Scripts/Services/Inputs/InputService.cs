using UnityEngine;

namespace Services.Inputs
{
    public abstract class InputService : IInputService
    {
        protected const string FIRE_BUTTON = "Fire";
        protected const string HORIZONTAL_AXIS = "Horizontal";
        protected const string VERTICAL_AXIS = "Vertical";

        public abstract Vector2 Axis { get; }

        public abstract bool IsAttackButtonUp();

        protected static bool IsUnityFireButtonCliked() =>
            Input.GetAxis(FIRE_BUTTON) > 0;

        protected static bool IsSimpleInputFireClicked() =>
            SimpleInput.GetButtonUp(FIRE_BUTTON);

        protected static Vector2 GetUnityAxis() =>
            new Vector2(Input.GetAxis(HORIZONTAL_AXIS), Input.GetAxis(VERTICAL_AXIS));

        protected Vector2 GetSimpleInputAxis() =>
            new Vector2(SimpleInput.GetAxis(HORIZONTAL_AXIS), SimpleInput.GetAxis(VERTICAL_AXIS));
    }
    
}