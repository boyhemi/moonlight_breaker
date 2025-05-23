using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonState : MonoBehaviour
{
    public enum ButtonStateGraphic { toggled, untoggled }
    public Sprite[] selectedImage;
    public Image currentButtonImage;
    public void SetButtonGraphic(int toggle)
    {
        if (toggle == (int)ButtonStateGraphic.toggled)
        {
            currentButtonImage.sprite = selectedImage[toggle];
        }
        else
        {
            currentButtonImage.sprite = selectedImage[toggle];
        }

    }

    public void Awake()
    {
        
    }


}
