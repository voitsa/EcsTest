using ECS.Components;
using ECS.Data;
using Leopotam.Ecs;
using UnityEngine;
using Utility;

namespace ECS.Builders
{
    public class EnemyBuilder : UnitBuilder
    {
        private readonly Transform _playerTarget;
        private readonly EnemyHealthView _enemyHealthViewPrefab;
        private readonly EnemyData _enemyData;

        public EnemyBuilder(EcsWorld world, EnemyData enemyData, EnemyHealthView enemyHealthViewPrefab, Transform playerTarget) : base(world)
        {
            _enemyData = enemyData;
            _playerTarget = playerTarget;
            _enemyHealthViewPrefab = enemyHealthViewPrefab;
        }

        protected override void SetupActor(UnitActorBuildData unitActorBuildData)
        {
            var entity = unitActorBuildData.Entity;
            var actor = unitActorBuildData.UnitActor;
            var actorTransform = actor.transform;

            ref var targetableComponent = ref entity.Get<TargetableComponent>();
            targetableComponent.transform = actorTransform;
            targetableComponent.team = Teams.Enemy;

            ref var followComponent = ref entity.Get<FollowComponent>();
            followComponent.target = _playerTarget;
            followComponent.stopDistance = _enemyData.StopDistance;

            ref var pickUpSpawnerTagComponent = ref entity.Get<PickUpSpawnerTagComponent>();
            pickUpSpawnerTagComponent.spawnPoint = actorTransform;

            var healthView = Object.Instantiate(_enemyHealthViewPrefab, actorTransform);
            healthView.Init(entity.Get<HealthComponent>().maxValue);

            ref var healthViewComponent = ref entity.Get<HealthViewComponent>();
            healthViewComponent.healthView = healthView;

            ref var damageInflictComponent = ref entity.Get<DamageInflictComponent>();
            damageInflictComponent.value = _enemyData.DamageValue;

            ref var meleeWeaponComponent = ref entity.Get<MeleeWeaponComponent>();
            meleeWeaponComponent.attackDelay = _enemyData.AttackDelay;
        }
    }
}