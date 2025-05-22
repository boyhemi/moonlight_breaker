using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BoardController : MonoBehaviour
{

    public BoardController init;

    public const int BOARD_SIZE = 10;

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

    // public TileController InitializeTile(int x, int y)
    // {

    // }


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
