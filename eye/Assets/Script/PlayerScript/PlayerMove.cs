using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float maxSpeed;// �ӵ�
    public float jumpPower; // ����
    private bool isdash;//�뽬
    public float dashspeed;
    public float defaultTime;
    private float dashTIme;
    private float defaultspeed;
    bool isground;
    [SerializeField]
    Transform pos;
    [SerializeField]
    float radius;
    [SerializeField]
    LayerMask layer;
    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    Animator anim;
    public int jumpcount;
    int Jumpcnt;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        Jumpcnt = jumpcount;
    }

    void Update()
    {
        isground = Physics2D.OverlapCircle(pos.position, radius, layer); //���� ��Ҵ°�?

        if (isground == true && Input.GetKeyDown("c")&&Jumpcnt>0) //���� 1
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
            if (anim.GetBool("isLeft") == true)
            {
                spriteRenderer.flipX = true;
            }
            anim.SetBool("isAttack", true);
        }

        if (Input.GetButtonUp("Horizontal")) //�ӵ�����
        {
            rigid.velocity = new Vector2(rigid.velocity.normalized.x * 0.5f, rigid.velocity.y);

        }
    }


    public void IdleAnimation()
    {
        anim.SetBool("isAttack", false);
    }

    void FixedUpdate()
    {

        float h = Input.GetAxisRaw("Horizontal");
        rigid.AddForce(Vector2.right * h, ForceMode2D.Impulse); //�̵�


        if (rigid.velocity.x > maxSpeed)
        {  //������
            anim.SetBool("isLeft", false);
            //spriteRenderer.flipX = false;
            rigid.velocity = new Vector2(maxSpeed, rigid.velocity.y);
        }
        else if (rigid.velocity.x < maxSpeed * (-1)) //����
        {
            anim.SetBool("isLeft", true);
            //spriteRenderer.flipX = true;
            rigid.velocity = new Vector2(maxSpeed * (-1), rigid.velocity.y);
        }

        if (Input.GetKeyDown("x"))
        {
            isdash= true;
        }

        if (dashTIme <= 0)
        {
            defaultspeed = maxSpeed;
            if (isdash)
            {
                dashTIme = defaultTime;
            }
        }else
        {
            dashTIme-=Time.deltaTime;
            defaultspeed = dashspeed;
        }
    }
}

