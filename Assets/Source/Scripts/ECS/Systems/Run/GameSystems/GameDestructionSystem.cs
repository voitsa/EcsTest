using ECS.Components;
using Leopotam.Ecs;

namespace ECS.Systems.GameSystems
{
    public class GameDestructionSystem : IEcsRunSystem
    {
        private readonly EcsFilter<GameComponent, GameLostEventComponent> _filter;

        public void Run()
        {
            foreach (var index in _filter)
            {
                ref var entity = ref _filter.GetEntity(index);
                entity.Del<GameLostEventComponent>();

                ref var gameComponent = ref _filter.Get1(index);
                gameComponent.gameState.StopGame();
            }
        }
    }
}