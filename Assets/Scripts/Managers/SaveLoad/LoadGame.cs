using System.Xml;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TopDownShooter
{
    public class LoadGame : MonoBehaviour
    {
        private string filePath = "Assets/Resources/XML/Save.xml";

        public void TransferToNextLevel()
        {
            XmlDocument xmlSave = new XmlDocument();

            xmlSave.Load(filePath);

            XmlElement isLoadingElement = xmlSave.CreateElement("isLoading");
            isLoadingElement.InnerText = "true";
            xmlSave.DocumentElement.AppendChild(isLoadingElement);

            xmlSave.Save(filePath);

            SceneManager.LoadScene(1);
        }
    }
}