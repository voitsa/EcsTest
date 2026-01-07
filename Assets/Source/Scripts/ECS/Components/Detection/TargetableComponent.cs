using UnityEngine;
using Utilitiy;

namespace ECS.Components.Detection
{
    public struct TargetableComponent
    {
        public Teams team;
        public Transform transform;
    }
}