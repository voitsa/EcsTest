using ECS.Data;
using ECS.Systems;
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
        private EcsSystems _lateUpdateSystems;
        private GameInitSystem _gameInitSystem;
        private GameData _gameData;

        [Inject]
        public void Construct(EcsWorld world, GameData gameData)
        {
            _world = world;
            _updateSystems = new EcsSystems(world);
            _fixedUpdateSystems = new EcsSystems(world);
            _gameData = gameData;
            _gameInitSystem = new GameInitSystem(_gameData);
        }

        private void Start()
        {
            _updateSystems.Add(_gameInitSystem);
            _updateSystems.Add(new DetectionSystem());
            _updateSystems.Add(new AutofireWeaponSystem());
            _updateSystems.Add(new RechargingSystem());
            _updateSystems.Add(new CameraFollowSystem());
            _updateSystems.Add(new PlayerFollowDamageSystem());

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

            _updateSystems.Init();
            _fixedUpdateSystems.Init();
        }

        private void Update()
        {
            _updateSystems.Run();
        }

        private void FixedUpdate()
        {
            _fixedUpdateSystems.Run();
        }

        private void OnDestroy()
        {
            _updateSystems?.Destroy();
            _world?.Destroy();
        }
    }
}