using TMPro;
using UnityEngine;

namespace TopDownShooter
{
    public class WeaponUIElement: MonoBehaviour, IEventManager
    {
        private PlayerController _playerController;

        [SerializeField]
        private GameObject[] _weaponImage;

        [SerializeField]
        protected TextMeshProUGUI _currentClipCapacitText;

        [SerializeField]
        protected TextMeshProUGUI _MaxClipCapacityText;

        private void Start()
        {
            _playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

            _MaxClipCapacityText.text = _playerController._maxCapacityClipRevolver.ToString();
            _currentClipCapacitText.text = _playerController._currentCapacityClipRevolver.ToString();
        }

        private void LateUpdate()
        {
            ChangeTitleWeapon();
        }

        private void ChangeTitleWeapon()
        {
            if (_playerController._withRevolver == true)
            {
                _weaponImage[0].SetActive(true);
                _weaponImage[1].SetActive(false);
                _MaxClipCapacityText.text = _playerController._maxCapacityClipRevolver.ToString();
                _currentClipCapacitText.text = _playerController._currentCapacityClipRevolver.ToString();
            }

            if (_playerController._withRevolver == false)
            {
                _weaponImage[0].SetActive(false);
                _weaponImage[1].SetActive(true);
                _MaxClipCapacityText.text = _playerController._maxCapacityClipRifle.ToString();
                _currentClipCapacitText.text = _playerController._currentCapacityClipRifle.ToString();
            }
        }
    }
}
