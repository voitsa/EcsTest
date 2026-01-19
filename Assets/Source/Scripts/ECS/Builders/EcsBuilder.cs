using Leopotam.Ecs;

namespace ECS.Builders
{
    public class EcsBuilder
    {
        protected EcsWorld _world;

        public EcsBuilder(EcsWorld world)
        {
            _world = world;
        }
    }
}