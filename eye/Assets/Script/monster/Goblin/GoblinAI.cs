using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class GoblinAI : MonoBehaviour
{
    [SerializeField] GameObject Player;       //�÷��̾� ���󰡱� ���� �÷��̾� �޾ƿ���
    [SerializeField] GameObject AttackPoint;  //���� ���� ��ȯ�� ���� ������ �޾ƿ���
    private Rigidbody2D monsterRigidbody;     //�̵��� ���� ������ٵ� �޾ƿ���
    new SpriteRenderer renderer;              //��¦�̱� ���� ������ �޾ƿ���
    Animator animator;                        //�ִϸ����� ����
    public float MoveSpeed;                   //�̵��ӵ�
    public float RotateSpeed;                 //ȸ���ӵ�
    public float delaytime;                   //������ Ÿ��
    public bool IsPlayerTrigger;              //�÷��̾� ��ҳ� �ȴ�ҳ�
    public bool LR;                           //���� ������
    float TempTimeA;                          //time ���� TempA
    float Atime;                              //time ���� A
    float Btime;                              //time ���� B
    bool IsDelay;                             //�����̰� �ɸ�����
    bool Attack;                              //���ݼ�ȯ�� ���� ����
    private void Start()
    {
        monsterRigidbody = GetComponent<Rigidbody2D>();
        renderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        MoveSpeed = 3.0f;
        RotateSpeed = 0.6f;
        delaytime = 1.5f;
        IsPlayerTrigger = false;
        LR = true;
        TempTimeA = 0.0f;
        Atime = 0.0f;
        Btime = 0.0f;
        IsDelay = false;
        Attack = false;
    }
    private void OnTriggerEnter2D(Collider2D collision) //������ �� �ִϸ��̼�, Ʈ���� ��
    {
        if (collision.gameObject.layer == 6)
        {
            IsPlayerTrigger = true;
            animator.SetBool("IsAttack", true);
            Attack = true;
        }
    }
    private void OnTriggerStay2D(Collider2D collision) //��Ÿ�� ������ �ִϸ��̼�, ���� �� ���� ����Ʈ ����
    {
        if (collision.gameObject.layer == 6)
        {
            TempTimeA += Time.deltaTime;
            if (TempTimeA >= 0.6f)
            {
                animator.SetBool("IsAttack", true);
                IsPlayerTrigger = true;
                TempTimeA = 0.0f;
                Attack = true;
            }
        }
    }
    public void Idle()
    {
        animator.SetBool("IsAttack", false);
    }
    void Update()
    {
        if (IsPlayerTrigger == true)
        {
            //Vector2 rot = new Vector2(transform.position.x - Player.transform.position.x, transform.position.y - Player.transform.position.y);
            //float Angle = Mathf.Atan2(rot.x, rot.y) * Mathf.Rad2Deg * -1;
            //Quaternion angleAxis = Quaternion.AngleAxis(Angle, Vector3.forward);
            //Quaternion rotation = Quaternion.Slerp(transform.rotation, angleAxis, RotateSpeed * Time.deltaTime);
            //transform.rotation = rotation;
            //�÷��̾ �Ĵٺ� �� �ֵ��� �÷��̾�� �ڽ����� atan2����, Slerp���� �� ���� ����� ���� ����

            if (Player.transform.position.x - 1 < transform.position.x)
            {
                animator.SetBool("IsRun", true);
                renderer.flipX = true;
                Vector3 newVelocity = new Vector3(-MoveSpeed, 0.0f, 0.0f);
                monsterRigidbody.velocity = newVelocity;
                LR = true;
            }
            if (Player.transform.position.x + 1 > transform.position.x)
            {
                animator.SetBool("IsRun", true);
                renderer.flipX = false;
                Vector3 newVelocity = new Vector3(MoveSpeed, 0.0f, 0.0f);
                monsterRigidbody.velocity = newVelocity;
                LR = false;
            }
            //�÷��̾�� �ڽ��� ��ǥ ��, �÷��̾� �������� �̵�
        }
        else
        {
            if (IsDelay == true)
            {
                animator.SetBool("IsRun", false);
                Atime += Time.deltaTime;
                Vector3 newVelocity = new Vector3(MoveSpeed, 0, 0) * 0;
                monsterRigidbody.velocity = newVelocity;
                if (Atime >= 1)
                {
                    renderer.flipX = LR ? true : false;
                }
                if (Atime >= 2)
                {
                    IsDelay = false;
                    Atime = 0.0f;
                }
            }
            else if (IsDelay == false)
            {
                Btime += Time.deltaTime;
                if (Btime > 1.5f)
                {
                    IsDelay = true;
                    Btime = 0.0f;
                    LR = LR = LR ? false : true;
                }
                else
                {
                    if (LR == true)
                    {
                        animator.SetBool("IsRun", true);
                        Vector3 newVelocity = new Vector3(MoveSpeed, 0, 0) * -1;
                        monsterRigidbody.velocity = newVelocity;

                    }
                    if (LR == false)
                    {
                        animator.SetBool("IsRun", true);
                        Vector3 newVelocity = new Vector3(MoveSpeed, 0, 0) * 1;
                        monsterRigidbody.velocity = newVelocity;
                    }
                }
            }
        }
        if (Attack == true)
        {
            Btime += Time.deltaTime;
            if (Btime >= 0.48f)
            {
                if (LR == true)
                {
                    Vector3 AttackPrePos = new Vector3(this.transform.position.x - 1.1f, transform.position.y, transform.position.z);
                    Destroy(Instantiate(AttackPoint, AttackPrePos, Quaternion.identity), 0.3f);
                    Attack = false;
                    Btime = 0.0f;
                }
                else if (LR == false)
                {
                    Vector3 AttackPrePos = new Vector3(transform.position.x + 1.1f, transform.position.y, transform.position.z);
                    Destroy(Instantiate(AttackPoint, AttackPrePos, Quaternion.identity), 0.3f);
                    Attack = false;
                    Btime = 0.0f;
                }
            }
        }
    }
}
