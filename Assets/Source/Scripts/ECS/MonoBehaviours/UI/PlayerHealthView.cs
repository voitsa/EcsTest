using UnityEngine;
using UnityEngine.UI;

namespace ECS
{
    public class PlayerHealthView : HealthView
    {
        [SerializeField] private Slider _healthSlider;

        public override void SetHealth(float current)
        {
            var maxHealth = MaxHealth;
            _healthSlider.value = Mathf.Clamp(current, 0f, maxHealth) / maxHealth;
        }
    }
}