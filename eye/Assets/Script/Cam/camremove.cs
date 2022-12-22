using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camremove : MonoBehaviour
{
    public Camera Cam1;
    public Camera Cam2;
    public Camera Cam3;
    public Camera Cam4;
    private void Update()
    {
        transform.position = EyesImage.PlayerPos;
    }
    void OnTriggerEnter2D(Collider2D collision) //카메라 전환 (스테이지 넘어 갈 때)
    {
        if (collision.gameObject.tag == "Stage1")
        {
            ShowCam1View();
            MonsterSpawn.State = 1;
            Debug.Log(MonsterSpawn.State);
        }
        if (collision.gameObject.tag == "Stage2")
        {
            ShowCam2View();
            MonsterSpawn.State = 2;
        }
        if (collision.gameObject.tag == "Stage3")
        {
            ShowCam3View();
            MonsterSpawn.State = 3;
        }
        if (collision.gameObject.tag == "BossStage")
        {
            ShowCamBossView();
            MonsterSpawn.State = 4;
        }
    }
    public void ShowCam1View()
    {
        Cam1.enabled = true;
        Cam2.enabled = false;
        Cam3.enabled= false;
        Cam4.enabled= false;
        Debug.Log("1");
    }
    public void ShowCam2View()
    {
        Cam1.enabled = false;
        Cam2.enabled = true;
        Cam3.enabled = false;
        Cam4.enabled = false;
        Debug.Log("2");
    }
    public void ShowCam3View()
    {
        Cam1.enabled = false;
        Cam2.enabled = false;
        Cam3.enabled = true;
        Cam4.enabled = false;
        Debug.Log("3");
    }
    public void ShowCamBossView()
    {
        Cam1.enabled = false;
        Cam2.enabled = false;
        Cam3.enabled = false;
        Cam4.enabled = true;
        Debug.Log("4");
    }
}
