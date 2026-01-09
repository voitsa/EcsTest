using UnityEngine;

namespace EntityActors
{
    public class ProjectileActor : MonoBehaviour
    {
        [field: SerializeField] public ParticleSystem MoveParticleSystem { get; private set; }
    }
}