using ECS.Components;
using ECS.Data;
using Leopotam.Ecs;
using UnityEngine;
using Utility;

namespace ECS.Builders
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
            var entity = unitActorBuildData.Entity;
            var actor = unitActorBuildData.UnitActor;

            PlayerView _playerView = Object.Instantiate(_playerViewPrefab);
            _playerView.HealthView.Init(entity.Get<HealthComponent>().maxValue);

            ref var cameraComponent = ref entity.Get<CameraComponent>();
            cameraComponent.camera = Camera.main;

            ref var targetableComponent = ref entity.Get<TargetableComponent>();
            targetableComponent.transform = actor.transform;
            targetableComponent.team = Teams.Player;

            ref var scoreViewComponent = ref entity.Get<ScoreViewComponent>();
            scoreViewComponent.scoreView = _playerView.ScoreView;

            ref var healthViewComponent = ref entity.Get<HealthViewComponent>();
            healthViewComponent.healthView = _playerView.HealthView;

            entity.Get<PlayerTagComponent>();
            entity.Get<RotationInputEventComponent>();
            entity.Get<MoveInputEventComponent>();
            entity.Get<PlayerTagComponent>();
            entity.Get<ScoreComponent>();
        }
    }
}