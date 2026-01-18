using ECS.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace ECS.Systems
{
    public class TrackingSystem : IEcsRunSystem
    {
        private readonly EcsFilter<TrackerComponent> _trackerFilter;
        private readonly EcsFilter<TargetableComponent> _targetableFilter;

        public void Run()
        {
            foreach (var index in _trackerFilter)
            {
                ref var tracker = ref _trackerFilter.Get1(index);
                Transform closestTarget = null;
                float minDistance = tracker.searchRadius;

                tracker.targetTransform = null;

                foreach (var target in _targetableFilter)
                {
                    ref var targetable = ref _targetableFilter.Get1(target);

                    if (targetable.team == tracker.selfTeam || tracker.selfTransform == null)
                        continue;

                    var distance = Vector2.Distance(tracker.selfTransform.position,
                        targetable.transform.position);

                    if (distance < minDistance)
                    {
                        minDistance = distance;
                        closestTarget = targetable.transform;
                    }

                    tracker.targetTransform = closestTarget;
                }
            }
        }
    }
}