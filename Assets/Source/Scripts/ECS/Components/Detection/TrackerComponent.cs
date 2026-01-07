using UnityEngine;
using Utilitiy;

namespace ECS.Components.Detection
{
    public struct TrackerComponent
    {
        public float searchRadius;
        public Teams selfTeam;
        public Transform selfTransform;
        public Transform targetTransform;
    }
}