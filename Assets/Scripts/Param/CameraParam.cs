using UnityEngine;

namespace TopDownShooter
{
    public class CameraParam: MonoBehaviour
    {
        [SerializeField, Range(2f, 20f), Tooltip("�������� ����������� ������")] 
        protected float _speedMovementCamera;

        [SerializeField, Range(1f, 40f), Tooltip("�������� �������� ������")] 
        protected float _speedRotationCamera;

        protected PlayerController _target;
        protected Transform _transformCameraPivot;
        protected Transform _transformCamera;
    }
}
