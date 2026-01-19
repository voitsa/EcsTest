using UnityEngine;

namespace ECS.Components
{
    public struct FollowComponent
    {
        public Transform target;
        public float stopDistance;
        public bool distanceReached;
    }
}