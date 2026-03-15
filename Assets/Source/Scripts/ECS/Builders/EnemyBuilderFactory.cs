using ECS.Data;
using Leopotam.Ecs;
using UnityEngine;
using Zenject;

namespace ECS.Builders
{
    public class EnemyBuilderFactory
    {
        private readonly GameData _gameData;
        private readonly DiContainer _container;

        public EnemyBuilderFactory(GameData gameData, DiContainer container)
        {
            _gameData = gameData;
            _container = container;
        }

        public EnemyBuilder Create(EcsWorld world, Transform playerTarget)
        {
            return _container.Instantiate<EnemyBuilder>(new object[]
            {
                world,
                _gameData.EnemyData,
                _gameData.UIData.EnemyHealthView,
                playerTarget
            });
        }
    }
}

