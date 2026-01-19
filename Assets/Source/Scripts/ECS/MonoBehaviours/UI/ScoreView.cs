using TMPro;
using UnityEngine;

namespace ECS
{
    public class ScoreView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _scoreLabel;

        public void SetScore(int scoreValue)
        {
            _scoreLabel.text = scoreValue.ToString();
        }
    }
}