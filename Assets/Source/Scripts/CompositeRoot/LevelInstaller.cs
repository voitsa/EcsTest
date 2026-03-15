using ECS;
using ECS.Builders;
using ECS.Data;
using UnityEngine;
using Zenject;

namespace CompositeRoot
{
    public class LevelInstaller : MonoInstaller
    {
        [SerializeField] private GameData _gameData;
        [SerializeField] private Loader _loader;

        public override void InstallBindings()
        {
            Container.BindInstance(_gameData);
            Container.Bind<IGameState>().To<GameState>().AsSingle().WithArguments(_loader);

            Container.Bind<EnemyBuilder>().AsTransient();
            Container.Bind<EnemyBuilderFactory>().AsSingle();
            Container.Bind<Camera>().FromMethod(_ => Camera.main).AsSingle();
        }
    }
}