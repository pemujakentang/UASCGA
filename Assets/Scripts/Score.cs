using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public static float score = 0.0f;
    private float endScore = 0.0f;
    public Text scoreText;

    void Start()
    {
        
    }

    void Update()
    {
        if (PlayerManager.isGameStarted)
        {
            score += Time.deltaTime;
        }
        if (PlayerManager.gameOver)
        {
            endScore = score;
            score = 0;
        }
        scoreText.text = ((int)score).ToString();

    }
}
