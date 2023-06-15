using UnityEngine;
using System.Xml;

namespace TopDownShooter
{
    public class SaveGame : MonoBehaviour
    {
        private PlayerParam _playerParam;

        private string filePath = "Assets/Resources/XML/Save.xml";

        private void Start()
        {
            _playerParam = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerParam>();
        }

        public void Save()
        {
            XmlDocument xmlSave = new XmlDocument();

            XmlElement rootNode = xmlSave.CreateElement("root");
            xmlSave.AppendChild(rootNode);

            XmlElement coinNode = xmlSave.CreateElement("_coin");
            coinNode.InnerText = _playerParam.CoinValue.ToString();
            rootNode.AppendChild(coinNode);

            xmlSave.Save(filePath);
        }
    }
}