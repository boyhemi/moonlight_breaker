using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeBlockAnimController : MonoBehaviour
{
    private float animDuration;
    private float animFraction;

    private Color currentColor;
    private Color newColor;

    public void SetAnim(float dir, Color col)
    {
        if (dir != 0.0f)
        {
            animDuration = dir;
            animFraction = 0.0f;
            currentColor = GetComponent<SpriteRenderer>().color;
            newColor = col;
        }
        else
        {
            GetComponent<SpriteRenderer>().color = col;
            enabled = false;
        }
        
    }

    void AnimationFade()
    {
        if (animFraction >1)
        {enabled = false;}

        animFraction += Time.deltaTime;

        GetComponent<SpriteRenderer>().color = Color.Lerp(currentColor, newColor, animFraction);
    }


    // Update is called once per frame
    void Update()
    {
        AnimationFade();
    }
}
