using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TopDownShooter
{
    public class TaskLocate : MonoBehaviour
    {
        private Camera _mainCamera;
        private Transform _parent;

        [SerializeField]
        private GameObject _canvas;

        [SerializeField]
        private TextMeshProUGUI _textTasks;

        [SerializeField]
        private Image _taskImage;

        [SerializeField]
        private GameObject[] _labelTasks;

        [SerializeField]
        private TasksData _tasksData;

        private string[] _tasksDiscription;

        public bool _isVillageChief;
        public bool _isÑemeteryÑaretaker;

        [Header("Âðåìÿ æèçíè ôðàçû"), SerializeField, Range(1f, 10f)]
        private float _timeToLife;

        private void Start()
        {
            _mainCamera = Camera.main;
            _parent = GetComponentInParent<Transform>();
            _tasksDiscription = new string[_tasksData.tasksDiscription.Length];

            SetTask();
        }

        private void LateUpdate()
        {
            RotateTextPanel();
        }

        private void RotateTextPanel()
        {
            transform.position = _parent.transform.position;
            _canvas.transform.rotation = _mainCamera.transform.rotation;
        }

        private void SetTask()
        {
            for (int i = 0; i < _tasksDiscription.Length; i++)
            {
                _tasksDiscription[i] = _tasksData.tasksDiscription[i];
            }

            for (int i = 0; i < _tasksDiscription.Length; i++)
            {
                _labelTasks[i].GetComponentInChildren<Text>().text = _tasksData.tasks[i];
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<PlayerController>() && _isVillageChief == true)
            {
                _textTasks.enabled = true;
                _taskImage.enabled = false;
                _textTasks.text = _tasksDiscription[1];
                _labelTasks[1].SetActive(true);
            }
            else if (other.GetComponent<PlayerController>() && _isÑemeteryÑaretaker == true)
            {
                _textTasks.enabled = true;
                _taskImage.enabled = false;
                _textTasks.text = _tasksDiscription[2];
                _labelTasks[2].SetActive(true);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.GetComponent<PlayerController>())
            {
                _textTasks.enabled = false;
            }
        }
    }
}
