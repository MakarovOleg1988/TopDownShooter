using TMPro;
using UnityEngine;

namespace TopDownShooter
{
    public class InteractionWithStore : MonoBehaviour
    {
        [SerializeField]
        private GameObject _StorePanel;

        [SerializeField]
        private StoreItemData _storeItemData;

        private PlayerParam _playerParam;
        private UnitParam _unitParam;

        [SerializeField]
        private TextMeshProUGUI _coinValueText;

        private void Start()
        {
            _unitParam = GameObject.FindGameObjectWithTag("Player").GetComponent<UnitParam>();
            _playerParam = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerParam>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<PlayerController>())
            {
                _StorePanel.SetActive(true);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.GetComponent<PlayerController>())
            {
                _StorePanel.SetActive(false);
            }
        }

        public void BuyItem1()
        {

            if (_playerParam.CoinValue >= _storeItemData.costItem[0])
            {
                _playerParam.CoinValue -= _storeItemData.costItem[0];
                _coinValueText.text = _playerParam.CoinValue.ToString();

                _unitParam.CurrentHealth = _unitParam.MaxHealth;
                _playerParam._currentHealthText.text = _unitParam.CurrentHealth.ToString();
            }
        }

        public void BuyItem2()
        {
            if (_playerParam.CoinValue >= _storeItemData.costItem[1])
            {
                _playerParam.CoinValue -= _storeItemData.costItem[1];
                _coinValueText.text = _playerParam.CoinValue.ToString();

                _unitParam.MaxHealth += 2;
                _playerParam._maxHealthText.text = _unitParam.MaxHealth.ToString();
            }
        }

        public void BuyItem3()
        {
            if (_playerParam.CoinValue >= _storeItemData.costItem[2])
            {
                _playerParam.CoinValue -= _storeItemData.costItem[2];
                _coinValueText.text = _playerParam.CoinValue.ToString();

                _playerParam.haveFlashlight = true;
            }
        }
    }
}