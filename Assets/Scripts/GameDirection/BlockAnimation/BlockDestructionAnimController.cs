using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;

public class BlockDestructionAnimController : MonoBehaviour
{
    public Sprite brokenBlock;

    public Vector3 desRotation = new Vector3(0,0, 360);

    public Vector3 desScale = new Vector3(0,0,0);

    public Color desColor = new Color(1,1,1,1);

    private float animDuration;
    private float animFraction;


    public void SetAnim(float time)
    {
        animDuration = time;
        animFraction = 0;
    }

// This will animate the destruction of block when the conditions are met.
    void AnimateDestruction()
    {
        if (animFraction >= 1)
        {
            if (transform.parent.childCount > 0)
                Destroy(gameObject);
            else
                Destroy(transform.parent.gameObject);
        }

        animFraction =+ Time.deltaTime / animDuration;

        transform.eulerAngles = Vector3.Lerp(new Vector3(0,0,0),desRotation, animFraction);
        transform.localScale = Vector3.Lerp(new Vector3(0,0,0),desScale, animFraction);
        
    
    }
    void Update()
    {
        AnimateDestruction();
    }

}
