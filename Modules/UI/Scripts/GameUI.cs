using UnityEngine;
using UnityEngine.SceneManagement;

namespace SpaceGame
{
    public class GameUI : MonoBehaviour
    {
        public void ShowGameOver() => _gameOverMenu.Show();
        public void FadeControls() => _controlHint.FadeOut();
        public void ShowControls() => _controlHint.gameObject.SetActive(true);

        public void OnRestart()
        {
            GameManager.Instance.Restart();
        }

        public void OnMainMenu()
        {
            SceneManager.LoadScene("Intro");
        }

        [SerializeField] private GameOverUI _gameOverMenu;
        [SerializeField] private ControlsHint _controlHint;
    }
}