using System.Xml;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TopDownShooter
{
    public class BootsTrap : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI _coinValueText;

        [SerializeField]
        private Toggle[] _taskTougle;

        private PlayerParam _playerParam;

        private TaskRules[] _taskRules;

        private bool isLoading = false;
        private int coin;
        private bool task1IsComplite;
        private bool task2IsComplite;
        private bool task3IsComplite;

        private string filePath = "Assets/Resources/XML/Save.xml";

        private void OnValidate()
        {
            _playerParam = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerParam>();
            _taskRules = FindObjectsOfType<TaskRules>();

            
            LoadData();
        }

        public void LoadData()
        {
            XmlDocument xmlDocument = new XmlDocument();

            xmlDocument.Load(filePath);

            XmlNode root = xmlDocument.SelectSingleNode("root");
            XmlNode isLoadingNode = root.SelectSingleNode("isLoading");
            XmlNode coinNode = root.SelectSingleNode("coin");
            XmlNode task1Node = root.SelectSingleNode("task1IsComplite");
            XmlNode task2Node = root.SelectSingleNode("task2IsComplite");
            XmlNode task3Node = root.SelectSingleNode("task3IsComplite");

            if (isLoadingNode != null) bool.TryParse(isLoadingNode.InnerText, out isLoading);

            if (coinNode != null) int.TryParse(coinNode.InnerText, out coin);
            if (task1Node != null) bool.TryParse(task1Node.InnerText, out task1IsComplite);
            if (task2Node != null) bool.TryParse(coinNode.InnerText, out task2IsComplite);
            if (task3Node != null) bool.TryParse(coinNode.InnerText, out task3IsComplite);

            Debug.Log($"coin: {coin}");
            Debug.Log($"task1IsComplite: {task1IsComplite}");
            Debug.Log($"task2IsComplite: {task2IsComplite}");
            Debug.Log($"task3IsComplite: {task3IsComplite}");

            if (isLoading == true)
            {
                _playerParam.CoinValue = coin;
                _coinValueText.text = _playerParam.CoinValue.ToString();

                foreach (TaskRules item in _taskRules)
                {
                    item._task1IsComplite = task1IsComplite;
                    item._task2IsComplite = task2IsComplite;
                    item._task3IsComplite = task3IsComplite;

                    if (item._task1IsComplite == true) _taskTougle[0].isOn = true;
                    if (item._task2IsComplite == true) _taskTougle[1].isOn = true;
                    if (item._task3IsComplite == true) _taskTougle[2].isOn = true;
                }
            }
        }
    }
}
