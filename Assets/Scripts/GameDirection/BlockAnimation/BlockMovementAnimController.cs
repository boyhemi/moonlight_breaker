using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class BlockMovementAnimController : MonoBehaviour
{
    private Vector3 movementStartPos;

    private Vector3 movementDestination;

    private float animDuration;
    private float animFraction;

    public void SetAnim(float time, Vector3 dir)
    {
        animDuration = time;
        animFraction = 0;
        movementStartPos = transform.position;
        movementDestination = dir;
    }

    void AnimateMovement()
    {
        if (animFraction >= 1)
        {
            transform.position = movementDestination;
            enabled = false;
        }

        animFraction += Time.deltaTime / animDuration;

        transform.position = Vector3.Lerp(movementStartPos, movementDestination, animFraction);
    }


    // Update is called once per frame
    void Update()
    {
        AnimateMovement();
    }
}
