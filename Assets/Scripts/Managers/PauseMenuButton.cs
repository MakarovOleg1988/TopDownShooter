using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TopDownShooter
{
    public class PauseMenuButton : MonoBehaviour
    {
        public static bool _gameIsPaused = false;

        [SerializeField]
        private TextMeshProUGUI _saveDoneText;

        private PlayerController _playerController;
        
        [SerializeField]
        private GameObject _pauseMenu;

        private void Start()
        {
            _playerController = FindObjectOfType<PlayerController>();
        }

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
            _playerController.enabled = false;
            _pauseMenu.SetActive(true);
            _saveDoneText.enabled = false;
            Time.timeScale = 0f;
            _gameIsPaused = true;
        }

        public void ResumeGame()
        {
            _playerController.enabled = true;
            _pauseMenu.SetActive(false);
            Time.timeScale = 1f;
            _gameIsPaused = false;
        }

        public void BackToMainMenu()
        {
            _playerController.enabled = true;
            Time.timeScale = 1f;
            SceneManager.LoadScene("MainMenu");
        }
    }
}
