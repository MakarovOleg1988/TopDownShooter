using UnityEngine;
using System.Xml;

namespace TopDownShooter
{
    public class LoadGame : MonoBehaviour
    {
        public int _coin;

        private string filePath = "Assets/Resources/XML/Save.xml";


        public void Load()
        {
            XmlDocument xmlDocument = new XmlDocument();

            xmlDocument.Load(filePath);

            XmlNode root = xmlDocument.SelectSingleNode("root");

            XmlNode coinNode = root.SelectSingleNode("_coin");
            if (coinNode != null)
            {
                int.TryParse(coinNode.InnerText, out _coin);
            }
            Debug.Log($"_coin: {_coin}");
        }
    }
}