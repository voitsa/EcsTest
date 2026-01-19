using ECS.Components;
using Leopotam.Ecs;

namespace ECS.Systems
{
    public class PlayerFollowDamageSystem : IEcsRunSystem
    {
        private readonly EcsFilter<DamageInflictComponent, FollowComponent, MeleeWeaponComponent> _filter;
        private readonly EcsFilter<PlayerTagComponent> _playerFilter;

        public void Run()
        {
            foreach (var index in _filter)
            {
                ref var damageInflictComponent = ref _filter.Get1(index);
                ref var followComponent = ref _filter.Get2(index);
                ref var meleeWeaponComponent  = ref _filter.Get3(index);

                var entity = _filter.GetEntity(index);

                if (!followComponent.distanceReached)
                    continue;

                if (entity.Has<RechargeComponent>())
                    continue;

                foreach (var playerIndex in _playerFilter)
                {
                    ref var playerComponent = ref _playerFilter.GetEntity(playerIndex);
                    ref var damageReceiveComponent = ref playerComponent.Get<DamageReceiveComponent>();
                    damageReceiveComponent.value = damageInflictComponent.value;
                }

                ref var rechargeComponent = ref entity.Get<RechargeComponent>();
                rechargeComponent.rechargeDuration = meleeWeaponComponent.attackDelay;
            }
        }
    }
}