using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamCanvas : MonoBehaviour
{
    public Canvas canvas;
    public Camera[] cam;

    private void Start()
    {
        cam[1].enabled = false;
        cam[2].enabled = false;
    }
    private void FixedUpdate()
    {
        if (cam[1].enabled == true)
            CamChange(1);
        if (cam[2].enabled == true)
            CamChange(2);
        if (cam[3].enabled == true)
            CamChange(3);
    }
    public void CamChange(int camnum)
    {
        canvas.worldCamera = cam[camnum];
    }
}
