using ECS;
using ECS.Components;
using ECS.Components.Game;
using EntityActors;
using Leopotam.Ecs;
using UnityEngine;

namespace Systems
{
    public class GameBuilder : EcsBuilder
    {
        public GameBuilder(EcsWorld world) : base(world)
        {
        }

        public void Build(GameActor actorPrefab, Loader loader)
        {
            var gameActor = Object.Instantiate(actorPrefab);
            var game = _world.NewEntity();

            ref var gameComponent = ref game.Get<GameComponent>();
            gameComponent.loader = loader;

            ref var destructionComponent = ref game.Get<DestructionComponent>();
            destructionComponent.destroyObject = gameActor.gameObject;

            ref var gameLostViewComponent = ref game.Get<GameLostViewComponent>();
            var gameLostView = gameActor.GetComponent<GameLostView>();
            gameLostView.Init(game);
            gameLostViewComponent.gameLostView = gameLostView;
        }
    }
}