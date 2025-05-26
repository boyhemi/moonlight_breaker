using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class DestructionController : MonoBehaviour
{

    public static DestructionController init;

    [HideInInspector]
    public int linesDestroyed;

    private BlockDestructionAnimController[,] brokenBlockAnimations = new BlockDestructionAnimController[BoardController.BOARD_SIZE, BoardController.BOARD_SIZE];

    private BoardController bc;

    private Vector2Int[] destroyedLinesPos = new Vector2Int[BoardController.BOARD_SIZE];

    // Initialize destuction logic
    public void InitiateDestruction()
    {
        linesDestroyed = 0;
        for (int i = 0; i < BoardController.BOARD_SIZE; i++)
        {
            destroyedLinesPos[i] = new Vector2Int(-1, -1);
        }
    }


    public void StartDestruction(int i, bool e)
    {
        if (e)
        {
            for (int a = 0; a < BoardController.BOARD_SIZE; a++)
                brokenBlockAnimations[i, a] = bc.boardBlockArray[i, a].GetComponent<BlockDestructionAnimController>();
        }
        else
        {
            for (int b = 0; b < BoardController.BOARD_SIZE; b++)
                brokenBlockAnimations[b, i] = bc.boardBlockArray[b, i].GetComponent<BlockDestructionAnimController>();
            
        }

        linesDestroyed++;

        for (int c = 0; c < BoardController.BOARD_SIZE; c++)
        {
            if (destroyedLinesPos[c] == new Vector2Int(-1, -1))
            {
                destroyedLinesPos[c] = e ? new Vector2Int(i, -1) : new Vector2Int(-1, i);
                break;
            }
        }
    }

    public IEnumerator AllBlocksDestruction()
    {
        AudioManager.init.PlaySFXAudio((int)AudioManager.sfxlips.DESTROYED_BLOCK);

        for (int a = 0; a < BoardController.BOARD_SIZE; a++)
        {
            for (int b = 0; b < BoardController.BOARD_SIZE; b++)
            {
                if (destroyedLinesPos[b] == new Vector2Int(-1, -1))
                    break;
                int bindex = BoardController.BOARD_SIZE - a - 1;
                Vector2Int dpos = destroyedLinesPos[b];

                if (dpos.x != -1 && brokenBlockAnimations[dpos.x, bindex] && !brokenBlockAnimations[dpos.x, bindex].enabled)
                    bc.boardBlockArray[dpos.x, bindex] = null;
                else if (dpos.y != -1 && brokenBlockAnimations[a, dpos.y] && !brokenBlockAnimations[a, dpos.y].enabled)
                    bc.boardBlockArray[a, dpos.y] = null;

            }
        }

        ScoreManager.init.IncrementScore(SpawnManager.init.GetEmptyBlockData(), linesDestroyed);
        BoardCheckerManager.init.VaildateBoardSpace(false);

        for (int c = 0; c < BoardController.BOARD_SIZE; c++)
        {
            for (int d = 0; d < BoardController.BOARD_SIZE; d++)
            {

                if (destroyedLinesPos[d] == new Vector2Int(-1, -1))
                    break;

                int bindex = BoardController.BOARD_SIZE - c - 1;
                Vector2Int dpos = destroyedLinesPos[d];
                if (dpos.x != -1 && brokenBlockAnimations[dpos.x, bindex] && !brokenBlockAnimations[dpos.x, bindex].enabled)
                {
                    brokenBlockAnimations[dpos.x, bindex].enabled = true;
                    brokenBlockAnimations[dpos.x, bindex].SetAnim(0.25f);
                    brokenBlockAnimations[dpos.x, bindex] = null;
                }
                else if (dpos.y != -1 && brokenBlockAnimations[c, dpos.y] && !brokenBlockAnimations[c, dpos.y].enabled)
                {
                    brokenBlockAnimations[c, dpos.y].enabled = true;
                    brokenBlockAnimations[c, dpos.y].SetAnim(0.25f);
                    brokenBlockAnimations[c, dpos.y] = null;
                }
            }

            yield return new WaitForSeconds(0.025f);
        }
    }

    public void ClearBlocks()
    {
        int ranBoard = (int)Random.Range(0, BoardController.BOARD_SIZE - 2.001f);
        if (BoardController.init.blockArray[0].blockSize.x >= BoardController.init.blockArray[0].blockSize.y)
        {
            for (int y = ranBoard; y < ranBoard + 3; y++)
            {
                for (int x = 0; x < BoardController.BOARD_SIZE; x++)
                {
                    TileController tc = BoardController.init.boardBlockArray[x, y];
                    if (tc)
                    {
                        tc.DestroyTile(0.25f);
                    }

                    BoardController.init.boardBlockArray[x, y] = null;
                }
            }
        }
        else
        {
            for (int x = ranBoard; x < ranBoard + 3; x++)
            {
                for (int y = 0; y < BoardController.BOARD_SIZE; y++)
                {
                    TileController tc = BoardController.init.boardBlockArray[x, y];
                    if (tc)
                    {
                        tc.DestroyTile(0.25f);
                    }

                    BoardController.init.boardBlockArray[x, y] = null;
                }
            }
        }
        BoardCheckerManager.init.VaildateBoard();
    }


    void Awake()
    {

        if (!init)
        {
            init = this;
        }

        bc = GetComponent<BoardController>();


    }
}
