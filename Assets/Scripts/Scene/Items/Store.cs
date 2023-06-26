using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace TopDownShooter
{
    public class Store : MonoBehaviour
    {
        [SerializeField]
        private StoreItemData _storeItemData;

        [SerializeField]
        private TextMeshProUGUI[] _nameItem;

        [SerializeField]
        private TextMeshProUGUI[] _costItem;

        [SerializeField]
        private TextMeshProUGUI[] _discriptionItem;

        [SerializeField]
        private Image[] _spriteItem;


        private void Start()
        {
            SetDataItem();
        }

        private void SetDataItem()
        {
            for (int i = 0; i < _storeItemData.nameItem.Length; i++)
            {
                _nameItem[i].text = _storeItemData.nameItem[i];
                _costItem[i].text = _storeItemData.costItem[i].ToString();
                _discriptionItem[i].text = _storeItemData.discriptionItem[i];
                _spriteItem[i].sprite = _storeItemData.spriteItem[i];
            }
        }
    }
}