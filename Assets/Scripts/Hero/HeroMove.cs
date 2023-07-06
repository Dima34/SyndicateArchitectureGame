using Infrastructure.Data;
using Infrastructure.Services;
using Infrastructure.Services.PersistantProgress;
using Services.Inputs;
using UnityEngine;

namespace Hero
{
    [RequireComponent(typeof(CharacterController))]
    [RequireComponent(typeof(HeroAnimator))]
    public class HeroMove : MonoBehaviour, ISavedProgressWriter, ISavedProgressReader
    {
        [SerializeField] private float _movementSpeed = 2f;

        private CharacterController _characterController;
        private IInputService _inputService;
        private Camera _camera;
        private HeroAnimator _heroAnimator;

        private void Awake()
        {
            _inputService = AllServices.Container.GetSingle<IInputService>();
            _characterController = GetComponent<CharacterController>();
            _heroAnimator = GetComponent<HeroAnimator>();
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
            AnimatePlayer(movementVector);
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

        private void AnimatePlayer(Vector3 movementVector)
        {
            if (movementVector.magnitude > 0)
                _heroAnimator.Move(movementVector.magnitude);
            else
                _heroAnimator.StopMoving();
        }

        void ApplyGravity()
        {
            _characterController.Move(Physics.gravity);
        }

        public void UpdateProgress(Progress progress)
        {
            var currentLevelData = progress.WorldData.GetCurrentLevelData();
            currentLevelData.PositionOnLevel = transform.position.AsVectorData();
        }

        public void LoadProgress(Progress progress)
        {
            Vector3Data positionOnLevel = progress.WorldData.GetCurrentLevelData()?.PositionOnLevel;

            if (positionOnLevel != null)
                Warp(positionOnLevel.AsUnityVector());
        }

        private void Warp(Vector3 newPosition)
        {
            // Enabling\disabling characterController because it dont like when we move his parent through transform.
            // He can stuck somewhere, so we disable it for that operation
            _characterController.enabled = false;
            var saveVector = new Vector3(0, _characterController.height, 0);
            transform.position = newPosition.AddVector(saveVector);
            _characterController.enabled = true;
        }
    }
}