using UnityEngine;

namespace TopDownShooter
{
    public class UnitParam : MonoBehaviour
    {
        protected Animator _anim;
        protected Rigidbody _rb;

        [SerializeField]
        protected UnitType _unitType;

        [Space(10f), Header("Базовые параметры"), SerializeField, Range(0.1f, 20f), Tooltip("Скорость перемещения")] 
        protected float _speedMovement;

        [SerializeField, Range(1f, 20f), Tooltip("Скорость перемещения бега")]
        protected float _speedRunning;

        [SerializeField, Range(0.1f, 5f), Tooltip("Скорость поворота персонажа")] 
        protected float _speedRotation;

        [SerializeField, Range(1f, 10f), Tooltip("Скорость перезарядки оружия персонажа")]
        protected float _reloadSpeed;

        [SerializeField]
        private int _health;
        public int CurrentHealth 
        {
            get { return _health; }
            set { _health = value; }
        }

        protected int _maxHealth;
        public int MaxHealth
        {
            get { return _maxHealth; }
            set { _maxHealth = value; }
        }
    }
}
