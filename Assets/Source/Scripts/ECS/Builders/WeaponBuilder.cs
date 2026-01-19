using ECS.Components;
using ECS.Data;
using Leopotam.Ecs;
using UnityEngine;

namespace ECS.Builders
{
    public class WeaponBuilder : EcsBuilder
    {
        public WeaponBuilder(EcsWorld world) : base(world)
        {
        }

        public EcsEntity Build(WeaponInitConfig weaponInitConfig, Transform container,
            Vector2 position)
        {
            var actor = Object.Instantiate(weaponInitConfig.WeaponActor, container);
            actor.transform.position = position;
            var entity = _world.NewEntity();

            ref var weaponComponent = ref entity.Get<WeaponComponent>();
            weaponComponent.projectileConfig = weaponInitConfig.ProjectileInitConfig;
            weaponComponent.projectileBuilder = new ProjectileBuilder(_world);
            weaponComponent.shotDelay = weaponInitConfig.ShotDelay;
            weaponComponent.shootingPoint = actor.ShootPoint;
            weaponComponent.damageValue = weaponInitConfig.DamageValue;

            return entity;
        }
    }
}