using Services.Inputs;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerMove : MonoBehaviour
    {
        [SerializeField] private CharacterController _characterController;
        [SerializeField] private float _movementSpeed = 2f;

        private IInputService _inputService;
        private Camera _camera;

        private void Awake()
        {
            _inputService = Game.InputService;
        }

        private void Start()
        {
            _camera = Camera.main;
        }

        private void Update()
        {
            Vector3 movementVector = Vector3.zero;

            if (_inputService.Axis.magnitude > 0)
            {
                movementVector = GetNewMovementVector();
                SetPlayerForwardDirection(movementVector);
            }

            MovePlayer(movementVector);
            ApplyGravity();
        }

        private Vector3 GetNewMovementVector()
        {
            Vector3 movementVector = _camera.transform.TransformDirection(_inputService.Axis);
            movementVector.Normalize();
            movementVector.y = 0;
            return movementVector;
        }

        private void SetPlayerForwardDirection(Vector3 movementVector)
        {
            transform.forward = movementVector;
        }

        private void MovePlayer(Vector3 movementVector)
        {
            Vector3 newPlayerPosition = _movementSpeed * movementVector * Time.deltaTime;

            _characterController.Move(newPlayerPosition);
        }

        void ApplyGravity()
        {
            _characterController.Move(Physics.gravity);
        }
    }
}