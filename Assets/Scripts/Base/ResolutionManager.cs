using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Animation;
using UnityEngine;
using UnityEngine.UI;

public class ResolutionManager : MonoBehaviour
{
 public const float TABLET_ASPECT_RATIO = 1.4f;
    public const float TABLET_HALF_WIDTH = 6.5f;
    public const float HALF_WIDTH = 5.35f;
    public const float BLOCK_SIZE = 1.2f;
    public const float BOARD_TILE_DISTANCE = 1f;
    public const float SCALED_BLOCK_TILE_DISTANCE = 1;
    public const float SCALED_BLOCK_SCALE = 0.6f;
    public const int SCORE_ICON_HEIGHT = 134;

    public static CanvasScaler mainCanvas;

    private ScreenOrientation screenOrientation;

    public static float GetAspectRatio()
    {
        return (float)Screen.height / Screen.width;
    }

    public static float GetOrthographicSize()
    {
        float ar = GetAspectRatio();
        return ar > TABLET_ASPECT_RATIO ? ar * HALF_WIDTH : ar * TABLET_HALF_WIDTH;
    }

    public static float ScreenToWorld(float y)
    {
        return y / Screen.height * GetOrthographicSize() * 2;
    }

    public static int WorldToScreen(float y)
    {
        float fs = GetOrthographicSize() * 2;
        return (int)((y - Camera.main.transform.position.y + fs / 2) / fs * Screen.height);
    }

    public static int GetScoreY()
    {
        float boardY = BoardController.BOARD_SIZE - BLOCK_SIZE / 2;
        return Screen.height - (Screen.height - WorldToScreen(boardY)) / 2; 
    }

    public static float GetBlockY()
    {
        return (-GetOrthographicSize() + Camera.main.transform.position.y - BLOCK_SIZE / 2) / 2;
    }

    public static Vector3 GetBoardTileScale()
    {
        float scale = BLOCK_SIZE - ScreenToWorld(BOARD_TILE_DISTANCE);
        return new Vector3(scale, scale, scale);
    }

    public static Vector3 GetScaledBlockTileScale()
    {
        float scale = BLOCK_SIZE - ScreenToWorld(SCALED_BLOCK_TILE_DISTANCE) / SCALED_BLOCK_SCALE;
        return new Vector3(scale, scale, scale);
    }

    public static Vector2 GetReferenceResolution()
    {
        float aspectRatio = (float)mainCanvas.referenceResolution.x/ mainCanvas.referenceResolution.y;
        return new Vector2(720, (int)(720 * aspectRatio));
    }

    public static void SetPositionY(RectTransform rt, int y)
    {
        Vector3 pos = rt.position;
        rt.position = new Vector3(pos.x, y, pos.z);
    }

    private void Awake()
	{
        StartCoroutine(SetResolution());
    }

    private void Update()
    {
        if (screenOrientation != Screen.orientation)
            StartCoroutine(SetResolution(0.1f));
    }

    private IEnumerator SetResolution(float s = 0)
    {
        screenOrientation = Screen.orientation;

        yield return new WaitForSeconds(s);

        Camera.main.orthographicSize = GetOrthographicSize();

        // int y = GetScoreY();
        
        // SetPositionY(GameManager.ins.bestScoreIconLayer.GetComponent<RectTransform>(), y);
        // SetPositionY(GameManager.ins.scoreLayer.GetComponent<RectTransform>(), y);

        float blockY = GetBlockY();
        for (int i = 0; i < BoardController.BLOCK_COUNT; i++)
        {
            if (BoardController.init.blockArray[i])
            {
                Vector3 p = BoardController.init.blockArray[i].transform.position;

                p = new Vector3(p.x, blockY, p.y);
                BoardController.init.blockArray[i].transform.position = p;
            }
        }

    }
}
