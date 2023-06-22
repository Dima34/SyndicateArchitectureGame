using System;
using UnityEngine;

namespace Logic
{
    public class TriggerZoneComponentToToggle : MonoBehaviour
    {
        // Just for visual debugging
        [SerializeField] protected bool _isHeroInZone;

        private const bool HERO_IN_ZONE_DEFAULT_VALUE = false; 
        
        private void Start() =>
            SetDefaultHeroInZoneValue();

        private void SetDefaultHeroInZoneValue() =>
            _isHeroInZone = HERO_IN_ZONE_DEFAULT_VALUE;

        public void Disable(GameObject collidedGameObject)
        {
            _isHeroInZone = false;
            OnHeroExited(collidedGameObject);
        }

        public void Enable(GameObject collidedGameObject)
        {
            _isHeroInZone = true;
             OnHeroEntered(collidedGameObject);
        }

        protected virtual void OnHeroExited(GameObject collidedGameObject) { }
        protected virtual void OnHeroEntered(GameObject collidedGameObject) { }
    }
}