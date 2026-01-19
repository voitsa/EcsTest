using ECS.Components;
using Leopotam.Ecs;

namespace ECS.Systems
{
    public class HealthViewSystem : IEcsRunSystem
    {
        private EcsFilter<HealthComponent, HealthViewComponent> _filter;

        public void Run()
        {
            foreach (var index in _filter)
            {
                ref var healthComponent = ref _filter.Get1(index);
                ref var healthViewComponent = ref _filter.Get2(index);

                healthViewComponent.healthView.SetHealth(healthComponent.currentValue);
            }
        }
    }
}