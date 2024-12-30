using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public static float score = 0.0f;
    public Text scoreText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerManager.isGameStarted)
        {
            score += Time.deltaTime;
        }
        if (PlayerManager.gameOver)
        {
            score = 0;
        }
        scoreText.text = ((int)score).ToString();

    }
}
