using Leopotam.Ecs;

namespace ECS.Systems
{
    public class PoolDestroySystem : IEcsRunSystem
    {
        private readonly EcsFilter<PoolDestructionComponent, DestructionEventComponent> _filter;

        public void Run()
        {
            foreach (var index in _filter)
            {
                ref var destructionComponent = ref _filter.Get1(index);
                destructionComponent.poolable.ReturnToPool();
                _filter.GetEntity(index).Destroy();
            }
        }
    }
}