using ECS.Components;
using Leopotam.Ecs;

namespace ECS.Systems
{
    public class FollowRotateSystem: IEcsRunSystem
    {
        private readonly EcsFilter<RotatableComponent, FollowComponent> _filter;

        public void Run()
        {
            foreach (var index in _filter)
            {
                ref var followComponent = ref _filter.Get2(index);
                ref var rotatableComponent = ref _filter.Get1(index);

                if (followComponent.target == null || rotatableComponent.transform == null)
                {
                    continue;
                }

                var direction = (followComponent.target.position - rotatableComponent.transform.position).normalized;
                rotatableComponent.transform.forward = direction;
                direction.z = 0;
            }
        }
    }
}