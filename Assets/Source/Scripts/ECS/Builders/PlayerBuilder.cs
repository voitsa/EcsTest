using ECS;
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
        private readonly PlayerView _playerViewPrefab;

        public PlayerBuilder(EcsWorld world, PlayerView playerViewPrefab) : base(world)
        {
            _playerViewPrefab = playerViewPrefab;
        }

        protected override void SetupActor(UnitActorBuildData unitActorBuildData)
        {
            var ecsEntity = unitActorBuildData.Entity;
            var unitActor = unitActorBuildData.UnitActor;

            PlayerView _playerView = Object.Instantiate(_playerViewPrefab);
            _playerView.HealthView.Init(ecsEntity.Get<HealthComponent>().maxValue);

            ref var cameraComponent = ref ecsEntity.Get<CameraComponent>();
            cameraComponent.camera = Camera.main;
            cameraComponent.defaultPosition = Camera.main.transform.position;
            cameraComponent.distanceRate = 2f;

            ref var targetableComponent = ref ecsEntity.Get<TargetableComponent>();
            targetableComponent.transform = unitActor.transform;
            targetableComponent.team = Teams.Player;

            ref var scoreViewComponent = ref ecsEntity.Get<ScoreViewComponent>();
            scoreViewComponent.scoreView = _playerView.ScoreView;

            ref var healthViewComponent = ref ecsEntity.Get<HealthViewComponent>();
            healthViewComponent.healthView = _playerView.HealthView;

            ecsEntity.Get<PlayerTagComponent>();
            ecsEntity.Get<RotationInputEventComponent>();
            ecsEntity.Get<MoveInputEventComponent>();
            ecsEntity.Get<PlayerTagComponent>();
            ecsEntity.Get<ScoreComponent>();
        }
    }
}