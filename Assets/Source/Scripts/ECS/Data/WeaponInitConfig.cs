using ECS.EntityActors;
using UnityEngine;

namespace ECS.Data
{
    [CreateAssetMenu(menuName = "WeaponInitData")]
    public class WeaponInitConfig : ScriptableObject
    {
        [field: SerializeField] public ProjectileInitConfig ProjectileInitConfig { get; private set; }
        [field: SerializeField] public WeaponActor WeaponActor { get; private set; }
        [field: SerializeField] public float ShotDelay { get; private set; }
        [field: SerializeField] public float DamageValue { get; private set; }
    }
}