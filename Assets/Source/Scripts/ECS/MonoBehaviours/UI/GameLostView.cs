using System;
using ECS.Components.Game;
using Leopotam.Ecs;
using UnityEngine;
using UnityEngine.UI;

namespace ECS
{
    public class GameLostView : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private Button _button;

        private Loader _loader;
        private EcsEntity _entity;

        private void OnDestroy()
        {
            _button.onClick.RemoveListener(OnClick);
        }

        public void Init(EcsEntity ecsEntity)
        {
            _entity = ecsEntity;
            SetState(false);
        }

        public void Activate()
        {
            _button.onClick.AddListener(OnClick);
            SetState(true);
        }

        private void SetState(bool state)
        {
            _canvasGroup.alpha = state ? 1f : 0f;
            _canvasGroup.interactable = state;
        }

        private void OnClick()
        {
            _entity.Get<GameRestartEventComponent>();
        }
    }
}