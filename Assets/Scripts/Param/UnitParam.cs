using UnityEngine;

namespace TopDownShooter
{
    public abstract class UnitParam : MonoBehaviour
    {
        [SerializeField, Range(1f, 20f), Tooltip("�������� �����������")] 
        protected float _speedMovement;

        [SerializeField, Range(0.1f, 5f), Tooltip("�������� �������� ���������")] 
        protected float _speedRotation;
    }
}
