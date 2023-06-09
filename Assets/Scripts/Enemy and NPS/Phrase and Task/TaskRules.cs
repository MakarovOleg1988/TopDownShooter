using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace TopDownShooter
{
    public class TaskRules : MonoBehaviour,IEventManager
    {
        [SerializeField]
        private GameObject _taskComplitePanel;

        [SerializeField]
        private GameObject _winPanel;

        [SerializeField]
        private TextMeshProUGUI _currentkillVampirePointText, _maxkillVampirePointText;

        [Header("Определитель номера миссии"), SerializeField]
        private bool _isTask1, _isTask2, _isTask3;

        [SerializeField]
        private Toggle[] _taskTougle;
        
        [HideInInspector]
        private int _maxkillVampirePoint = 3;

        [HideInInspector]
        private int _currentkillVampirePoint;

        public bool _task1IsComplite { get; set; }
        public bool _task2IsComplite { get; set; }
        public bool _task3IsComplite { get; set; }

        private void Start()
        {
            IEventManager._onSetKillVampire += KillPointCalculation;

            _maxkillVampirePointText.text = _maxkillVampirePoint.ToString();
        }

        private void LateUpdate()
        {
            CompliteThirdTask();
            CompliteFirstMission();
        }

        public void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<PlayerController>() == null) return;

            //CompliteFirstTask
            if (_isTask1 == true)
            {
                _task1IsComplite = true;
                _taskTougle[0].isOn = true;
                StartCoroutine(TaskCompliteCoroutine());
                this.GetComponent<BoxCollider>().enabled = false;
            }

            //CompliteSecondTask
            if (_isTask2 == true)
            {
                _task2IsComplite = true;
                _taskTougle[1].isOn = true;
                StartCoroutine(TaskCompliteCoroutine());
                this.GetComponent<BoxCollider>().enabled = false;
            }
        }

        private void KillPointCalculation()
        {
            _currentkillVampirePoint++;
            _currentkillVampirePointText.text = _currentkillVampirePoint.ToString();
        }

        public void CompliteThirdTask()
        {
            if (_currentkillVampirePoint >= _maxkillVampirePoint)
            {
                _task3IsComplite = true;
                _taskTougle[2].isOn = true;
                StartCoroutine(TaskCompliteCoroutine());
            }
        }

        private IEnumerator TaskCompliteCoroutine()
        {
            _taskComplitePanel.SetActive(true);
            yield return new WaitForSeconds(2.5f);
            _taskComplitePanel.SetActive(false);
        }

        public void CompliteFirstMission()
        {
            if (_task1IsComplite == true && _task2IsComplite == true && _task3IsComplite == true)
            {
                _winPanel.SetActive(true);
            }
        }

        private void OnDestroy()
        {
            IEventManager._onSetKillVampire -= KillPointCalculation;
        }
    }
}
