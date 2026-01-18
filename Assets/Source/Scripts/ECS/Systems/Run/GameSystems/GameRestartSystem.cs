using ECS.Components;
using ECS.Components.Game;
using Leopotam.Ecs;
using UnityEngine;

namespace ECS.Systems.GameSystems
{
    public class GameRestartSystem : IEcsRunSystem
    {
        private readonly EcsFilter<GameComponent, GameRestartEventComponent> _filter;
        private readonly EcsFilter<DestructionComponent> _destructionFilter;
        private readonly EcsWorld _world;

        public void Run()
        {
            foreach (var index in _filter)
            {
                ref var entity = ref _filter.GetEntity(index);
                entity.Del<GameRestartEventComponent>();

                foreach (var destructionIndex in _destructionFilter)
                {
                    ref var destruction = ref _destructionFilter.Get1(destructionIndex);
                    Object.Destroy(destruction.destroyObject);
                }

                ref var game = ref _filter.Get1(index);
                game.loader.Restart();
            }
        }
    }
}