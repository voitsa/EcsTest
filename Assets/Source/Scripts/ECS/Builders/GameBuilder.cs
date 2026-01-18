using ECS.Components;
using ECS.EntityActors;
using Leopotam.Ecs;
using UnityEngine;

namespace ECS.Builders
{
    public class GameBuilder : EcsBuilder
    {
        public GameBuilder(EcsWorld world) : base(world)
        {
        }

        public void Build(GameActor actorPrefab, Loader loader)
        {
            var entity = _world.NewEntity();
            var actor = Object.Instantiate(actorPrefab);

            ref var gameComponent = ref entity.Get<GameComponent>();
            gameComponent.loader = loader;

            ref var destructionComponent = ref entity.Get<DestructionComponent>();
            destructionComponent.destroyObject = actor.gameObject;

            ref var gameLostViewComponent = ref entity.Get<GameLostViewComponent>();
            var gameLostView = actor.GetComponent<GameLostView>();
            gameLostView.Init(entity);
            gameLostViewComponent.gameLostView = gameLostView;
        }
    }
}