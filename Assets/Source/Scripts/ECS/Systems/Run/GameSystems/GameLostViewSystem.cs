using ECS.Components;
using Leopotam.Ecs;

namespace ECS.Systems.GameSystems
{
    public class GameLostViewSystem : IEcsRunSystem
    {
        private readonly EcsFilter<GameLostViewComponent, GameLostEventComponent> _filter;

        public void Run()
        {
            foreach (var index in _filter)
            {
                ref var gameLostViewComponent = ref _filter.Get1(index);
                gameLostViewComponent.gameLostView.Activate();
            }
        }
    }
}