using UnityEngine;

namespace ECS.Data
{
    [CreateAssetMenu(menuName = "UIConfig")]
    public class UIData : ScriptableObject
    {
        [field: SerializeField] public EnemyHealthView EnemyHealthView {get; private set;}
        [field: SerializeField] public PlayerView PlayerView {get; private set;}
    }
}