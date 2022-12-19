using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float maxSpeed;// 속도
    public float jumpPower; // 점프
    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    Animator anim;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {

        if (Input.GetKeyDown("c"))// && !anim.GetBool("isJumping")) //점프
        {
            rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            //anim.SetBool("isJumping", true);
        }

        if (Input.GetKeyDown("z"))//공격
        {
            anim.SetBool("isAttack", true);
        }

        if (Input.GetButtonUp("Horizontal")) //속도제한
        {
            rigid.velocity = new Vector2(rigid.velocity.normalized.x * 0.5f, rigid.velocity.y);

        }

        //if (Mathf.Abs(rigid.velocity.x) < 0.3) //애니메이션
           // anim.SetBool("isWorking", false);
        //else
          //  anim.SetBool("isWorking", true);

    }

    public void IdleAnimation()
    {
        anim.SetBool("isAttack", false);
    }

    void FixedUpdate()
    {

        float h = Input.GetAxisRaw("Horizontal");
        rigid.AddForce(Vector2.right * h, ForceMode2D.Impulse); //이동


        if (rigid.velocity.x > maxSpeed)
        {  //오른쪽
            anim.SetBool("isLeft", false);
            //spriteRenderer.flipX = false;
            rigid.velocity = new Vector2(maxSpeed, rigid.velocity.y);
        }
        else if (rigid.velocity.x < maxSpeed * (-1)) //왼쪽
        {
            anim.SetBool("isLeft", true);
            //spriteRenderer.flipX = true;
            rigid.velocity = new Vector2(maxSpeed * (-1), rigid.velocity.y);
        }

        if (rigid.velocity.y < 0)
        {
            //Debug.DrawRay(rigid.position, Vector3.down, new Color(0, 1, 0));
            RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, Vector3.down, 1, LayerMask.GetMask("Platform"));
            if (rayHit.collider != null) // 바닥 감지
            {
                if (rayHit.distance < 0.5f)
                {
                    anim.SetBool("isJumping", false);
                }
            }
        }

    }
}

