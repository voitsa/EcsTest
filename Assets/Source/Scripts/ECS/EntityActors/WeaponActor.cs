using UnityEngine;

namespace ECS.EntityActors
{
    public class WeaponActor : MonoBehaviour
    {
        [field: SerializeField] public Transform ShootPoint { get; private set; }
    }
}