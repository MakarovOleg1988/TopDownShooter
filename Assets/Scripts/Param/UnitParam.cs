using UnityEngine;

namespace TopDownShooter
{
    public abstract class UnitParam : MonoBehaviour
    {
        [SerializeField, Range(1f, 20f), Tooltip("Скорость перемещения")] 
        protected float _speedMovement;

        [SerializeField, Range(0.1f, 5f), Tooltip("Скорость поворота персонажа")] 
        protected float _speedRotation;
    }
}
