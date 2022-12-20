using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    public GameObject[] BossHitBox;
    public GameObject[] BossAttackBox;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowBossHitBox(int hit)
    {
        BossHitBox[hit].SetActive(true);
    }
    public void ShowBossAttackBox(int attack)
    {
        BossHitBox[attack].SetActive(false);
        BossAttackBox[attack].SetActive(true);
        Invoke("notBossAttackBox", 0.1f);
    }
    public void notBossAttackBox()
    {
        BossAttackBox[0].SetActive(false);
        BossAttackBox[1].SetActive(false);
        BossAttackBox[2].SetActive(false);
        BossAttackBox[3].SetActive(false);
    }
}
