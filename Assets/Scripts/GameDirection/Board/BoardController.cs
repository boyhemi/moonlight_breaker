using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BoardController : MonoBehaviour
{

    public static BoardController init;

    public const int BOARD_SIZE = 9;

    public const int BLOCK_AMOUNT = 3;

    public const int BLOCK_COUNT = 7;


    public GameObject grid;
    public GameObject blockTile;
    public GameObject[] blockPrefabData = new GameObject[7];
    public Transform gameTransform;
    public Transform boardTransform;
    public Color boardColor;
    public Color highlightColor;

    [HideInInspector]
    public Vector3 boardScaleTile;
    [HideInInspector]
    public Vector3 finalScaleBlockTileScale;
    [HideInInspector]
    public SpriteRenderer[,] boardTileArray = new SpriteRenderer[BOARD_SIZE, BOARD_SIZE];
    [HideInInspector]
    public TileController[,] boardBlockArray = new TileController[BOARD_SIZE, BOARD_SIZE];
    [HideInInspector]
    public BlockController[] blockArray = new BlockController[BLOCK_COUNT];


    public bool IsInBoardRange(Vector2 b, Vector2 r)
    {
        return b.x >= -0.5f && r.x <= BOARD_SIZE - 0.5f &&
               b.y >= -0.5f && r.y <= BOARD_SIZE - 0.5f;
    }
    public bool IsBoardEmpty(BlockController bcont, Vector2 o)
    {
        for (int i = 0; i < bcont.blockStructure.Length; i++)
        {
            if (bcont.transform.GetChild(i).name == "Tile")
            {
                Vector2Int structureIndex = bcont.blockStructure[i];

                if (boardBlockArray[(int)o.x + structureIndex.x, (int)o.y + structureIndex.y])
                {
                    return false;
                }
            }
        }
        return true;
    }

    public static int Rng(int min, int max)
    {
        return (int)Random.Range(min, max - 0.000001f);
    }


    public void InitiateBlockMovement(int ib)
    {
        while (ib < BLOCK_AMOUNT - 1)
        {
            blockArray[ib] = blockArray[ib + 1];
            blockArray[ib].SetPosition(ib, false);
            blockArray[ib].MoveBlock(0.2f, blockArray[ib].basePos);
            blockArray[++ib] = null;
        }

        blockArray[ib] = SpawnManager.init.RevealBlocks(ib + 2, Rng(0, BLOCK_COUNT));
        blockArray[ib].SetPosition(ib, false);
        blockArray[ib].MoveBlock(0.2f, blockArray[ib].basePos);
    }

    public void DraggedBlocks()
    {
        BlockController bc = TouchController.init.draggedTeteromino;
        Vector2Int c = bc.GetInitialBlocks();

        for (int x = c.x; x < c.x + bc.blockSize.x; x++)
            BoardCheckerManager.init.CheckLines(BoardCheckerManager.LineType.Vertical, x, true);
        for (int y = c.y; y < c.y + bc.blockSize.y; y++)
            BoardCheckerManager.init.CheckLines(BoardCheckerManager.LineType.Horizontal, y, true);
    }

    private void InitializeBoard()
    {
        Vector3 resolutionData = ResolutionManager.GetBoardTileScale();

        for (int y = 0; y < BOARD_SIZE; y++)
        {
            for (int x = 0; x < BOARD_SIZE; x++)
            {
                Transform t = Instantiate(grid, boardTransform).transform;
                t.position = new Vector3(x,y, 0);
                t.localScale = resolutionData;
                boardTileArray[x, y] = t.GetComponent<SpriteRenderer>();
            }
        }

        for (int i = 0; i < BLOCK_AMOUNT; i++)
            // if (!DataManager.init.GetBoolean(i + "block"))
                blockArray[i] = SpawnManager.init.RevealBlocks(i, Rng(0, BLOCK_COUNT));

    }    
    

    void Awake()
    {
        if (!init)
            init = this;

        boardScaleTile = ResolutionManager.GetBoardTileScale();
        finalScaleBlockTileScale = ResolutionManager.GetScaledBlockTileScale();
        InitializeBoard();
    }
}
