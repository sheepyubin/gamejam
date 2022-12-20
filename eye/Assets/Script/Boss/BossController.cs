using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    public GameObject[] BossHitBox;
    public GameObject[] BossAttackBox;
    public GameObject BossSkill;
    public GameObject[] BossSkillspawn;

    public Animator anime;
    int ani = 0;
    // Start is called before the first frame update

    void Start()
    {
        anime = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            ani++;
            Debug.Log(ani);
            anime.SetInteger("count", ani);
        }
        if (ani >= 5)
        {
            ani = 0;
        }
        if (Input.GetKeyDown(KeyCode.B))
            anime.SetInteger("count", 3);
        if (Input.GetKeyDown(KeyCode.N))
            anime.SetInteger("count", 4);
    }

    public void ShowBossHitBox(int hit)
    {
        BossHitBox[hit].SetActive(true);
    }
    public void ShowBossAttackBox(int attack)
    {
        BossHitBox[attack].SetActive(false);
        BossAttackBox[attack].SetActive(true);
    }
    public void notBossAttackBox(int attack)
    {
        BossAttackBox[attack].SetActive(false);
    }

    public void BossSkillProduce(float y)
    {
        Instantiate(BossSkill, new Vector3(y, 6f, 0), Quaternion.identity);
    }
}
