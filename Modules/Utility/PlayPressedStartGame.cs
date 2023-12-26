using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayPressedStartGame : MonoBehaviour
{
    private void StartGame()
    {
        SceneManager.LoadScene("Game");
    }
}