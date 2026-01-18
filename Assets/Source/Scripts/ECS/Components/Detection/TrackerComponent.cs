using UnityEngine;
using Utility;

namespace ECS.Components
{
    public struct TrackerComponent
    {
        public float searchRadius;
        public Teams selfTeam;
        public Transform selfTransform;
        public Transform targetTransform;
    }
}