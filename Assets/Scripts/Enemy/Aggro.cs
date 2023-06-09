using System;
using System.Collections;
using UnityEngine;

namespace Enemy
{
    public class Aggro : MonoBehaviour
    {
        [SerializeField] private TriggerObserver _triggerObserver;
        [SerializeField] private Follow _follow;
        [SerializeField] private float _cooldown = 2f;
        
        private Coroutine _disableAggroCoroutine;

        private void Start()
        {
            _triggerObserver.TriggerEnter += TriggerEnter;
            _triggerObserver.TriggerExit += TriggerExit;

            SwitchFollowOFF();
        }

        private void TriggerEnter(Collider obj)
        {
            StopAggroCoroutineIfExist();   
            SwitchFollowON();
        }

        private void TriggerExit(Collider obj)
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
            Debug.Log("Un Aggro process started");
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