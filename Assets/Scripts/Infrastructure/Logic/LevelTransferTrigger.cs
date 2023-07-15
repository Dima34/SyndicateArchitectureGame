using Infrastructure.Services;
using Infrastructure.Services.LevelTransferService;
using UnityEngine;

namespace Infrastructure.Logic
{
    public class LevelTransferTrigger : MonoBehaviour
    {
        [SerializeField] private string _transferTo;
        [SerializeField] private BoxCollider _collider;
        private bool _used = false;

        
        private ILevelTransferService _levelTransferService;

        private void Awake() =>
            _levelTransferService = AllServices.Container.GetSingle<ILevelTransferService>();

        private void OnTriggerEnter(Collider other)
        {
            if (!_used)
            {
                _used = true;
                _levelTransferService.Transfer(_transferTo);
            }
        }

        private void OnDrawGizmos()
        {
            if(!_collider)
                return;

            Gizmos.color = new Color(255, 150, 30, 200);
            Gizmos.DrawCube(transform.position + _collider.center, _collider.size);
        }
    }
}