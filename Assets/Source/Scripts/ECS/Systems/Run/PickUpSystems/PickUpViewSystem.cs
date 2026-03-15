using ECS.Components;
using Leopotam.Ecs;

namespace ECS.Systems
{
    public class PickUpViewSystem : IEcsRunSystem
    {
        private readonly EcsFilter<ScoreComponent, ScoreViewComponent> _filter;

        public void Run()
        {
            foreach (var index in _filter)
            {
                ref var scoreComponent = ref _filter.Get1(index);
                ref var scoreViewComponent = ref _filter.Get2(index);

                scoreViewComponent.scoreView.SetScore(scoreComponent.score);
            }
        }
    }
}