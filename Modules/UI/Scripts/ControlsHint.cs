using UnityEngine;

public class ControlsHint : MonoBehaviour
{
    public float FadeOutTime;

    public void FadeOut()
    {
        LeanTween.alphaCanvas(GetComponent<CanvasGroup>(), 0, FadeOutTime)
            .setOnComplete(() => gameObject.SetActive(false));
    }
}