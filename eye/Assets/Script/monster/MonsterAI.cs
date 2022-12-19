using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class MonsterAI : MonoBehaviour
{
    private Rigidbody2D monsterRigidbody;

    [SerializeField] GameObject Player; //플레이어 따라가기 위한 플레이어 받아오기
    public float MoveLength;  //이동범위
    public float MoveSpeed;   //이동속도
    public float RotateSpeed; //회전속도
    public float delaytime;   //딜레이 타임

    float time;               //델타타임 연산시 필요한 타임
    bool move;                //알아서 돌아다닐지 말지
    bool IsPlayerTrigger;     //플레이어 닿았나 안닿았나
    bool LR;                  //왼쪽 오른쪽

    Vector3 ThisPos;          //기본 지정 좌표


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
        if (IsPlayerTrigger == true) //플레이어와 충돌시
        {
            Vector2 rot = new Vector2(transform.position.x - Player.transform.position.x, transform.position.y - Player.transform.position.y);
            float Angle = Mathf.Atan2(rot.x, rot.y) * Mathf.Rad2Deg * -1;
            Quaternion angleAxis = Quaternion.AngleAxis(Angle, Vector3.forward);
            Quaternion rotation = Quaternion.Slerp(transform.rotation, angleAxis, RotateSpeed * Time.deltaTime);
            transform.rotation = rotation;
            //플레이어를 쳐다볼 수 있도록 플레이어와 자신으로 atan2연산, Slerp연산 후 나온 결과로 각도 변경

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
            //플레이어와 자신의 위치 비교, 플레이어쪽으로 이동
        }
        if (IsPlayerTrigger == false) //플레이어와 충돌하지 않았을 때
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
        //몬스터가 좌우로 움직이고, 좌,우 끝 도달시 2초간 멈춤
    }
}
