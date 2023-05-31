using UnityEngine;
using UnityEngine.UI;

namespace TopDownShooter
{
    public class PlayerParam : UnitParam
    {
        protected NewControls _controller;
        protected Camera _mainCamera;

        protected Vector2 _lookMouse;
        protected Vector3 _rotationTarget;
        protected PoolForRevolverProjectile _poolRevolver;
        protected PoolForRifleProjectile _poolRifle;

        [Space(10f), Header("Параметры оружия и оборудования игрока"), SerializeField]
        protected Transform[] _weapon;

        public Transform[] _targetFire;

        [SerializeField]
        protected GameObject _flashlight;

        public bool FlashlightIsActive { get; set; } = false;
        public bool TaskMenuIsActive { get; set; } = false;
        public bool _withRevolver = true;
        protected bool _canShoot = true;
        public int _currentCapacityClipRevolver;
        public int _maxCapacityClipRevolver;
        public int _currentCapacityClipRifle;
        public int _maxCapacityClipRifle;
        
        [SerializeField]
        protected ParticleSystem[] _shootParticleSystem;

        [Space(10f), Header("Панели и состояния"), SerializeField]
        protected Slider _healthBar;

        [SerializeField]
        protected GameObject _taskPanel;

        [SerializeField]
        protected GameObject _losePanel;
    }
}
