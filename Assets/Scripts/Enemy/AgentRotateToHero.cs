using UnityEngine;

namespace Enemy
{
    public class AgentRotateToHero : Follow
    {
        [SerializeField] private float _speed;
        
        private Vector3 _directionToLook;
        
        private void Update()
        {
            if (HeroAssigned()) 
                RotateTowardsHero();
        }

        private bool HeroAssigned() =>
            _heroTransform != null;

        private void RotateTowardsHero()
        {
            UpdateDirectionToLook();

            transform.rotation = SmoothedRotation(CurrentRotation(), _directionToLook);
        }

        private Quaternion CurrentRotation() =>
            transform.rotation;

        private void UpdateDirectionToLook()
        {
            Vector3 heroDirection = _heroTransform.position - transform.position;
            _directionToLook = new Vector3(heroDirection.x, transform.position.y, heroDirection.z);
        }

        private Quaternion SmoothedRotation(Quaternion rotation, Vector3 positionToLook)
        {
            Quaternion targetRotation = GetTargetRotation(positionToLook);
            float speedFactor = GetSpeedFactor();
            
            return Quaternion.Lerp(rotation, targetRotation, speedFactor);
        }

        private Quaternion GetTargetRotation(Vector3 directionToLook) =>
            Quaternion.LookRotation(directionToLook);

        private float GetSpeedFactor() =>
            _speed * Time.deltaTime;
    }
}