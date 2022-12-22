using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThorMove : MonoBehaviour
{
    public float maxSpeed;// �ӵ�
    public float jumpPower; // ����
    public Vector2 Range;
    public LayerMask Monster;
    [SerializeField] GameObject AttackPointPos;
    [SerializeField] GameObject AttackPoint;
    [SerializeField] GameObject Skill;
    Collider2D[] hit;
    Vector3[] MonsterPos = new Vector3[20];
    bool isground;
    [SerializeField]Transform pos;
    [SerializeField] float radius;
    [SerializeField] LayerMask layer;
    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    Animator anim;
    public int jumpcount;
    int Jumpcnt;
    int i = 0;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        Jumpcnt = jumpcount;
    }

    public void ThorSkill()
    {
        hit = Physics2D.OverlapBoxAll(transform.position, Range, 0, Monster); //���Ϳ� ��Ҵ°�?
        for (i = 0; i < hit.Length; i++)
        {
            MonsterPos[i] = hit[i].transform.position;
            //Debug.Log("X: " + MonsterPos[i].x + " Y: " + MonsterPos[i].y);
            //Instantiate(SkillPos, hit[i].transform.position, hit[i].transform.rotation);
            Destroy(Instantiate(Skill, MonsterPos[i], Quaternion.identity), 0.4f);
        }
    }
    void Update()
    {
        EyesImage.PlayerPos = transform.position;
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
            Destroy(Instantiate(AttackPoint,AttackPointPos.transform),0.1f);
        }

        if (Input.GetKeyDown("x")) //��ų
        {
            anim.SetBool("isSkill", true);
        }

        if (Input.GetButtonUp("Horizontal")) //�ӵ�����
        {
            rigid.velocity = new Vector2(rigid.velocity.normalized.x * 0.5f, rigid.velocity.y);

        }

        if (Mathf.Abs(rigid.velocity.x) < 0.01) //Idle or walk
            anim.SetBool("isWalk", false);
        else
            anim.SetBool("isWalk", true);

    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, Range);
        Gizmos.DrawWireSphere(pos.position, radius);
    }
    public void IdleAnimation()
    {
        anim.SetBool("isAttack", false);
    }
    public void IdleAnimationSkilly()
    {
        anim.SetBool("isSkill", false);
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
