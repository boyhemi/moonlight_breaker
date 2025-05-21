using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleBlockAnimController : MonoBehaviour
{
    private BlockController blockController;
    private float animDuration;
    private float animFraction;

    private Vector3[] baseBlockScaleArray = new Vector3[2];

    private Vector3[] destinationBlockArray = new Vector3[2];

    public void Awake()
    {
        blockController = GetComponent<BlockController>();
    }

    public void SetAnim(bool state, float time)
    {
        animDuration = time;
        animFraction = 0;

        baseBlockScaleArray[0] = transform.localScale;
        // baseBlockScaleArray[1] = state ? blockController.currentlyScale: blockController.baseScale;
        destinationBlockArray[0] = state ? Vector3.one : new Vector3(0.6f, 0.6f, 0.6f);
        // destinationBlockArray[1] = state ? blockController.baseScale: blockController.currentlyScale;    
    }

    void AnimScale()
    {
        if (animFraction >= 1)
        {
            enabled = false;
        }

        animFraction += Time.deltaTime / animDuration;

        transform.localScale = Vector3.Lerp(baseBlockScaleArray[0], destinationBlockArray[0], animFraction);
        // blockController.ScaleTiles(Vector3.Lerp(baseBlockScaleArray[1], destinationBlockArray[1], animFraction));

    }

    // Update is called once per frame
    void Update()
    {
        AnimScale();
    }
}
