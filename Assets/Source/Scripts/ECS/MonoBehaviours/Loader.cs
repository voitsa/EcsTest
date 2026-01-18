using ECS.Components;
using ECS.Data;
using ECS.Systems;
using ECS.Systems.GameSystems;
using Leopotam.Ecs;
using Systems;
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
        private GameData _gameData;

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
        public void Construct(GameData gameData)
        {
            _gameData = gameData;
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
            _gameInitSystem = new GameInitSystem(this, _gameData, _world);
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
            _gameControlSystems.Add(new GameLostSystem());
            _gameControlSystems.Add(new GameLostViewSystem());
            _gameControlSystems.Add(new GameDestructionSystem());
            _gameControlSystems.Add(new GameRestartSystem());
        }

        private void SetFixedUpdateSystems()
        {
            _fixedUpdateSystems.Add(new PlayerMoveSystem());
            _fixedUpdateSystems.Add(new PlayerRotateSystem());
            _fixedUpdateSystems.Add(new MoveInputSystem());
            _fixedUpdateSystems.Add(new RotationInputSystem());
            _fixedUpdateSystems.Add(new ProjectileMoveSystem());
            _fixedUpdateSystems.Add(new FollowMoveSystem());
            _fixedUpdateSystems.Add(new FollowRotateSystem());
            _fixedUpdateSystems.Add(new RandomTurretRotationSystem());
            _fixedUpdateSystems.Add(new TrackingFollowSystem());
            _fixedUpdateSystems.Add(new TrackingSystem());
            _fixedUpdateSystems.Add(new ShootingSystem(_world));
            _fixedUpdateSystems.Add(new CollisionDamageSystem());
            _fixedUpdateSystems.Add(new ScorePickUpCollisionSystem());
            _fixedUpdateSystems.Add(new PickUpCollisionSystem());
            _fixedUpdateSystems.Add(new PickUpViewSystem());
            _fixedUpdateSystems.Add(new HealthDamageSystem());
            _fixedUpdateSystems.Add(new HealthViewSystem());
            _fixedUpdateSystems.Add(new ProjectileCollisionSystem());
            _fixedUpdateSystems.Add(new DestructionPickUpSpawnSystem(_world, _gameData.PickUpsInitConfig));
            _fixedUpdateSystems.Add(new CollisionComponentDestructionSystem());
            _fixedUpdateSystems.Add(new DestroySystem());
        }

        private void SetUpdateSystems()
        {
            _updateSystems.Add(_gameInitSystem);
            _updateSystems.Add(new DetectionSystem());
            _updateSystems.Add(new AutofireWeaponSystem());
            _updateSystems.Add(new RechargingSystem());
            _updateSystems.Add(new CameraFollowSystem());
            _updateSystems.Add(new PlayerFollowDamageSystem());
        }
    }
}