using System.Collections;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

namespace TopDownShooter
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(PoolForRevolverProjectile))]
    [RequireComponent(typeof(PoolForRifleProjectile))]
    public class PlayerController : PlayerParam, IEventManager, IDamageAblePlayer
    {
        private PlayerController _playerController;

        private void OnEnable()
        {
            _controller.NewActionMap.Enable();
            _controller.NewActionMap.SimpleShoot.performed += SimpleShoot;
            _controller.NewActionMap.AlternativeShoot.performed += AlternativeShoot;
            _controller.NewActionMap.ActiveFlashlight.performed += ActiveFlashlight;
            _controller.NewActionMap.ReloadGun.performed += ReloadGun;
            _controller.NewActionMap.ChangeWeapon.performed += ChangeWeapon;
            _controller.NewActionMap.ActiveTasksMenu.performed += ActiveTasksMenu;
        }

        private void Awake()
        {
            _mainCamera = Camera.main;
            _controller = new NewControls();
        }

        private void Start()
        {
            MaxHealth = CurrentHealth;
            _healthBar.value = MaxHealth - CurrentHealth;
            _healthBar.maxValue = MaxHealth;
            _currentHealthText.text = CurrentHealth.ToString();
            _maxHealthText.text = MaxHealth.ToString();

            _currentCapacityClipRevolver = _maxCapacityClipRevolver;
            _currentCapacityClipRifle = _maxCapacityClipRifle;

            _shootParticleSystem[0].Stop();
            _shootParticleSystem[1].Stop();

            _poolRevolver = GetComponent<PoolForRevolverProjectile>();
            _poolRifle = GetComponent<PoolForRifleProjectile>();

            _playerController = GetComponent<PlayerController>();
            _anim = GetComponentInChildren<Animator>();
            _rb = GetComponent<Rigidbody>();
            _miniMapCanvas = GetComponentInChildren<Canvas>();
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
            
            if (movement.x == 0f && movement.z == 0) _anim.SetBool("IsMoving", false);
            else _anim.SetBool("IsMoving", true);

            transform.Translate(movement * _speedMovement * Time.deltaTime, Space.World);
        }

        public void SimpleShoot(CallbackContext context)
        {
            if (_canShoot == true && _currentCapacityClipRevolver > 0 && _withRevolver == true)
            {
                IEventManager.SendSetShootRevolver();

                Vector3 positionFire = _targetFire[0].position;
                Quaternion rotationFire = _targetFire[0].rotation;

                _poolRevolver.GetFreeElement(positionFire, rotationFire);
                _currentCapacityClipRevolver--;
                _shootParticleSystem[0].Play();
                _anim.SetBool("IsShooting", true);
                _canShoot = false;
            }
            if (_canShoot == true && _currentCapacityClipRifle > 0 && _withRevolver == false)
            {
                IEventManager.SendSetShootRifle();

                Vector3 positionFire = _targetFire[1].position;
                Quaternion rotationFire = _targetFire[1].rotation;

                _poolRifle.GetFreeElement(positionFire, rotationFire);
                _currentCapacityClipRifle--;
                _shootParticleSystem[1].Play();
                _canShoot = false;
            }

            else IEventManager.SendSetEmptyShootRevolver();

            StartCoroutine(TimerbetweenShootCoroutine());
        }

        public void AlternativeShoot(CallbackContext context)
        {

        }

        private void ChangeWeapon(CallbackContext context)
        {
            IEventManager.SendSetChangeWeapon();

            if (_withRevolver == false) 
            {
                _weapon[0].localPosition = new Vector3(-0.02f, 0.12f, 0.02f);
                _weapon[0].localRotation = Quaternion.Euler(-76f, 22f, 70f);

                _weapon[1].localPosition = new Vector3(0.03f, 0.1f, -0.2f);
                _weapon[1].localRotation = Quaternion.Euler(70f, -90f, 0f);
                _withRevolver = true;
            }

            else if(_withRevolver == true)
            {
                _weapon[0].localPosition = new Vector3(0.3f, -0.55f, -0.25f);
                _weapon[0].localRotation = Quaternion.Euler(-180f, -75f, 185f);

                _weapon[1].localPosition = new Vector3(-0.3f, 0.15f, 0.5f);
                _weapon[1].localRotation = Quaternion.Euler(185f, 330f, 190f);
                _withRevolver = false;
            }
        }

        private void ReloadGun(CallbackContext context)
        {
            if (_withRevolver == true)
            {
                IEventManager.SendSetReloadWeapon();
                _currentCapacityClipRevolver = _maxCapacityClipRevolver;
            }
            else if (_withRevolver == false)
            {
                IEventManager.SendSetReloadWeapon();
                _currentCapacityClipRifle = _maxCapacityClipRifle;
            }
        }

        private IEnumerator TimerbetweenShootCoroutine()
        {
            yield return new WaitForSeconds(_reloadSpeed);
            _anim.SetBool("IsShooting", false);
            _shootParticleSystem[0].Stop();
            _shootParticleSystem[1].Stop();
            _canShoot = true;
        }

        public void ActiveFlashlight(CallbackContext context)
        {
            if (haveFlashlight == false) return;

            if (FlashlightIsActive == true)
            {
                IEventManager.SendSetActiveFlashlight();
                _flashlight.SetActive(true);
                FlashlightIsActive = false;
            }
            else if (FlashlightIsActive == false)
            {
                IEventManager.SendSetActiveFlashlight();
                _flashlight.SetActive(false);
                FlashlightIsActive = true;
            } 
        }

        public void ActiveTasksMenu(CallbackContext context)
        {
            if (TaskMenuIsActive == true)
            {
                _taskPanel.SetActive(true);
                _miniMapCanvas.enabled = true;
                TaskMenuIsActive = false;
            }
            else if (TaskMenuIsActive == false)
            {
                _taskPanel.SetActive(false);
                _miniMapCanvas.enabled = false;
                TaskMenuIsActive = true;
            }
        }

        public void ApplyDamagePlayer(int damage)
        {
            if (CurrentHealth > 0)
            {
                CurrentHealth -= damage;             
                _anim.SetTrigger("GetDamage");
                CheckCurrentHealth();
            }

            if (CurrentHealth <= 0)
            {
                _anim.SetTrigger("IsDying");
                StartCoroutine(SetDeathCoroutine());
            }
        }

        public void CheckCurrentHealth()
        {
            _currentHealthText.text = CurrentHealth.ToString();
            _healthBar.value = MaxHealth - CurrentHealth;
        }

        private IEnumerator SetDeathCoroutine()
        {
            _losePanel.SetActive(true);
            _playerController.enabled = false;
            yield return new WaitForSeconds(4f);
        }

        private void OnDisable()
        {
            _controller.Dispose();
            _controller.NewActionMap.SimpleShoot.performed -= SimpleShoot;
            _controller.NewActionMap.AlternativeShoot.performed -= AlternativeShoot;
            _controller.NewActionMap.ActiveFlashlight.performed -= ActiveFlashlight;
            _controller.NewActionMap.ReloadGun.performed -= ReloadGun;
            _controller.NewActionMap.ActiveTasksMenu.performed -= ActiveTasksMenu;
        }

        private void OnDestroy()
        {
            _controller.NewActionMap.Disable();
        }
    }
}
