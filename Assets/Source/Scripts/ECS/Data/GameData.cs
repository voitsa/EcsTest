using ECS.EntityActors;
using UnityEngine;

namespace ECS.Data
{
    [CreateAssetMenu(menuName = "GameConfigs")]
    public class GameData : ScriptableObject
    {
        [field: SerializeField] public GameActor GameActorPrefab { get; private set; }
        [field: SerializeField] public UnitInitConfig PlayerInitConfig { get; private set; }
        [field: SerializeField] public EnemyData EnemyData { get; private set; }
        [field: SerializeField] public UIData UIData { get; private set; }
        [field: SerializeField] public TurretInitConfig TurretInitConfig { get; private set; }
        [field: SerializeField] public PickUpsInitConfig PickUpsInitConfig { get; private set; }
    }
}