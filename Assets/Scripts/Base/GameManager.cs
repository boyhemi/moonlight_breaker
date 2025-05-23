using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager init;
    public GameObject settingsPanel, settingBG, gameOver;

    public TextMeshProUGUI currentScoreText, highScoreText;

    private bool isGameOver;

    void Awake()
    {
        if (init == null)
        {
            init = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        InitalizeBlocks();

    }

    void InitalizeBlocks(bool isResetBlocks = false)
    {
        if (isResetBlocks)
        {
            for (int a = 0; a <  BoardController.BOARD_SIZE; a++)
            {
                for (int b = 0; b < BoardController.BOARD_SIZE; b++)
                {
                    if (BoardController.init.boardBlockArray[b, a])
                    {
                        Destroy(BoardController.init.boardBlockArray[b, a].gameObject);
                        BoardController.init.boardBlockArray[b, a] = null;
                    }
                }
            }
        }
        
        for (int i = 0; i < BoardController.BLOCK_AMOUNT; i++)
        {
            Destroy(BoardController.init.blockArray[i].gameObject);
            int x = BoardController.Rng(0, BoardController.BLOCK_AMOUNT);
            BoardController.init.blockArray[i] = SpawnManager.init.RevealBlocks(i, x);
        }
    }

    public void OpenSettings(bool isOpen)
    {
        settingsPanel.SetActive(isOpen);
        settingBG.SetActive(isOpen);
    }

    public void GameOver()
    {
        ShowScores();
        gameOver.SetActive(true);
        isGameOver = true;


        for (int a = BoardController.BOARD_SIZE - 1; a >= 0; a--)
        {
            for (int x = 0; x < BoardController.BOARD_SIZE; x++)
            {
                TileController tc = BoardController.init.boardBlockArray[x, a];
                if (tc)
                    tc.FadeTile(0.25f, new Color(0.09f, 0.122f, 0.153f));

                if (x % 2 == 0)
                {

                }
            }
        }
    }

    void ShowScores()
    {
        currentScoreText.text = ScoreManager.init.currentScoreText.text;
        highScoreText.text = ScoreManager.init.highScoreText.text;
        AudioManager.init.PlayBGMAudio((int)AudioManager.bgmClip.GAME_OVER);
    }


    public void RetryButton()
    {
        isGameOver = false;
        if (DataManager.init.GetBoolean(DataConstants.BGM_STATE))
        {
            AudioManager.init.PlayBGMAudio((int)AudioManager.bgmClip.PLAYING);
        }
        InitalizeBlocks(true);
        gameOver.SetActive(false);


    }


    public void PauseGame(bool isPaused)
    {
        if (isPaused)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
