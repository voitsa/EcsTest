using UnityEngine;

namespace ECS.Components
{
    public struct CameraComponent
    {
        public Camera camera;
        public Vector3 defaultPosition;
        public float distanceRate;
        public Vector2 minBound;
        public Vector2 maxBound;
    }
}