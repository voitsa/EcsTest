using ECS;
using ECS.Components;
using ECS.Components.Detection;
using ECS.Data;
using Leopotam.Ecs;
using UnityEngine;
using Utilitiy;

namespace Systems
{
    public class EnemyBuilder : UnitBuilder
    {
        private readonly Transform _playerTarget;
        private readonly EnemyHealthView _enemyHealthViewPrefab;
        private readonly float _stopDistance;

        public EnemyBuilder(EcsWorld world, EnemyHealthView enemyHealthViewPrefab, Transform playerTarget, float stopDistance) : base(world)
        {
            _stopDistance = stopDistance;
            _playerTarget = playerTarget;
            _enemyHealthViewPrefab = enemyHealthViewPrefab;
        }

        protected override void SetupActor(UnitActorBuildData unitActorBuildData)
        {
            var ecsEntity = unitActorBuildData.Entity;
            var unitActor = unitActorBuildData.UnitActor;
            var actorTransform = unitActor.transform;

            ref var targetableComponent = ref ecsEntity.Get<TargetableComponent>();
            targetableComponent.transform = actorTransform;
            targetableComponent.team = Teams.Enemy;

            ref var followComponent = ref ecsEntity.Get<FollowComponent>();
            followComponent.target = _playerTarget;
            followComponent.stopDistance = _stopDistance;

            ref var pickUpSpawnerTagComponent = ref ecsEntity.Get<PickUpSpawnerTagComponent>();
            pickUpSpawnerTagComponent.spawnPoint = actorTransform;

            var healthView = Object.Instantiate(_enemyHealthViewPrefab, actorTransform);

            ref var healthViewComponent = ref ecsEntity.Get<HealthViewComponent>();
            healthViewComponent.healthView = healthView;

            ref var damageInflictComponent = ref ecsEntity.Get<DamageInflictComponent>();
            damageInflictComponent.value = 20f;
        }
    }
}