using ECS.Components;
using ECS.Components.Detection;
using ECS.Components.Input;
using ECS.Data;
using Leopotam.Ecs;
using UnityEngine;
using Utilitiy;

namespace Systems
{
    public class PlayerBuilder : UnitBuilder
    {
        public PlayerBuilder(EcsWorld world) : base(world)
        {
        }

        protected override void SetupActor(UnitActorBuildData unitActorBuildData)
        {
            var ecsEntity = unitActorBuildData.Entity;
            var unitActor = unitActorBuildData.UnitActor;

            ref var cameraComponent = ref ecsEntity.Get<CameraComponent>();
            cameraComponent.camera = Camera.main;
            cameraComponent.defaultPosition = Camera.main.transform.position;
            cameraComponent.distanceRate = 2f;

            ref var targetableComponent = ref ecsEntity.Get<TargetableComponent>();
            targetableComponent.transform = unitActor.transform;
            targetableComponent.team = Teams.Player;

            ecsEntity.Get<PlayerTagComponent>();
            ecsEntity.Get<RotationInputEventComponent>();
            ecsEntity.Get<MoveInputEventComponent>();
            ecsEntity.Get<PlayerTagComponent>();
        }
    }
}