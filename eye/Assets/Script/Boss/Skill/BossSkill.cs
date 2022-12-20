using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSkill : MonoBehaviour
{
    float x;
    public Animator BossSkillAnime;
    // Start is called before the first frame update
    void Start()
    {
        x = gameObject.transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(gameObject.transform.position, new Vector2(x, -3.5f), 0.03f);
        BossSkillAnime = GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 6)
        {

        }
    }
    public void SkillStop()
    {
        Destroy(gameObject);
    }
    public void Boom()
    {
        BossSkillAnime.SetBool("isBoom", true);
    }
}
