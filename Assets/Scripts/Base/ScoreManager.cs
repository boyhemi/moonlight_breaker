using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class ScoreManager : MonoBehaviour
{

    public static ScoreManager init;
    public TextMeshProUGUI currentScoreText;
    public TextMeshProUGUI highScoreText;

    private int score = 0;
    private int highScore = 0;
    // Start is called before the first frame update

    void Awake()
    {
        if (!init)
        {
            init = this;
        }
    }
    void Start()
    {
        LoadHighScore();
    }

    void LoadHighScore()
    {
        if (String.IsNullOrEmpty(DataManager.init.GetInt(DataConstants.HIGH_SCORE).ToString()))
        {
            DataManager.init.SetInt(DataConstants.HIGH_SCORE, 0);
            highScoreText.text = DataManager.init.GetInt(DataConstants.HIGH_SCORE).ToString();
        }
        else
        {
            highScoreText.text = DataManager.init.GetInt(DataConstants.HIGH_SCORE).ToString();
        }
        currentScoreText.text = score.ToString();
    }

    public void IncrementScore(int cleared, int line)
    {
        int pts = (BoardController.BOARD_SIZE + cleared / 5) * line;
        pts += (int)(pts * (line / 3.0f - 0.333f));

        score += pts;
        currentScoreText.text = score.ToString();
        DataManager.init.SetInt(DataConstants.CURRENT_SCORE, score);

        highScore = DataManager.init.GetInt(DataConstants.HIGH_SCORE);
        if (score > highScore)
        {
            DataManager.init.SetInt(DataConstants.HIGH_SCORE, score);
            highScore = DataManager.init.GetInt(DataConstants.HIGH_SCORE);
            highScoreText.text = highScore.ToString();
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
