using System;
using Data;
using ECS.Data;
using Leopotam.Ecs;
using Systems;
using UnityEngine;
using UnityEngine.Serialization;
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

        [Inject]
        public void Construct(EcsWorld world, GameInitSystem gameInitSystem)
        {
            _world = world;
            _updateSystems = new EcsSystems(world);
            _fixedUpdateSystems = new EcsSystems(world);
            _gameInitSystem = gameInitSystem;
        }

        private void Start()
        {
            _updateSystems.Add(_gameInitSystem);
            _updateSystems.Add(new DetectionSystem());
            _updateSystems.Add(new AutofireWeaponSystem());
            _updateSystems.Add(new RechargingSystem());
            _updateSystems.Add(new CameraFollowSystem());

            _fixedUpdateSystems.Add(new RotationInputSystem());
            _fixedUpdateSystems.Add(new MoveInputSystem());
            _updateSystems.Add(new PlayerMoveSystem());
            _updateSystems.Add(new PlayerRotateSystem());
            _fixedUpdateSystems.Add(new TrackingSystem());
            _fixedUpdateSystems.Add(new TrackingFollowSystem());
            _fixedUpdateSystems.Add(new FollowMoveSystem());
            _fixedUpdateSystems.Add(new FollowRotateSystem());
            _fixedUpdateSystems.Add(new ShootingSystem());
            _fixedUpdateSystems.Add(new CollisionDamageSystem());
            _fixedUpdateSystems.Add(new HealthDamageSystem());
            _fixedUpdateSystems.Add(new CameraDistanceSystem());
            _fixedUpdateSystems.Add(new ProjectileCollisionSystem());
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