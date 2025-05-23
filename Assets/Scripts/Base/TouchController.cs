using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TouchController : MonoBehaviour
{
   public static TouchController init;

    [HideInInspector]
    public Vector3 lastTouchPos;
    [HideInInspector]
    public BlockController draggedTeteromino;

    private ScreenOrientation screenOrientation;

    private Vector3 startPos;
    private Vector2Int lastPos = new Vector2Int(-1, -1);
    private SpriteRenderer[] draggedTiles = new SpriteRenderer[9];

    public void ResetBlock()
    {
        if (draggedTeteromino)
        {
            MoveDraggedBlock();
            ResetDraggedBlock();
        }
    }

	private void Awake()
	{
        if (!init)
            init = this;
    }

	private void Update()
	{
        if (screenOrientation != Screen.orientation)
            ResetBlock();

        if (Input.touchCount > 0)
        {
            Touch ts = Input.GetTouch(0);
            
            switch (ts.phase)
            {
                //// Began Touch
                case TouchPhase.Began:
                    Ray ray = Camera.main.ScreenPointToRay(ts.position);
                    RaycastHit hit;

                    if (Physics.Raycast(ray, out hit, 15))
                    {
                        screenOrientation = Screen.orientation;

                        Collider b = hit.collider;

                        if (b.tag == "Teteromino" && b.GetComponent<BlockController>().isMovable && !b.GetComponent<BlockController>().IsBlockMoving())
                        {
                            draggedTeteromino = b.GetComponent<BlockController>();

                            draggedTeteromino.BlockScaler(true, 0.2f);

                            Color cl = draggedTeteromino.initalColor; cl.a = 0.66f;
                            draggedTeteromino.ChangeBlockColor(cl);

                            Vector3 p = draggedTeteromino.transform.position;
                            startPos = Camera.main.ScreenToWorldPoint(ts.position);
                            startPos = new Vector3(startPos.x - p.x, startPos.y - p.y, 0);
                        }
                    }
                    break;
                /// moved touch
                case TouchPhase.Moved:
                    if (!draggedTeteromino)
                        return;
                    Vector3 pos = Camera.main.ScreenToWorldPoint(ts.position);
                    pos = new Vector3(pos.x, pos.y, -2);

                    draggedTeteromino.transform.position = pos - startPos;

                    Vector3 size = draggedTeteromino.GetComponent<BlockController>().blockSize;
                    size = new Vector3(size.x - 1, size.y - 1, 0);

                    Vector2 origin = draggedTeteromino.transform.GetChild(0).position;
                    Vector2 end = draggedTeteromino.transform.GetChild(0).position + size;

                    if (IsInTouchRange(origin, end) && IsEmpty(draggedTeteromino, RoundVector2(origin)))
                    {
                        Vector2Int start = RoundVector2(origin);

                        if (lastPos != start)
                        {
                            RemoveAllHighlights();
                            BoardController.init.DraggedBlocks();

                            for (int i = 0; i < draggedTeteromino.blockStructure.Length; i++)
                            {
                                if (draggedTeteromino.transform.GetChild(i).name == "Tile")
                                {
                                    Vector2Int initBlocks = draggedTeteromino.blockStructure[i];
                                    draggedTiles[i] = BoardController.init.boardTileArray[start.x + initBlocks.x, start.y + initBlocks.y];
                                    draggedTiles[i].color = BoardController.init.highlightColor;
                                }
                            }
                        }

                        lastPos = start;
                    }
                    else
                    {
                        RemoveAllHighlights();
                        lastPos = new Vector2Int(-1, -1);
                    }
                    break;
                // ended touch
                case TouchPhase.Ended:
                    if (draggedTeteromino)
                    {
                        Vector3 dragSize = draggedTeteromino.blockSize;
                        dragSize = new Vector3(dragSize.x - 1, dragSize.y - 1, 0);

                        Vector2 origins = draggedTeteromino.transform.GetChild(0).position;
                        Vector2 ends = draggedTeteromino.transform.GetChild(0).position + dragSize;

                        // Added In Board
                        if (IsInTouchRange(origins, ends) && IsEmpty(draggedTeteromino, RoundVector2(origins)))
                        {
                            lastTouchPos = BlockPosition(origins, dragSize);

                            draggedTeteromino.MoveBlock(0.08f, lastTouchPos);
                            draggedTeteromino.ChangeBlockColor(draggedTeteromino.initalColor);
                            draggedTeteromino.GetComponent<BoxCollider>().enabled = false;
                            draggedTeteromino.enabled = false;

                            Vector2Int start = RoundVector2(origins);

                            for (int i = 0; i < draggedTeteromino.blockStructure.Length; i++)
                            {
                                Vector2Int initalBlocks = draggedTeteromino.blockStructure[i];

                                if (draggedTeteromino.transform.GetChild(i).name == "Tile")
                                {
                                    TileController b = draggedTeteromino.transform.GetChild(i).GetComponent<TileController>();
                                    BoardController.init.boardBlockArray[start.x + initalBlocks.x, start.y + initalBlocks.y] = b;
                                }
                            }

                            AudioManager.init.PlaySFXAudio((int)AudioManager.sfxlips.UI_TAP);

                            BoardController.init.InitiateBlockMovement(draggedTeteromino.posId);
                            BoardCheckerManager.init.VaildateBoard();
                        }
                        
                        else
                        {
                            MoveDraggedBlock();
                        }

                        ResetDraggedBlock();
                    }

                    break;
            }
        }
	}

    private Vector2Int RoundVector2(Vector2 v)
    {
        return new Vector2Int((int)(v.x + 0.5f), (int)(v.y + 0.5f));
    }

    private Vector3 BlockPosition(Vector2 o, Vector2 s)
    {
        Vector3 off = Vector3.zero;

        if (s.x % 2 == 1)
            off.x = 0.5f;
        if (s.y % 2 == 1)
            off.y = 0.5f;

        return new Vector3((int)(o.x + 0.5f) + (int)(s.x / 2), (int)(o.y + 0.5f) + (int)(s.y / 2), -1) + off;
    }

    private bool IsInTouchRange(Vector2 o, Vector2 e)
    {
        return BoardController.init.IsInBoardRange(o, e);
    }

    private bool IsEmpty(BlockController bc, Vector2 o)
    {
        return BoardController.init.IsBoardEmpty(bc, o);
    }

    private void RemoveAllHighlights()
    {
        // if (!GameManager.ins.gameOver)
            foreach (TileController tc in BoardController.init.boardBlockArray)
                if (tc)
                    tc.FadeTile(0.2f, tc.initColor);

        for (int i = 0; i < 9; i++)
        {
            if (draggedTiles[i])
            {
                draggedTiles[i].color = BoardController.init.boardColor;
                draggedTiles[i] = null;
            }
        }
    }
	
	private void OnApplicationPause(bool isPaused)
	{
        if (isPaused)
            ResetBlock();
	}
	
    private void MoveDraggedBlock()
    {
        draggedTeteromino.BlockScaler(false, 0.2f);
        draggedTeteromino.MoveBlock(0.25f, draggedTeteromino.basePos);
        draggedTeteromino.ChangeBlockColor(draggedTeteromino.initalColor);
    }

    private void ResetDraggedBlock()
    {
        startPos = Vector3.zero;
        draggedTeteromino = null;
        RemoveAllHighlights();
    }
}
