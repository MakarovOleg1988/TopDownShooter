using UnityEngine.InputSystem;
using UnityEngine;

namespace TopDownShooter
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerController : PlayerParam
    {
        private Vector2 _lookMouse;
        private Vector3 _rotationTarget;

        private void OnEnable()
        {
            _controller.NewActionMap.Enable();
        }

        private void Awake()
        {
            _mainCamera = Camera.main;
            _controller = new NewControls();
        }

        private void Start()
        {
            _rbPlayer = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            MovementPLayer();
            RotationPlayer();
        }

        private void RotationPlayer()
        {
            _lookMouse = _controller.NewActionMap.MouseLook.ReadValue<Vector2>();

            RaycastHit hit;
            Ray ray = _mainCamera.ScreenPointToRay(_lookMouse);

            if (Physics.Raycast(ray, out hit))
            {
                _rotationTarget = hit.point;
            }

            Vector3 lookPosition = _rotationTarget - transform.position;
            lookPosition.y = 0f;

            Quaternion rotation = Quaternion.LookRotation(lookPosition);

            Vector3 aimDirection = new Vector3(_rotationTarget.x, 0f, _rotationTarget.z);

            if (aimDirection != Vector3.zero)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, _speedRotation);
            }
        }

        private void MovementPLayer()
        {
            Vector2 moveValue = _controller.NewActionMap.Movement.ReadValue<Vector2>();
            Vector3 movement = new Vector3(moveValue.x, 0f, moveValue.y);

            transform.Translate(movement * _speedMovement * Time.deltaTime, Space.World);
        }

        private void OnDisable()
        {
            _controller.Dispose();
        }

        private void OnDestroy()
        {
            _controller.NewActionMap.Disable();
        }
    }
}
