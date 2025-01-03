using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static bool gameOver;
    public GameObject gameOverPanel;

    public static bool isGameStarted;

    public GameObject StartingText;
    public GameObject Score;

    public static int numberOfCoins;

    // Background music
    public AudioClip backgroundMusic;
    public AudioClip startSound;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        gameOver = false;
        Time.timeScale = 1;
        isGameStarted = false;
        numberOfCoins = 0;
        Score.SetActive(false);

        // Get the AudioSource component and configure it
        audioSource = GetComponent<AudioSource>();
        if (audioSource != null && backgroundMusic != null)
        {
            audioSource.clip = backgroundMusic;
            audioSource.loop = true;
            audioSource.Play();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (gameOver)
        {
            Time.timeScale = 0;
            gameOverPanel.SetActive(true);
            Score.SetActive(false);
        }

        if (SwipeManager.tap && !isGameStarted)
        {
            isGameStarted = true;
            audioSource.PlayOneShot(startSound);
            Score.SetActive(true);
            Destroy(StartingText);
        }
    }
}