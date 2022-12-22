using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GokuMove : MonoBehaviour
{
    public Camera Cam1;
    public Camera Cam2;
    public Camera Cam3;
    public float maxSpeed;// 속도
    public float jumpPower; // 점프
    public float SkillSpeed; //스킬 속도
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
    public static Vector3 PlayerPos;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        Jumpcnt = jumpcount;
    }

    void Update()
    {
        PlayerPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        isground = Physics2D.OverlapCircle(pos.position, radius, layer); //땅에 닿았는가?

        if (isground == true && Input.GetKeyDown("c") && Jumpcnt > 0) //점프 1
        {
            rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
        }
        if (isground == false && Input.GetKeyDown("c") && Jumpcnt > 0) //점프 2
        {
            rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
        }
        if (Input.GetKeyUp(KeyCode.C))
        {
            Jumpcnt--; //n단
        }
        if (isground)
        {
            Jumpcnt = jumpcount; //0이하로 내려가면 점프 불가
        }

        if (Input.GetKeyDown("z"))//공격모션
        {
            anim.SetBool("isAttack", true);
        }

        if (Input.GetKeyDown("x")) //스킬
        {
            anim.SetBool("isSkill", true);
        }
        if (anim.GetBool("isSkill"))
        {
            if (transform.rotation.y == 0)
                transform.Translate(transform.right * SkillSpeed * Time.deltaTime); //------->
            else
                transform.Translate(transform.right * -1 * SkillSpeed * Time.deltaTime); //------->
        }

        if (Input.GetButtonUp("Horizontal")) //속도제한
        {
            rigid.velocity = new Vector2(rigid.velocity.normalized.x * 0.5f, rigid.velocity.y);

        }

        if (Mathf.Abs(rigid.velocity.x) < 0.01) //Idle or walk
            anim.SetBool("isWalk", false);
        else
            anim.SetBool("isWalk", true);
    }


    void OnTriggerEnter2D(Collider2D collision) //카메라 전환 (스테이지 넘어 갈 때)
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
    }
    public void ShowCam2View()
    {
        Cam1.enabled = false;
        Cam2.enabled = true;
    }
    public void ShowCam3View()
    {
        Cam2.enabled = false;
        Cam3.enabled = true;
    }
    public void IdleAnimation()
    {
        anim.SetBool("isAttack", false);
    }
    public void IdleAnimationSkill()
    {
        anim.SetBool("isSkill", false);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(pos.position, radius);
    }
    void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");
        rigid.AddForce(Vector2.right * h, ForceMode2D.Impulse); //이동


        if (rigid.velocity.x >= maxSpeed)
        {  //오른쪽
            transform.rotation = Quaternion.Euler(0, 0, 0);
            rigid.velocity = new Vector2(maxSpeed, rigid.velocity.y);
        }
        else if (rigid.velocity.x <= maxSpeed * (-1)) //왼쪽
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
            rigid.velocity = new Vector2(maxSpeed * (-1), rigid.velocity.y);
        }
    }
}
