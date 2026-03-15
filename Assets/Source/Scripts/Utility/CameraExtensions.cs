using UnityEngine;

namespace Utility
{
    public static class CameraExtensions
    {
        public static Bound GetWorldBound(this Camera camera, float planeY = 0f)
        {

            if (camera == null)
                camera = Camera.main;

            Plane plane = new Plane(Vector3.up, new Vector3(0, planeY, 0));
            var bound = new Bound();
            Ray rayBL = camera.ViewportPointToRay(new Vector3(0, 0, 0));

            if (plane.Raycast(rayBL, out float enterBL))
            {
                Vector3 point = rayBL.GetPoint(enterBL);
                bound.Min = new Vector2(point.x, point.z);
            }

            Ray rayTR = camera.ViewportPointToRay(new Vector3(1, 1, 0));

            if (plane.Raycast(rayTR, out float enterTR))
            {
                Vector3 point = rayTR.GetPoint(enterTR);
                bound.Max = new Vector2(point.x, point.z);
            }

            return bound;
        }
    }
}
