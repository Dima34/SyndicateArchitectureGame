using Infrastructure.Data;
using Infrastructure.Services;
using Infrastructure.Services.PersistantProgress;
using Services.Inputs;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Player
{
    [RequireComponent(typeof(CharacterController))]
    public class HeroMove : MonoBehaviour, ISavedProgressWriter, ISavedProgressReader
    {
        [SerializeField] private float _movementSpeed = 2f;

        private CharacterController _characterController;
        private IInputService _inputService;
        private Camera _camera;

        private void Awake()
        {
            _inputService = AllServices.Container.GetSingle<IInputService>();
            _characterController = GetComponent<CharacterController>();
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

        public void UpdateProgress(PlayerProgress progress)
        {
            progress.WorldData.PositionOnLevel = new PositionOnLevel(
                CurrentLevelName(),
                transform.position.AsVectorData());
        }

        private string CurrentLevelName() =>
            SceneManager.GetActiveScene().name;

        public void LoadProgress(PlayerProgress progress)
        {
            var savedLevelName = progress.WorldData.PositionOnLevel.LevelName;

            if (CurrentLevelName() == savedLevelName)
            {
                Vector3Data savedPosition = progress.WorldData.PositionOnLevel.Position;
                
                if (progress.WorldData.PositionOnLevel.Position != null) 
                    Warp(savedPosition.AsUnityVector());
            }
        }

        private void Warp(Vector3 to)
        {
            // Enabling\disabling characterController because it dont like when we move his parent through transform.
            // He can stuck somewhere, so we disable it for that operation
            _characterController.enabled = false;
            transform.position = to;
            _characterController.enabled = true;
        }
    }
}