using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace TopDownShooter
{
    public class TaskRules : MonoBehaviour
    {
        [SerializeField]
        private GameObject _taskComplitePanel;

        [Header("Определитель номера миссии"), SerializeField]
        private bool _isTask1;

        [SerializeField]
        private bool _isTask2;

        [SerializeField]
        private bool _isTask3;

        [SerializeField]
        private Toggle[] _taskTougle;

        public bool _task1IsComplite { get; private set; }
        public bool _task2IsComplite { get; private set; }
        public bool _task3IsComplite { get; private set; }

        public void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<PlayerController>() == null) return;
            
            if (_isTask1 == true)
            {
                _task1IsComplite = true;
                _taskTougle[0].isOn = true;
                StartCoroutine(TaskCompliteCoroutine());
            }

            if (_isTask2 == true)
            {
                _task2IsComplite = true;
                _taskTougle[1].isOn = true;
                StartCoroutine(TaskCompliteCoroutine());
            }

            if (_isTask3 == true)
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
    }
}
