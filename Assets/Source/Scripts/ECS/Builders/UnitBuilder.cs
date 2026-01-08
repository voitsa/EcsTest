using ECS.Components;
using ECS.Components.Movement;
using ECS.Data;
using ECS.MonoBehaviours;
using EntityActors;
using Leopotam.Ecs;
using UnityEngine;

namespace Systems
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

            var ecsEntity = unitActorBuildData.Entity;
            var unitActor = unitActorBuildData.UnitActor;

            ecsEntity.Get<CollisionDestructionComponent>();

            var colliderObserver = unitActor.GetComponent<ColliderObserver>();
            colliderObserver.Initialize(_world, ecsEntity);

            var transform = unitActor.transform;

            ref var movableComponent = ref ecsEntity.Get<MovableComponent>();
            movableComponent.transform = unitActor.transform;
            movableComponent.moveSpeed = unitInitConfig.DefaultSpeed;

            ref var rotatableComponent = ref ecsEntity.Get<RotatableComponent>();
            rotatableComponent.transform = transform;

            ref var healthComponent = ref ecsEntity.Get<HealthComponent>();
            healthComponent.maxValue = unitInitConfig.HealthValue;
            healthComponent.currentValue = unitInitConfig.HealthValue;
            healthComponent.unit = unitActor.gameObject;

            SetupActor(unitActorBuildData);

            return unitActorBuildData.UnitActor;
        }

        protected abstract void SetupActor(UnitActorBuildData unitActorBuildData);
    }
}