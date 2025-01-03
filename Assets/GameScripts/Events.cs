using UnityEngine.SceneManagement;
using UnityEngine;

public class Events : MonoBehaviour
{
    public void ReplayGame()
    {
        PlayerManager.gameOver = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
