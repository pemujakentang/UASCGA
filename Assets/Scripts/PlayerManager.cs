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
    // Start is called before the first frame update
    void Start()
    {
        gameOver = false;
        Time.timeScale = 1;
        isGameStarted = false;
        numberOfCoins = 0;
        Score.SetActive(false);
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
        
        if(SwipeManager.tap && !isGameStarted)
        {
            isGameStarted = true;
            Score.SetActive(true);
            Destroy(StartingText);
        }
    }
}
