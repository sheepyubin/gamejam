using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EyesImage : MonoBehaviour
{
    public Image EyeRenderer;
    public Sprite[] Eyes;
    public void changeImage(int eye)
    {
        EyeRenderer.sprite = Eyes[eye];
    }
}
