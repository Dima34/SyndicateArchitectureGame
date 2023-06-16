using UnityEngine;

namespace Enemy
{
    public class Follow : MonoBehaviour
    {
        protected Transform _heroTransform;

        public void Construct(Transform heroTransform)
        {
            _heroTransform = heroTransform;
        }
    }
}