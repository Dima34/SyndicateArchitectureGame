using UnityEngine;

namespace StaticData
{
    [CreateAssetMenu(fileName = "HeroStaticData", menuName = "StaticData/Hero")]
    public class HeroStaticData : ScriptableObject
    {
        [Range(1,300)]
        public float MaxHp = 50f;
        
        [Range(1,300)]
        public float Damage = 5f;
        
        [Range(0.1f,10)]
        public float HitRadius = 2f;
        
        [Range(0.1f,10)]
        public float HitForwardOffset = 3f;

        [Range(1,10)]
        public int HitObjectPerHit = 3;
    }
}