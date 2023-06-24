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

            XmlElement coinNode = xmlSave.CreateElement("_coin");
            coinNode.InnerText = _playerParam.CoinValue.ToString();
            rootNode.AppendChild(coinNode);

            XmlElement taskNode1 = xmlSave.CreateElement("_task1IsComplite");
            taskNode1.InnerText = taskRules._task1IsComplite.ToString();
            rootNode.AppendChild(taskNode1);

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