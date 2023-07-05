using System;
using Infrastructure.Data;
using Infrastructure.Services.PersistantProgress;
using Infrastructure.States;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Windows
{
    public abstract class WindowBase : MonoBehaviour
    {
        [SerializeField] private Button _closeButton;

        private IPersistantProgressService _progressService;
        private IGameProcessService _processService;
        
        protected IPersistantProgressService ProgressService
        {
            get => _progressService;
            set => _progressService = value;
        }

        public Progress Progress => _progressService.Progress;

        private void Awake() =>
            OnAwake();

        private void Start()
        {
            Initialize();
            PauseGame();
            SubscribeUpdates();
        }

        private void PauseGame() =>
            _processService.PauseGame();

        private void OnDestroy()
        {
            ResumeGame();
            Cleanup();
        }

        protected virtual void OnAwake() =>
            _closeButton.onClick.AddListener(() => Destroy(gameObject));

        public void Construct(IPersistantProgressService progressService, IGameProcessService processService)
        {
            _progressService = progressService;
            _processService = processService;
        }

        protected virtual void Initialize()
        {
            _processService.PauseGame();
        }

        protected virtual void SubscribeUpdates()
        {
        }

        private void ResumeGame() =>
            _processService.ResumeGame();

        protected virtual void Cleanup()
        {
        }
    }
}