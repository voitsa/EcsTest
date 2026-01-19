using UnityEngine;

namespace ECS.EntityActors
{
    public class TurretActor : MonoBehaviour
    {
        [field: SerializeField] public WeaponActor WeaponActor { get; private set; }
        [field: SerializeField] public Vector3[] WeaponPositions  { get; private set; }
    }
}