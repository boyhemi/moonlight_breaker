using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class ScoreManager : MonoBehaviour
{

    public static ScoreManager init;
    public TextMeshProUGUI currentScore;
    public TextMeshProUGUI highScore;
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
            highScore.text = DataManager.init.GetInt(DataConstants.HIGH_SCORE).ToString();
        }
        currentScore.text = "0";
    }

    public void IncrementScore(int boardCountDeleted, int line)
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
