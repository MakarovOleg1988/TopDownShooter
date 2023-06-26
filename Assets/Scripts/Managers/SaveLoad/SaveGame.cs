using UnityEngine;
using TMPro;
using System.Xml;
using System.Collections;

namespace TopDownShooter
{
    public class SaveGame : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI _saveDoneText;

        private PlayerParam _playerParam;
        private TaskRules taskRules;

        private string filePath = "Assets/Resources/XML/Save.xml";

        private void Start()
        {
            _playerParam = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerParam>();
            taskRules = FindObjectOfType<TaskRules>();
        }

        public void Save()
        {
            XmlDocument xmlSave = new XmlDocument();

            XmlElement rootNode = xmlSave.CreateElement("root");
            xmlSave.AppendChild(rootNode);

            XmlElement coinNode = xmlSave.CreateElement("coin");
            coinNode.InnerText = _playerParam.CoinValue.ToString();
            rootNode.AppendChild(coinNode);

            XmlElement taskNode1 = xmlSave.CreateElement("task1IsComplite");
            taskNode1.InnerText = taskRules._task1IsComplite.ToString();
            rootNode.AppendChild(taskNode1);

            XmlElement taskNode2 = xmlSave.CreateElement("task2IsComplite");
            taskNode2.InnerText = taskRules._task2IsComplite.ToString();
            rootNode.AppendChild(taskNode2);

            XmlElement taskNode3 = xmlSave.CreateElement("task3IsComplite");
            taskNode3.InnerText = taskRules._task3IsComplite.ToString();
            rootNode.AppendChild(taskNode3);

            xmlSave.Save(filePath);

            StartCoroutine(SaveDoneTextCoroutine());
        }

        private IEnumerator SaveDoneTextCoroutine()
        {
            _saveDoneText.enabled = true;
            yield return new WaitForSeconds(3f);
            _saveDoneText.enabled = false;
        }

    }
}