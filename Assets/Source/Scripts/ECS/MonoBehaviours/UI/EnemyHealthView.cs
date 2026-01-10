using TMPro;
using UnityEngine;

namespace ECS
{
    public class EnemyHealthView : HealthView
    {
        [SerializeField] private TextMeshProUGUI _healthLabel;
        public override void SetHealth(float current)
        {
            _healthLabel.text = current.ToString();
        }
    }
}