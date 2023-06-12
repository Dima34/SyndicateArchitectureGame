using UnityEngine;

namespace Logic
{
    public class TriggerZoneObserver : MonoBehaviour
    {
        [SerializeField] private TriggerZoneComponentToToggle _componentToToggle;
        [SerializeField] private TriggerZone triggerZone;

        private void Start()
        {
            triggerZone.ZoneEnter += ZoneEnter;
            triggerZone.ZoneExit += ZoneExit;
        }

        private void ZoneEnter(Collider collider) =>
            _componentToToggle.Enable(collider.gameObject);

        private void ZoneExit(Collider collider) =>
            _componentToToggle.Disable(collider.gameObject);
    }
}