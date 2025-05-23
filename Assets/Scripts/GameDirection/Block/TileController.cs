using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileController : MonoBehaviour
{
    [HideInInspector]
    public Color initColor;

    private void OnEnable() {}

    public void FadeTile(float de, Color col)
    {
        FadeBlockAnimController animController = GetComponent<FadeBlockAnimController>();
        animController.enabled = true;
        animController.SetAnim(de, col);

    }

    public void FallTile(float de, BlockFallAnimController.FallDirection dir)
    {
        BlockFallAnimController animController = GetComponent<BlockFallAnimController>();
        animController.enabled = true;
        animController.SetAnim(de, dir);

    }

    public void DestroyTile(float de)
    {
        BlockDestructionAnimController animController = GetComponent<BlockDestructionAnimController>();
        animController.enabled = true;
        animController.SetAnim(de);

    }


    private void Awake()
    {
        initColor = GetComponent<SpriteRenderer>().color;   
    }
}
