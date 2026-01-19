using ECS.Components;
using ECS.Data;
using ECS.EntityActors;
using Leopotam.Ecs;
using UnityEngine;

namespace ECS.Builders
{
    public abstract class UnitBuilder : EcsBuilder
    {
        public UnitBuilder(EcsWorld world) : base(world)
        {
        }

        public UnitActor BuildUnit(UnitInitConfig unitInitConfig, Vector3 spawnPoint)
        {
            UnitActorBuildData unitActorBuildData = new UnitActorBuildData();
            unitActorBuildData.UnitActor = Object.Instantiate(unitInitConfig.UnitPrefab, spawnPoint, Quaternion.identity);
            unitActorBuildData.Entity = _world.NewEntity();

            var entity = unitActorBuildData.Entity;
            var actor = unitActorBuildData.UnitActor;
            var actorGameObject = actor.gameObject;

            ref var destructionComponent = ref entity.Get<DestructionComponent>();
            destructionComponent.destroyObject = actorGameObject;

            entity.Get<CollisionDestructionComponent>();

            var colliderObserver = actor.GetComponent<ColliderObserver>();
            colliderObserver.Initialize(_world, entity);

            var transform = actor.transform;

            ref var movableComponent = ref entity.Get<MovableComponent>();
            movableComponent.transform = actor.transform;
            movableComponent.moveSpeed = unitInitConfig.DefaultSpeed;

            ref var rotatableComponent = ref entity.Get<RotatableComponent>();
            rotatableComponent.transform = transform;

            ref var healthComponent = ref entity.Get<HealthComponent>();
            healthComponent.maxValue = unitInitConfig.HealthValue;
            healthComponent.currentValue = unitInitConfig.HealthValue;

            SetupActor(unitActorBuildData);

            return unitActorBuildData.UnitActor;
        }

        protected abstract void SetupActor(UnitActorBuildData unitActorBuildData);
    }
}