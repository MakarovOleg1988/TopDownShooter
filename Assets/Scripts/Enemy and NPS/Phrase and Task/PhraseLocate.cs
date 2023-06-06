using TMPro;
using UnityEngine;

namespace TopDownShooter
{
    public class PhraseLocate : MonoBehaviour
    {
        private Camera _mainCamera;

        [SerializeField]
        private Transform _parent;

        [SerializeField]
        private GameObject _canvas;

        [SerializeField]
        private TextMeshProUGUI _textPhrase;

        [SerializeField]
        private PhraseData _phraseData;

        public string[] _phrases;

        [Header("Время жизни фразы"), SerializeField, Range(1f, 10f)]
        private float _timeToLife;

        private void Start()
        {
            _mainCamera = Camera.main;
            _phrases = new string[_phraseData.phrases.Length];

            Phrase();
        }

        private void LateUpdate()
        {
            RotateTextPanel();
        }

        private void RotateTextPanel()
        {
            transform.position = _parent.transform.position;
            transform.rotation = _parent.transform.rotation;
            _canvas.transform.rotation = _mainCamera.transform.rotation;
        }

        private void Phrase()
        {
            for (int i = 0; i < _phrases.Length; i++)
            {
                _phrases[i] = _phraseData.phrases[i];
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<PlayerController>())
            {
                int random = Random.Range(0, _phrases.Length - 1);
                _textPhrase.enabled = true;
                _textPhrase.text = _phrases[random];
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.GetComponent<PlayerController>())
            {
                _textPhrase.enabled = false;
            }
        }
    }
}
