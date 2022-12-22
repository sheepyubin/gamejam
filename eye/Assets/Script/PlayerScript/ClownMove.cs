using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClownMove : MonoBehaviour
{
    public float maxSpeed;// �ӵ�
    public float jumpPower; // ����
    bool isground;
    public float time;
    public float cooltime;
    bool temp;
    [SerializeField]
    Transform ArrowPos;
    [SerializeField]
    GameObject Arrow;
    [SerializeField]
    Transform pos;
    [SerializeField]
    float radius;
    [SerializeField]
    LayerMask layer;
    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    Animator anim;
    Transform trans;
    public int jumpcount;
    int Jumpcnt;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        Jumpcnt = jumpcount;
        temp = true;
    }

    void Update()
    {
        isground = Physics2D.OverlapCircle(pos.position, radius, layer); //���� ��Ҵ°�?

        if (isground == true && Input.GetKeyDown("c") && Jumpcnt > 0) //���� 1
        {
            rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
        }
        if (isground == false && Input.GetKeyDown("c") && Jumpcnt > 0) //���� 2
        {
            rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
        }
        if (Input.GetKeyUp(KeyCode.C))
        {
            Jumpcnt--; //n��
        }
        if (isground)
        {
            Jumpcnt = jumpcount; //0���Ϸ� �������� ���� �Ұ�
        }

        if (Input.GetKeyDown("z"))//���ݸ��
        {
            anim.SetBool("isAttack", true);
        }

        if (Input.GetKeyDown("x")) //��ų
        {
            if (temp)
            {
            anim.SetBool("isSkill", true);
            //Invoke("ItTime", time * 3 / 4);
            Invoke("TimeEnd", time);                                
            }
        }

        if (Input.GetButtonUp("Horizontal")) //�ӵ�����
        {
            rigid.velocity = new Vector2(rigid.velocity.normalized.x * 0.5f, rigid.velocity.y);

        }

        if (Mathf.Abs(rigid.velocity.x) < 0.01) //Idle or walk
        {
            anim.SetBool("isWalk", false);
        }
        else
            anim.SetBool("isWalk", true);
    }

    void TimeEnd()
    {
        anim.SetBool("isTime", true);
        Invoke("TimeEnd2", cooltime);
        temp = false;
    }

    void TimeEnd2()
    {
        temp = true;
        anim.SetBool("isTime", false);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(pos.position, radius);
    }
    public void IdleAnimation()
    {
        anim.SetBool("isAttack", false);
    }
    public void IdleAnimationSkill()
    {
        anim.SetBool("isSkill", false);
    }
    public void ShotArrow() //ȭ�� ������ ����
    {
        Instantiate(Arrow, ArrowPos.position, transform.rotation);
    }

    void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");
        rigid.AddForce(Vector2.right * h, ForceMode2D.Impulse); //�̵�


        if (rigid.velocity.x >= maxSpeed)
        {  //������
            transform.rotation = Quaternion.Euler(0, 0, 0);
            rigid.velocity = new Vector2(maxSpeed, rigid.velocity.y);
        }
        else if (rigid.velocity.x <= maxSpeed * (-1)) //����
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
            rigid.velocity = new Vector2(maxSpeed * (-1), rigid.velocity.y);
        }
    }
}
