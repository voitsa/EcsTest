using UnityEngine;
using Utility;

namespace ECS.Components
{
    public struct TargetableComponent
    {
        public Teams team;
        public Transform transform;
    }
}