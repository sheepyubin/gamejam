using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSkill : MonoBehaviour
{
    float x;
    // Start is called before the first frame update
    void Start()
    {
        x = gameObject.transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(gameObject.transform.position, new Vector2(x, -3), 0.03f);
    }
}
