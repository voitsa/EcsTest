using UnityEngine;

namespace ECS
{
    public class PlayerView : MonoBehaviour
    {
        [field: SerializeField] public PlayerHealthView HealthView { get; private set; }
        [field: SerializeField] public ScoreView ScoreView { get; private set; }
    }
}