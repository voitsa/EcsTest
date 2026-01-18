using ECS;
using ECS.Data;
using EntityActors;
using Leopotam.Ecs;
using UnityEngine;
using Utility;

namespace Systems
{
    public class GameInitSystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly EcsWorld _world;
        private readonly Loader _loader;
        private readonly GameData _gameData;
        private readonly float _enemySpawnDelay;

        private GameBuilder _gameBuilder;
        private EnemyBuilder _enemyBuilder;
        private UnitActor _playerActor;
        private Camera _camera;
        private float _timePassed;

        public GameInitSystem(Loader loader, GameData gameData, EcsWorld world)
        {
            _loader = loader;
            _gameData = gameData;
            _world = world;
            _enemySpawnDelay = gameData.EnemyData.SpawnDelay;
        }

        public void Init()
        {
            _playerActor = CreatePlayer(new PlayerBuilder(_world, _gameData.UIData.PlayerView));
            _enemyBuilder = new EnemyBuilder(_world, _gameData.EnemyData, _gameData.UIData.EnemyHealthView, _playerActor.Transform);
            new GameBuilder(_world).Build(_gameData.GameActorPrefab, _loader);
            _timePassed = 0f;
            _camera = Camera.main;
        }

        public void Run()
        {
            _timePassed += Time.deltaTime;

            if (_timePassed < _enemySpawnDelay)
                return;

            _timePassed = 0f;
            var point = _camera.GetWorldBound().GetRandomPointOnBorder();
            Debug.Log(point);
            var position = new Vector3(point.x, 0f,  point.y);
            SpawnEnemy(position);
        }

        private void SpawnEnemy(Vector3 position)
        {
            _enemyBuilder.BuildUnit(_gameData.EnemyData.EnemyInitConfig, position);
        }

        private UnitActor CreatePlayer(UnitBuilder builder)
        {
            var playerActor = builder.BuildUnit(_gameData.PlayerInitConfig, Vector3.zero);
            new TurretBuilder(_world).CreateTurret(_gameData.TurretInitConfig, playerActor.Transform);
            return playerActor;
        }
    }
}