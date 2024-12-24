using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject Menu;
    public GameObject Difficulty;


    private void Start()
    {
        // Pastikan hanya mainMenu yang aktif saat awal
        ShowMainMenu();
    }

    // Menampilkan Menu Utama
    public void ShowMainMenu()
    {
        Menu.SetActive(true);
        Difficulty.SetActive(false);
    }

    // Menampilkan Menu Pemilihan Difficulty
    public void ShowDifficultySelection()
    {
        Menu.SetActive(false);
        Difficulty.SetActive(true);
    }

    // Fungsi untuk memilih kesulitan dan memuat scene yang sesuai
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
            default:
                Debug.LogError("Invalid difficulty selected: " + difficulty);
                break;
        }
    }

    // Fungsi untuk keluar dari game
    public void QuitGame()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }
}
