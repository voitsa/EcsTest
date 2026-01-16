using Data;
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

        private readonly UnitInitConfig _playerInitConfig;
        private readonly UnitInitConfig _enemyInitConfig;
        private readonly TurretInitConfig _turretInitConfig;
        private readonly UIData _uiData;

        private readonly float _spawnDelay;

        private TurretBuilder _turretBuilder;
        private EnemyBuilder _enemBuilder;
        private UnitActor _playerActor;
        private float _timePassed;
        private Camera _camera;

        public GameInitSystem(GameData gameData)
        {
            _playerInitConfig = gameData.PlayerInitConfig;
            _enemyInitConfig = gameData.EnemyInitConfig;
            _turretInitConfig = gameData.TurretInitConfig;
            _uiData = gameData.UIData;
            _spawnDelay = gameData.SpawnDelay;
        }

        public void Init()
        {
            _playerActor = CreatePlayer(new PlayerBuilder(_world, _uiData.PlayerView));
            _timePassed = 0f;
            _camera = Camera.main;
        }

        public void Run()
        {
            _timePassed += Time.deltaTime;

            if (_timePassed < _spawnDelay)
                return;

            _timePassed = 0f;
            var point = _camera.GetWorldBound().GetRandomPointOnBorder();
            Debug.Log(point);
            var position = new Vector3(point.x, 0f,  point.y);
            SpawnEnemy(_playerActor, position);
        }

        private void SpawnEnemy(UnitActor playerActor, Vector3 position)
        {
            var enemyBuilder = new EnemyBuilder(_world, _uiData.EnemyHealthView, playerActor.Transform, 20f);
            enemyBuilder.BuildUnit(_enemyInitConfig, position);
        }

        private UnitActor CreatePlayer(UnitBuilder builder)
        {
            var playerActor = builder.BuildUnit(_playerInitConfig, Vector3.zero);
            new TurretBuilder(_world).CreateTurret(_turretInitConfig, playerActor.Transform);
            return playerActor;
        }
    }
}