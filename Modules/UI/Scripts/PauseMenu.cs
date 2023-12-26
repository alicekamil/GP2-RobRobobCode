using UnityEngine;
using UnityEngine.UI;

namespace SpaceGame
{
    public class PauseMenu : MonoBehaviour
    {
        public static bool GameIsPaused = false;

        public void Resume()
        {
            pauseMenuUI.SetActive(false);
            Time.timeScale = 1f;
            GameIsPaused = false;
        }

        public void Pause()
        {
            pauseMenuUI.SetActive(true);
            _resumeButton.Select();
            Time.timeScale = 0f;
            GameIsPaused = true;
        }

        public void ReturnToMenu()
        {
            GameManager.Instance.GoMenu();
            Time.timeScale = 1f;
            GameIsPaused = false;
        }

        public void Restart()
        {
            GameManager.Instance.Restart();
            GameIsPaused = false;
        }
        
        private void Update()
        {
            if (Input.GetButtonDown("Pause"))
            {
                if (GameIsPaused)
                {
                    Resume();
                }
                else
                {
                    Pause();
                }
            }
        }
        
        [SerializeField] private GameObject pauseMenuUI;
        [SerializeField] private Button _resumeButton;
    }
}