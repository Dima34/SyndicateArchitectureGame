using System;
using Infrastructure.Data;
using Infrastructure.Services.PersistantProgress;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Windows
{
    public abstract class WindowBase : MonoBehaviour
    {
        [SerializeField] private Button _closeButton;

        private IPersistantProgressService _progressService;

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
            SubscribeUpdates();
        }

        private void OnDestroy() =>
            Cleanup();

        protected virtual void OnAwake() =>
            _closeButton.onClick.AddListener(() => Destroy(gameObject));

        public void Construct(IPersistantProgressService progressService) =>
            _progressService = progressService;

        protected virtual void Initialize()
        {
        }

        protected virtual void SubscribeUpdates()
        {
        }

        protected virtual void Cleanup()
        {
        }
    }
}