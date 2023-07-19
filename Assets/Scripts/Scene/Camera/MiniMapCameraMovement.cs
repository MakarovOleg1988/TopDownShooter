using TopDownShooter;
using UnityEngine;

namespace TopDownShooter
{
    public class MiniMapCameraMovement : MonoBehaviour
    {
        [SerializeField]
        private Transform _player;

        private void LateUpdate()
        {
            if (_player == null) return;

            Vector3 newPosition = _player.transform.position;
            newPosition.y = transform.position.y;
            transform.position = newPosition;
        }
    }
}
