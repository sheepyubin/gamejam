using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//카메라, 포탈 테스트용 
public class move_test : MonoBehaviour
{
    public float maxSpeed;
    Rigidbody2D rigid;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");
        rigid.AddForce(Vector2.right * h, ForceMode2D.Impulse);


        if (rigid.velocity.x > maxSpeed)
        { 
            rigid.velocity = new Vector2(maxSpeed, rigid.velocity.y);
        }
        else if (rigid.velocity.x < maxSpeed * (-1))
        {
            rigid.velocity = new Vector2(maxSpeed * (-1), rigid.velocity.y);
        }
    }
}
