using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockFallAnimController : MonoBehaviour
{

    public enum FallDirection {Left, Right};

    public enum FallState {Up, Down}

    private float animDuration;

    private float[] animFractionArray = new float[2];

    private FallState fallState;
    
    private Vector3 basePosition;

    private Vector3 animRotation;

    private Vector3[] animPositions = new Vector3[2];

    public void SetAnim(float time, FallDirection dir)
    {
        animDuration = time;
        animFractionArray[0] = animFractionArray[1] = 0;
        fallState = FallState.Up;
        basePosition = transform.position;

        animRotation = new Vector3(0, 0, dir == FallDirection.Left ? 45 : -45);

        float x = dir == FallDirection.Left ? -0.25f : 0.25f;
        Vector3 p = basePosition;

        animPositions[0] = new Vector3(p.x + x, p.y + 0.3f, p.z);
        animPositions[1] = new Vector3(p.x + x * 2.5f, p.y - 20, p.z);
    }

    void AnimFall()
    {
        if (animFractionArray[0] >= 1)
        {
            if (fallState == FallState.Up)
            {
                animFractionArray[0] = 0;
                fallState = FallState.Down;
                basePosition = animPositions[0];
            }
            else
            {
                enabled = false;
            }
        }

        if (fallState == FallState.Up)
            animFractionArray[0] += Time.deltaTime / animDuration / 0.1f;
        else
            animFractionArray[0] += Time.deltaTime / animDuration / 0.9f;
        animFractionArray[1] += Time.deltaTime / animDuration / 0.5f;

        transform.position = Vector3.Lerp(basePosition, animPositions[(int)fallState], animFractionArray[0]);
        transform.eulerAngles = Vector3.Lerp(Vector3.zero, animRotation, animFractionArray[1]);
    }

    // Update is called once per frame
    void Update()
    {
        AnimFall();
    }
}
