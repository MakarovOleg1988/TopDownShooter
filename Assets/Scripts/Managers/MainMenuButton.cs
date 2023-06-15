using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TopDownShooter
{
    public class MainMenuButton : MonoBehaviour
    {
        public void StartGame()
        {
            IEventManager.SendSetClickButton();
            StartCoroutine(StartGameCoroutine());
        }

        private IEnumerator StartGameCoroutine()
        {
            yield return new WaitForSeconds(0.2f);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        public void ExitGame()
        {
            IEventManager.SendSetClickButton();
            StartCoroutine(ExitCoroutine());
        }

        private IEnumerator ExitCoroutine()
        {
            yield return new WaitForSeconds(0.2f);
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_STANDALONE_WIN && !UNITY_EDITOR
            Application.Quit();
#endif
        }
    }
}
