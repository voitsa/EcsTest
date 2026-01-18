using ECS.Components;
using ECS.EntityActors;
using Leopotam.Ecs;
using Utility;

namespace ECS.Builders
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

            var actor = _pool.Get(weapon.shootingPoint.position, weapon.shootingPoint.rotation);
            var entity = _world.NewEntity();
            actor.GetComponent<ColliderObserver>().Initialize(_world, entity);
            actor.Initialize(_ => _pool.ReturnToPool(actor));
            entity.Get<CollisionDestructionComponent>();

            ref var destructionComponent = ref entity.Get<PoolDestructionComponent>();
            destructionComponent.poolable = actor;

            ref var movableComponent = ref entity.Get<MovableComponent>();
            movableComponent.moveSpeed = weapon.projectileConfig.Speed;
            movableComponent.transform = actor.transform;

            ref var damageInflictComponent = ref entity.Get<DamageInflictComponent>();
            damageInflictComponent.value = weapon.damageValue;

            entity.Get<ProjectileComponent>();
            entity.Get<CollisionDamageAllowComponent>();
        }
    }
}