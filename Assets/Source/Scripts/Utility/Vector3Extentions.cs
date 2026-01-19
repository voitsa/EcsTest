using UnityEngine;

namespace Utility
{
    public static class Vector3Extensions
    {
        public static Vector3 RandomNormalized(
            this Vector3 _,
            bool zeroX = false,
            bool zeroY = false,
            bool zeroZ = false)
        {
            Vector3 v;

            do
            {
                v = new Vector3(
                    zeroX ? 0f : Random.Range(-1f, 1f),
                    zeroY ? 0f : Random.Range(-1f, 1f),
                    zeroZ ? 0f : Random.Range(-1f, 1f)
                );
            } while (v.sqrMagnitude < Mathf.Epsilon);

            return v.normalized;
        }
    }
}