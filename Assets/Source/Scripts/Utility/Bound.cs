using UnityEngine;

namespace Utility
{
    public struct Bound
    {
        public Vector2 Min;
        public Vector2 Max;

        public Vector2 GetRandomPointOnBorder(float offset = 0f)
        {
            bool vertical = Random.value < 0.5f;

            float x = vertical
                ? (Random.value < 0.5f ? Min.x - offset : Max.x + offset)
                : Random.Range(Min.x, Max.x);

            float y = vertical
                ? Random.Range(Min.y, Max.y)
                : (Random.value < 0.5f ? Min.y - offset : Max.y + offset);

            return new Vector2(x, y);
        }
    }
}