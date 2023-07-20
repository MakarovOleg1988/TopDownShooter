using System.Xml;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TopDownShooter
{
    public class LoadGame : MonoBehaviour
    {
        private string filePath;

        public void TransferToNextLevel()
        {
#if UNITY_EDITOR
            filePath = "Assets/Resources/XML/Save.xml";
#elif UNITY_STANDALONE_WIN && !UNITY_EDITOR  
            filePath = "Save.xml";
#endif

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