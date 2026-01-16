using UnityEngine;

namespace Utility
{
    public static class CameraExtentions
    {
        public static Bound GetWorldBound(this Camera cam, float planeY = 0f)
        {

            if (cam == null) cam = Camera.main;

            Plane plane = new Plane(Vector3.up, new Vector3(0, planeY, 0));

            var bound = new Bound();

            Ray rayBL = cam.ViewportPointToRay(new Vector3(0, 0, 0));
            if (plane.Raycast(rayBL, out float enterBL))
            {
                Vector3 point = rayBL.GetPoint(enterBL);
                bound.Min = new Vector2(point.x, point.z);
            }

            Ray rayTR = cam.ViewportPointToRay(new Vector3(1, 1, 0));
            if (plane.Raycast(rayTR, out float enterTR))
            {
                Vector3 point = rayTR.GetPoint(enterTR);
                bound.Max = new Vector2(point.x, point.z);
            }

            return bound;
        }
    }
}