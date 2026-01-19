using ECS.Components;
using Leopotam.Ecs;

namespace ECS.Systems
{
    public class TrackingFollowSystem : IEcsRunSystem
    {
        private readonly EcsFilter<TrackerComponent, FollowComponent> _trackerFilter;

        public void Run()
        {
            foreach (var index in _trackerFilter)
            {
                ref var tracker = ref _trackerFilter.Get1(index);
                ref var follow = ref _trackerFilter.Get2(index);

                follow.target = tracker.targetTransform;
            }
        }
    }
}