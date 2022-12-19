using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class MonsterAI : MonoBehaviour
{
    private Rigidbody2D monsterRigidbody;

    [SerializeField] GameObject Player; //�÷��̾� ���󰡱� ���� �÷��̾� �޾ƿ���
    public float MoveLength;  //�̵�����
    public float MoveSpeed;   //�̵��ӵ�
    public float RotateSpeed; //ȸ���ӵ�
    public float delaytime;   //������ Ÿ��

    float time;               //��ŸŸ�� ����� �ʿ��� Ÿ��
    bool move;                //�˾Ƽ� ���ƴٴ��� ����
    bool IsPlayerTrigger;     //�÷��̾� ��ҳ� �ȴ�ҳ�
    bool LR;                  //���� ������

    Vector3 ThisPos;          //�⺻ ���� ��ǥ


    void Start()
    {
        monsterRigidbody = GetComponent<Rigidbody2D>();
        
        time = 0.0f;
        move = false;
        IsPlayerTrigger = false;
        LR = true;

        ThisPos = transform.position;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.layer == 6)
        {
            IsPlayerTrigger = true;
        }
    }

    void Update()
    {
        if (IsPlayerTrigger == true) //�÷��̾�� �浹��
        {
            Vector2 rot = new Vector2(transform.position.x - Player.transform.position.x, transform.position.y - Player.transform.position.y);
            float Angle = Mathf.Atan2(rot.x, rot.y) * Mathf.Rad2Deg * -1;
            Quaternion angleAxis = Quaternion.AngleAxis(Angle, Vector3.forward);
            Quaternion rotation = Quaternion.Slerp(transform.rotation, angleAxis, RotateSpeed * Time.deltaTime);
            transform.rotation = rotation;
            //�÷��̾ �Ĵٺ� �� �ֵ��� �÷��̾�� �ڽ����� atan2����, Slerp���� �� ���� ����� ���� ����

            if (Player.transform.position.x - 1 < transform.position.x)
            {
                Vector3 newVelocity = new Vector3(-MoveSpeed, 0.0f, 0.0f);
                monsterRigidbody.velocity = newVelocity;
            }
            if (Player.transform.position.x + 1 > transform.position.x)
            {
                Vector3 newVelocity = new Vector3(MoveSpeed, 0.0f, 0.0f);
                monsterRigidbody.velocity = newVelocity;
            }
            //�÷��̾�� �ڽ��� ��ġ ��, �÷��̾������� �̵�
        }
        if (IsPlayerTrigger == false) //�÷��̾�� �浹���� �ʾ��� ��
        {
            if (ThisPos.x + MoveLength / 2 < transform.position.x && LR == true)
            {
                LR = false;
                move = false;
            }
            else if (ThisPos.x - MoveLength / 2 > transform.position.x && LR == false)
            {
                LR = true;
                move = false;
            }

            if (move == true)
            {
                if (LR == false)
                {
                    Vector3 newVelocity = new Vector3(MoveSpeed, 0.0f, 0.0f) * 0.4f * -1;
                    monsterRigidbody.velocity = newVelocity;
                }
                else
                {
                    Vector3 newVelocity = new Vector3(MoveSpeed, 0.0f, 0.0f) * 0.4f;
                    monsterRigidbody.velocity = newVelocity;
                }
            }
            else if (move == false)
            {
                time += Time.deltaTime;
                if (time > delaytime)
                {
                    time = 0.0f;
                    move = true;
                }
                Vector3 newVelocity = new Vector3(0.0f, 0.0f, 0.0f);
                monsterRigidbody.velocity = newVelocity;
            }
        }
        //���Ͱ� �¿�� �����̰�, ��,�� �� ���޽� 2�ʰ� ����
    }
}
