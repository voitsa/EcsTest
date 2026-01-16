using ECS.Components;
using ECS.Components.Detection;
using Leopotam.Ecs;

namespace Systems
{
    public class HealthViewSystem : IEcsRunSystem
    {
        private EcsFilter<HealthComponent, HealthViewComponent> _filter;

        public void Run()
        {
            foreach (var index in _filter)
            {
                ref var health = ref _filter.Get1(index);
                ref var view = ref _filter.Get2(index);

                view.healthView.SetHealth(health.currentValue);
            }
        }
    }
}