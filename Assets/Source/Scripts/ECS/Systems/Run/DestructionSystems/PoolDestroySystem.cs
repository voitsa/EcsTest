using ECS.Components;
using Leopotam.Ecs;

namespace Systems
{
    public class PoolDestroySystem : IEcsRunSystem
    {
        private readonly EcsFilter<PoolDestructionComponent, DestructionEventComponent> _filter;

        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var destruction = ref _filter.Get1(i);
                destruction.poolable.ReturnToPool();
                _filter.GetEntity(i).Destroy();
            }
        }
    }
}