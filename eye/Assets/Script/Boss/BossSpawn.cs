using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawn : MonoBehaviour
{
    public GameObject Boss;
    public GameObject Spawn;
    private void OnTriggerEnter2D(Collider2D collision) //�÷��̾�� ��Ҵٸ�
    {
        if (collision.CompareTag("Player"))
        {
            Instantiate(Boss, Spawn.transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
