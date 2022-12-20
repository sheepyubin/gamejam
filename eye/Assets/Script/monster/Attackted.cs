using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.VirtualTexturing.Debugging;

public class Attackted : MonoBehaviour
{
    new SpriteRenderer renderer;
    public float HP = 100.0f;
    public float damage = 0.0f;
    public float delay = 0.3f;
    const short temp = 4;
    int i = 0;
    float Btime;
    float Atime;
    bool IsTansparency;
    bool IsAttackted = false;

    private void OnTriggerStay2D(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "monster1":
                damage = 1.0f;
                attackted(damage);
                break;
            case "monster2":
                damage = 4.0f;
                attackted(damage);
                break;
            case "monster3":
                damage = 9.0f;
                attackted(damage);
                break;
            case "monster4":
                damage = 14.0f;
                attackted(damage);
                break;
        }
        IsAttackted = true;
    }
    private void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
        IsTansparency = false;
        IsAttackted = false;
    }
    void attackted(float damage)
    {
        if (IsAttackted == false)
        {
            IsAttackted = true;
            HP -= damage;

        }
        if (HP < 0)
        {
        }
        IsAttackted = true;
    }
    void Update()
    {
        if (IsAttackted == true)
        {
            Btime += Time.deltaTime;
            if (Btime > delay)
            {
                IsTansparency = !IsTansparency;
                if (IsTansparency)
                {
                    i++;
                    renderer.color = new Color(1, 1, 1, 0.2f);
                    Btime = 0.0f;
                }
                else
                {
                    i++;
                    renderer.color = new Color(1, 1, 1, 1.0f);
                    Btime = 0.0f;
                }
                if (i >= temp)
                {
                    i = 0;
                    IsAttackted = false;
                }
            }
        }
        else
        {
            Debug.Log(HP);
        }
    }
}