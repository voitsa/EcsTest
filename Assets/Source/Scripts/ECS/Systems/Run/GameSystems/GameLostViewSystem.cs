using ECS.Components.Game;
using Leopotam.Ecs;

namespace ECS.Systems.GameSystems
{
    public class GameLostViewSystem : IEcsRunSystem
    {
        private readonly EcsFilter<GameLostViewComponent, GameLostEventComponent> _filter = null;

        public void Run()
        {
            foreach (var index in _filter)
            {
                ref var gameLostView = ref _filter.Get1(index);
                gameLostView.gameLostView.Activate();
            }
        }
    }
}