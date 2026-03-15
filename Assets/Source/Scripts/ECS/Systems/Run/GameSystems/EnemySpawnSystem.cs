using ECS.Builders;
using ECS.Components;
using ECS.Data;
using Leopotam.Ecs;
using UnityEngine;
using Utility;

namespace ECS.Systems
{
    public class EnemySpawnSystem : IEcsRunSystem
    {
        private readonly EcsWorld _world;
        private readonly GameData _gameData;
        private readonly EnemyBuilderFactory _enemyBuilderFactory;
        private readonly Camera _camera;

        private readonly float _enemySpawnDelay;

        private readonly EcsFilter<PlayerTagComponent, TargetableComponent> _playerFilter;

        private EnemyBuilder _enemyBuilder;
        private float _timePassed;

        public EnemySpawnSystem(EcsWorld world, GameData gameData, EnemyBuilderFactory enemyBuilderFactory, Camera camera)
        {
            _world = world;
            _gameData = gameData;
            _enemyBuilderFactory = enemyBuilderFactory;
            _camera = camera;
            _enemySpawnDelay = gameData.EnemyData.SpawnDelay;
        }

        public void Run()
        {
            if (_playerFilter.IsEmpty())
                return;

            if (_enemyBuilder == null)
            {
                ref var playerTargetable = ref _playerFilter.Get2(0);
                _enemyBuilder = _enemyBuilderFactory.Create(_world, playerTargetable.transform);
            }

            _timePassed += Time.deltaTime;

            if (_timePassed < _enemySpawnDelay)
                return;

            _timePassed = 0f;

            var bound = _camera.GetWorldBound();
            var point = bound.GetRandomPointOnBorder();
            var position = new Vector3(point.x, 0f, point.y);
            _enemyBuilder.BuildUnit(_gameData.EnemyData.EnemyInitConfig, position);
        }
    }
}

