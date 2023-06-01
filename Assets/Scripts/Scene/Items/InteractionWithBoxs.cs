using UnityEngine;
using UnityEngine.UI;

namespace TopDownShooter
{
    public class InteractionWithBoxs : MonoBehaviour
    {
        private InteractionWithBoxs _interactionWithBoxs;

        [SerializeField]
        private GameObject _coins;

        [SerializeField]
        private Image _icon; 

        [SerializeField, Range(1f, 10f)]
        private float _pushSpeed;

        private Canvas _canvas;
        private Camera _mainCamera;

        private void Start()
        {
            _interactionWithBoxs = GetComponent<InteractionWithBoxs>();
            _canvas = GetComponentInChildren<Canvas>();
            _mainCamera = Camera.main;
        }

        private void LateUpdate()
        {
            RotateBoxPanel();
        }

        private void RotateBoxPanel()
        {
            _canvas.transform.rotation = _mainCamera.transform.rotation;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<PlayerController>())
            {
                _coins.SetActive(true);
                _coins.AddComponent<Rigidbody>();
                _coins.GetComponent<Rigidbody>().AddForce(Vector3.up * _pushSpeed, ForceMode.Impulse);
                _icon.enabled = false;
                _interactionWithBoxs.enabled = false;
            }
        }
    }
}
