using UnityEngine;

namespace SpaceGame
{
    public class IconBounce : MonoBehaviour
    {
        // Start is called before the first frame update
        private void Start()
        {
            LeanTween.moveLocalY(gameObject, 3f, 0.33f).setEase(LeanTweenType.easeInOutSine)
                .setLoopType(LeanTweenType.pingPong);
        }
    }
}