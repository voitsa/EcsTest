using ECS.Components;
using ECS.Components.Detection;
using Leopotam.Ecs;
using UnityEngine;

namespace Systems
{
    public class PlayerFollowDamageSystem : IEcsRunSystem
    {
        private readonly EcsFilter<DamageInflictComponent, FollowComponent> _filter;
        private readonly EcsFilter<PlayerTagComponent> _playerFilter;

        public void Run()
        {
            foreach (var index in _filter)
            {
                ref var damageInflictComponent = ref _filter.Get1(index);
                ref var followComponent = ref _filter.Get2(index);

                if (!followComponent.distanceReached)
                    continue;

                foreach (var playerIndex in _playerFilter)
                {
                    ref var playerComponent = ref _playerFilter.GetEntity(playerIndex);
                    ref var damageReceiveComponent = ref playerComponent.Get<DamageReceiveComponent>();
                    damageReceiveComponent.value = damageInflictComponent.value;
                }
            }
        }
    }
}