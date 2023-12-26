using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SpaceGame
{
    public class GameOverUI : MonoBehaviour
    {
        public void Show()
        {
            gameObject.SetActive(true);
            _restartButton.Select();
            _scoreText.text = $"Score: {GameManager.Instance.Score}";
            _hscoreText.text = $"Best: {GameManager.Instance.Highscore}";
        }

        public Button _restartButton;
        public TMP_Text _scoreText;
        public TMP_Text _hscoreText;
        public RectTransform _overlay;
        public RectTransform _text;
    }
}