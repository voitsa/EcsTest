using ECS.Components;
using ECS.Components.Movement;
using ECS.MonoBehaviours;
using EntityActors;
using Leopotam.Ecs;
using Utility;

namespace Systems
{
    public class ProjectileBuilder : EcsBuilder
    {
        private ObjectPool<ProjectileActor> _pool;

        public ProjectileBuilder(EcsWorld world) : base(world)
        {
        }

        public void Build(WeaponComponent weapon)
        {
            if (_pool == null)
                _pool = new ObjectPool<ProjectileActor>(weapon.projectileConfig.ProjectilePrefab);

            var projectileActor = _pool.Get(weapon.shootingPoint.position, weapon.shootingPoint.rotation);
            var projectileEntity = _world.NewEntity();
            projectileActor.GetComponent<ColliderObserver>().Initialize(_world, projectileEntity);
            projectileActor.Initialize(_ => _pool.ReturnToPool(projectileActor));
            projectileEntity.Get<CollisionDestructionComponent>();

            ref var destructionComponent = ref projectileEntity.Get<PoolDestructionComponent>();
            destructionComponent.poolable = projectileActor;

            ref var projectileComponent = ref projectileEntity.Get<ProjectileComponent>();
            projectileComponent.projectile = projectileActor.gameObject;

            ref var movableComponent = ref projectileEntity.Get<MovableComponent>();
            movableComponent.moveSpeed = weapon.projectileConfig.Speed;
            movableComponent.transform = projectileActor.transform;

            ref var damageInflictComponent = ref projectileEntity.Get<DamageInflictComponent>();
            damageInflictComponent.value = weapon.damageValue;

            projectileEntity.Get<CollisionDamageAllowComponent>();
        }
    }
}