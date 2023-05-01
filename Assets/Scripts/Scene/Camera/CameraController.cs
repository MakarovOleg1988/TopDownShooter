using UnityEngine;

namespace TopDownShooter
{
    public class CameraController : CameraParam
    {
        private void Start()
        {
            _target = transform.parent.GetComponent<PlayerController>();

            transform.parent = null;
        }

        private void LateUpdate()
        {
            transform.position = Vector3.Lerp(transform.position, _target.transform.position, _speedMovementCamera * Time.deltaTime);
        }
    }
}
