using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class BoardCheckerManager : MonoBehaviour
{

    public enum LineType {Horizontal = 0, Vertical = 1}
    public static BoardCheckerManager init;

    void Awake()
    {
        if (!init)
            init = this;
    }

    // Checks blocks if it's the range of the board.
    public bool BlockChecker(int bc)
    {
        for (int y = 0; y < BoardController.BOARD_SIZE; y++)
        {
            for (int x = 0; x < BoardController.BOARD_SIZE; x++)
            {
                Vector2 size = new Vector2(BoardController.init.blockArray[bc].blockSize.x - 1, BoardController.init.blockArray[bc].blockSize.y - 1);
                Vector2 origin = new Vector2(x, y);
                Vector2 end = origin + size;

                if (BoardController.init.IsInBoardRange(origin, end) && BoardController.init.IsBoardEmpty(BoardController.init.blockArray[bc], origin))
                    return true;
            }
        }

        return false;
    }

// Validates available board space 
    public void VaildateBoardSpace(bool isValidated)
    {
        int spaceCount = 0;
        for (int y = 0; y < BoardController.BLOCK_AMOUNT; y++)
        {

            if (BlockChecker(y))
            {
                BoardController.init.blockArray[y].isMovable = true;
                Color col = BoardController.init.blockArray[y].GetBlockColor();
                col.a = 1;
                BoardController.init.blockArray[y].ChangeBlockColor(col);
            }
            else
            {
                BoardController.init.blockArray[y].isMovable = false;
                spaceCount++;
                Color col = BoardController.init.blockArray[y].GetBlockColor();
                col.a = 0.5f;
                BoardController.init.blockArray[y].ChangeBlockColor(col);
            }

            //TO DO GAME OVER

            if (isValidated && spaceCount == BoardController.BLOCK_AMOUNT)
            {
                GameManager.init.GameOver();
            }
            else if (spaceCount == BoardController.BLOCK_AMOUNT)
                GameManager.init.GameOver();


        }
    }

    public void VaildateBoard(bool isVaildated = false)
    {
        DestructionController.init.InitiateDestruction();

        for (int x = 0; x < BoardController.BOARD_SIZE; x++)
            CheckLines(LineType.Vertical, x);
        for (int y = 0; y < BoardController.BOARD_SIZE; y++)
            CheckLines(LineType.Horizontal, y);

        if (DestructionController.init.linesDestroyed > 0)
            StartCoroutine(DestructionController.init.AllBlocksDestruction());
        else
            VaildateBoardSpace(isVaildated);
    }



    // Checks if line is horizontal or vertical
    public void CheckLines(LineType line, int a, bool isVaildated = false)
    {
        switch (line)
        {
            case LineType.Vertical:
                if (isVaildated)
                {
                    TileController[,] tc = new TileController[BoardController.BOARD_SIZE, BoardController.BOARD_SIZE];
                    Array.Copy(BoardController.init.boardBlockArray, tc, BoardController.init.boardBlockArray.Length);

                    BlockController bc = TouchController.init.draggedTeteromino;
                    Vector2Int ib = bc.GetInitialBlocks();

                    for (int x = 0; x < bc.blockStructure.Length; x++)
                    {
                        if (bc.transform.GetChild(x).name == "Tile")
                        {
                            TileController tcb = bc.transform.GetChild(x).GetComponent<TileController>();
                            tc[ib.x + bc.blockStructure[x].x, ib.y + bc.blockStructure[x].y] = tcb;
                        }
                    }

                    for (int i = 0; i < BoardController.BOARD_SIZE; i++)
                    {
                        if (!tc[a, i])
                        {
                            return;
                        }
                    }

                    for (int i = 0; i < BoardController.BOARD_SIZE; i++)
                    {
                        if (BoardController.init.boardBlockArray[a, i])
                        {
                            BoardController.init.boardBlockArray[a, i].FadeTile(0.2f, bc.initalColor);
                        }
                    }

                }
                else
                {
                    for (int i = 0; i < BoardController.BOARD_SIZE; i++)
                    {
                        if (!BoardController.init.boardBlockArray[a, i])
                        {
                            return;
                        }
                    }

                    DestructionController.init.StartDestruction(a, true);
                }
            break;

            case LineType.Horizontal:
                if (isVaildated)
                {
                    TileController[,] tc = new TileController[BoardController.BOARD_SIZE, BoardController.BOARD_SIZE];
                    Array.Copy(BoardController.init.boardBlockArray, tc, BoardController.init.boardBlockArray.Length);

                    BlockController bc = TouchController.init.draggedTeteromino;
                    Vector2Int ic = bc.GetInitialBlocks();
                    for (int i = 0; i < bc.blockStructure.Length; i++)
                    {
                        if (bc.transform.GetChild(i).name == "Tile")
                        {
                            TileController tcb = bc.transform.GetChild(i).GetComponent<TileController>();
                            tc[ic.x + bc.blockStructure[i].x, ic.y + bc.blockStructure[i].y] = tcb;
                        }
                    }

                    for (int x = 0; x < BoardController.BOARD_SIZE; x++)
                        if (!tc[x, a])
                            return;

                    for (int x = 0; x < BoardController.BOARD_SIZE; x++)
                        if (BoardController.init.boardBlockArray[x, a])
                            BoardController.init.boardBlockArray[x, a].FadeTile(0.2f, bc.initalColor);
                }
                else
                {
                    for (int x = 0; x < BoardController.BOARD_SIZE; x++)
                        if (!BoardController.init.boardBlockArray[x, a])
                            return;

                    DestructionController.init.StartDestruction(a, false);
                }

            break;
            
        }
       
    }



}
