using CompositeRoot;
using ECS.Data;
using ECS.Systems;
using ECS.Systems.GameSystems;
using Leopotam.Ecs;
using UnityEngine;
using Zenject;

namespace ECS
{
    public class Loader : MonoBehaviour
    {
        private EcsWorld _world;
        private EcsSystems _updateSystems;
        private EcsSystems _fixedUpdateSystems;
        private EcsSystems _gameControlSystems;
        private GameInitSystem _gameInitSystem;
        private EnemySpawnSystem _enemySpawnSystem;
        private GameData _gameData;
        private IGameState _gameState;
        private DiContainer _container;

        private void Start()
        {
            SetSystems();
        }

        private void Update()
        {
            _gameControlSystems.Run();
            _updateSystems?.Run();
        }

        private void FixedUpdate()
        {
            _fixedUpdateSystems?.Run();
        }

        private void OnDestroy()
        {
            _updateSystems?.Destroy();
            _world?.Destroy();
        }

        [Inject]
        public void Construct(GameData gameData, IGameState gameState, DiContainer container)
        {
            _gameData = gameData;
            _gameState = gameState;
            _container = container;
            Init();
        }

        public void Stop()
        {
            _updateSystems?.Destroy();
            _fixedUpdateSystems?.Destroy();
            _updateSystems = null;
            _fixedUpdateSystems = null;
        }

        public void Restart()
        {
            _gameControlSystems.Destroy();
            _gameControlSystems = null;
            _world?.Destroy();
            _world = null;
            Init();
            SetSystems();
        }

        private void Init()
        {
            _world = new EcsWorld();
            _gameControlSystems = new EcsSystems(_world);
            _updateSystems = new EcsSystems(_world);
            _fixedUpdateSystems = new EcsSystems(_world);
            _gameInitSystem = _container.Instantiate<GameInitSystem>(new object[] { this, _world });
            _enemySpawnSystem = _container.Instantiate<EnemySpawnSystem>(new object[] { _world });
        }

        private void SetSystems()
        {
            SetGameControlSystems();
            SetUpdateSystems();
            SetFixedUpdateSystems();
            _gameControlSystems.Init();
            _updateSystems.Init();
            _fixedUpdateSystems.Init();
        }

        private void SetGameControlSystems()
        {
            AddGameStateSystems();
        }

        private void SetFixedUpdateSystems()
        {
            AddMovementSystems();
            AddTrackingSystems();
            AddWeaponSystems();
            AddCollisionSystems();
            AddPickUpSystems();
            AddHealthSystems();
            AddDestructionSystems();
        }

        private void SetUpdateSystems()
        {
            AddInitAndSpawnSystems();
            AddRealtimeGameplaySystems();
        }

        private void AddGameStateSystems()
        {
            _gameControlSystems.Add(new GameLostSystem());
            _gameControlSystems.Add(new GameLostViewSystem());
            _gameControlSystems.Add(new GameDestructionSystem());
            _gameControlSystems.Add(new GameRestartSystem());
        }

        private void AddMovementSystems()
        {
            _fixedUpdateSystems.Add(new PlayerMoveSystem());
            _fixedUpdateSystems.Add(new PlayerRotateSystem());
            _fixedUpdateSystems.Add(new MoveInputSystem());
            _fixedUpdateSystems.Add(new RotationInputSystem());
            _fixedUpdateSystems.Add(new ProjectileMoveSystem());
            _fixedUpdateSystems.Add(new FollowMoveSystem());
            _fixedUpdateSystems.Add(new FollowRotateSystem());
            _fixedUpdateSystems.Add(new RandomTurretRotationSystem());
        }

        private void AddTrackingSystems()
        {
            _fixedUpdateSystems.Add(new TrackingFollowSystem());
            _fixedUpdateSystems.Add(new TrackingSystem());
        }

        private void AddWeaponSystems()
        {
            _fixedUpdateSystems.Add(new ShootingSystem());
        }

        private void AddCollisionSystems()
        {
            _fixedUpdateSystems.Add(new CollisionDamageSystem());
            _fixedUpdateSystems.Add(new ScorePickUpCollisionSystem());
            _fixedUpdateSystems.Add(new PickUpCollisionSystem());
        }

        private void AddPickUpSystems()
        {
            _fixedUpdateSystems.Add(new PickUpViewSystem());
        }

        private void AddHealthSystems()
        {
            _fixedUpdateSystems.Add(new HealthDamageSystem());
            _fixedUpdateSystems.Add(new HealthViewSystem());
        }

        private void AddDestructionSystems()
        {
            _fixedUpdateSystems.Add(new ProjectileCollisionSystem());
            _fixedUpdateSystems.Add(new DestructionPickUpSpawnSystem(_world, _gameData.PickUpsInitConfig));
            _fixedUpdateSystems.Add(new CollisionComponentDestructionSystem());
            _fixedUpdateSystems.Add(new PoolDestroySystem());
            _fixedUpdateSystems.Add(new DestroySystem());
        }

        private void AddInitAndSpawnSystems()
        {
            _updateSystems.Add(_gameInitSystem);
            _updateSystems.Add(_enemySpawnSystem);
        }

        private void AddRealtimeGameplaySystems()
        {
            _updateSystems.Add(new DetectionSystem());
            _updateSystems.Add(new AutofireWeaponSystem());
            _updateSystems.Add(new RechargingSystem());
            _updateSystems.Add(new PlayerFollowDamageSystem());
        }
    }
}