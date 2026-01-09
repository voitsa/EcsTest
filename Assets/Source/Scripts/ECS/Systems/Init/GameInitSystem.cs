using Data;
using ECS.Components.Input;
using ECS.Data;
using ECS.MonoBehaviours;
using EntityActors;
using Leopotam.Ecs;
using UnityEngine;

namespace Systems
{
    public class GameInitSystem : IEcsInitSystem
    {
        private readonly EcsWorld _world;

        private readonly UnitInitConfig _playerInitConfig;
        private readonly UnitInitConfig _enemyInitConfig;

        private readonly TurretInitConfig _turretInitConfig;
        private readonly WeaponInitConfig _turretWeaponInitConfig;
        private readonly PickUpsInitConfig _pickUpsInitConfig;
        private readonly WeaponInitConfig _mainWeaponInitConfig;
        private WeaponBuilder _weaponBuilder;
        private TurretBuilder _turretBuilder;
        private PickUpBuilder _pickUpBuilder;

        public GameInitSystem(GameData gameData)
        {
            _playerInitConfig = gameData.PlayerInitConfig;
            _enemyInitConfig = gameData.EnemyInitConfig;
            _turretInitConfig = gameData.TurretInitConfig;
            _turretWeaponInitConfig = gameData.TurretWeaponInitConfig;
            _mainWeaponInitConfig = gameData.MainWeaponInitConfig;
            _pickUpsInitConfig = gameData.PickUpsInitConfig;
        }

        public void Init()
        {
            _weaponBuilder = new WeaponBuilder(_world);
            _turretBuilder = new TurretBuilder(_world);
            var playerBuilder = new PlayerBuilder(_world);
            var pickUpBuilder = new PickUpBuilder(_world);

            var playerActor = CreatePlayer(playerBuilder);
            var enemyBuilder = new EnemyBuilder(_world, playerActor.Transform);

            for (int i = 0; i < 1; i++)
            {
                var enemySpawnPosition = new Vector3(Random.Range(-150f, 150f), 0f, Random.Range(-150f, 150f));
                enemyBuilder.BuildUnit(_enemyInitConfig, enemySpawnPosition);
            }
        }

        private UnitActor CreatePlayer(UnitBuilder builder)
        {
            var playerActor = builder.BuildUnit(_playerInitConfig, Vector3.zero);
            _turretBuilder.CreateTurret(_turretInitConfig, playerActor.Transform);

            return playerActor;
        }
    }
}