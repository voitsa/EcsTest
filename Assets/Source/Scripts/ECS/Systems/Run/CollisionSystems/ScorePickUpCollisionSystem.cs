using ECS.Components;
using Leopotam.Ecs;

namespace ECS.Systems
{
    public class ScorePickUpCollisionSystem : IEcsRunSystem
    {
        private readonly EcsFilter<ScorePickUpComponent, CollisionEnterComponent> _filter;

        public void Run()
        {
            foreach (var index in _filter)
            {
                ref var scorePickUpComponent = ref _filter.Get1(index);
                ref var collisionComponent = ref _filter.Get2(index);

                var otherEntity = collisionComponent.other;

                if (!otherEntity.IsAlive() || !otherEntity.Has<ScoreComponent>())
                    continue;

                ref var scoreComponent = ref otherEntity.Get<ScoreComponent>();
                scoreComponent.score += scorePickUpComponent.pickUpScore;

                ref var entity = ref _filter.GetEntity(index);
                entity.Get<DestructionEventComponent>();
                entity.Del<CollisionEnterComponent>();
            }
        }
    }
}