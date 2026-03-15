using CompositeRoot;
using ECS.Builders;
using ECS.Data;
using ECS.EntityActors;
using Leopotam.Ecs;
using UnityEngine;
using Utility;

namespace ECS.Systems
{
    public class GameInitSystem : IEcsInitSystem
    {
        private readonly EcsWorld _world;
        private readonly Loader _loader;
        private readonly GameData _gameData;
        private readonly IGameState _gameState;

        private GameBuilder _gameBuilder;
        private UnitActor _playerActor;

        public GameInitSystem(Loader loader, EcsWorld world, GameData gameData, IGameState gameState)
        {
            _loader = loader;
            _world = world;
            _gameData = gameData;
            _gameState = gameState;
        }

        public void Init()
        {
            _playerActor = CreatePlayer(new PlayerBuilder(_world, _gameData.UIData.PlayerView));
            new GameBuilder(_world).Build(_gameData.GameActorPrefab, _gameState);
        }

        private UnitActor CreatePlayer(UnitBuilder builder)
        {
            var playerActor = builder.BuildUnit(_gameData.PlayerInitConfig, Vector3.zero);
            new TurretBuilder(_world).CreateTurret(_gameData.TurretInitConfig, playerActor.Transform);
            return playerActor;
        }
    }
}