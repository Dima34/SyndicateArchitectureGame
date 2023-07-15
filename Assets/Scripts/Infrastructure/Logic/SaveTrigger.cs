using Infrastructure.Services;
using Infrastructure.Services.SaveLoad;
using UnityEngine;

namespace Logic
{
    [RequireComponent(typeof(BoxCollider))]
    public class SaveTrigger : MonoBehaviour
    {
        private ISaveLoadService _saveloadService;
        [SerializeField] private BoxCollider _collider;

        private void Awake()
        {
            _saveloadService = AllServices.Container.GetSingle<ISaveLoadService>();
        }

        private void OnTriggerEnter(Collider other)
        {
            _saveloadService.UpdateAndSaveProgress();
            Debug.LogWarning("Progress saved!");
            gameObject.SetActive(false);
        }

        private void OnDrawGizmos()
        {
            if(!_collider)
                return;

            Gizmos.color = new Color(30, 200, 30, 130);
            Gizmos.DrawCube(transform.position + _collider.center, _collider.size);
        }
    }
}