using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager init;

    public void Awake()
    {
        if (!init)
            init = this;
    }

    public TileController SpawnTiles(int x, int y)
    {
        BoardController.init.boardBlockArray[x, y] = Instantiate(BoardController.init.grid, BoardController.init.boardTransform).GetComponent<TileController>();
        Vector3 vectorPos = new Vector3(x, y, -1);
        BoardController.init.boardBlockArray[x, y].transform.position = vectorPos;
        BoardController.init.boardBlockArray[x, y].transform.localScale = BoardController.init.boardScaleTile;

        return BoardController.init.boardBlockArray[x, y];
    }


    public BlockController RevealBlocks(int i, int p)
    {
        BlockController initBlocks = Instantiate(BoardController.init.blockPrefabData[p], BoardController.init.gameTransform).GetComponent<BlockController>();
        initBlocks.SetPosition(i);
        return initBlocks;
    }

    public int GetEmptyBlockData()
    {
        int block = 0;
        foreach (TileController tile in BoardController.init.boardBlockArray)
            if (!tile)
                block++;
        return block;
    }    
    
}
