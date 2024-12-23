using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public static bool gameOver;
    public GameObject gameOverPanel;

    public static bool isGameStarted;
    public GameObject StartingText;
    public GameObject MainMenu;

    public Button PlayButton; // Gunakan Button untuk event listener
    public static int numberOfCoins;

    private bool isWaitingForTap = false; // Indikator apakah menunggu pemain tap

    void Start()
    {
        gameOver = false;
        Time.timeScale = 1;
        isGameStarted = false;
        numberOfCoins = 0;

        MainMenu.SetActive(true);
        StartingText.SetActive(false);
        gameOverPanel.SetActive(false);

        // Tambahkan listener untuk PlayButton
        PlayButton.onClick.AddListener(OnPlayButtonPressed);
    }

    void Update()
    {
        // Jika game over, tampilkan panel game over
        if (gameOver)
        {
            Time.timeScale = 0;
            gameOverPanel.SetActive(true);
            Debug.Log("Game Over triggered");
        }

        // Jika menunggu tap dan pemain menekan layar
        if (isWaitingForTap && SwipeManager.tap)
        {
            isGameStarted = true;
            isWaitingForTap = false;
            StartingText.SetActive(true);
            Debug.Log("Game started by tap");
            Destroy(MainMenu, 1f);
        }
    }

    // Dipanggil saat PlayButton ditekan
    void OnPlayButtonPressed()
    {
        MainMenu.SetActive(false);
        StartingText.SetActive(true);
        isWaitingForTap = true; // Aktifkan indikator menunggu tap
    }
}
