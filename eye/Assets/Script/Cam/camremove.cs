using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camremove : MonoBehaviour
{
    public Camera Cam1;
    public Camera Cam2;
    public Camera Cam3;
    private void Update()
    {
        transform.position = EyesImage.PlayerPos;
    }
    void OnTriggerEnter2D(Collider2D collision) //카메라 전환 (스테이지 넘어 갈 때)
    {
        if (collision.gameObject.tag == "Stage1")
        {
            ShowCam1View();
        }
        if (collision.gameObject.tag == "Stage2")
        {
            ShowCam2View();
        }
        if (collision.gameObject.tag == "Stage3")
        {
            ShowCam3View();
        }
    }
    public void ShowCam1View()
    {
        Cam1.enabled = true;
        Cam2.enabled = false;
        Cam3.enabled= false;
        Debug.Log("1");
    }
    public void ShowCam2View()
    {
        Cam1.enabled = false;
        Cam2.enabled = true;
        Cam3.enabled = false;
        Debug.Log("3");
    }
    public void ShowCam3View()
    {
        Cam1.enabled = false;
        Cam2.enabled = false;
        Cam3.enabled = true;
        Debug.Log("4");
    }
}
