using UnityEngine;

namespace _Project.Logic.StaticData
{
    [CreateAssetMenu(fileName = "HeroData", menuName = "StaticData/Hero")]
    public class HeroStaticData : ScriptableObject
    {
        [Range(30f, 150f)]
        public float Hp;

        [Range(1, 15)]
        public float Damage;

        [Range(1f, 3f)]
        public float DamageRadius;
    }
}