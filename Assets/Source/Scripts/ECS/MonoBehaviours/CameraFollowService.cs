using UnityEngine;

namespace ECS
{
    public class CameraFollowService : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        private Transform _target;

        public void Init(Transform target, Camera camera)
        {
            _target = target;
            _camera = camera;
        }

        private void LateUpdate()
        {
            if (_camera == null || _target == null)
                return;

            var targetPosition = _target.position;
            var cameraPositionY = _camera.transform.position.y;
            _camera.transform.position = new Vector3(targetPosition.x, cameraPositionY, targetPosition.z);
        }
    }
}

