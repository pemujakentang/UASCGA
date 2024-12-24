using UnityEngine.SceneManagement;
using UnityEngine;

public class Events : MonoBehaviour
{
    public void ReplayGame()
    {
        // Reset the gameOver flag
        PlayerManager.gameOver = false;
        // Reload the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        // Debugg so the scene name is printed in the console
        Debug.Log("Scene Name: " + SceneManager.GetActiveScene().name);

    }
}
