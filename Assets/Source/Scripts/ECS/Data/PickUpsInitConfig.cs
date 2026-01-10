using EntityActors;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(menuName = "PickUpsInitData")]
    public class PickUpsInitConfig : ScriptableObject
    {
        [field: SerializeField] public PickUpActor PickUpActor { get; private set; }
        [field: SerializeField] public float SpawnProbability { get; private set; }
        [field: SerializeField] public Vector3 SpawnOffset { get; private set; }
        [field: SerializeField] public int PickUpScoreValue { get; private set; }
    }
}