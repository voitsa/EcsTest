using Data;
using EntityActors;
using UnityEngine;

namespace ECS.Data
{
    [CreateAssetMenu(menuName = "ProjectileInitData")]
    public class ProjectileInitConfig : ScriptableObject
    {
        [field: SerializeField] public ProjectileActor ProjectilePrefab { get; private set; }
        [field: SerializeField] public float Speed { get; private set; }
    }
}