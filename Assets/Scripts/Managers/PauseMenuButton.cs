using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TopDownShooter
{
    public class PauseMenuButton : MonoBehaviour
    {
        public static bool _gameIsPaused = false;
        
        [SerializeField]
        private GameObject _pauseMenu;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (_gameIsPaused) ResumeGame();
                else PauseGame();
            }
        }

        public void PauseGame()
        {
            _pauseMenu.SetActive(true);
            Time.timeScale = 0f;
            _gameIsPaused = true;
        }

        public void ResumeGame()
        {
            _pauseMenu.SetActive(false);
            Time.timeScale = 1f;
            _gameIsPaused = false;
        }

        public void BackToMainMenu()
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}
