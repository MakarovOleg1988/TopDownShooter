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

        [SerializeField]
        private Toggle[] _taskTougle; 

        public bool _task1IsComplite { get; private set; }

        public void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<PlayerController>())
            {
                _task1IsComplite = true;
                _taskTougle[0].isOn = true;
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
