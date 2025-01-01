using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public static float score = 0.0f;
    private float endScore = 0.0f;
    public Text scoreText;
    private PlayerController playerController;

    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
    }

    void Update()
    {
        if (PlayerManager.isGameStarted)
        {
            score += (playerController.forwardSpeed/10) * Time.deltaTime;
        }
        if (PlayerManager.gameOver)
        {
            endScore = score;
            score = 0;
        }
        scoreText.text = ((int)score).ToString();

    }
}
