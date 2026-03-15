using ECS.Components;
using Leopotam.Ecs;

namespace ECS.Systems.GameSystems
{
    public class GameLostSystem : IEcsRunSystem
    {
        private readonly EcsFilter<GameComponent> _filter;
        private readonly EcsFilter<PlayerTagComponent> _playerFilter;

        public void Run()
        {
            if (!_playerFilter.IsEmpty())
                return;

            foreach (var index in _filter)
            {
                ref var entity = ref _filter.GetEntity(index);
                entity.Get<GameLostEventComponent>();
            }
        }
    }
}