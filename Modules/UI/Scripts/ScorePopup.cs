using TMPro;
using UnityEngine;

namespace SpaceGame
{
    public class ScorePopup : MonoBehaviour
    {
        public void Show(int score)
        {
            _text.text = $"+{score}";
            LeanTween.moveY(gameObject, transform.position.y + 1f, 0.5f);
            LeanTween.scale(gameObject, Vector3.zero, 0.21f).setDelay(0.35f).setEaseInBack()
                .setOnComplete(() => Destroy(gameObject));
        }

        public TMP_Text _text;
    }
}