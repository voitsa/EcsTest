using Leopotam.Ecs;
using UnityEngine;

namespace ECS.Components
{
    public struct CollisionEnterComponent
    {
        public EcsEntity other;
        public Vector3 collisionPoint;
    }
}