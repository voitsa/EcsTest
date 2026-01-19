using Leopotam.Ecs;

namespace ECS.Components
{
    public struct TriggerEnterComponent
    {
        public EcsEntity other;
        public EcsEntity self;
    }
}