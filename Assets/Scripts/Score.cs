using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public static float score = 0.0f;
    private float endScore;   
    public Text scoreText;
    public Text endScoreText;
    private PlayerController playerController;

    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
        score = 0.0f;
    }

    void Update()
    {
        if (PlayerManager.isGameStarted)
        {
            score += (playerController.forwardSpeed/10) * Time.deltaTime;
        }
        scoreText.text = ((int)score).ToString();
        endScoreText.text = ((int)score).ToString();

    }
}
