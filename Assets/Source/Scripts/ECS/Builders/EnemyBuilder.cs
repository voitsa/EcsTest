using ECS.Components;
using ECS.Components.Detection;
using ECS.Data;
using EntityActors;
using Leopotam.Ecs;
using UnityEngine;
using Utilitiy;

namespace Systems
{
    public class EnemyBuilder : UnitBuilder
    {
        private Transform _playerTarget;

        public EnemyBuilder(EcsWorld world, Transform playerTarget) : base(world)
        {
            _playerTarget = playerTarget;
        }

        protected override void SetupActor(UnitActorBuildData unitActorBuildData)
        {
            var ecsEntity = unitActorBuildData.Entity;
            var unitActor = unitActorBuildData.UnitActor;

            ref var targetableComponent = ref ecsEntity.Get<TargetableComponent>();
            targetableComponent.transform = unitActor.transform;
            targetableComponent.team = Teams.Enemy;

            ref var followComponent = ref ecsEntity.Get<FollowComponent>();
            followComponent.target = _playerTarget;
        }
    }
}