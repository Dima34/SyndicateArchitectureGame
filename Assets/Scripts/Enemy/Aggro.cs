using System.Collections;
using Logic;
using UnityEngine;

namespace Enemy
{
    public class Aggro : MonoBehaviour
    {
        [SerializeField] private TriggerZone _triggerZone;
        [SerializeField] private Follow _follow;
        [SerializeField] private float _cooldown = 2f;
        
        private Coroutine _disableAggroCoroutine;

        private void Start()
        {
            _triggerZone.ZoneEnter += ZoneEnter;
            _triggerZone.ZoneExit += ZoneExit;

            SwitchFollowOFF();
        }

        private void ZoneEnter(Collider obj)
        {
            StopAggroCoroutineIfExist();   
            SwitchFollowON();
        }

        private void ZoneExit(Collider obj)
        {
            if(_disableAggroCoroutine == null)
                _disableAggroCoroutine = StartCoroutine(SwitchAgroOffAfterCooldown());
        }

        private void StopAggroCoroutineIfExist()
        {
            TryBreakCoroutine(ref _disableAggroCoroutine);
        }

        private void TryBreakCoroutine(ref Coroutine coroutineId)
        {
            if (coroutineId == null)
                return;
                
            StopCoroutine(coroutineId);
            coroutineId = null;
        }

        private IEnumerator SwitchAgroOffAfterCooldown()
        {
            float timer = 0;

            yield return new WaitForSeconds(_cooldown);
            
            SwitchFollowOFF();
        }

        public void SwitchFollowON() =>
            _follow.enabled = true;
        
        public void SwitchFollowOFF() =>
            _follow.enabled = false;
    }
}