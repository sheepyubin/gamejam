using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ArtemisMove : MonoBehaviour
{
    public Camera Cam1;
    public Camera Cam2;
    public Camera Cam3;
    public float maxSpeed;// �ӵ�
    public float jumpPower; // ����
    public Vector2 Range;
    public LayerMask Monster;
    bool isground;
    [SerializeField] GameObject Skill;
    Collider2D[] hit;
    Vector3[] MonsterPos = new Vector3[20];
    [SerializeField]    Transform ArrowPos;
    [SerializeField]    GameObject Arrow;
    [SerializeField]    Transform pos;
    [SerializeField]    float radius;
    [SerializeField]    LayerMask layer;
    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    Animator anim;
    Transform trans;
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
    public void ArtemisSkill()
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
            anim.SetBool("isAttack", true);
        }

        if (Input.GetKeyDown("x")) //��ų
        {
            anim.SetBool("isSkill", true);
        }

        if (Input.GetButtonUp("Horizontal")) //�ӵ�����
        {
            rigid.velocity = new Vector2(rigid.velocity.normalized.x * 0.5f, rigid.velocity.y);

        }

    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, Range);
        Gizmos.DrawWireSphere(pos.position, radius);
    }
    public void flipx()
    {
        spriteRenderer.flipX = false;
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

    void OnTriggerEnter2D(Collider2D collision) //ī�޶� ��ȯ (�������� �Ѿ� �� ��)
    {
        if (collision.gameObject.tag == "Stage1")
        {
            ShowCam1View();
        }
        if (collision.gameObject.tag == "Stage2")
        {
            ShowCam2View();
        }
        if (collision.gameObject.tag == "Stage3")
        {
            ShowCam3View();
        }
    }
    public void ShowCam1View()
    {
        Cam1.enabled = true;
        Cam2.enabled = false;
        Cam3.enabled = false;
    }
    public void ShowCam2View()
    {
        Cam1.enabled = false;
        Cam2.enabled = true;
        Cam3.enabled = false;
    }
    public void ShowCam3View()
    {
        Cam1.enabled = false;
        Cam2.enabled = false;
        Cam3.enabled = true;
    }

    void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");
        rigid.AddForce(Vector2.right * h, ForceMode2D.Impulse); //�̵�


        if (rigid.velocity.x > maxSpeed)
        {  //������
            anim.SetBool("isLeft", false);
            transform.rotation = Quaternion.Euler(0, 0, 0);
            //spriteRenderer.flipX = false;
            rigid.velocity = new Vector2(maxSpeed, rigid.velocity.y);
        }
        else if (rigid.velocity.x < maxSpeed * (-1)) //����
        {
            anim.SetBool("isLeft", true);
            transform.rotation = Quaternion.Euler(0, 180, 0);
            rigid.velocity = new Vector2(maxSpeed * (-1), rigid.velocity.y);
        }
    }
}

