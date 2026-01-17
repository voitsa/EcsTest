using ECS.Components;
using ECS.Components.Movement;
using Leopotam.Ecs;

namespace Systems
{
    public class FollowRotateSystem: IEcsRunSystem
    {
        private readonly EcsFilter<RotatableComponent, FollowComponent> _filter;

        public void Run()
        {
            foreach (var entity in _filter)
            {
                ref var followComponent = ref _filter.Get2(entity);
                ref var rotatableComponent = ref _filter.Get1(entity);

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