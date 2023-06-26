using UnityEngine;
using TMPro;

namespace TopDownShooter
{
    public class InteractionWithCoin : MonoBehaviour
    {
        private PlayerParam _playerParam;

        private Rigidbody _rbCoin;

        [SerializeField, Range(10, 150)]
        private int _maxCapacityCoininBox;

        [SerializeField]
        private TextMeshProUGUI _coinValueText;

        private void Start()
        {
            _playerParam = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerParam>();
            _rbCoin = GetComponent<Rigidbody>();
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.GetComponent<PlayerController>())
            {
                int randomCoin = Random.Range(10, _maxCapacityCoininBox);
                _playerParam.CoinValue += randomCoin;
                _coinValueText.text = _playerParam.CoinValue.ToString();
                ReturnToPool();
            }
        }

        private void ReturnToPool()
        {
            gameObject.SetActive(false);
            transform.position = Vector3.zero;
            transform.rotation = new Quaternion(0f, 0f, 0f, 0f);
            _rbCoin.velocity = Vector3.zero;
        }
    }
}
