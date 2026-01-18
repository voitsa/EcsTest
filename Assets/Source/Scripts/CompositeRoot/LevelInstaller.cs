using Data;
using ECS;
using ECS.Data;
using Leopotam.Ecs;
using Systems;
using UnityEngine;
using Zenject;

namespace CompositeRoot
{
    public class LevelInstaller : MonoInstaller
    {
        [SerializeField] private GameData _gameData;

        public override void InstallBindings()
        {
            Container.BindInstance(_gameData);
        }
    }
}