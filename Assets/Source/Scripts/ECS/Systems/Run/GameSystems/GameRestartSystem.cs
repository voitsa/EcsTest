using ECS.Components;
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
                    ref var destructionComponent = ref _destructionFilter.Get1(destructionIndex);
                    Object.Destroy(destructionComponent.destroyObject);
                }

                ref var gameComponent = ref _filter.Get1(index);
                gameComponent.gameState.RestartGame();
            }
        }
    }
}