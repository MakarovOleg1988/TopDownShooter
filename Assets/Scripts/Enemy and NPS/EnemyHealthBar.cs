using UnityEngine;

namespace TopDownShooter
{
    public class EnemyHealthBar: MonoBehaviour
    {
        private Camera _mainCamera;
        private Transform _parent;

        [SerializeField]
        private GameObject _canvas;

        private void Start()
        {
            _mainCamera = Camera.main;
            _parent = GetComponentInParent<Transform>();

        }

        private void LateUpdate()
        {
            RotateEnemyHealthBar();
        }

        private void RotateEnemyHealthBar()
        {
            transform.position = _parent.transform.position;
            _canvas.transform.rotation = _mainCamera.transform.rotation;
        }
    }
}
