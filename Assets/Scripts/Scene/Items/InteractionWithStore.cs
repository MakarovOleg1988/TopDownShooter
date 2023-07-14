using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace TopDownShooter
{
    public class InteractionWithStore : MonoBehaviour
    {
        [SerializeField]
        private Button[] _buyButton; 
        
        [SerializeField]
        private GameObject _StorePanel;

        [SerializeField]
        private GameObject _canvas;

        [SerializeField]
        private StoreItemData _storeItemData;

        private Camera _mainCamera;
        private Transform _parent;
        private PlayerParam _playerParam;
        private UnitParam _unitParam;
        private PlayerController _playerController;

        [SerializeField]
        private TextMeshProUGUI _coinValueText;

        private void Start()
        {
            _mainCamera = Camera.main;
            _parent = GetComponentInParent<Transform>();

            _playerController = FindObjectOfType<PlayerController>();
            _unitParam = GameObject.FindGameObjectWithTag("Player").GetComponent<UnitParam>();
            _playerParam = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerParam>();
        }

        private void LateUpdate()
        {
            SetColorButton();
            RotateStorePanel();
        }

        private void RotateStorePanel()
        {
            transform.position = _parent.transform.position;
            _canvas.transform.rotation = _mainCamera.transform.rotation;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<PlayerController>())
            {
                _playerController.enabled = false;
                _StorePanel.SetActive(true);
            }
        }

        public void ExitStore()
        {
            _playerController.enabled = true;
            _StorePanel.SetActive(false);
        }

        private void SetColorButton()
        {
            for (int i = 0; i < _buyButton.Length; i++)
            {
                if (_playerParam.CoinValue >= _storeItemData.costItem[i])
                {
                    _buyButton[i].interactable = true;
                    var colors = _buyButton[i].colors;
                    colors.normalColor = new Color(255, 255, 255, 150);
                    _buyButton[i].colors = colors;
                }
                else
                {
                    _buyButton[i].interactable = false;
                    var colors = _buyButton[i].colors;
                    colors.normalColor = new Color(255, 255, 255, 50);
                    _buyButton[i].colors = colors;
                }
            }
        }

        public void BuyItem1()
        {
            if (_playerParam.CoinValue >= _storeItemData.costItem[0])
            {
                _playerParam.CoinValue -= _storeItemData.costItem[0];
                _coinValueText.text = _playerParam.CoinValue.ToString();

                _unitParam.CurrentHealth = _unitParam.MaxHealth;
                _playerController.CheckCurrentHealth();
            }
        }

        public void BuyItem2()
        {
            if (_playerParam.CoinValue >= _storeItemData.costItem[1])
            {
                _playerParam.CoinValue -= _storeItemData.costItem[1];
                _coinValueText.text = _playerParam.CoinValue.ToString();

                _unitParam.MaxHealth += 2;
                _playerController.CheckCurrentHealth();
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