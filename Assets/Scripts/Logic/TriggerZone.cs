using System;
using UnityEngine;

namespace Logic
{
    [RequireComponent(typeof(Collider))]
    public class TriggerZone : MonoBehaviour
    {
        public event Action<Collider> ZoneEnter;
        public event Action<Collider> ZoneExit;
        
        private void OnTriggerEnter(Collider collider) =>
            ZoneEnter?.Invoke(collider);

        private void OnTriggerExit(Collider collider) =>
            ZoneExit?.Invoke(collider);
    }
}