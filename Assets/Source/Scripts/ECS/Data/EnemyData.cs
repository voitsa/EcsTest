using UnityEngine;

namespace ECS.Data
{
    [CreateAssetMenu(menuName = "EnemyConfig")]
    public class EnemyData : ScriptableObject
    {
        [field: SerializeField] public UnitInitConfig EnemyInitConfig { get; private set; }
        [field: SerializeField] public float SpawnDelay { get; private set; }
        [field: SerializeField] public float DamageValue { get; private set; }
        [field: SerializeField] public float AttackDelay { get; private set; }
        [field: SerializeField] public float StopDistance { get; private set; }
    }
}