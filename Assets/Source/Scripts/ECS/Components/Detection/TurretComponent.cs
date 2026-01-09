using System.Collections.Generic;
using Leopotam.Ecs;

namespace ECS.Components.Detection
{
    public struct TurretComponent
    {
        public List<EcsEntity> weapons;
    }
}