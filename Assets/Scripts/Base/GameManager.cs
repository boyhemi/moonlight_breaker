using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public GameManager init;
    public GameObject settingsPanel;

    public bool isGameOver;

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

    void InitalizeBlocks()
    {
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
