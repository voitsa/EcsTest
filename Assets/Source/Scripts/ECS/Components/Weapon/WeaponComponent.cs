using ECS.Data;
using EntityActors;
using Systems;
using UnityEngine;

namespace ECS.Components
{
    public struct WeaponComponent
    {
        public ProjectileBuilder projectileBuilder;
        public ProjectileInitConfig projectileConfig;
        public Transform shootingPoint;
        public float shotDelay;
        public float damageValue;
    }
}