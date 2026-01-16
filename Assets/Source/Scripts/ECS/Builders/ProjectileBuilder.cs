using ECS.Components;
using ECS.Components.Movement;
using ECS.Data;
using ECS.MonoBehaviours;
using EntityActors;
using Leopotam.Ecs;
using UnityEngine;
using Utilitiy;

namespace Systems
{
    public class ProjectileBuilder : EcsBuilder
    {
        private ProjectileActor _projectileActor;
        private EcsEntity _projectileEntity;

        public ProjectileBuilder(EcsWorld world) : base(world)
        {
        }

        public void Build(WeaponComponent weapon)
        {
            _projectileActor =
                Object.Instantiate(weapon.ProjectileConfig.ProjectilePrefab, weapon.shootingPoint.position, weapon.shootingPoint.rotation);
            _projectileEntity = _world.NewEntity();
            _projectileActor.GetComponent<ColliderObserver>().Initialize(_world, _projectileEntity);

            _projectileEntity.Get<CollisionDestructionComponent>();

            ref var projectileComponent = ref _projectileEntity.Get<ProjectileComponent>();
            projectileComponent.projectile = _projectileActor.gameObject;

            ref var movableComponent = ref _projectileEntity.Get<MovableComponent>();
            movableComponent.moveSpeed = weapon.ProjectileConfig.Speed;
            movableComponent.transform = _projectileActor.transform;

            ref var damageInflictComponent = ref _projectileEntity.Get<DamageInflictComponent>();
            damageInflictComponent.value = weapon.damageValue;

            _projectileEntity.Get<CollisionDamageAllowComponent>();
        }
    }
}