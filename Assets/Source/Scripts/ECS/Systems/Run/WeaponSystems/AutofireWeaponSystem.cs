using ECS.Components;
using ECS.Components.Detection;
using Leopotam.Ecs;

namespace Systems
{
    public class AutofireWeaponSystem : IEcsRunSystem
    {
        private readonly EcsFilter<TurretComponent> _filter;

        public void Run()
        {
            foreach (var index in _filter)
            {
                ref var turret = ref _filter.Get1(index);

                foreach (var weapon in turret.weapons)
                {
                    if (weapon.Has<RechargeComponent>())
                        continue;

                    weapon.Get<ShotComponent>();
                }
            }
        }
    }
}