using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject Menu;
    public GameObject Difficulty;


    private void Start()
    {
        ShowMainMenu();
    }

    public void ShowMainMenu()
    {
        Menu.SetActive(true);
        Difficulty.SetActive(false);
    }

    public void ShowDifficultySelection()
    {
        Menu.SetActive(false);
        Difficulty.SetActive(true);
    }

    public void SelectDifficulty(string difficulty)
    {
        Debug.Log("Selected difficulty: " + difficulty);

       switch (difficulty)
        {
            case "Easy":
                SceneManager.LoadScene("Easy");
                break;
            case "Medium":
                SceneManager.LoadScene("Medium");
                break;
            case "Hard":
                SceneManager.LoadScene("Hard");
                break;
        }
    }

    public void QuitGame()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }
}
