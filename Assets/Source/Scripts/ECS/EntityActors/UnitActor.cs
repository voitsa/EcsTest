using UnityEngine;

namespace ECS.EntityActors
{
    public class UnitActor : MonoBehaviour
    {
        [field: SerializeField] public Transform Transform { get; private set; }
    }
}