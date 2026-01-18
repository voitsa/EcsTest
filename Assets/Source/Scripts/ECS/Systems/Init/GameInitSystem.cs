using Data;
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

        private GameBuilder _gameBuilder;
        private UnitActor _playerActor;
        private float _timePassed;
        private Camera _camera;

        public GameInitSystem(Loader loader, GameData gameData, EcsWorld world)
        {
            _loader = loader;
            _gameData = gameData;
            _world = world;
        }

        public void Init()
        {
            _playerActor = CreatePlayer(new PlayerBuilder(_world, _gameData.UIData.PlayerView));
            new GameBuilder(_world).Build(_gameData.GameActorPrefab, _loader);
            _timePassed = 0f;
            _camera = Camera.main;
        }

        public void Run()
        {
            _timePassed += Time.deltaTime;

            if (_timePassed < _gameData.SpawnDelay)
                return;

            _timePassed = 0f;
            var point = _camera.GetWorldBound().GetRandomPointOnBorder();
            Debug.Log(point);
            var position = new Vector3(point.x, 0f,  point.y);
            SpawnEnemy(_playerActor, position);
        }

        private void SpawnEnemy(UnitActor playerActor, Vector3 position)
        {
            var enemyBuilder = new EnemyBuilder(_world, _gameData.UIData.EnemyHealthView, playerActor.Transform, 20f);
            enemyBuilder.BuildUnit(_gameData.EnemyInitConfig, position);
        }

        private UnitActor CreatePlayer(UnitBuilder builder)
        {
            var playerActor = builder.BuildUnit(_gameData.PlayerInitConfig, Vector3.zero);
            new TurretBuilder(_world).CreateTurret(_gameData.TurretInitConfig, playerActor.Transform);
            return playerActor;
        }
    }
}