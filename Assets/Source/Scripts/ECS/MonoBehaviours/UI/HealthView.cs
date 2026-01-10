using UnityEngine;
using UnityEngine.UI;

namespace ECS
{
    public abstract class HealthView : MonoBehaviour
    {
        private float _maxHealth;

        public float MaxHealth => _maxHealth;

        public void Init(float maxHealth)
        {
            _maxHealth = maxHealth;
        }

        public abstract void SetHealth(float current);
    }
}